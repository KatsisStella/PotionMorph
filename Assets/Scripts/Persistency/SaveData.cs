using System;
using System.Collections.Generic;

namespace PotionMorph.Persistency
{
    public class SaveData
    {
        public Size CurrentBreast { set; get; } = Size.Medium;
        public Size? CurrentPenis { set; get; } = null;
        public Size CurrentHair { set; get; } = Size.Medium;
        public Size CurrentBodyHair {  set; get; } = Size.Small;
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

        public void GrowAll()
        {
            UpdateBreast(true);
            UpdatePenis(true);
            UpdateHair(true);

            PersistencyManager.Instance.Save();
        }

        public void AddPenis()
        {
            TogglePenis(true);

            PersistencyManager.Instance.Save();
        }

        public void ReduceAll()
        {
            UpdateBreast(false);
            UpdatePenis(false);
            UpdateHair(false);

            PersistencyManager.Instance.Save();
        }
    }

    public enum Size
    {
        Small,
        Medium,
        Big
    }
}