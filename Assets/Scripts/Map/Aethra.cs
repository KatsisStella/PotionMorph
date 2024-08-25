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
            if (collision.TryGetComponent<Container>(out var container) && !container.CanReceiveIngredient)
            {
                RecipeManager.Instance.LoadRecipe(container.GetAllIngredients().Select(x => x.Info).ToArray());
                container.Empty();
                GameManager.Instance.PlayPreviewAnim(_drinking);
            }
        }
    }
}
