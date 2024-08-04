using PotionMorph.SO;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

namespace PotionMorph.Manager
{
    public class RecipeManager : MonoBehaviour
    {
        public static RecipeManager Instance { private set; get; }

        [SerializeField]
        private TMP_Text _recipeText;

        private void Awake()
        {
            Instance = this;
        }

        public void LoadRecipe(IngredientInfo[] ingredients)
        {
            _recipeText.gameObject.SetActive(true);

            if (ingredients.Distinct().Count() == 1)
            {
                _recipeText.text = ingredients[0].ThreeName;
            }
            else if (ingredients.Distinct().Count() == 2)
            {
                var groups = ingredients.GroupBy(x => x.Name).OrderByDescending(x => x.Count()).ToArray();
                _recipeText.text = $"{groups[0].ElementAt(0).TwoAdjective} {groups[1].ElementAt(0).SingleAdjective}";
            }
            else
            {
                _recipeText.text = "Unidentified mix";
            }

            StartCoroutine(RemoveRecipeText());
        }

        private IEnumerator RemoveRecipeText()
        {
            yield return new WaitForSeconds(3f);
            _recipeText.gameObject.SetActive(false);
        }
    }
}
