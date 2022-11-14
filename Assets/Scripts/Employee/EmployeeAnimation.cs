using UnityEngine;

namespace Employee
{
    public class EmployeeAnimation : MonoBehaviour
    {
        private Animator _animator;
        private float _velocity;
        private static readonly int VelocityHash = Animator.StringToHash("Velocity");
        
        private void Start() => _animator = GetComponentInChildren<Animator>();
 
        private void Update()
        {
            if (EmployeeController.isEmployeeWorking)
            {
                if(_velocity < 1.0f)
                    _velocity += Time.deltaTime;
            }
            else
            {
                if(_velocity > 0.0f)
                    _velocity -= Time.deltaTime;
            }
            
            _animator.SetFloat(VelocityHash, _velocity);
        }
    }
}
