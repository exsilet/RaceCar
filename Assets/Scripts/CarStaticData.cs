using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "CarData", menuName = "CarData/Car")]
    public class CarStaticData : ScriptableObject
    {
        public Sprite UIIcon;
        public GameObject PrefabSelect;
        public GameObject PrefabCarOnTheTrack;
        public int StartPrice;
        public int Coins;
        
        [Range(0f, 5f)] public float Speed;
        [Range(0, 30)] public int Level;
    }
}