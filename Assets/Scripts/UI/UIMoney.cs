using DefaultNamespace;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIMoney : MonoBehaviour
    {
        [SerializeField] private TMP_Text _moneyText;
        [SerializeField] private PlayerMoney _playerMoney;

        private int _coefficient = 1000000;
        private string _drop = "K";
        private int _money;
        private long _currentMoney;
        
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

        private void CountMoney(long money)
        {
            if (money > _coefficient)
            {
                _currentMoney = (int) (money / _coefficient);
                
                _moneyText.text = $"{_currentMoney}{_drop}";
            }
            else
            {
                _moneyText.text = $"{money}";
            }
        }
    }
}