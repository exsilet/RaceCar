using System.Collections.Generic;
using TMPro;
using UIExtensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class CarView : MonoBehaviour
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private Image _background;
        [SerializeField] private Button _sellButton;
        [SerializeField] private TMP_Text _price;
        [SerializeField] private float _count;
        
        private CarStaticData _item;
        private int _priceValue;
        private int _carLevel;
        private int _startPrice;
        private int _currentPrice;
        
        public int CurrentPrice => _currentPrice;
        public int PriceValue => _priceValue;
        public int CarLevel => _carLevel;
        public CarStaticData CarData => _item;
        public event UnityAction<CarStaticData, CarView> SellButtonClick;
        
        public void SetItem(CarStaticData item)
        {
            _item = item;
            _iconImage.sprite = item.UIIcon;
            _startPrice = item.StartPrice;
            _priceValue = _startPrice;
            _price.text = item.StartPrice.ToString();
            _carLevel = item.Level;
        }
        
        private void OnEnable() => _sellButton.Add(OnButtonClick);
        private void OnDisable() => _sellButton.Remove(OnButtonClick);
        private void OnButtonClick() => SellButtonClick?.Invoke(_item, this);
        
        public void SetPrice()
        {
            if (_priceValue == 0 )
            {
                _priceValue = _startPrice;
                _price.text = _priceValue.ToString();
            }
            else
            {
                _priceValue = (int)(_priceValue * _count);
                _currentPrice = _priceValue;
                _price.text = _priceValue.ToString();
            }
        }
    }
}