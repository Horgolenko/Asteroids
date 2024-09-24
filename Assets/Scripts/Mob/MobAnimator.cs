using UnityEngine;

namespace Mob
{
    public class MobAnimator : MonoBehaviour
    {
        private static readonly int Run = Animator.StringToHash("running");
        private static readonly int Hit = Animator.StringToHash("hitting");
        private static readonly int Die = Animator.StringToHash("dying");
        
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void RunAnimation()
        {
            _animator.SetBool(Run, true);
        }
        
        public void HitAnimation()
        {
            _animator.SetBool(Hit, true);
        }
        
        public void DieAnimation()
        {
            _animator.SetBool(Run, false);
            _animator.SetBool(Hit, false);
            _animator.SetBool(Die, true);
        }
    }
}
