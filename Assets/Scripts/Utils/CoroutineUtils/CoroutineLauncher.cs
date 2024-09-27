using System.Collections.Generic;
using Zenject;

namespace Utils.CoroutineUtils
{
    public class CoroutineLauncher : ITickable
    {
        private readonly List<UpdateLine> _lines = new(500);

        public void AddUpdate(UpdateLine line)
        {
            _lines.Add(line);
        }
        
        public void RemoveUpdate(UpdateLine line)
        {
            _lines.Remove(line);
        }

        public void Tick()
        {
            var size = _lines.Count;
            var now = TimeUtils.GetTimestamp();
            for (int i = size - 1; i >= 0; i--)
            {
                var line = _lines[i];
                    
                var diff = now - line.lastCallTimestamp;
                    
                if (diff < line.delta) continue;

                line.updateMethod.Invoke();
                line.lastCallTimestamp = now;
            }
        }
    }
}