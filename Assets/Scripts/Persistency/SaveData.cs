using System;
using System.Collections.Generic;

namespace PotionMorph.Persistency
{
    public class SaveData
    {
        public Size CurrentBreast { set; get; } = Size.Medium;
        public Size? CurrentPenis { set; get; } = null;
        public Size CurrentHair { set; get; } = Size.Big;
        public List<string> DiscoveredRecipes { set; get; } = new();

        public void GrowAll()
        {
            if (CurrentBreast < Size.Big) CurrentBreast++;
            if (CurrentPenis != null && CurrentPenis < Size.Big) CurrentPenis++;
            if (CurrentHair < Size.Big) CurrentHair++;

            PersistencyManager.Instance.Save();
        }

        public void AddPenis()
        {
            if (CurrentPenis == null) CurrentPenis = Size.Medium;

            PersistencyManager.Instance.Save();
        }

        public void ReduceAll()
        {
            if (CurrentBreast > Size.Small) CurrentBreast--;
            if (CurrentPenis != null && CurrentPenis > Size.Small) CurrentPenis--;
            if (CurrentHair > Size.Medium) CurrentHair--;

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