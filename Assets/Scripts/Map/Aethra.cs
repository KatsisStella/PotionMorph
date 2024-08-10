using PotionMorph.Manager;
using System.Linq;
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
                    RecipeManager.Instance.LoadRecipe(spatula.Ingredients.Select(x => x.Info).ToArray());
                    spatula.Empty();
                    GameManager.Instance.PlayPreviewAnim();
                }
            }
        }

        private void OnMouseUpAsButton()
        {
            AethraManager.Instance.ShowChoices();
        }
    }
}
