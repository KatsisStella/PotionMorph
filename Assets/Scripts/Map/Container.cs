using UnityEngine;

namespace PotionMorph.Map
{
    public class Container : MonoBehaviour, IProp
    {
        [SerializeField]
        private Sprite[] _liquidSprite;

        public bool CanGrab { private set; get; } = true;

        public bool CanReceiveIngredient { private set; get; } = true;

        public Rigidbody2D Rigidbody { private set; get; }
        private SpriteRenderer _sr;
        public IMachine AssociatedMachine { set; get; }

        private Sprite _emptySprite;

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            _sr = GetComponent<SpriteRenderer>();
            _emptySprite = _sr.sprite;
        }

        public void Fill(Ingredient ingredient)
        {
            CanReceiveIngredient = false;
            _sr.color = ingredient.Color;
            _sr.sprite = _liquidSprite[(int)ingredient.LiquidState];
        }
    }
}
