using UnityEngine;

namespace PotionMorph.Map
{
    public class Ingredient : MonoBehaviour, IProp
    {
        [SerializeField] private LiquidState _liquidState;
        [SerializeField] private Color _color;

        public LiquidState LiquidState => _liquidState;
        public Color Color => _color;

        public bool CanGrab => true;

        public Rigidbody2D Rigidbody { private set; get; }
        public IMachine AssociatedMachine { set; get; }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }
    }
}
