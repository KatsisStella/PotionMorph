﻿using PotionMorph.Persistency;
using UnityEngine;

namespace PotionMorph.Manager
{
    public class AethraManager : MonoBehaviour
    {
        public static AethraManager Instance { private set; get; }

        [SerializeField]
        private GameObject _aethraTriggerArea;

        [SerializeField]
        private Transform[] _containers;

        [SerializeField]
        private GameObject[] _boobs, _penises, _bodyHairs, _pregBoobs;

        [SerializeField]
        private GameObject _frontPonytail, _frontBraids, _frontSmallHair, _frontLongHair;

        [SerializeField]
        private GameObject _backPonytail, _backBraids, _backSmallHair, _backLongHair;

        [SerializeField]
        private GameObject _exprIdle, _exprHorny, _exprExcited, _exprSurprised, _exprBlush;

        [SerializeField]
        private GameObject _femaleJuice, _urine, _pheromones, _sweat;

        [SerializeField]
        private GameObject _cum, _milk;

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
            if (sd.IsPregnant)
            {
                _pregBoobs[Mathf.Clamp((int)sd.CurrentBreast, 0, _pregBoobs.Length - 1)].SetActive(true);
            }
            else
            {
                _boobs[(int)sd.CurrentBreast].SetActive(true);
            }
            if (sd.CurrentPenis != null) _penises[(int)sd.CurrentPenis].SetActive(true);
            switch (sd.CurrentHair)
            {
                case HairStyle.Ponytail:
                    _frontPonytail.SetActive(true);
                    _backPonytail.SetActive(true);
                    break;

                case HairStyle.Braids:
                    _frontBraids.SetActive(true);
                    _backBraids.SetActive(true);
                    break;

                case HairStyle.SmallHair:
                    _frontSmallHair.SetActive(true);
                    _backSmallHair.SetActive(true);
                    break;

                case HairStyle.LongHair:
                    _frontLongHair.SetActive(true);
                    _backLongHair.SetActive(true);
                    break;
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
    }
}