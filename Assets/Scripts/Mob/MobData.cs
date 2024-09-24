using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class MobData : MonoBehaviour
    {
        [SerializeField]
        private MobType _mobType;
        [SerializeField]
        private SideType _sideType;

        private List<int> _gateIds = new();
        
        public MobType mobType => _mobType;
        public SideType sideType => _sideType;

        public void Init(int id)
        {
            _gateIds.Add(id);
        }
        
        public void SetSide(SideType sideType)
        {
            _sideType = sideType;
        }

        public bool IsVisited(int id)
        {
            return _gateIds.Any(t => t == id);
        }

        public void Clear()
        {
            _gateIds.Clear();
        }
    }
}
