using PotionMorph.Map;
using System.Collections;
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

        public void ShowRecipeText(Ingredient[] ingredients)
        {
            _recipeText.gameObject.SetActive(true);

            _recipeText.text = "Unidentified mix";

            StartCoroutine(RemoveRecipeText());
        }

        private IEnumerator RemoveRecipeText()
        {
            yield return new WaitForSeconds(3f);
            _recipeText.gameObject.SetActive(false);
        }
    }
}
