using System;

namespace Game
{
    [Flags]
    public enum MobType
    {
        Red = 1 << 0,
        Green = 1 << 1,
        Blue = 1 << 2,
    }
}
