using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PotionMorph.Map
{
    public class Detector : MonoBehaviour
    {
        private readonly List<GameObject> _contained = new();
        public IReadOnlyList<GameObject> Contained => _contained.AsReadOnly();

        public UnityEvent<GameObject> OnAdded { get; } = new();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _contained.Add(collision.gameObject);
            OnAdded?.Invoke(collision.gameObject);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            _contained.Remove(collision.gameObject);
        }
    }
}
