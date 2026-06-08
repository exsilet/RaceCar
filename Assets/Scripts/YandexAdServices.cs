using System;
using System.Collections;
using UnityEngine;
using YG;

namespace DefaultNamespace
{
    public class YandexAdServices : MonoBehaviour
    {
        [Header("Rewarded")] [SerializeField] private RandomSpawnCar _randomSpawn;
        [SerializeField] private Boost _boost;

        [Header("Interstitial")]
        [Tooltip("Минимальный интервал между interstitial рекламой (секунды)")]
        [SerializeField]
        private float _autoShowInterval = 180f;

        [Tooltip("Задержка перед первым показом после запуска (секунды)")] [SerializeField]
        private float _firstAdDelay = 60f;

        public static Action RewardClosed;
        public static Action InterstitialClosed;

        private const string RewardBoost = "boost";
        private const string RewardRandomCar = "random_car";

        private void OnEnable()
        {
            YG2.onRewardAdv += OnReward;
            YG2.onCloseInterAdv += OnInterstitialClose;
            YG2.onErrorInterAdv += OnInterstitialError;
        }

        private void OnDisable()
        {
            YG2.onRewardAdv -= OnReward;
            YG2.onCloseInterAdv -= OnInterstitialClose;
            YG2.onErrorInterAdv -= OnInterstitialError;
        }

        private void Start()
        {
            StartCoroutine(AutoInterstitialLoop());
        }

        private IEnumerator AutoInterstitialLoop()
        {
            yield return new WaitForSecondsRealtime(_firstAdDelay);

            while (true)
            {
                ShowInterstitial();
                yield return new WaitForSecondsRealtime(_autoShowInterval);
            }
        }

        public void ShowInterstitial()
        {
            if (YG2.nowAdsShow) return;
            YG2.InterstitialAdvShow();
        }

        private void OnInterstitialClose()
        {
            InterstitialClosed?.Invoke();
        }

        private void OnInterstitialError()
        {
            Debug.Log("[YandexAdServices] Interstitial error");
        }

        public void ShowRewardBoost()
        {
            if (YG2.nowAdsShow) return;
            YG2.RewardedAdvShow(RewardBoost, SpeedBooster);
        }

        public void ShowRewardRandomCar()
        {
            if (YG2.nowAdsShow) return;
            YG2.RewardedAdvShow(RewardRandomCar, BoosterRandomCar);
        }

        private void OnReward(string id)
        {
            switch (id)
            {
                case RewardBoost: SpeedBooster(); break;
                case RewardRandomCar: BoosterRandomCar(); break;
            }

            RewardClosed?.Invoke();
        }

        private void SpeedBooster() => _boost.Booster();
        private void BoosterRandomCar() => _randomSpawn.SpawnRandomCar();
    }
}