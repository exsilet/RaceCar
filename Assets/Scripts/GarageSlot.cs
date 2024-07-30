using SaveData;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class GarageSlot : MonoBehaviour
    {
        [SerializeField] private Image _imageSlot;
        [SerializeField] private SaveLoadService _saveLoad;

        private Spawn _spawn;
        private bool _inTheGarage;
        private Car _currentCar;
        private int _carLevel;
        private int _slotNumber;
        public bool InTheGarage => _inTheGarage;
        public int CarLevel => _carLevel;
        public int SlotNumber => _slotNumber;
        
        public event UnityAction<GarageSlot> CreateCar;

        private void Start()
        {
            LoadSpawnCar();
        }

        public void Initialized(Spawn spawn, int slotNumber)
        {
            _spawn = spawn;
            _slotNumber = slotNumber;
        }

        public void NewCar(Car car)
        {
            _currentCar = car;
            _carLevel = car.Level;
            _saveLoad.SaveGarageSlotData(this);
            CreateCar?.Invoke(this);
        }

        public void IsGarage()
        {
            _inTheGarage = !_inTheGarage;
        }

        public void DestroyMinions(GameObject selectedObject)
        {
            Destroy(selectedObject);
        }

        private void LoadSpawnCar()
        {
            _carLevel = _saveLoad.ReadCar(this);
            
            if (_carLevel > 0)
            {
                _spawn.SpawnCar(_carLevel, this);
            }
        }
    }
}