using UnityEngine;

namespace PotionMorph.Map
{
    public interface IProp
    {
        public IMachine AssociatedMachine { set; get; }
        public bool CanGrab { get; }
        public bool CanReceiveIngredient { get; }
        public Rigidbody2D Rigidbody { get; }
    }
}
