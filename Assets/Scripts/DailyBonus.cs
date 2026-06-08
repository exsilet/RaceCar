using System;
using SaveData;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class DailyBonus : MonoBehaviour
    {
        [Header("UI")] [SerializeField] private GameObject _bonusPanel;
        [SerializeField] private Button _claimButton;
        [SerializeField] private TMP_Text _rewardAmountText;
        [SerializeField] private TMP_Text _dayCounterText; // "День 14" / "День 53"
        [SerializeField] private TMP_Text _cycleText; // "Серия x3" (опционально)

        [Tooltip("7 объектов-иконок дней на панели (день 1..7 текущего цикла)")] [SerializeField]
        private GameObject[] _dayObjects;

        [Header("Базовые награды за 7 дней (цикл 1)")] [SerializeField]
        private int[] _baseRewards = { 100, 150, 200, 300, 400, 500, 1000 };

        [Tooltip("Во сколько раз растут награды каждый новый цикл из 7 дней")] [SerializeField]
        private float _cycleMultiplier = 2f;

        [Header("Зависимости")] [SerializeField]
        private PlayerMoney _playerMoney;

        [SerializeField] private SaveLoadService _saveLoad;

        private int _totalDayStreak;
        private int _currentCycle;
        private int _dayInCycle;

        private void Start()
        {
            LoadState();
            if (IsBonusAvailable())
                ShowPanel();
        }

        private void OnEnable() => _claimButton.onClick.AddListener(ClaimBonus);
        private void OnDisable() => _claimButton.onClick.RemoveListener(ClaimBonus);

        private bool IsBonusAvailable()
        {
            string lastDate = _saveLoad.ReadLastBonusDate();
            if (string.IsNullOrEmpty(lastDate)) return true;

            string today = DateTime.UtcNow.ToString("yyyy-MM-dd");
            return lastDate != today;
        }

        private void LoadState()
        {
            _totalDayStreak = _saveLoad.ReadBonusDay();

            string lastDate = _saveLoad.ReadLastBonusDate();
            if (!string.IsNullOrEmpty(lastDate))
            {
                if (DateTime.TryParse(lastDate, out DateTime last))
                {
                    double daysPassed = (DateTime.UtcNow - last).TotalDays;
                    if (daysPassed >= 2)
                    {
                        _totalDayStreak = _currentCycle * 7;
                    }
                }
            }

            RecalcCycleAndDay();
        }

        private void RecalcCycleAndDay()
        {
            _currentCycle = _totalDayStreak / 7;
            _dayInCycle = _totalDayStreak % 7;
        }

        private void ShowPanel()
        {
            _bonusPanel.SetActive(true);
            HighlightCurrentDay();

            int reward = GetTodayReward();
            if (_rewardAmountText != null)
                _rewardAmountText.text = UIMoney.FormatMoney(reward);

            if (_dayCounterText != null)
                _dayCounterText.text = $"День {_totalDayStreak + 1}";

            if (_cycleText != null && _currentCycle > 0)
                _cycleText.text = $"Серия x{Mathf.RoundToInt(Mathf.Pow(_cycleMultiplier, _currentCycle))}";
        }

        private void ClaimBonus()
        {
            int reward = GetTodayReward();
            _playerMoney.AddMoney(reward);

            _totalDayStreak++;
            RecalcCycleAndDay();

            string today = DateTime.UtcNow.ToString("yyyy-MM-dd");
            _saveLoad.SaveDailyBonus(today, _totalDayStreak);

            _bonusPanel.SetActive(false);
        }

        private int GetTodayReward()
        {
            if (_baseRewards == null || _baseRewards.Length == 0) return 100;

            int idx = Mathf.Clamp(_dayInCycle, 0, _baseRewards.Length - 1);
            float multiplier = Mathf.Pow(_cycleMultiplier, _currentCycle);
            return Mathf.RoundToInt(_baseRewards[idx] * multiplier);
        }

        private void HighlightCurrentDay()
        {
            for (int i = 0; i < _dayObjects.Length; i++)
            {
                if (_dayObjects[i] == null) continue;

                bool isToday = (i == _dayInCycle);
                bool isPast = (i < _dayInCycle);

                var cg = _dayObjects[i].GetComponent<CanvasGroup>();
                if (cg != null)
                    cg.alpha = isPast ? 0.5f : 1f;

                _dayObjects[i].transform.localScale = isToday
                    ? Vector3.one * 1.15f
                    : Vector3.one;
            }
        }
    }
}