using UIExtensions;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class Boost : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Inventory _inventory;

        private bool _isBoosted = false;
        private float _boostDuration = 20.0f;
        private float _boostDurationMax = 20.0f;
        public float _speedMultiplier = 2.0f;

        private void Update()
        {
            if (_isBoosted)
            {
                _boostDuration -= Time.deltaTime;
                if (_boostDuration <= 0.0f)
                {
                    _isBoosted = false;
                    _boostDuration = _boostDurationMax;
                    _inventory.NormalSpeedCar(_speedMultiplier);
                    _button.interactable = true;
                }
            }
        }

        private void OnEnable()
        {
            _button.Add(Booster);
        }

        private void OnDisable()
        {
            _button.Remove(Booster);
        }

        private void Booster()
        {
            //добавить рекламу
            
            if (!_isBoosted)
            {
                _isBoosted = true;
                _inventory.CarBooster(_speedMultiplier);
                _button.interactable = false;
            }
        }
    }
}