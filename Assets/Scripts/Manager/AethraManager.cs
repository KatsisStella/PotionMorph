using PotionMorph.Persistency;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PotionMorph.Manager
{
    public class AethraManager : MonoBehaviour
    {
        public static AethraManager Instance { private set; get; }

        [SerializeField]
        private GameObject _aethraTriggerArea;

        [SerializeField]
        private Transform _choiceContainer;

        [SerializeField]
        private GameObject _choicePrefab;

        [SerializeField]
        private Transform _boobsContainer, _penisesContainer, _frontHairContainer, _backHairContainer;

        [SerializeField]
        private GameObject[] _boobs, _penises, _frontHairs, _backHairs;

        [SerializeField]
        private RuntimeAnimatorController _cumAnim, _milkAnim;

        [SerializeField]
        private GameObject _cum, _milk;

        private bool CanMilk => PersistencyManager.Instance.SaveData.CurrentBreast == Size.Big;
        private bool CanTakeCum => PersistencyManager.Instance.SaveData.CurrentPenis != null;

        private void Awake()
        {
            Instance = this;

            UpdateAethra();
        }

        public void UpdateAethra()
        {
            // Disable all
            for (int i = 0; i < _boobsContainer.childCount; i++) _boobsContainer.GetChild(i).gameObject.SetActive(false);
            for (int i = 0; i < _penisesContainer.childCount; i++) _penisesContainer.GetChild(i).gameObject.SetActive(false);
            for (int i = 0; i < _frontHairContainer.childCount; i++) _frontHairContainer.GetChild(i).gameObject.SetActive(false);
            for (int i = 0; i < _backHairContainer.childCount; i++) _backHairContainer.GetChild(i).gameObject.SetActive(false);

            // Spawn things
            var sd = PersistencyManager.Instance.SaveData;
            _boobs[(int)sd.CurrentBreast].SetActive(true);
            if (sd.CurrentPenis != null) _penises[(int)sd.CurrentPenis].SetActive(true);
            _frontHairs[(int)sd.CurrentHair - 1].SetActive(true);
            _backHairs[(int)sd.CurrentHair - 1].SetActive(true);
        }

        private void RemoveAllChoices()
        {
            _aethraTriggerArea.SetActive(true);
            for (int i = 0; i < _choiceContainer.childCount; i++)
            {
                Destroy(_choiceContainer.GetChild(i).gameObject);
            }
        }

        private void AddChoice(string text, System.Action action)
        {
            var go = Instantiate(_choicePrefab, _choiceContainer);
            go.GetComponentInChildren<TMP_Text>().text = text;
            go.GetComponent<Button>().onClick.AddListener(new(action));
        }

        public void ShowChoices()
        {
            RemoveAllChoices();
            _aethraTriggerArea.SetActive(false);

            if (CanMilk) AddChoice("Milking", () => { RecipeManager.Instance.SpawnIngredient(_milk); GameManager.Instance.PlayPreviewAnim(_milkAnim); RemoveAllChoices(); });
            if (CanTakeCum) AddChoice("Jacking", () => { RecipeManager.Instance.SpawnIngredient(_cum); GameManager.Instance.PlayPreviewAnim(_cumAnim); RemoveAllChoices(); });

            AddChoice("Cancel", () => { _aethraTriggerArea.SetActive(true); RemoveAllChoices(); });
        }
    }
}
