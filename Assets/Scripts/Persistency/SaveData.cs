using PotionMorph.Manager;
using System;
using System.Collections.Generic;

namespace PotionMorph.Persistency
{
    public class SaveData
    {
        public Size CurrentBreast { set; get; } = Size.Small;
        public Size? CurrentPenis { set; get; } = null;
        public HairStyle CurrentHair { set; get; } = HairStyle.LongHair;
        public Size CurrentBodyHair { set; get; } = Size.Small;
        public Expression CurrentExpression { set; get; } = Expression.Idle;
        public bool HavePheromoneCloud { set; get; } = false;
        public bool HaveSweat { set; get; } = false;
        public Juice Juice { set; get; } = Juice.None;
        public bool IsPregnant = false; public List<AethraIngredient> AethraIngredients { set; get; } = new();

        public List<string> DiscoveredRecipes { set; get; } = new();

        public void UpdateBreast(bool increase)
        {
            CurrentBreast = (Size)Math.Clamp((int)CurrentBreast + (increase ? 1 : -1), (int)Size.Small, (int)Size.Big);
            if (CurrentBreast == Size.Big) RecipeManager.Instance.AddIngredient(AethraIngredient.Milk);
        }

        public void ResetBreast()
        {
            CurrentBreast = Size.Medium;
        }

        public void UpdatePenis(bool increase)
        {
            if (CurrentPenis == null) return;
            CurrentPenis = (Size)Math.Clamp((int)CurrentPenis + (increase ? 1 : -1), (int)Size.Small, (int)Size.Big);
        }

        public void SetHair(HairStyle hairStyle)
        {
            CurrentHair = hairStyle;
        }

        public void UpdateBodyHair(bool increase)
        {
            CurrentBodyHair = (Size)Math.Clamp((int)CurrentBodyHair + (increase ? 1 : -1), (int)Size.Small, (int)Size.Medium);
            if (increase) RecipeManager.Instance.AddIngredient(AethraIngredient.Pubes);
        }

        public void TogglePenis(bool enable)
        {
            CurrentPenis = enable ? Size.Medium : null;
            if (enable) RecipeManager.Instance.AddIngredient(AethraIngredient.Cum);
        }

        public void TogglePheromoneCloud(bool enable)
        {
            HavePheromoneCloud = enable;
            if (enable) RecipeManager.Instance.AddIngredient(AethraIngredient.Pheromones);
        }

        public void ToggleSweat(bool enable)
        {
            HaveSweat = enable;
            if (enable) RecipeManager.Instance.AddIngredient(AethraIngredient.Sweat);
        }

        public void TogglePregnancy(bool enable)
        {
            IsPregnant = enable;
        }

        public void SetJuice(Juice juice)
        {
            Juice = juice;
            if (juice == Juice.FemaleJuice) RecipeManager.Instance.AddIngredient(AethraIngredient.FemaleJuice);
            else if (juice == Juice.Urine) RecipeManager.Instance.AddIngredient(AethraIngredient.Urine);
        }

        public void SetExpression(Expression expression)
        {
            CurrentExpression = expression;
            if (expression == Expression.Horny) RecipeManager.Instance.AddIngredient(AethraIngredient.Saliva);
        }

    }

    public enum AethraIngredient
    {
        Cum,
        FemaleJuice,
        Milk,
        Pheromones,
        Pubes,
        Saliva,
        Sweat,
        Urine
    }

    public enum HairStyle
    {
        Ponytail,
        Braids,
        SmallHair,
        LongHair
    }

    public enum Size
    {
        Small,
        Medium,
        Big
    }

    public enum Expression
    {
        Idle,
        Horny,
        Excited,
        Surprised,
        Blush
    }

    public enum Juice
    {
        None,
        FemaleJuice,
        Urine
    }
}