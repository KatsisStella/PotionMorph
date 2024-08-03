using UnityEngine;

namespace PotionMorph.Map
{
    public interface IProp
    {
        public IMachine AssociatedMachine { set; get; }
        public bool CanGrab { get; }
        public Rigidbody2D Rigidbody { get; }

        public bool IsPendingDeletion { set; get; }
    }
}
