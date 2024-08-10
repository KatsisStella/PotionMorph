using System;
using UnityEngine;

namespace PotionMorph.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/RecipeInfo", fileName = "RecipeInfo")]
    public class RecipeInfo : ScriptableObject
    {
        public string Name;

        public IngredientInfo[] Ingredients;

        public RecipeEffect Effect;
    }

    public enum RecipeEffect
    {
        None,
        GrowAll,
        ReduceAll,
        AddPenis
    }
}