using System.Collections.Generic;
using Cars;
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
        private int _maxLevel = 1;
        private int _nextLevelCar;
 
        public int CurrentMaxLevel => _maxLevel;
        public int NextLevelCar => _nextLevelCar;
        public bool IsFull => GetFreeSlots().Count == 0;
        public List<GarageSlot> GetSlots() => _garageSlots;
 
        private void Start()
        {
            _maxLevel = _saveLoad.ReadMaxLevelCar();
            _nextLevelCar = _saveLoad.ReadOpenLevelCar();
 
            foreach (var slot in _garageSlots)
            {
                slot.gameObject.SetActive(true);
                slot.Initialized(_spawner, slot.GetHashCode());
            }
        }
 
        private void OnEnable()
        {
            foreach (GarageSlot slot in _garageSlots)
                slot.CreateCar += SpawnerOnCreateCar;
        }
 
        private void OnDisable()
        {
            foreach (GarageSlot slot in _garageSlots)
                slot.CreateCar -= SpawnerOnCreateCar;
        }
        
        public GarageSlot RandomSlot()
        {
            List<GarageSlot> freeSlots = GetFreeSlots();
 
            if (freeSlots.Count == 0)
                return null;
 
            int randomIndex = Random.Range(0, freeSlots.Count);
            return freeSlots[randomIndex];
        }
 
        public void NewCar(Car car)
        {
            if (car != null)
                _cars.Add(car);
            
            _cars.RemoveAll(c => c == null);
        }
 
        public void RemoveCar(Car car)
        {
            _cars.Remove(car);
            _cars.RemoveAll(c => c == null);
        }
 
        public void CarBooster(float speed)
        {
            _cars.RemoveAll(c => c == null);
            foreach (Car car in _cars)
            {
                car.GetComponent<SplineFollower>().followSpeed *= speed;
                car.SetBooster();
            }
        }
 
        public void NormalSpeedCar(float speed)
        {
            _cars.RemoveAll(c => c == null);
            foreach (Car car in _cars)
            {
                if (car.BoosterSpeed)
                {
                    car.GetComponent<SplineFollower>().followSpeed /= speed;
                    car.SetBoosterFalse();
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
                _spawnCar.SetLevelCar(_maxLevel);
                _saveLoad.SaveMaxLevelCar(_maxLevel);
                _nextLevelCar = _nextLevel.NextLevel;
            }
        }
 
        private List<GarageSlot> GetFreeSlots()
        {
            List<GarageSlot> free = new();
            foreach (GarageSlot slot in _garageSlots)
            {
                if (!slot.InTheGarage)
                    free.Add(slot);
            }
            return free;
        }
 
        private void SpawnerOnCreateCar(GarageSlot position)
            => _position = position;
    }
}