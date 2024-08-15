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
        private Transform[] _containers;

        [SerializeField]
        private GameObject[] _boobs, _penises, _frontHairs, _backHairs, _bodyHairs;

        [SerializeField]
        private GameObject _exprIdle, _exprHorny, _exprExcited, _exprSurprised, _exprBlush;

        [SerializeField]
        private GameObject _femaleJuice, _urine, _pheromones, _sweat;

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
            foreach (var c in _containers)
            {
                for (int i = 0; i < c.childCount; i++) c.GetChild(i).gameObject.SetActive(false);
            }

            // Spawn things
            var sd = PersistencyManager.Instance.SaveData;
            _boobs[(int)sd.CurrentBreast].SetActive(true);
            if (sd.CurrentPenis != null) _penises[(int)sd.CurrentPenis].SetActive(true);
            _frontHairs[(int)sd.CurrentHair].SetActive(true);
            _backHairs[(int)sd.CurrentHair].SetActive(true);
            if (sd.CurrentBodyHair > Size.Small)
            {
                _backHairs[(int)sd.CurrentHair - 1].SetActive(true);
            }
            switch (sd.CurrentExpression)
            {
                case Expression.Idle: _exprIdle.SetActive(true); break;
                case Expression.Horny: _exprHorny.SetActive(true); break;
                case Expression.Excited: _exprExcited.SetActive(true); break;
                case Expression.Surprised: _exprSurprised.SetActive(true); break;
                case Expression.Blush: _exprBlush.SetActive(true); break;
            }
            switch (sd.Juice)
            {
                case Juice.None: break;
                case Juice.FemaleJuice: _femaleJuice.SetActive(true); break;
                case Juice.Urine: _urine.SetActive(true); break;
            }
            if (sd.HavePheromoneCloud) _pheromones.SetActive(true);
            if (sd.HaveSweat) _sweat.SetActive(true);
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
