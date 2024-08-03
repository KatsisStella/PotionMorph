using PotionMorph.Manager;
using UnityEngine;

namespace PotionMorph.Map
{
    public abstract class AConsumeMachine : MonoBehaviour, IMachine
    {
        protected Detector _detector;

        protected Container _container;

        protected void Consume(Ingredient ingredient)
        {
            GameManager.Instance.Drop(ingredient); // Make sure we aren't holding it
            TreatConsumption(ingredient);
            Destroy(ingredient.gameObject);
        }

        protected abstract void TreatConsumption(Ingredient ingredient);

        public void Unregister(IProp prop)
        {
            _container.Rigidbody.bodyType = RigidbodyType2D.Dynamic;
            _container = null;
        }

        private void Awake()
        {
            _detector = GetComponentInChildren<Detector>();
            _detector.OnAdded.AddListener((go) => // When an ingredient is added, we check if it was be mixed
            {
                if (_container != null && _container.CanReceiveIngredient && go.TryGetComponent<Ingredient>(out var ingredient))
                {
                    Consume(ingredient);
                }
            });
        }
    }
}
