using System;

namespace OpenPGP.Core
{
    /// <summary>
    /// A key identifier as defined in RFC 4880 at http://tools.ietf.org/html/rfc4880#section-3.3
    /// </summary>
    [CLSCompliant(false)]
    public class KeyIdentifier
    {
        private readonly ulong _Id;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyIdentifier"/> class.
        /// </summary>
        /// <param name="id">The 8-byte key identifier.</param>
        public KeyIdentifier(ulong id)
        {
            _Id = id;
        }
    }
}
