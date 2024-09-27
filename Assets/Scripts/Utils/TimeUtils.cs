using System;

namespace Utils
{
    public static class TimeUtils
    {
        public const long FrameDelta = 17L; // 1 / 60 * 1000L
        public const long SpeedChangeDelta = 150L;

        private static readonly DateTime StartTimestamp = new(1970, 1, 1);
        
        public static long GetTimestamp()
        {
            return (long) DateTime.UtcNow.Subtract(StartTimestamp).TotalMilliseconds;
        }
    }
}
