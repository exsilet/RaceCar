using System.Collections.Generic;
using Cars;
using Music;
using SaveData;
using UI;
using UnityEngine;

namespace DefaultNamespace
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private Spawn _spawn;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private Transform _panel;
        [SerializeField] private PlayerMoney _money;
        [SerializeField] private CarView _carView;
        [SerializeField] private List<CarStaticData> _carStatic;
        [SerializeField] private SoundVolume _sound;
        [SerializeField] private SaveLoadService _saveLoad;

        private int _levelCar = 1;
        private int _maxLevelCar;
        private int _currentLevelCar = 2;
        private int _countNextLevel = 5;
        private GarageSlot _slot;
        private long _currentMoney;
        private bool _nextLevelCar = true;

        private void Start()
        {
            _levelCar = _saveLoad.ReadOpenLevelCar();
            _maxLevelCar = _saveLoad.ReadMaxLevelCar();
            
            foreach (CarStaticData data in _carStatic)
            {
                if (_levelCar == data.Level) 
                    _carView.Initialize(data);
            }
        }

        private void OnEnable() => _carView.SellButtonClick += ByuCar;
        private void OnDisable() => _carView.SellButtonClick -= ByuCar;

        private void ByuCar(CarStaticData data, CarView view)
        {
            _slot = _inventory.RandomSlot();

            if (_slot == null)
            {
                _panel.gameObject.SetActive(true);
            }
            else
            {
                ByuToCar(data, view);
            }
        }

        private void ByuToCar(CarStaticData data, CarView view)
        {
            _currentMoney = _money.Money;
                
            SetLevelCar();

            if (_currentMoney >= view.PriceValue)
            {
                _money.BuyCar(view.PriceValue);
                _sound.ShopCarSound();
                _spawn.SpawnCar(data.Level, _slot);
                    
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
    }
}