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
        
        public void SetCarLevel(CarStaticData carStaticData)
        {
            _icon.sprite = carStaticData.UIIcon;
            _currentSpeed.maxValue = carStaticData.Speed;
            _currentCoin.maxValue = carStaticData.Coins;
        }
    }
}