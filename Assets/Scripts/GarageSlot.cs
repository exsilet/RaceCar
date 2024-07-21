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
        private GameObject _currentGarage;
        private Car _currentCar;
        private int _carLevel;
        public bool InTheGarage => _inTheGarage;
        public int CarLevel => _carLevel;
        
        public event UnityAction<GarageSlot> CreateCar;

        private void Start()
        {
            LoadSpawnCar();
            //_inTheGarage = _saveLoad.ReadBusy(name);
        }

        private void OnDestroy()
        {
            if (_inTheGarage)
            {
                _saveLoad.SaveCar(_currentCar);
                _saveLoad.SaveGarageSlotData(this);
            }
        }

        public void Initialized(Spawn spawn)
        {
            _spawn = spawn;
        }

        public void NewCar(Car car)
        {
            _currentCar = car;
            _carLevel = car.Level;
            CreateCar?.Invoke(this);
        }

        public void IsGarage() => _inTheGarage = !_inTheGarage;

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