using TMPro;
using UIExtensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DefaultNamespace
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
        
        private int _priceValue;
        private int _carLevel;
        private int _nextLevel = 1;
        private CarView _carView;
        private CarStaticData _carStaticData;
        public int PriceValue => _priceValue;
        public int CarLevel => _carLevel;
        public event UnityAction<CarStaticData, CarViewShop> SellShopButtonClick;

        private void OnEnable()
        {
            _sellButton.Add(SellShopCar);
        }

        private void OnDisable()
        {
            _sellButton.Remove(SellShopCar);
        }

        public void Initialize(CarStaticData carData, CarView carView)
        {
            _carStaticData = carData;
            _carView = carView;
            _carLevel = carData.Level;
            _iconImage.sprite = carData.UIIcon;
            _currentCoin.value = carData.Speed;
            _currentSpeed.value = carData.Coins;

            if (carData.Level == _carView.CarLevel)
            {
                if (_carView.PriceValue == 0)
                {
                    _price.text = carData.StartPrice.ToString();
                }
                else
                {
                    _priceValue = _carView.PriceValue;
                    _price.text = _carView.CurrentPrice.ToString();
                }
            }
            else
            {
                _price.text = carData.StartPrice.ToString();
            }
        }

        public void SetPrice()
        {
            _carView.SetPrice();
        }

        public void OpenCarView()
        {
            _iconImage.color = new Color(1f, 1f, 1f, 1f);
            _background.color = new Color(1f, 1f, 1f, 1f);
            CarStatistics(true, false);
            OpenNewCarView();
        }
        
        public void CloseCarView()
        {
            _iconImage.color = new Color(0f, 0f, 0f, 1f);
            _background.color = new Color(1f, 1f, 1f, 0.2f);
            CarStatistics(false, true);
        }

        private void OpenNewCarView()
        {
            if (_carStaticData.Level <= _carView.CarLevel)
            {
                Debug.Log(" level " + _carView.CarLevel);
                
                _sellButton.gameObject.SetActive(true);
                _closeBye.gameObject.SetActive(false);
            }
            else
            {
                Debug.Log(" noy " + _carView.CarLevel);
                _sellButton.gameObject.SetActive(false);
                _closeBye.gameObject.SetActive(true);
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

        private void SellShopCar()
        {
            SellShopButtonClick?.Invoke(_carStaticData, this);
        }
    }
}