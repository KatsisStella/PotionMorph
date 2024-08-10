using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PotionMorph.Manager
{
    public class AethraManager : MonoBehaviour
    {
        public static AethraManager Instance { private set; get; }

        [SerializeField]
        private Transform _choiceContainer;

        [SerializeField]
        private GameObject _choicePrefab;

        [SerializeField]
        private Transform _boobsContainer, _penisesContainer;

        [SerializeField]
        private GameObject[] _boobs, _penises;

        [SerializeField]
        private RuntimeAnimatorController _cumAnim, _milkAnim;

        [SerializeField]
        private GameObject _cum, _milk;

        private Size _currentBreast = Size.Medium;
        private Size? _currentPenis;

        private bool CanMilk => _currentBreast == Size.Big;
        private bool CanTakeCum => _currentPenis != null;

        private void Awake()
        {
            Instance = this;
        }

        public void GrowAll()
        {
            if (_currentBreast < Size.Big) _currentBreast++;
            if (_currentPenis != null && _currentPenis < Size.Big) _currentPenis++;

            UpdateAethra();
        }

        public void AddPenis()
        {
            if (_currentPenis == null) _currentPenis = Size.Medium;

            UpdateAethra();
        }

        public void ReduceAll()
        {
            if (_currentBreast > Size.Small) _currentBreast--;
            if (_currentPenis != null && _currentPenis > Size.Small) _currentPenis--;

            UpdateAethra();
        }

        private void UpdateAethra()
        {
            // Disable all
            for (int i = 0; i < _boobsContainer.childCount; i++) _boobsContainer.GetChild(i).gameObject.SetActive(false);
            for (int i = 0; i < _penisesContainer.childCount; i++) _penisesContainer.GetChild(i).gameObject.SetActive(false);

            // Spawn things
            _boobs[(int)_currentBreast].SetActive(true);
            if (_currentPenis != null) _penises[(int)_currentPenis].SetActive(true);
        }

        private void RemoveAllChoices()
        {
            for (int i = 0; i < _choiceContainer.childCount; i++)
            {
                Destroy(_choiceContainer.GetChild(i).gameObject);
            }
        }

        private void AddChoice(string text, Action action)
        {
            var go = Instantiate(_choicePrefab, _choiceContainer);
            go.GetComponentInChildren<TMP_Text>().text = text;
            go.GetComponent<Button>().onClick.AddListener(new(action));
        }

        public void ShowChoices()
        {
            RemoveAllChoices();

            if (CanMilk) AddChoice("Milking", () => { RecipeManager.Instance.SpawnIngredient(_milk); GameManager.Instance.PlayPreviewAnim(_milkAnim); });
            if (CanTakeCum) AddChoice("Jacking", () => { RecipeManager.Instance.SpawnIngredient(_cum); GameManager.Instance.PlayPreviewAnim(_cumAnim); });

            AddChoice("Cancel", RemoveAllChoices);
        }

        public enum Size
        {
            Small,
            Medium,
            Big
        }
    }
}
