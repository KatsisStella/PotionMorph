using UnityEngine;

namespace PotionMorph.Map
{
    public class Ingredient : MonoBehaviour, IProp
    {
        public bool CanGrab => true;
        public bool CanReceiveIngredient => false;

        public Rigidbody2D Rigidbody { private set; get; }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }
    }
}
