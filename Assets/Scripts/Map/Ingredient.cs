using UnityEngine;

namespace PotionMorph.Map
{
    public class Ingredient : MonoBehaviour, IProp
    {
        public bool CanGrab => true;

        public Rigidbody2D Rigidbody { private set; get; }
        public IMachine AssociatedMachine { set; get; }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }
    }
}
