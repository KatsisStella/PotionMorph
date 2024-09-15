using PotionMorph.SO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PotionMorph.Map
{
    public class RecipeData : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _name;

        [SerializeField]
        private Transform _iconsPrefab;

        [SerializeField]
        private GameObject _iconsTransform;

        public void Init(RecipeInfo info)
        {
            _name.text = info.Name;
            foreach (var i in info.Ingredients)
            {
                var go = Instantiate(_iconsTransform, _iconsPrefab);
                go.GetComponent<Image>().sprite = i.Sprite;
            }
        }
    }
}
