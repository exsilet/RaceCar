using System.Collections.Generic;
using Cars;
using Dreamteck.Splines;
using UnityEngine;

namespace DefaultNamespace
{
    public class Spawn : MonoBehaviour
    {
        [SerializeField] private List<CarStaticData> _carStatic;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private SplineComputer _track;

        private int _levelCar;
        private GarageSlot _slot;
        private CarStaticData _carData;

        public void SpawnCar(int levelCar, GarageSlot slot)
        {
            _levelCar = levelCar;
            _slot = slot;

            foreach (var carData in _carStatic)
            {
                if (carData.Level == levelCar)
                {
                    _carData = carData;
                    NewCarSpawn(carData);
                    break;
                }
            }
            
            _inventory.MaxLevel(_levelCar, _carData);
        }

        private void NewCarSpawn(CarStaticData carData)
        {
            GameObject car = Instantiate(carData.PrefabSelect, _slot.transform.position, carData.PrefabSelect.transform.rotation);
            GameObject carOnTheTrack = Instantiate(carData.PrefabCarOnTheTrack, _spawnPoint.transform.position, carData.PrefabSelect.transform.rotation);
            
            Car currentCar = car.GetComponent<Car>();
            Car currentCarUnit = carOnTheTrack.GetComponent<Car>();
            Selectable carSelect = car.GetComponent<Selectable>();
            
            currentCar.SetCarStaticData(carData, _slot, carSelect);
            currentCarUnit.SetCarStaticData(carData, _slot, carSelect);
            
            currentCar.transform.SetParent(_slot.transform);
            currentCarUnit.transform.SetParent(_spawnPoint);
            
            currentCarUnit.GetComponent<SplineUser>().spline = _track;
            
            _inventory.NewCar(currentCarUnit);
            _slot.IsGarage();
            _slot.NewCar(currentCar);
        }
    }
}