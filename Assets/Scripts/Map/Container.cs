using System;
using System.Linq;
using UnityEngine;

namespace PotionMorph.Map
{
    public class Container : MonoBehaviour, IProp
    {
        [SerializeField]
        private SpriteRenderer _liquid;

        [SerializeField]
        private Sprite[] _liquidSprite;

        public bool CanGrab { set; get; } = true;

        public bool CanReceiveIngredient { private set; get; } = true;

        public Rigidbody2D Rigidbody { private set; get; }
        public IMachine AssociatedMachine { set; get; }
        public bool IsPendingDeletion { set; get; }

        public Ingredient[] Ingredients { private set; get; } = Array.Empty<Ingredient>();

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Fill(params Ingredient[] ingredients)
        {
            CanReceiveIngredient = false;
            Color c = ingredients[0].Color;
            foreach (var curr in ingredients.Skip(1))
            {
                c += curr.Color;
            }
            _liquid.color = c / ingredients.Length;
            _liquid.sprite = _liquidSprite[Mathf.RoundToInt(ingredients.Sum(x => (float)x.LiquidState) / ingredients.Length)];
            _liquid.gameObject.SetActive(true);
            Ingredients = ingredients;
        }
    }
}
