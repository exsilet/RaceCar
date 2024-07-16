using System;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class Finished : MonoBehaviour
    {
        [SerializeField] private PlayerMoney _player;
        
        private int _count;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Car car))
            {
                _player.AddMoney(car.CarStaticData.Coins);
            }
        }
    }
}