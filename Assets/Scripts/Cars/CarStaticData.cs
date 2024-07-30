using UnityEngine;

namespace Cars
{
    public abstract class CarStaticData : ScriptableObject
    {
        public Sprite UIIcon;
        public GameObject PrefabSelect;
        public GameObject PrefabCarOnTheTrack;
        public int StartPrice;
        public int Coins;
        
        [Range(0f, 5f)] public float Speed;
        [Range(0, 50)] public int Level;
    }
}