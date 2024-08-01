using UnityEngine;

namespace PotionMorph.Map
{
    public class Container : MonoBehaviour, IProp
    {
        public bool CanGrab { private set; get; } = true;

        public bool CanReceiveIngredient { private set; get; } = true;

        public Rigidbody2D Rigidbody { private set; get; }
        public IMachine AssociatedMachine { set; get; }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }
    }
}
