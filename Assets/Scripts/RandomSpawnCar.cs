using System.Collections.Generic;
using Music;
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
        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private float _spawnDuration = 120.0f;
        [SerializeField] private CarLevelUp _levelUp;
        [SerializeField] private SoundVolume _sound;

        private GarageSlot _slot;
        private int _maxLevelCar = 1;
        private bool _isSpawned = false;
        private int _randomLevelCar;
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
                _particle.gameObject.SetActive(false);
                _button.interactable = false;
                
                if (_timer >= _spawnDuration)
                {
                    _isSpawned = false;
                    _particle.gameObject.SetActive(true);
                    _button.interactable = true;
                }
            }
        }

        public void SpawnRandomCar()
        {
            _timer = 0.0f;
            _isSpawned = true;

            _slot = _inventory.RandomSlot();
            RandomCar(_slot);
        }

        public void SetLevelCar(int maxLevelCar)
        {
            if (_currentMaxLevel <= maxLevelCar)
            {
                _maxLevelCar = _inventory.CurrentMaxLevel;
                _button.gameObject.SetActive(true);
            }
        }

        private void RandomCar(GarageSlot slot)
        {
            _randomLevelCar = Random.Range(1, _maxLevelCar);
            _spawner.SpawnCar(_randomLevelCar, slot);
            
            foreach (var carData in _carStatic)
            {
                if (carData.Level == _randomLevelCar)
                {
                    _levelUp.gameObject.SetActive(true);
                    _levelUp.SetCarLevel(carData);
                    _sound.BoxCarSound();
                    break;
                }
            }
        }
    }
}