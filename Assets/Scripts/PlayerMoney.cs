using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class PlayerMoney : MonoBehaviour
    {
        [SerializeField] private int _money;
        
        public int Money => _money;
        
        public event UnityAction<int> CurrentMoneyChanged;
        
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