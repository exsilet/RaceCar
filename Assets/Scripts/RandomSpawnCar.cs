using System.Collections.Generic;
using Cars;
using Music;
using TMPro;
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
        [SerializeField] private float _spawnDuration;
        [SerializeField] private CarLevelUp _levelUp;
        [SerializeField] private SoundVolume _sound;
        [SerializeField] private Image _iconDescription;
        [SerializeField] private TMP_Text _cooldownText;
        [SerializeField] private TMP_Text _textReward;

        private GarageSlot _slot;
        private int _maxLevelCar = 1;
        private bool _isSpawned = false;
        private int _randomLevelCar;
        private int _currentMaxLevel = 3;
        private float _timer;
        private bool _isCoolingDown;
        private int _minutes = 60;

        private void Start()
        {
            _button.gameObject.SetActive(false);
            _maxLevelCar = _inventory.CurrentMaxLevel;
            SetLevelCar(_maxLevelCar);
        }

        private void Update()
        {
            if (_isSpawned)
            {
                UpdateCooldownTimer();
                
                if (_timer <= 0)
                {
                    _isSpawned = false;
                    UpdateButtonState();
                }
            }
        }

        public void SpawnRandomCar()
        {
            _timer = _spawnDuration;
            _isSpawned = true;

            _slot = _inventory.RandomSlot();
            RandomCar(_slot);
        }

        public void SetLevelCar(int maxLevelCar)
        {
            if (_currentMaxLevel <= maxLevelCar)
            {
                _maxLevelCar = _inventory.CurrentMaxLevel;
                UpdateButtonState();
            }
        }
        
        private void UpdateCooldownTimer()
        {
            _timer -= Time.deltaTime;
            
            UpdateCooldownText();
            UpdateButtonState();
        }
        
        private void UpdateCooldownText()
        {
            if (_timer > 0)
            {
                float minutes = Mathf.FloorToInt(_timer / _minutes);
                float seconds = Mathf.FloorToInt(_timer % _minutes);
                _cooldownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
        }
        
        private void UpdateButtonState()
        {
            if (_isSpawned)
            {
                _button.interactable = false;
                _cooldownText.gameObject.SetActive(true);
                _particle.gameObject.SetActive(false);
                _iconDescription.gameObject.SetActive(false);
                _textReward.gameObject.SetActive(false);
            }
            else
            {
                _button.interactable = true;
                _button.gameObject.SetActive(true);
                _cooldownText.gameObject.SetActive(false);
                _particle.gameObject.SetActive(true);
                _iconDescription.gameObject.SetActive(true);
                _textReward.gameObject.SetActive(true);
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