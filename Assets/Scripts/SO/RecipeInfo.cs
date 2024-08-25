using System;
using UnityEngine;

namespace PotionMorph.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/RecipeInfo", fileName = "RecipeInfo")]
    public class RecipeInfo : ScriptableObject
    {
        public string Name;

        public string Description;

        public IngredientInfo[] Ingredients;

        public RecipeEffect[] Effect;
    }

    public enum RecipeEffect
    {
        IncreaseBreast,
        DecreaseBreast,
        IncreasePenis,
        DecreasePenis,
        IncreaseHair,
        DecreaseHair,
        IncreasePubes,
        DecreasePubes,
        EnablePenis,
        DisablePenis,
        EnablePheromones,
        DisablePheromones,
        EnableSweat,
        DisableSweat,
        EnablePregnancy,
        DisablePregnancy,
        UnsetJuice,
        SetUrine,
        SetFemaleJuice,
        SetExpIdle,
        SetExpHorny,
        SetExpExcited,
        SetExpSurprised,
        SetExpBlush,
        SetPonytail,
        SetBraids,
        SetSmallHair,
        SetLongHair
    }
}