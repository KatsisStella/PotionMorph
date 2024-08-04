using UnityEngine;

namespace PotionMorph.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/IngredientInfo", fileName = "IngredientInfo")]
    public class IngredientInfo : ScriptableObject
    {
        public string Name;

        public string SingleAdjective;
        public string TwoAdjective;
        public string ThreeName;
    }
}