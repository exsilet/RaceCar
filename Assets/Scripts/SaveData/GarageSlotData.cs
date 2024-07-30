using System;

namespace SaveData
{
    [Serializable]
    public class GarageSlotData
    {
        public int SlotNumber;
        public int CarLevel;
        public bool IsGarage;
        
        public GarageSlotData(int slotNumber, int carLevel, bool isGarage)
        {
            SlotNumber = slotNumber;
            CarLevel = carLevel;
            IsGarage = isGarage;
        }
    }
}