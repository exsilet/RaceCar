using System.Collections.Generic;
using Cars;
using DefaultNamespace;
using Music;
using SaveData;
using UnityEngine;

namespace UI
{
    public class ShopPanel : MonoBehaviour
    {
        [SerializeField] private Spawn _spawn;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private Transform _panel;
        [SerializeField] private PlayerMoney _money;
        [SerializeField] private CarView _carView;
        [SerializeField] private List<CarStaticData> _carStatic;
        [SerializeField] private SoundVolume _sound;
        [SerializeField] private SaveLoadService _saveLoad;
        
        [SerializeField] private ShopItemViewFactory _shopItemViewFactory;
        [SerializeField] private Transform _itemsParent;

        private readonly int _defaultMinionsCount = 1;
        private GarageSlot _slot;
        private List<CarViewShop> _viewShops = new List<CarViewShop>();
        private int _maxLevelCar = 1;
        private int _currentLevelCar = 2;
        private int _currentLevelCarShop;
        
        public void Show(IEnumerable<CarItem> items)
        {
            Clear();
            
            foreach (CarItem item in items)
            {
                CarViewShop spawnedItem = _shopItemViewFactory.Get(item, _itemsParent, _saveLoad);
                
                spawnedItem.SellShopButtonClick += OnByuShopCar;
                
                _viewShops.Add(spawnedItem);
            }
            
            CloseCar();
            OpenCar();
        }

        private void OnByuShopCar(CarStaticData data, CarViewShop view)
        {
            if (data == null)
                return;

            _slot = _inventory.RandomSlot();

            if (_slot == null)
                _panel.gameObject.SetActive(true);
            else
                ByuToCar(data, view);
        }

        private void ByuToCar(CarStaticData data, CarViewShop view)
        {
            if (_money.Money <= 0)
                return;

            if (_money.Money >= view.PriceValue)
            {
                _money.BuyCar(view.PriceValue);
                _spawn.SpawnCar(view.CarLevel, _slot);
                _sound.ShopCarSound();

                if (_maxLevelCar >= _currentLevelCar)
                {
                    view.SetPrice();
                }
            }
        }

        private void Clear()
        {
            foreach (CarViewShop item in _viewShops)
            {
                item.SellShopButtonClick -= OnByuShopCar;
                Destroy(item.gameObject);
            }

            _viewShops.Clear();
        }

        private void SetLevelCar()
        {
            _maxLevelCar = _inventory.CurrentMaxLevel;
            _currentLevelCarShop = _inventory.NextLevelCar;
        }

        private void CloseCar()
        {
            foreach (CarViewShop view in _viewShops)
            {
                view.CloseCarView();
            }
        }

        private void OpenCar()
        {
            SetLevelCar();

            for (int i = 0; i <= _maxLevelCar - _defaultMinionsCount; i++)
            {
                Debug.Log(" open car " + _viewShops[i].CarLevel);
                _viewShops[i].OpenCarView(_currentLevelCarShop);
            }
        }
    }
}