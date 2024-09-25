using Entities.Abstract;

namespace Entities.Projectile
{
    public class ProjectileMover : AMover
    {
        public void StartMoving()
        {
            _active = true;
        }

        public void Stop()
        {
            _active = false;
        }
    }
}
