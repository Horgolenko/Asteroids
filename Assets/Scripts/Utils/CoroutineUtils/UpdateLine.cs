using System;

namespace Utils.CoroutineUtils
{
    public class UpdateLine
    {
        public readonly long delta;
        public readonly Action updateMethod;

        private int counter;

        public long lastCallTimestamp { get; set; }

        public UpdateLine(Action updateMethod, long delta)
        {
            this.delta = delta;
            this.updateMethod = updateMethod;
        }
    }
}
