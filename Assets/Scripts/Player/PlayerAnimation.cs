using UnityEngine;

namespace Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private float acceleration = 2.0f;
        [SerializeField] private float deceleration = 2.0f;
        
        private Animator _animator;
        private float _velocity;
        private static readonly int VelocityHash = Animator.StringToHash("Velocity");
        
        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            switch (PlayerMovement.Instance.isMoving)
            {
                case true when _velocity < 1.0f:
                    _velocity += Time.deltaTime * acceleration;
                    break;
                case false when _velocity > 0.0f:
                    _velocity -= Time.deltaTime * deceleration;
                    break;
            }

            if (!PlayerMovement.Instance.isMoving && _velocity < 0.0f)
            {
                _velocity = 0f;
            }
            
            _animator.SetFloat(VelocityHash, _velocity);
        }
    }
}
