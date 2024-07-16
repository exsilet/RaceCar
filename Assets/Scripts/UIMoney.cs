using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class UIMoney : MonoBehaviour
    {
        [SerializeField] private TMP_Text _moneyText;
        [SerializeField] private PlayerMoney _playerMoney;

        private void Start()
        {
            _moneyText.text = _playerMoney.Money.ToString();
        }

        private void OnEnable()
        {
            _playerMoney.CurrentMoneyChanged += CountMoney;
        }

        private void OnDisable()
        {
            _playerMoney.CurrentMoneyChanged -= CountMoney;
        }

        private void CountMoney(int money)
        {
            _moneyText.text = $"{money}";
        }
    }
}