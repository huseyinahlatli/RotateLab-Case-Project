using UnityEngine;

namespace Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        private Animator _animator;
        private float _velocity;
        private const float TransitionSpeed = 2.0f;
        private static readonly int VelocityHash = Animator.StringToHash("Velocity");
        
        private void Start() => _animator = GetComponent<Animator>();

        private void Update()
        {
            switch (PlayerMovement.Instance.isMoving)
            {
                case true when _velocity < 1.0f:
                    _velocity += Time.deltaTime * TransitionSpeed;
                    break;
                case false when _velocity > 0.0f:
                    _velocity -= Time.deltaTime * TransitionSpeed;
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
