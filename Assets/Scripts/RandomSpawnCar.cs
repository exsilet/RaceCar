using System;
using System.Collections.Generic;
using UIExtensions;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class RandomSpawnCar : MonoBehaviour
    {
        [SerializeField] private List<CarStaticData> _carStatic;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Button _button;
        [SerializeField] private Spawn _spawner;

        private GarageSlot _slot;
        private int _maxLevelCar = 1;
        private bool _isSpawned = false;
        private int _randomLevelCar;
        private float _spawnDuration = 60.0f;
        private int _currentMaxLevel = 3;
        private float _timer;

        private void Start()
        {
            _button.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (_isSpawned)
            {
                _timer += Time.deltaTime;
                if (_timer >= _spawnDuration)
                {
                    _isSpawned = false;
                    _button.interactable = true;
                }
            }
        }

        private void OnEnable()
        {
            _button.Add(SpawnRandomCar);
        }

        private void OnDisable()
        {
            _button.Remove(SpawnRandomCar);
        }

        public void SetLevelCar(int maxLevelCar)
        {
            if (_currentMaxLevel <= maxLevelCar)
            {
                _maxLevelCar = _inventory.CurrentMaxLevel;
                _button.gameObject.SetActive(true);
            }
        }

        private void SpawnRandomCar()
        {
            //добавить рекламу
            
            _timer = 0.0f;
            _isSpawned = true;
            _button.interactable = false;

            _slot = _inventory.RandomSlot();

            RandomCar(_slot);
        }

        private void RandomCar(GarageSlot slot)
        {
            _randomLevelCar = Random.Range(1, _maxLevelCar);
            _spawner.SpawnCar(_randomLevelCar, slot);
        }
    }
}