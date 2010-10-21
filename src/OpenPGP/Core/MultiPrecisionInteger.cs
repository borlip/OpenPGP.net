namespace OpenPGP.Core
{
    /// <summary>
    /// A multi-precision integer, as defined in RFC 4880 at http://tools.ietf.org/html/rfc4880#section-3.2
    /// </summary>
    public class MultiPrecisionInteger
    {
        private readonly int _NumberOfBits;
        private readonly byte[] _Bits;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPrecisionInteger"/> class.
        /// </summary>
        /// <param name="numberOfBits">The number of bits.</param>
        /// <param name="bits">The bits.</param>
        public MultiPrecisionInteger(int numberOfBits, byte[] bits)
        {
            //TODO: validate that length of bits matches the size of the array. Should be (MPI.length + 7)/8
            _NumberOfBits = numberOfBits;
            _Bits = bits;
        }
    }
}
