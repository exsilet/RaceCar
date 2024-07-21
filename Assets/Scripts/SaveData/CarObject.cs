using System;

namespace SaveData
{
    [Serializable]
    public class CarObject
    {
        public string NameSlot;
        public int Level;
        public float Speed;
        
        public CarObject(string nameSlot, float speed, int level)
        {
            NameSlot = nameSlot;
            Level = level;
            Speed = speed;
        }
    }
}