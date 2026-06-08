using DefaultNamespace;
using UnityEngine;
using YG;

public class Finished : MonoBehaviour
{
    [SerializeField] private PlayerMoney _player;

    private int _score;

    private void Start()
    {
        if (YG2.isSDKEnabled)
            LoadSaveCloud();
    }
        
    private void OnEnable()  => YG2.onGetSDKData += LoadSaveCloud;
    private void OnDisable() => YG2.onGetSDKData -= LoadSaveCloud;
 
    private void OnDestroy() => SaveCloud();
 
    private void LoadSaveCloud()
    {
        _score = YG2.saves.Score;
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
        YG2.SetLeaderboard("LeadersForEarnings", _score);
        YG2.saves.Score = _score;
        YG2.SaveProgress();
    }
}