using System.Collections.Generic;
using Dreamteck.Splines;
using Music;
using SaveData;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private List<GarageSlot> _garageSlots;
        [SerializeField] private CarLevelUp _carLevelUp;
        [SerializeField] private SwapNextLevel _nextLevel;
        [SerializeField] private RandomSpawnCar _spawnCar;
        [SerializeField] private SoundVolume _soundVolume;
        [SerializeField] private SaveLoadService _saveLoad;
        [SerializeField] private Spawn _spawner;
        
        private List<Car> _cars = new();
        private int _count = 0;
        private GarageSlot _position;
        private int _randomSlot;
        private int _maxLevel = 1;
        
        public int CurrentMaxLevel => _maxLevel;

        private void Start()
        {
            _maxLevel = _saveLoad.ReadMaxLevelCar();
            
            foreach (GarageSlot garageSlot in _garageSlots)
            {
                // garageSlot.gameObject.SetActive(_count < 2);
                // if (_count < 2)
                //     _count++;
                garageSlot.gameObject.SetActive(true);
                garageSlot.Initialized(_spawner);
            }
        }

        private void OnEnable()
        {
            foreach (GarageSlot slot in _garageSlots)
            {
                slot.CreateCar += SpawnerOnCreateCar;
            }
        }

        private void OnDisable()
        {
            foreach (GarageSlot slot in _garageSlots)
            {
                slot.CreateCar -= SpawnerOnCreateCar;
            }
        }

        public GarageSlot RandomSlot()
        {
            _randomSlot = Random.Range(0, _garageSlots.Count);

            for (int i = 0; i <= _garageSlots.Count; i++)
            {
                Debug.Log(" i " + i);

                if (!_garageSlots[i].InTheGarage)
                {
                    Debug.Log(" slot " + _garageSlots[i]);
                    
                    return _garageSlots[i];
                }
            }

            return null;
        }

        public void NewCar(Car car)
        {
            _cars.Add(car);
            
            for (int i = 0; i < _cars.Count; i++)
            {
                if (_cars[i] == null)
                {
                    _cars.RemoveAt(i);
                    _cars.Reverse();
                }
            }
        }

        public void CarBooster(float speed)
        {
            foreach (Car carSpeed in _cars)
            {
                if (carSpeed != null)
                {
                    carSpeed.GetComponent<SplineFollower>().followSpeed *= speed;
                    carSpeed.SetBooster();
                }
            }
        }

        public void NormalSpeedCar(float speed)
        {
            foreach (Car carSpeed in _cars)
            {
                if (carSpeed != null)
                {
                    if (carSpeed.BoosterSpeed)
                    {
                        carSpeed.GetComponent<SplineFollower>().followSpeed /= speed;
                        carSpeed.SetBoosterFalse();
                    }
                }
            }
        }

        public void MaxLevel(int levelCar, CarStaticData data)
        {
            if (_maxLevel < levelCar)
            {
                _carLevelUp.gameObject.SetActive(true);
                _soundVolume.NewCarSound();
                _carLevelUp.SetCarLevel(data);
                _maxLevel = levelCar;
                _nextLevel.NextCar(data);
                NewSlot();
                _spawnCar.SetLevelCar(_maxLevel);
                _saveLoad.SaveMaxLevelCar(_maxLevel);
            }
        }

        private void NewSlot()
        {
            if (_count <= _garageSlots.Count)
            {
                _garageSlots[_count].gameObject.SetActive(true);
                _count++;
            }
        }

        private void SpawnerOnCreateCar(GarageSlot position)
            => _position = position;
    }
}