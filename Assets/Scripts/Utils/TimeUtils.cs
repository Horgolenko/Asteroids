using System;

namespace Utils
{
    public static class TimeUtils
    {
        public const long FrameDelta = 17; // 1 / 60 * 1000L

        private static DateTime _startTimestamp = new(1970, 1, 1);
        
        public static long GetTimestamp()
        {
            return (long) DateTime.UtcNow.Subtract(_startTimestamp).TotalMilliseconds;
        }
    }
}
