using UnityEngine;
using UnityEngine.InputSystem;

namespace PotionMorph.Manager
{
    public class GameManager : MonoBehaviour
    {
        private Camera _cam;

        private Rigidbody2D _follower;

        private void Awake()
        {
            _cam = Camera.main;
        }

        private void FixedUpdate()
        {
            if (_follower != null)
            {
                _follower.linearVelocity = (_cam.ScreenToWorldPoint(MousePos) - _follower.transform.position) * 10f;
            }
        }

        private Vector2 MousePos =>
#if UNITY_ANDROID && !UNITY_EDITOR
                Touchscreen.current.primaryTouch.position.ReadValue();
#else
                Mouse.current.position.ReadValue();
#endif

        public void OnMousePressed(InputAction.CallbackContext value)
        {
            if (value.phase == InputActionPhase.Performed)
            {
                // Mouse position
                var mousePos = MousePos;
                var ray = _cam.ScreenPointToRay(mousePos);
                var hit = Physics2D.Raycast(ray.origin, ray.direction, float.MaxValue, LayerMask.GetMask("Prop"));
                if (hit.collider != null)
                {
                    _follower = hit.collider.GetComponent<Rigidbody2D>();
                    _follower.gravityScale = 0f;
                }
            }
            else if (value.phase == InputActionPhase.Canceled)
            {
                if (_follower != null)
                {
                    _follower.gravityScale = 1f;
                    _follower = null;
                }
            }
        }
    }
}
