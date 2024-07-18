using System;
using UnityEngine;
using YG;

namespace DefaultNamespace
{
    public class YandexAdServices : MonoBehaviour
    {
        [SerializeField] private RandomSpawnCar _randomSpawn;
        [SerializeField] private Boost _boost;
        
        public static Action RewardClosed;

        private void OnEnable()
        {
            YandexGame.RewardVideoEvent += Rewarded;
            YandexGame.OpenVideoEvent += OpenVideoReward;
            YandexGame.CloseVideoEvent += CloseVideoReward;
        }

        private void OnDisable()
        {
            YandexGame.RewardVideoEvent -= Rewarded;
            YandexGame.OpenVideoEvent -= OpenVideoReward;
            YandexGame.CloseVideoEvent -= CloseVideoReward;
        }
        
        private void Rewarded(int id)
        {
            switch (id)
            {
                case 1:
                    SpeedBooster();
                    break;
                case 2:
                    BoosterRandomCar();
                    break;
            }
        }

        private void OpenVideoReward()
        {
            Time.timeScale = 0;
        }

        private void CloseVideoReward()
        {
            RewardClosed?.Invoke();
            Time.timeScale = 1;
        }

        private void SpeedBooster()
        {
            _boost.Booster();
        }
        
        private void BoosterRandomCar()
        {
            _randomSpawn.SpawnRandomCar();
        }
    }
}