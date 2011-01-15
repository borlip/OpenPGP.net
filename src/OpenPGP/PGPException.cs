using System;
using System.Runtime.Serialization;

namespace OpenPGP
{
    [Serializable]
    public class PGPException : Exception
    {
        public PGPException()
        {
        }

        public PGPException(string message) : base(message)
        {
        }

        public PGPException(string message, Exception inner) : base(message, inner)
        {
        }

        protected PGPException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
