using System;
using System.Collections.Generic;
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
        
        private readonly int _defaultMinionsCount = 1;
        private GarageSlot _slot;
        private int _currentMoney;
        private List<CarViewShop> _viewShops = new List<CarViewShop>();
        private int _maxLevelCar = 1;
        private int _currentLevelCar = 2;

        private void Start()
        {
            CloseCar();
            OpenCar();
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
            
            foreach (CarViewShop carViewShop in _viewShops)
            {
                carViewShop.SellShopButtonClick += ByuShopCar;
            }
        }

        private void OnDisable()
        {
            foreach (CarViewShop carViewShop in _viewShops)
            {
                carViewShop.SellShopButtonClick -= ByuShopCar;
            }
        }

        private void ByuShopCar(CarStaticData data, CarViewShop view)
        {
            _slot = _inventory.RandomSlot();
            _currentMoney = _money.Money;

            if (_currentMoney <= 0)
                return;
            
            if (_slot == null)
            {
                _panel.gameObject.SetActive(true);
            }
            else
            {
                ByuToCar(data, view);
            }
        }

        private void ByuToCar(CarStaticData data, CarViewShop view)
        {
            if (_currentMoney >= view.PriceValue)
            {
                _money.BuyCar(view.PriceValue);
                _spawn.SpawnCar(view.CarLevel, _slot);
                
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