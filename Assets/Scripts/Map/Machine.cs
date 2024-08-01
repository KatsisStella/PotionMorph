using PotionMorph.Manager;
using PotionMorph.Map;
using UnityEngine;

public class Machine : MonoBehaviour, IMachine
{
    [SerializeField]
    private Transform _slotPos;

    private IProp _registeredProp;

    public void Unregister(IProp prop)
    {
        _registeredProp.Rigidbody.bodyType = RigidbodyType2D.Dynamic;
        _registeredProp = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IProp>(out var prop) && prop.CanReceiveIngredient && _registeredProp == null)
        {
            GameManager.Instance.Drop(prop);
            prop.Rigidbody.bodyType = RigidbodyType2D.Kinematic;
            prop.Rigidbody.linearVelocity = Vector2.zero;
            prop.Rigidbody.transform.position = _slotPos.position;
            _registeredProp = prop;
        }
    }
}
