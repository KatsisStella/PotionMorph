using PotionMorph.Manager;
using System.Linq;
using UnityEngine;

namespace PotionMorph.Map
{
    public class LiquidifierMachine : AConsumeMachine<Ingredient>
    {
        [SerializeField]
        private Transform _slotPos;

        protected override void TreatConsumption(Ingredient ingredient)
        {
            _container.Fill(ingredient);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<Container>(out var container) && container.CanReceiveIngredient && _container == null)
            {
                GameManager.Instance.Drop(container);
                container.Rigidbody.bodyType = RigidbodyType2D.Kinematic;
                container.Rigidbody.linearVelocity = Vector2.zero;
                container.Rigidbody.transform.position = _slotPos.position;
                container.AssociatedMachine = this;
                _container = container;

                // We put a glass under, is there any ingredient waiting to be mixed?
                var ingredient = _detector.Contained.Select(x => x.GetComponent<Ingredient>()).FirstOrDefault(x => x != null);
                if (ingredient != null)
                {
                    Consume(ingredient);
                }
            }
        }
    }
}
