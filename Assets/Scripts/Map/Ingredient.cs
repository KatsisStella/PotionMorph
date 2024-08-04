using PotionMorph.SO;
using UnityEngine;

namespace PotionMorph.Map
{
    public class Ingredient : MonoBehaviour, IProp
    {
        [SerializeField] private IngredientInfo _info;

        public IngredientInfo Info => _info;
        public LiquidState LiquidState => _info.LiquidState;
        public Color Color => _info.Color;

        public bool CanGrab => true;

        public Rigidbody2D Rigidbody { private set; get; }
        public IMachine AssociatedMachine { set; get; }
        public bool IsPendingDeletion { set; get; }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }
    }
}
