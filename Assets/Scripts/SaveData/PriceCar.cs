using System;

namespace SaveData
{
    [Serializable]
    public class PriceCar
    {
        public string NameSlot;
        public int CurrentPrise;
        
        public PriceCar(string nameSlot, int currentPrise)
        {
            NameSlot = nameSlot;
            CurrentPrise = currentPrise;
        }
    }
}