using PotionMorph.Map;
using PotionMorph.Persistency;
using UnityEngine;

namespace PotionMorph.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/IngredientInfo", fileName = "IngredientInfo")]
    public class IngredientInfo : ScriptableObject
    {
        public string Name;

        public AethraIngredient AethraIngredient;
        public LiquidState LiquidState;
        public Color Color;

        public string SingleAdjective;
        public string TwoAdjective;
        public string ThreeName;

        public string Hint;
    }
}