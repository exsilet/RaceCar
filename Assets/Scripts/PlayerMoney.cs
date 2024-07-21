using SaveData;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class PlayerMoney : MonoBehaviour
    {
        [SerializeField] private int _money;
        [SerializeField] private SaveLoadService _saveLoad;
        
        public int Money => _money;
        public event UnityAction<int> CurrentMoneyChanged;
        
        private void Start()
        {
            _money = _saveLoad.ReadMoney();
            CurrentMoneyChanged?.Invoke(_money);
        }

        private void OnDestroy() => 
            _saveLoad.SaveMoney(_money);

        public void BuyCar(int buyCar)
        {
            if (_money >= buyCar)
            {
                _money -= buyCar;
                CurrentMoneyChanged?.Invoke(_money);
            }
        }
        
        public void AddMoney(int money)
        {
            _money += money;
            CurrentMoneyChanged?.Invoke(_money);
        }
    }
}