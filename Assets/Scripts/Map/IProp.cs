using UnityEngine;

namespace PotionMorph.Map
{
    public interface IProp
    {
        public bool CanGrab { get; }
        public bool CanReceiveIngredient { get; }
        public Rigidbody2D Rigidbody { get; }
    }
}
