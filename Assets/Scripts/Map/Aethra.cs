using PotionMorph.Manager;
using System.Linq;
using UnityEngine;

namespace PotionMorph.Map
{
    public class Aethra : MonoBehaviour
    {
        [SerializeField]
        private RuntimeAnimatorController _drinking;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Spatula"))
            {
                var spatula = collision.GetComponent<Container>();
                if (spatula.HasAtLeast(3))
                {
                    RecipeManager.Instance.LoadRecipe(spatula.GetAllIngredients().Select(x => x.Info).ToArray());
                    spatula.Empty();
                    GameManager.Instance.PlayPreviewAnim(_drinking);
                }
            }
        }
    }
}
