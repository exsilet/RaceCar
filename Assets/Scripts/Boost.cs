using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class Boost : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private float _boostDuration;
        [SerializeField] private float _boosterSpawn;

        private bool _isBoosted = false;
        private bool _isCoolingDown = false;
        private float _boostDurationMax = 30.0f;
        public float _speedMultiplier = 2.0f;
        
        private void Update()
        {
            if (!_isCoolingDown && _isBoosted)
            {
                _boostDurationMax -= Time.deltaTime;
                _button.interactable = false;
                if (_boostDurationMax <= 0.0f)
                {
                    _isBoosted = false;
                    _boostDurationMax = _boostDuration;
                    _inventory.NormalSpeedCar(_speedMultiplier);
                    StartCoroutine(StartCooldown());
                }
            }
        }

        public void Booster()
        {
            if (!_isBoosted && !_isCoolingDown)
            {
                _isBoosted = true;
                _inventory.CarBooster(_speedMultiplier);
            }
        }

        private IEnumerator StartCooldown()
        {
            _isCoolingDown = true;
            _button.interactable = false;

            yield return new WaitForSeconds(_boosterSpawn);

            _isCoolingDown = false;
            _button.interactable = true;
        }
    }
}