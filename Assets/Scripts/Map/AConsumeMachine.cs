using PotionMorph.Manager;
using UnityEngine;

namespace PotionMorph.Map
{
    public abstract class AConsumeMachine<TAccept> : MonoBehaviour, IMachine
        where TAccept : MonoBehaviour, IProp
    {
        protected Detector _detector;

        protected Container _container;

        protected void Consume(TAccept ingredient)
        {
            GameManager.Instance.Drop(ingredient); // Make sure we aren't holding it
            TreatConsumption(ingredient);
            Destroy(ingredient.gameObject);
            ingredient.IsPendingDeletion = true;
        }

        protected abstract void TreatConsumption(TAccept ingredient);
        protected virtual void DenyConsumption(GameObject ingredient)
        { }

        public virtual void Unregister(IProp prop)
        {
            _container.Rigidbody.bodyType = RigidbodyType2D.Dynamic;
            _container = null;
        }

        protected virtual bool CanTreat(GameObject go, out TAccept output)
        {
            if (_container != null && _container.CanReceiveIngredient && go.TryGetComponent(out output) && !output.IsPendingDeletion)
            {
                return true;
            }
            output = null;
            return false;
        }

        protected virtual void Awake()
        {
            _detector = GetComponentInChildren<Detector>();
            _detector.OnAdded.AddListener((go) => // When an ingredient is added, we check if it was be mixed
            {
                if (CanTreat(go, out var ingredient))
                {
                    Consume(ingredient);
                }
                else
                {
                    DenyConsumption(go);
                }
            });
        }
    }
}
