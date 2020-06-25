using System;

namespace DataSource.Application.Extensions
{
    /// <summary>
    /// Define extensions to manage generation time interval.
    /// </summary>
    public static class TimeIntervalExtensions
    {
        /// <summary>
        /// Extension method to convert time interval from string to int format.
        /// </summary>
        /// <param name="timeIntervalString">Time interval in string format</param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns>Time interval in integer format.</returns>
        public static int ToInteger(this string timeIntervalString)
        {
            var success = Int32.TryParse(timeIntervalString, out var timeInterval);
            if (!success)
            {
                throw new InvalidCastException(nameof(timeIntervalString));
            }

            if (timeInterval <=0)
            {
                throw new ArgumentOutOfRangeException(nameof(timeIntervalString));
            }

            return timeInterval;
        }

        /// <summary>
        /// Extension method to conver time interval from secods to milliseconds.
        /// </summary>
        /// <param name="timeIntervalSeconds">Time interval in seconds.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns>Time interval in milliseconds.</returns>
        public static int ToMilliseconds(this int timeIntervalSeconds)
        {
            if (timeIntervalSeconds <= 0)
            {
                throw new ArgumentNullException(nameof(timeIntervalSeconds));
            }

            return timeIntervalSeconds * 1000;
        }
    }
}