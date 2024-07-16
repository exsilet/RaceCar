using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class SwapNextLevel : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private CarView _carView;
        [SerializeField] private List<CarStaticData> _carData;
        [SerializeField] private ShopPanel _panel;

        private bool _nextLevelCar = true;
        private int _levelCar = 1;
        private int _maxLevel;
        private CarStaticData _data;

        private List<int> _countNextCar = new() { 5, 7, 9, 11, 13, 15, 17, 19 };

        public void NextCar(CarStaticData data)
        {
            foreach (var carLevel in _countNextCar)
            {
                if (data.Level == carLevel)
                {
                    _levelCar++;

                    foreach (CarStaticData staticData in _carData)
                    {
                        if (staticData.Level == _levelCar)
                        {
                            _carView.SetItem(staticData);
                        }
                    }
                }
            }
        }
    }
}