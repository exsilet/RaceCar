using UnityEngine;
using UnityEngine.UI;
using YG;

namespace DefaultNamespace
{
    public class GiftVideoAd : MonoBehaviour
    {
        [SerializeField] private Button _rewardButton;
        
        private void OnEnable()
        {
            YandexAdServices.RewardClosed += RewardClosed;
        }

        private void OnDisable()
        {
            YandexAdServices.RewardClosed -= RewardClosed;
        }

        public void WatchVideoAd(int id)
        {
            //SoundManager.Click();
            YandexGame.RewVideoShow(id);
            _rewardButton.interactable = false;
        }

        private void RewardClosed()
        {
            _rewardButton.interactable = true;
        }
    }
}