using PotionMorph.Manager;
using UnityEngine;

namespace PotionMorph.Map
{
    public class Aethra : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Spatula"))
            {
                var spatula = collision.GetComponent<Container>();
                if (spatula.Ingredients.Length >= 3)
                {
                    spatula.Empty();
                    GameManager.Instance.PlayPreviewAnim();
                }
            }
        }
    }
}
