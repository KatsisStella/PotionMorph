using PotionMorph.Manager;
using PotionMorph.Map;
using UnityEngine;

public class Machine : MonoBehaviour
{
    [SerializeField]
    private Transform _slotPos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IProp>(out var prop) && prop.CanReceiveIngredient)
        {
            GameManager.Instance.Drop(prop);
            prop.Rigidbody.bodyType = RigidbodyType2D.Kinematic;
            prop.Rigidbody.linearVelocity = Vector2.zero;
            prop.Rigidbody.transform.position = _slotPos.position;
        }
    }
}
