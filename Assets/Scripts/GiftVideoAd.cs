using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class GiftVideoAd : MonoBehaviour
    {
        [SerializeField] private Button _rewardBoostButton;
        [SerializeField] private YandexAdServices _adServices;

        private void OnEnable()  => YandexAdServices.RewardClosed += OnRewardClosed;
        private void OnDisable() => YandexAdServices.RewardClosed -= OnRewardClosed;
        
        public void WatchVideoAd(int id)
        {
            _rewardBoostButton.interactable = false;
 
            switch (id)
            {
                case 1: _adServices.ShowRewardBoost();     break;
                case 2: _adServices.ShowRewardRandomCar(); break;
            }
        }
 
        private void OnRewardClosed() => _rewardBoostButton.interactable = true;
    }
}