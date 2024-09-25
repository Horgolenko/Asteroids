using System;
using UnityEngine;

namespace Interfaces
{
    public interface IPoolable
    {
        GameObject GameObject { get; }
        event Action<IPoolable> Destroyed;
        void Reset();
    }
}
