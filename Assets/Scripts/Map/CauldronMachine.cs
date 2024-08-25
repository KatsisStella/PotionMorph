using PotionMorph.Manager;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PotionMorph.Map
{
    public class CauldronMachine : AConsumeMachine<Ingredient>
    {
        [SerializeField]
        private Container _spatula;

        private readonly List<Ingredient> _ingredients = new();

        private Vector3 _baseSpatulaPos;

        protected override void Awake()
        {
            base.Awake();

            _baseSpatulaPos = _spatula.transform.position;

            // Put back spatula on the cauldron
            _detector.OnAdded.AddListener((go) =>
            {
                if (go.CompareTag("Spatula") && !go.GetComponent<Container>().HasAny)
                {
                    _spatula.Rigidbody.linearVelocity = Vector2.zero;
                    _spatula.Rigidbody.bodyType = RigidbodyType2D.Kinematic;
                    _spatula.transform.position = _baseSpatulaPos;
                    _spatula.AssociatedMachine = this;
                    _spatula.CanGrab = false;
                    _container = _spatula;
                }
            });
        }

        private void Start()
        {
            _container = _spatula;
            _spatula.Rigidbody.bodyType = RigidbodyType2D.Kinematic;
            _spatula.CanGrab = false;
            _spatula.AssociatedMachine = this;
        }

        protected override bool CanTreat(GameObject go, out Ingredient output)
        {
            return base.CanTreat(go, out output) && _spatula.AssociatedMachine != null;
        }

        protected override void TreatConsumption(Ingredient ingredient)
        {
            Debug.Log($"Adding {ingredient.name} to cauldron");
            _ingredients.Add(ingredient);
            if (_ingredients.Count >= 3)
            {
                _spatula.Fill(_ingredients.ToArray());
                _ingredients.Clear();
                _spatula.CanGrab = true;
            }
        }

        protected override void DenyConsumption(GameObject ingredient)
        {
            base.DenyConsumption(ingredient);

            if (!ingredient.CompareTag("Spatula") && ingredient.TryGetComponent<Rigidbody2D>(out var rb))
            {
                if (ingredient.TryGetComponent<IProp>(out var prop))
                {
                    GameManager.Instance.Drop(prop);
                }
                rb.AddForce(new Vector2(Random.Range(-.25f, .25f), 1f).normalized * 20f, ForceMode2D.Impulse);
            }
        }
    }
}
