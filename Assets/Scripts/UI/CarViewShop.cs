using Cars;
using SaveData;
using TMPro;
using UIExtensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class CarViewShop : MonoBehaviour
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private Image _background;
        [SerializeField] private Button _sellButton;
        [SerializeField] private TMP_Text _price;
        [SerializeField] private TMP_Text _closeSpeed;
        [SerializeField] private TMP_Text _closeEarning;
        [SerializeField] private TMP_Text _closeBye;
        [SerializeField] private Slider _currentSpeed;
        [SerializeField] private Slider _currentCoin;
        [SerializeField] private float _count;
        
        private int _priceValue;
        private int _carLevel;
        private int _nextLevel = 1;
        private int _currentPrice;
        private int _nextPrice;
        private SaveLoadService _saveLoad;
        private CarStaticData _carStaticData;
        private float _coefficient = 0.0016f;
        public int PriceValue => _priceValue;
        public int CarLevel => _carLevel;
        public event UnityAction<CarStaticData, CarViewShop> SellShopButtonClick;

        public void Initialize(CarStaticData carData, int price, SaveLoadService saveLoad)
        {
            _carStaticData = carData;
            _carLevel = carData.Level;
            _saveLoad = saveLoad;
            _iconImage.sprite = carData.UIIcon;
            _currentSpeed.value = carData.Speed;
            _currentCoin.value = carData.Coins * _coefficient;
            PriceToCar(carData, price);
        }

        private void OnEnable() => _sellButton.Add(SellShopCar);
        private void OnDisable() => _sellButton.Remove(SellShopCar);

        public void SetPrice()
        {
            _priceValue = (int)(_priceValue * _count);
            _currentPrice = _priceValue;
            _price.text = _priceValue.ToString();
            SaveCurrentPrice();
        }

        public void OpenCarView(int carLevel)
        {
            _iconImage.color = new Color(1f, 1f, 1f, 1f);
            _background.color = new Color(1f, 1f, 1f, 1f);
            CarStatistics(true, false);
            OpenNewCarView(carLevel);
        }

        public void CloseCarView()
        {
            _iconImage.color = new Color(0f, 0f, 0f, 1f);
            _background.color = new Color(1f, 1f, 1f, 0.2f);
            CarStatistics(false, true);
        }

        private void OpenNewCarView(int carLevel)
        {
            if (_carStaticData.Level <= carLevel)
            {
                _sellButton.gameObject.SetActive(true);
                _closeBye.gameObject.SetActive(false);
            }
            else
            {
                _sellButton.gameObject.SetActive(false);
                _closeBye.gameObject.SetActive(true);
            }
        }

        private void PriceToCar(CarStaticData data, int price)
        {
            if (data.StartPrice == price)
            {
                _priceValue = data.StartPrice;
                _price.text = _priceValue.ToString();
            }
            else
            {
                _priceValue = price;
                _price.text = _priceValue.ToString();
            }
        }

        private void CarStatistics(bool isOpen, bool isClose)
        {
            _sellButton.gameObject.SetActive(isOpen);
            _currentCoin.gameObject.SetActive(isOpen);
            _currentSpeed.gameObject.SetActive(isOpen);
            _closeSpeed.gameObject.SetActive(isClose);
            _closeEarning.gameObject.SetActive(isClose);
        }
        
        private void SaveCurrentPrice()
        {
            var carLevel = _carLevel.ToString();
            _saveLoad.SavePriceCar(carLevel, _priceValue);
        }

        private void SellShopCar()
        {
            SellShopButtonClick?.Invoke(_carStaticData, this);
        }
    }
}