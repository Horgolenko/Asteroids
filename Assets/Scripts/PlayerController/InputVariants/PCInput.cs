using UnityEngine;

namespace PlayerController.InputVariants
{
    public class PCInput : AInput
    {
        public override float Horizontal()
        {
            return Input.GetAxis("Horizontal");
        }
        
        public override float Vertical()
        {
            return Input.GetAxis("Vertical");
        }

        public override bool Shoot()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }
    }
}
