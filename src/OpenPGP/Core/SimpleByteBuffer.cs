using System;

namespace OpenPGP.Core
{
    /// <summary>
    /// A simple implementation of a buffer of bytes.
    /// </summary>
    public class SimpleByteBuffer
    {
        private const int MinimumBufferSize = 8;
        private const int DefaultBufferSize = 16384;

        private byte[] _Buffer;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleByteBuffer"/> class.
        /// </summary>
        public SimpleByteBuffer() : this(DefaultBufferSize)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleByteBuffer"/> class.
        /// </summary>
        /// <param name="bufferSize">Size of the buffer.</param>
        public SimpleByteBuffer(int bufferSize)
        {
            if (bufferSize < MinimumBufferSize)
            {
                throw new ArgumentOutOfRangeException("bufferSize", string.Format("bufferSize must be >= {0}", MinimumBufferSize));
            }
            BufferSize = bufferSize;
            _Buffer = new byte[BufferSize];
        }

        /// <summary>
        /// Gets the size of the buffer.
        /// </summary>
        /// <value>The size of the buffer.</value>
        public int BufferSize { get; private set; }

        /// <summary>
        /// Gets the number of bytes available.
        /// </summary>
        /// <value>The number of bytes available.</value>
        public int BytesAvailable { get; private set; }

        /// <summary>
        /// Gets or sets the write position.
        /// </summary>
        /// <value>The write position.</value>
        public int WritePosition { get; private set; }

        /// <summary>
        /// Gets or sets the read position.
        /// </summary>
        /// <value>The read position.</value>
        public int ReadPosition { get; private set; }

        /// <summary>
        /// Writes the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="count">The count.</param>
        public void Write(byte[] data, int offset, int count)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException("offset");
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count");
            }
            if (offset + count > data.Length)
            {
                throw new ArgumentException(string.Format("The sum of offset and count is larger than the data length"));
            }

            if (WritePosition + count > BufferSize)
            {
                throw new PGPException("Buffer overflow");
            }
            Buffer.BlockCopy(data, offset, _Buffer, WritePosition, count);
            BytesAvailable += count;
            WritePosition += count;
        }

        /// <summary>
        /// Reads data from the buffer into destination.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="count">The count.</param>
        public int Read(byte[] destination, int offset, int count)
        {
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException("offset");
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count");
            }
            if (offset + count > destination.Length)
            {
                throw new ArgumentException(string.Format("The sum of offset and count is larger than the destination length"));
            }

            var bytesToRead = count;
            if (bytesToRead > BytesAvailable)
            {
                bytesToRead = BytesAvailable;
            }
            if (bytesToRead == 0)
            {
                return 0;
            }

            Buffer.BlockCopy(_Buffer, ReadPosition, destination, offset, bytesToRead);
            ReadPosition += bytesToRead;

            BytesAvailable -= bytesToRead;
            if (BytesAvailable <= 0)
            {
                WritePosition = 0;
                ReadPosition = 0;
            }

            return bytesToRead;
        }
    }
}
