using UnityEngine;

namespace PotionMorph.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/RecipeInfo", fileName = "RecipeInfo")]
    public class RecipeInfo : ScriptableObject
    {
        public string Name;

        public IngredientInfo[] Ingredients;
    }
}