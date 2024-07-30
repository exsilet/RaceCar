using SaveData;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class PlayerMoney : MonoBehaviour
    {
        [SerializeField] private long _money;
        [SerializeField] private SaveLoadService _saveLoad;
        
        public long Money => _money;
        public event UnityAction<long> CurrentMoneyChanged;
        
        private void Start()
        {
            _money = _saveLoad.ReadMoney();
            CurrentMoneyChanged?.Invoke(_money);
        }

        public void BuyCar(int buyCar)
        {
            if (_money >= buyCar)
            {
                _money -= buyCar;
                _saveLoad.SaveMoney(_money);
                CurrentMoneyChanged?.Invoke(_money);
            }
        }
        
        public void AddMoney(int money)
        {
            _money += money;
            CurrentMoneyChanged?.Invoke(_money);
            _saveLoad.SaveMoney(_money);
        }
    }
}