using DefaultNamespace;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIMoney : MonoBehaviour
    {
        [SerializeField] private TMP_Text _moneyText;
        [SerializeField] private PlayerMoney _playerMoney;
 
        private void Start()
        {
            _moneyText.text = FormatMoney(_playerMoney.Money);
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
            _moneyText.text = FormatMoney(money);
        }
 
        public static string FormatMoney(long money)
        {
            if (money >= 1_000_000_000_000L)
                return $"{money / 1_000_000_000_000.0:0.##}T";
 
            if (money >= 1_000_000_000L)
                return $"{money / 1_000_000_000.0:0.##}B";
 
            if (money >= 1_000_000L)
                return $"{money / 1_000_000.0:0.##}M";
 
            if (money >= 1_000L)
                return $"{money / 1_000.0:0.##}K";
 
            return money.ToString();
        }
    }
}