using Cars;
using SaveData;
using TMPro;
using UIExtensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class CarView : MonoBehaviour
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private Image _background;
        [SerializeField] private Button _sellButton;
        [SerializeField] private TMP_Text _price;
        [SerializeField] private float _count;
        [SerializeField] private SaveLoadService _saveLoad;

        private CarStaticData _item;
        private int _priceValue;
        private int _carLevel;
        private int _currentPrice;
        private int _coefficient = 1000000;
        private string _drop = "K";
        private int _currentMoney;

        public int CurrentPrice => _currentPrice;
        public int PriceValue => _priceValue;
        public int CarLevel => _carLevel;
        public CarStaticData CarData => _item;
        public event UnityAction<CarStaticData, CarView> SellButtonClick;

        public void Initialize(CarStaticData item)
        {
            if (item.Level != _carLevel)
            {
                _item = item;
                _iconImage.sprite = item.UIIcon;
                _carLevel = item.Level;
                ReadePrice(item, item.StartPrice);
            }
        }

        private void OnEnable() => _sellButton.Add(OnButtonClick);
        private void OnDisable() => _sellButton.Remove(OnButtonClick);
        private void OnButtonClick() => SellButtonClick?.Invoke(_item, this);

        public void SetPrice()
        {
            if (_priceValue == 0)
            {
                MoneyCount(_priceValue);
            }
            else
            {
                _priceValue = (int)(_priceValue * _count);
                _currentPrice = _priceValue;
                MoneyCount(_priceValue);
                SaveCurrentPrice();
            }
        }

        private void ReadePrice(CarStaticData data, int price)
        {
            if (data.Level > 0) 
                _priceValue = _saveLoad.ReadPriceCar(data.Level.ToString());
            
            if (_priceValue == 0)
            {
                _priceValue = data.StartPrice;
                _price.text = data.StartPrice.ToString();
            }
            else
            {
                MoneyCount(_priceValue);
                //_price.text = _priceValue.ToString();
            }
        }

        private void SaveCurrentPrice()
        {
            var carLevel = _carLevel.ToString();
            _saveLoad.SavePriceCar(carLevel, _priceValue);
        }
        
        private void MoneyCount(int money)
        {
            if (money > _coefficient)
            {
                _currentMoney = (int) (money / _coefficient);
                
                _price.text  = $"{_currentMoney}{_drop}";
                
                _price.text = money.ToString();
            }
            else
            {
                _price.text = money.ToString();
            }
        }
    }
}