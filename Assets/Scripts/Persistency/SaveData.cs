using System;
using System.Collections.Generic;

namespace PotionMorph.Persistency
{
    public class SaveData
    {
        public Size CurrentBreast { set; get; } = Size.Small;
        public Size? CurrentPenis { set; get; } = null;
        public Size CurrentHair { set; get; } = Size.Medium;
        public Size CurrentBodyHair { set; get; } = Size.Small;
        public Expression CurrentExpression { set; get; } = Expression.Idle;
        public bool HavePheromoneCloud { set; get; } = false;
        public bool HaveSweat { set; get; } = false;
        public Juice Juice { set; get; } = Juice.None;
        public bool IsPregnant = false;
        public List<string> DiscoveredRecipes { set; get; } = new();

        public void UpdateBreast(bool increase)
        {
            CurrentBreast = (Size)Math.Clamp((int)CurrentBreast + (increase ? 1 : -1), (int)Size.Small, (int)Size.Big);
        }

        public void UpdatePenis(bool increase)
        {
            if (CurrentPenis == null) return;
            CurrentPenis = (Size)Math.Clamp((int)CurrentPenis + (increase ? 1 : -1), (int)Size.Small, (int)Size.Big);
        }

        public void UpdateHair(bool increase)
        {
            CurrentHair = (Size)Math.Clamp((int)CurrentHair + (increase ? 1 : -1), (int)Size.Small, (int)Size.Medium);
        }

        public void UpdateBodyHair(bool increase)
        {
            CurrentBodyHair = (Size)Math.Clamp((int)CurrentBodyHair + (increase ? 1 : -1), (int)Size.Small, (int)Size.Medium);
        }

        public void TogglePenis(bool enable)
        {
            CurrentPenis = enable ? Size.Medium : null;
        }

        public void TogglePheromoneCloud(bool enable)
        {
            HavePheromoneCloud = enable;
        }

        public void ToggleSweat(bool enable)
        {
            HaveSweat = enable;
        }

        public void TogglePregnancy(bool enable)
        {
            IsPregnant = enable;
        }

        public void SetJuice(Juice juice)
        {
            Juice = juice;
        }

        public void SetExpression(Expression expression)
        {
            CurrentExpression = expression;
        }

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