using System;

namespace SaveData
{
    [Serializable]
    public class GarageSlotData
    {
        public string NameSlot;
        public int CarLevel;
        public bool IsGarage;
        
        public GarageSlotData(string nameSlot, int carLevel, bool isGarage)
        {
            NameSlot = nameSlot;
            CarLevel = carLevel;
            IsGarage = isGarage;
        }
    }
}