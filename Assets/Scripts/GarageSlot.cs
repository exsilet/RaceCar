using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class GarageSlot : MonoBehaviour
    {
        [SerializeField] private Image _imageSlot;

        private bool _inTheGarage;
        private GameObject _currentGarage;
        private Car _currentCar;
        public bool InTheGarage => _inTheGarage;

        public event UnityAction<GarageSlot> CreateCar;

        public void Placed()
        {
        }

        private void Start()
        {
            // if (transform.GetChild(1) == null)
            //     return;
            //
            // if (this.transform.GetChild(1) != null)
            // {
            //     IsGarage();
            // }
        }

        public void NewCar(Car car)
        {
            _currentCar = car;
            CreateCar?.Invoke(this);
        }

        public void IsGarage()
        {
            _inTheGarage = !_inTheGarage;
        }

        public void ObjectOffset(GarageSlot garageSlot)
        {
            if (!garageSlot._inTheGarage)
            {
                //garageSlot._data = null;
                garageSlot._currentCar = null;
            }
        }

        public void DestroyMinions(GameObject selectedObject)
        {
            Destroy(selectedObject);
        }
    }
}