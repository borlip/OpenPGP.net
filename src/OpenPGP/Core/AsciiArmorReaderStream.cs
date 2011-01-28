using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace OpenPGP.Core
{
    /// <summary>
    /// A stream that strips away ASCII armor and returns the data inside.
    /// </summary>
    public class AsciiArmorReaderStream : Stream
    {
        private readonly Dictionary<string, string> _Headers = new Dictionary<string, string>();

        private readonly Stream _InputStream;
        private StreamReader _InputReader;
        private int _LineNumber;
        private bool _IsEndOfInput;
        private long _ComputedCrc = Crc24Computer.InitializeCrc();
        private MemoryStream _OutputStream;
        private bool _IsDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="AsciiArmorReaderStream"/> class.
        /// </summary>
        /// <param name="inputStream">The input stream.</param>
        public AsciiArmorReaderStream(Stream inputStream)
        {
            if (inputStream == null)
            {
                throw new ArgumentNullException("inputStream");
            }
            _InputStream = inputStream;
        }

        /// <summary>
        /// Gets the headers.
        /// </summary>
        /// <value>The headers.</value>
        public Dictionary<string, string> Headers
        {
            get { return _Headers; }
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="T:System.IO.Stream"/> and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (_InputReader != null)
            {
                _InputReader.Close();
            }
            if (_OutputStream != null)
            {
                _OutputStream.Close();
            }
            base.Dispose(disposing);
            _IsDisposed = true;
        }

        /// <summary>
        /// Does nothing.
        /// </summary>
        public override void Flush()
        {
            // does nothing
        }

        /// <summary>
        /// Not supported in <see cref="AsciiArmorReaderStream" />.
        /// </summary>
        /// <param name="offset">Not used.</param>
        /// <param name="origin">Not used.</param>
        /// <returns>Nothing.</returns>
        /// <exception cref="InvalidOperationException">All operations.</exception>
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Not supported in <see cref="AsciiArmorReaderStream" />.
        /// </summary>
        /// <param name="value">Not used.</param>
        /// <exception cref="InvalidOperationException">All operations.</exception>
        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Reads the next <paramref name="count"/> bytes from the stream into <paramref name="buffer"/> at <paramref name="offset"/>.
        /// </summary>
        /// <param name="buffer">The destination buffer.</param>
        /// <param name="offset">The offset into the buffer.</param>
        /// <param name="count">The number of bytes to read.</param>
        /// <returns>The number of bytes actually read.</returns>
        /// <exception cref="T:System.ArgumentException">
        /// The sum of <paramref name="offset"/> and <paramref name="count"/> is larger than the buffer length.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException">
        /// 	<paramref name="buffer"/> is null.
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// 	<paramref name="offset"/> or <paramref name="count"/> is negative.
        /// </exception>
        /// <exception cref="T:System.IO.IOException">
        /// An I/O error occurs.
        /// </exception>
        /// <exception cref="T:System.ObjectDisposedException">
        /// Methods were called after the stream was closed.
        /// </exception>
        /// <example cref="T:System.OutOfMemoryException">
        /// There is insufficient memory to allocate a buffer to read from the input stream.
        /// </example>
        public override int Read(byte[] buffer, int offset, int count)
        {
            if (_IsDisposed)
            {
                throw new ObjectDisposedException("The stream is closed");
            }
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException("offset");
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count");
            }
            if (offset + count > buffer.Length)
            {
                throw new ArgumentException("The sum of offset and count is larger than the buffer length");
            }

            // NOOP
            if (count == 0)
            {
                return 0;
            }

            EnsureReaderInitialized();
            return _OutputStream.Read(buffer, 0, count);
        }

        private void EnsureReaderInitialized()
        {
            if (_InputReader == null)
            {
                _InputReader = new StreamReader(_InputStream);
                ReadHeaderLine();
                ReadHeaders();
                if (_IsEndOfInput)
                {
                    throw new PGPException("No data found after ASCII armor headers");
                }

                ReadDataIntoMemoryStream();
            }
        }

        private void ReadHeaderLine()
        {
            // find first line of armor
            string input;
            while ((input = ReadLineFromInputStream()) != null)
            {
                if (ArmorHelper.IsAsciiArmorHeaderLine(input))
                {
                    return;
                }
            }

            throw new PGPException("Unable to locate beginning of ASCII armor");
        }

        private void ReadHeaders()
        {
            string input;
            while ((input = ReadLineFromInputStream()) != null)
            {
                input = input.TrimEnd();
                if (input.Length == 0) return; // blank line ends headers

                var header = ArmorHelper.ParseHeader(input);
                if (header != null)
                {
                    AddHeader(header.Name, header.Value);
                }
                else
                {
                    throw new PGPException(string.Format(CultureInfo.CurrentCulture, "Malformed header on line {0}", _LineNumber));
                }
            }
        }

        private void AddHeader(string key, string value)
        {
            if (_Headers.ContainsKey(key))
            {
                _Headers[key] = _Headers[key] + value;
            }
            else
            {
                _Headers.Add(key, value);
            }
        }

        private void ReadDataIntoMemoryStream()
        {
            _OutputStream = new MemoryStream();
            while (true)
            {
                var input = ReadLineFromInputStream();
                if (_IsEndOfInput)
                {
                    throw new PGPException("Unexpected end of ASCII armor file");
                }
                if (input.StartsWith(AsciiArmorConstants.ArmorLinePrefix, StringComparison.Ordinal))
                {
                    throw new PGPException("End of armor reached before checksum");
                }
                if (input.StartsWith(AsciiArmorConstants.ChecksumPrefix, StringComparison.Ordinal))
                {
                    ProcessCrcLine(input);
                    break;
                }

                DecodeLine(input);
            }
            _OutputStream.Seek(0, SeekOrigin.Begin);
        }

        private void ProcessCrcLine(string input)
        {
            ValidateCrcLine(input);
            var crc = GetCrcFromArmor(input);
            if (crc != _ComputedCrc)
            {
                throw new PGPException("Bad CRC");
            }
        }

        private void ValidateCrcLine(string input)
        {
            if (input.Length != 5)
            {
                throw new PGPException(string.Format(CultureInfo.CurrentCulture, "CRC on line {0} should be 5 characters long. It is {1} characters long.", _LineNumber, input.Length));
            }
        }

        private uint GetCrcFromArmor(string input)
        {
            var crcBytes = ConvertFromBase64(input.Substring(1));
            var bytesToConvert = new byte[4];
            bytesToConvert[0] = 0;
            Buffer.BlockCopy(crcBytes, 0, bytesToConvert, 1, 3);
            return BigEndianBitConverter.ToUInt32(bytesToConvert, 0);
        }

        private void DecodeLine(string input)
        {
            var decoded = ConvertFromBase64(input);
            _ComputedCrc = Crc24Computer.ComputeCrc(decoded, _ComputedCrc);
            _OutputStream.Write(decoded, 0, decoded.Length);
        }

        private byte[] ConvertFromBase64(string input)
        {
            try
            {
                return Convert.FromBase64String(input);
            }
            catch (FormatException)
            {
                throw new PGPException(string.Format(CultureInfo.CurrentCulture, "Data on line {0} is not valid", _LineNumber));
            }
        }

        private string ReadLineFromInputStream()
        {
            var input = _InputReader.ReadLine();
            if (input != null)
            {
                _LineNumber++;
            }
            else
            {
                _IsEndOfInput = true;
            }
            return input;
        }

        /// <summary>
        /// Not implemented in <see cref="AsciiArmorReaderStream" />.
        /// </summary>
        /// <param name="buffer">Not used.</param>
        /// <param name="offset">Not used.</param>
        /// <param name="count">Not used.</param>
        /// <exception cref="InvalidOperationException">All operations.</exception>
        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Gets a value indicating whether the current stream supports reading.
        /// </summary>
        /// <value></value>
        /// <returns>Always returns <c>true</c>.
        /// </returns>
        public override bool CanRead
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a value indicating whether the current stream supports seeking.
        /// </summary>
        /// <value></value>
        /// <returns>Always returns <c>false</c>.
        /// </returns>
        public override bool CanSeek
        {
            get { return false; }
        }

        /// <summary>
        /// Gets a value indicating whether the current stream supports writing.
        /// </summary>
        /// <value></value>
        /// <returns>Always returns <c>false</c>.
        /// </returns>
        public override bool CanWrite
        {
            get { return false; }
        }

        /// <summary>
        /// When overridden in a derived class, gets the length in bytes of the stream.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// A long value representing the length of the stream in bytes.
        /// </returns>
        /// <exception cref="T:System.ObjectDisposedException">
        /// Methods were called after the stream was closed.
        /// </exception>
        public override long Length
        {
            get 
            {
                if (_IsDisposed)
                {
                    throw new ObjectDisposedException("The stream is closed");
                }
                EnsureReaderInitialized();
                return _OutputStream.Length; 
            }
        }

        /// <summary>
        /// Gets or sets the position within the current stream.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The current position within the stream.
        /// </returns>
        /// <exception cref="T:System.IO.IOException">
        /// An I/O error occurs.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        /// The stream does not support seeking.
        /// </exception>
        /// <exception cref="T:System.ObjectDisposedException">
        /// Methods were called after the stream was closed.
        /// </exception>
        public override long Position
        {
            get
            {
                if (_IsDisposed)
                {
                    throw new ObjectDisposedException("The stream is closed");
                }
                EnsureReaderInitialized();
                return _OutputStream.Position;
            }
            set { throw new NotSupportedException("Seeking is not supported"); }
        }
    }
}
