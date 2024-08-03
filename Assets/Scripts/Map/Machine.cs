using PotionMorph.Manager;
using PotionMorph.Map;
using System.Linq;
using UnityEngine;

public class Machine : MonoBehaviour, IMachine
{
    [SerializeField]
    private Transform _slotPos;

    private IProp _registeredProp;
    private Detector _detector;

    private void Awake()
    {
        _detector = GetComponentInChildren<Detector>();
        _detector.OnAdded.AddListener((go) => // When an ingredient is added, we check if it was be mixed
        {
            if (_registeredProp != null && go.TryGetComponent<Ingredient>(out var ingredient))
            {
                Consume(ingredient);
            }
        });
    }

    private void Consume(Ingredient ingredient)
    {
        GameManager.Instance.Drop(ingredient); // Make sure we aren't holding it
        Destroy(ingredient.gameObject);
    }

    public void Unregister(IProp prop)
    {
        _registeredProp.Rigidbody.bodyType = RigidbodyType2D.Dynamic;
        _registeredProp = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Container>(out var container) && container.CanReceiveIngredient && _registeredProp == null)
        {
            GameManager.Instance.Drop(container);
            container.Rigidbody.bodyType = RigidbodyType2D.Kinematic;
            container.Rigidbody.linearVelocity = Vector2.zero;
            container.Rigidbody.transform.position = _slotPos.position;
            _registeredProp = container;

            // We put a glass under, is there any ingredient waiting to be mixed?
            var ingredient = _detector.Contained.Select(x => x.GetComponent<Ingredient>()).FirstOrDefault(x => x != null);
            if (ingredient != null)
            {
                Consume(ingredient);
            }
        }
    }
}
