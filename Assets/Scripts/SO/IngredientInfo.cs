using PotionMorph.Map;
using UnityEngine;

namespace PotionMorph.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/IngredientInfo", fileName = "IngredientInfo")]
    public class IngredientInfo : ScriptableObject
    {
        public string Name;

        public LiquidState LiquidState;
        public Color Color;

        public string SingleAdjective;
        public string TwoAdjective;
        public string ThreeName;

        public string Hint;
    }
}