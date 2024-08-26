using System.Collections.Generic;
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

        private readonly List<Ingredient> _ingredients = new();
        public bool HasAny => _ingredients.Any();
        public bool HasAtLeast(int number) => _ingredients.Count >= number;
        public int IngredientCount => _ingredients.Count;
        public IReadOnlyList<Ingredient> GetAllIngredients() => _ingredients.AsReadOnly();

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (transform.position.y < -10f)
            {
                Destroy(gameObject);
            }
        }

        public void Fill(params Ingredient[] newIngredients)
        {
            CanReceiveIngredient = false;
            Color c = newIngredients[0].Color;
            foreach (var curr in newIngredients.Skip(1))
            {
                c += curr.Color;
            }
            _liquid.color = c / newIngredients.Length;
            _liquid.sprite = _liquidSprite[Mathf.RoundToInt(newIngredients.Sum(x => (float)x.LiquidState) / newIngredients.Length)];
            _liquid.gameObject.SetActive(true);
            _ingredients.AddRange(newIngredients);
        }

        public void Empty()
        {
            _liquid.gameObject.SetActive(false);
            _ingredients.Clear();
            CanReceiveIngredient = true;
        }
    }
}
