using System.Collections.Generic;
using Cars;
using SaveData;
using UI;
using UnityEngine;

namespace DefaultNamespace
{
    public class SwapNextLevel : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private CarView _carView;
        [SerializeField] private List<CarStaticData> _carData;
        [SerializeField] private ShopPanel _panel;
        [SerializeField] private SaveLoadService _saveLoad;

        private bool _nextLevelCar = true;
        private int _levelCar = 1;
        private int _maxLevel;
        private CarStaticData _data;
        
        private List<int> _countNextCar = new() { 5, 7, 9, 11, 13, 15, 17, 19 };
        
        public int NextLevel => _levelCar;
        
        private void Start()
        {
            _maxLevel = _inventory.CurrentMaxLevel;
            _levelCar = _saveLoad.ReadOpenLevelCar();

            foreach (var carLevel in _countNextCar)
            {
                if (carLevel == _maxLevel)
                {
                    foreach (CarStaticData staticData in _carData)
                    {
                        if (staticData.Level == _levelCar)
                        {
                            _carView.Initialize(staticData);
                        }
                    }
                }
            }
        }

        public void NextCar(CarStaticData data)
        {
            foreach (var carLevel in _countNextCar)
            {
                if (data.Level == carLevel)
                {
                    _levelCar++;
                    _saveLoad.SaveOpenLevelCar(_levelCar);

                    foreach (CarStaticData staticData in _carData)
                    {
                        if (staticData.Level == _levelCar)
                        {
                            _carView.Initialize(staticData);
                        }
                    }
                }
            }
        }
    }
}