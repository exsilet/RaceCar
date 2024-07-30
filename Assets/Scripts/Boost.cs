using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class Boost : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private float _boostDuration;
        [SerializeField] private float _cooldownDuration;
        [SerializeField] private Image _iconReward;
        [SerializeField] private TMP_Text _cooldownText;
        [SerializeField] private TMP_Text _textReward;

        private bool _isBoosted = false;
        private bool _isCoolingDown = false;
        private float _boostDurationMax = 30.0f;
        private float _currentCooldown;
        private float _cooldownTime;
        public float _speedMultiplier = 2.0f;
        private int _minutes = 60;

        private void Start()
        {
            UpdateButtonState();
        }

        private void Update()
        {
            if (_isBoosted)
            {
                _boostDurationMax -= Time.deltaTime;
                _button.interactable = false;
                
                UpdateCooldownTimer();
                
                if (_boostDurationMax < 0) 
                    EndSpeedCar();

                if (_cooldownTime <= 0.0f) 
                    EndBoost();
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

        private void EndBoost()
        {
            _isBoosted = false;
            _cooldownText.gameObject.SetActive(false);
            _textReward.gameObject.SetActive(true);
            _iconReward.gameObject.SetActive(true);
            _boostDurationMax = _boostDuration;
            _inventory.NormalSpeedCar(_speedMultiplier);
            StartCoroutine(StartCooldown());
            UpdateButtonState();
        }

        private void EndSpeedCar()
        {
            _inventory.NormalSpeedCar(_speedMultiplier);
        }

        private IEnumerator StartCooldown()
        {
            _isCoolingDown = true;

            yield return new WaitForSeconds(1.0f);

            _isCoolingDown = false;
            UpdateButtonState();
        }

        private void UpdateCooldownTimer()
        {
            _isCoolingDown = true;
            _cooldownTime -= Time.deltaTime;
            
            UpdateCooldownText();
            UpdateButtonState();
        }

        private void UpdateButtonState()
        {
            if (_isCoolingDown)
            {
                _cooldownText.gameObject.SetActive(true);
                _textReward.gameObject.SetActive(false);
                _iconReward.gameObject.SetActive(false);
            }
            else
            {
                _cooldownTime = _cooldownDuration + _boostDuration;
                _cooldownText.gameObject.SetActive(false);
                _textReward.gameObject.SetActive(true);
                _iconReward.gameObject.SetActive(true);
                _button.interactable = true;
            }
        }

        private void UpdateCooldownText()
        {
            if (_cooldownTime > 0)
            {
                float minutes = Mathf.FloorToInt(_cooldownTime / _minutes);
                float seconds = Mathf.FloorToInt(_cooldownTime % _minutes);
                _cooldownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
        }
    }
}