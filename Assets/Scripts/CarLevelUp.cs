using Cars;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class CarLevelUp : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Slider _currentSpeed;
        [SerializeField] private Slider _currentCoin;
        
        private float _coefficient = 0.0016f;
        
        public void SetCarLevel(CarStaticData carStaticData)
        {
            _icon.sprite = carStaticData.UIIcon;
            _currentSpeed.value = carStaticData.Speed;
            _currentCoin.value = carStaticData.Coins * _coefficient;
        }
    }
}