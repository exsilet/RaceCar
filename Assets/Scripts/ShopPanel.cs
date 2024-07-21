using System;
using System.Collections.Generic;
using Music;
using UnityEngine;

namespace DefaultNamespace
{
    public class ShopPanel : MonoBehaviour
    {
        [SerializeField] private Spawn _spawn;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private Transform _panel;
        [SerializeField] private PlayerMoney _money;
        [SerializeField] private CarView _carView;
        [SerializeField] private List<CarStaticData> _carStatic;
        [SerializeField] private List<CarViewShop> _viewShopsPrefabs;
        [SerializeField] private SoundVolume _sound;

        private readonly int _defaultMinionsCount = 1;
        private GarageSlot _slot;
        private List<CarViewShop> _viewShops = new List<CarViewShop>();
        private int _maxLevelCar = 1;
        private int _currentLevelCar = 2;

        private void Start()
        {
            CloseCar();
            OpenCar();

            foreach (CarViewShop carViewShop in _viewShops)
            {
                carViewShop.SellShopButtonClick += ByuShopCar;
            }
        }

        private void Update()
        {
            OpenCar();
        }

        private void OnEnable()
        {
            for (int i = 0; i < _carStatic.Count; i++)
            {
                _viewShops.Add(_viewShopsPrefabs[i]);
                _viewShopsPrefabs[i].Initialize(_carStatic[i], _carView);
            }
        }

        private void OnDestroy()
        {
            foreach (CarViewShop carViewShop in _viewShops)
            {
                carViewShop.SellShopButtonClick -= ByuShopCar;
            }
        }

        private void ByuShopCar(CarStaticData data, CarViewShop view)
        {
            if(data == null)
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

        private void SetLevelCar()
        {
            _maxLevelCar = _inventory.CurrentMaxLevel;
        }

        private void CloseCar()
        {
            foreach (CarViewShop viewShopsPrefab in _viewShopsPrefabs)
            {
                viewShopsPrefab.CloseCarView();
            }
        }

        private void OpenCar()
        {
            SetLevelCar();

            for (int i = 0; i <= _maxLevelCar - _defaultMinionsCount; i++)
            {
                _viewShopsPrefabs[i].OpenCarView();
            }
        }
    }
}