using System;

namespace OpenPGP.Core
{
    /// <summary>
    /// A timestamp as defined in RFC 4880 at http://tools.ietf.org/html/rfc4880#section-3.5
    /// </summary>
    [CLSCompliant(false)]
    public class PGPTime
    {
        private static DateTime _OriginTime = new DateTime(1970, 1, 1);

        /// <summary>
        /// The time.
        /// </summary>
        public uint Time { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PGPTime"/> class.
        /// </summary>
        /// <param name="time">The time represented as the number of seconds since 1/1/1970.</param>
        public PGPTime(uint time)
        {
            Time = time;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PGPTime"/> class.
        /// </summary>
        /// <param name="time">The time.</param>
        public PGPTime(DateTime time)
        {
            if (time < _OriginTime)
            {
                throw new ArgumentException("time must be after 1/1/1970", "time");
            }

            var span = time - _OriginTime;
            Time = Convert.ToUInt32(Math.Floor(span.TotalSeconds));
        }

        /// <summary>
        /// Returns the time as a DateTime.
        /// </summary>
        /// <returns>The time as a DateTime</returns>
        public DateTime ToDateTime()
        {
            return _OriginTime.AddSeconds(Time);
        }
    }
}
