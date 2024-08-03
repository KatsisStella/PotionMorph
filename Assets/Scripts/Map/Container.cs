using System;
using System.Linq;
using UnityEngine;

namespace PotionMorph.Map
{
    public class Container : MonoBehaviour, IProp
    {
        [SerializeField]
        private Sprite[] _liquidSprite;

        public bool CanGrab { set; get; } = true;

        public bool CanReceiveIngredient { private set; get; } = true;

        public Rigidbody2D Rigidbody { private set; get; }
        private SpriteRenderer _sr;
        public IMachine AssociatedMachine { set; get; }

        private Sprite _emptySprite;

        public Ingredient[] Ingredients { private set; get; } = Array.Empty<Ingredient>();

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            _sr = GetComponent<SpriteRenderer>();
            _emptySprite = _sr.sprite;
        }

        public void Fill(params Ingredient[] ingredients)
        {
            CanReceiveIngredient = false;
            Color c = ingredients[0].Color;
            foreach (var curr in ingredients.Skip(1))
            {
                c += curr.Color;
            }
            _sr.color = c;
            _sr.sprite = _liquidSprite[Mathf.RoundToInt(ingredients.Sum(x => (float)x.LiquidState) / ingredients.Length)];
            Ingredients = ingredients;
        }
    }
}
