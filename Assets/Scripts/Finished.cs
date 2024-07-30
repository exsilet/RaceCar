using DefaultNamespace;
using UnityEngine;
using YG;

public class Finished : MonoBehaviour
{
    [SerializeField] private PlayerMoney _player;

    private int _score;

    private void Start()
    {
        if (YandexGame.SDKEnabled == true)
        {
            LoadSaveCloud();
        }
    }
        
    private void OnEnable() => YandexGame.GetDataEvent += LoadSaveCloud;
    private void OnDisable() => YandexGame.GetDataEvent -= LoadSaveCloud;

    private void OnDestroy()
    {
        SaveCloud();
    }

    private void LoadSaveCloud()
    {
        _score = YandexGame.savesData.Score;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Car car))
        {
            _player.AddMoney(car.CarStaticData.Coins);
            _score += car.CarStaticData.Coins;
            SaveCloud();
        }
    }

    private void SaveCloud()
    {
        YandexGame.savesData.Score = _score;
        YandexGame.NewLeaderboardScores("LeadersForEarnings", _score);
        YandexGame.SaveProgress();
    }
}