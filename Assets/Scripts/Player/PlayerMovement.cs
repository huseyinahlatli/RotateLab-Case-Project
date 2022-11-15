using UnityEngine;
using Singleton;

namespace Player
{
    [RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
    public class PlayerMovement : MonoSingleton<PlayerMovement>
    {
        [SerializeField] private DynamicJoystick joystick;
        [SerializeField] private float moveSpeed;
    
        [HideInInspector] public bool isMoving;
        private Rigidbody _rigidbody;
        private Vector3 _movement;
        
        private void Start() => _rigidbody = GetComponent<Rigidbody>();

        private void FixedUpdate()
        {
            PlayerMove();

            if (joystick.Horizontal != 0 || joystick.Vertical != 0)
            {
                isMoving = true;
                Direction();
            }
            else
            {
                isMoving = false;
            }
        }

        private void PlayerMove()
        {
            _movement = new Vector3(joystick.Horizontal, 0, joystick.Vertical) * (moveSpeed * Time.deltaTime);
            _rigidbody.velocity = _movement;
        }

        private void Direction()
        {
            if (_movement != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
            }
        }
    }
}
