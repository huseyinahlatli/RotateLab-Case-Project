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
            _velocity = EmployeeController.isEmployeeWorking ? 1.0f : 0.0f;
            _animator.SetFloat(VelocityHash, _velocity);
        }
    }
}
