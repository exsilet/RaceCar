using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Car : MonoBehaviour
    {
        [SerializeField] private CarStaticData _carStaticData;

        private GarageSlot _slot;
        private Selectable _selectable;
        private bool _boosterSpeed = false;
        public bool BoosterSpeed => _boosterSpeed;
        public CarStaticData CarStaticData => _carStaticData;
        public GarageSlot Slot => _slot;

        
        public int level
        {
            get
            {
                return _carStaticData.Level;
            }
        }

        private void Update()
        {
            if (_selectable == null)
            {
                Destroy(this.gameObject);
            }
        }
        
        public void SetBooster()
        {
            _boosterSpeed = true;
        }
        
        public void SetBoosterFalse()
        {
            _boosterSpeed = false;
        }

        public void SetCarStaticData(CarStaticData carStaticData, GarageSlot slot, Selectable carSelectable)
        {
            _carStaticData = carStaticData;
            _slot = slot;
            _selectable = carSelectable;
        }
    }
}