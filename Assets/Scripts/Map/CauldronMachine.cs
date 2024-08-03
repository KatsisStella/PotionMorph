using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PotionMorph.Map
{
    public class CauldronMachine : AConsumeMachine<Container>
    {
        [SerializeField]
        private Container _spatula;

        private readonly List<Container> _ingredients = new();

        protected override void Awake()
        {
            base.Awake();
            _container = _spatula;
            _spatula.Rigidbody.bodyType = RigidbodyType2D.Kinematic;
            _spatula.CanGrab = false;
        }

        protected override bool CanTreat(GameObject go, out Container output)
        {
            return base.CanTreat(go, out output) && output.Ingredients.Any();
        }

        protected override void TreatConsumption(Container ingredient)
        {
            if (!ingredient.Ingredients.Any()) return;

            Debug.Log($"Adding {ingredient.name} to cauldron (+{ingredient.Ingredients.Length})");
            _ingredients.Add(ingredient);
            if (_ingredients.Sum(x => x.Ingredients.Length) >= 3)
            {
                _spatula.Fill(_ingredients.SelectMany(x => x.Ingredients).ToArray());
                _spatula.CanGrab = true;
            }
        }
    }
}
