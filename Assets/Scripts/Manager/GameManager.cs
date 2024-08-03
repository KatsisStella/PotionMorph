using PotionMorph.Map;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PotionMorph.Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { private set; get; }

        [SerializeField]
        private Animator _previewAnim;

        private Camera _cam;

        private IProp _follower;

        private void Awake()
        {
            Instance = this;
            _cam = Camera.main;
        }

        private void FixedUpdate()
        {
            if (_follower != null)
            {
                _follower.Rigidbody.linearVelocity = (_cam.ScreenToWorldPoint(MousePos) - _follower.Rigidbody.transform.position) * 10f;
            }
        }

        public void PlayPreviewAnim()
        {
            _previewAnim.gameObject.SetActive(true);
            _previewAnim.SetTrigger("Run");
        }

        private Vector2 MousePos =>
#if UNITY_ANDROID && !UNITY_EDITOR
                Touchscreen.current.primaryTouch.position.ReadValue();
#else
                Mouse.current.position.ReadValue();
#endif

        public void Drop(IProp prop)
        {
            if (_follower != null && _follower == prop)
            {
                _follower.Rigidbody.gravityScale = 1f;
                _follower = null;
            }
        }

        public void OnMousePressed(InputAction.CallbackContext value)
        {
            if (value.phase == InputActionPhase.Performed)
            {
                // Mouse position
                var mousePos = MousePos;
                var ray = _cam.ScreenPointToRay(mousePos);
                var hit = Physics2D.Raycast(ray.origin, ray.direction, float.MaxValue, LayerMask.GetMask("Prop"));
                if (hit.collider != null && hit.collider.TryGetComponent<IProp>(out var prop) && prop.CanGrab)
                {
                    if (prop.AssociatedMachine != null)
                    {
                        prop.AssociatedMachine.Unregister(prop);
                        prop.AssociatedMachine = null;
                    }
                    _follower = prop;
                    _follower.Rigidbody.bodyType = RigidbodyType2D.Dynamic;
                    _follower.Rigidbody.gravityScale = 0f;
                }
            }
            else if (value.phase == InputActionPhase.Canceled)
            {
                Drop(_follower);
            }
        }
    }
}
