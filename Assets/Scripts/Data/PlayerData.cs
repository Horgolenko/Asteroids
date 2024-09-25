using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] private int _maxShotAmount;
        [SerializeField] private int _maxLifeAmount;
        [SerializeField] private int _enemeisToKill;
    }
}
