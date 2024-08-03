using PotionMorph.Manager;
using UnityEngine;

namespace PotionMorph.Map
{
    public class Aethra : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<Container>(out var container) && container.Ingredients.Length >= 3)
            {
                container.Empty();
                GameManager.Instance.PlayPreviewAnim();
            }
        }
    }
}
