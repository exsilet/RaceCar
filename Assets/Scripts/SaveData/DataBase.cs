using System;
using System.Collections.Generic;
using DefaultNamespace;

namespace SaveData
{
    [Serializable]
    public class DataBase
    {
        public long AllMoney;
        public int MaxLevelCar;
        public int OpenCarCount;
        
        public List<GarageSlotData> GaragesSlotData = new();
        public List<PriceCar> PriceCars = new();
        
        public DataBase()
        {
            AllMoney = 60;
            MaxLevelCar = 1;
            OpenCarCount = 1;
        }
        
        public void AddGarageSlot(GarageSlot slot)
        {
            foreach (var data in GaragesSlotData)
            {
                if (data.SlotNumber == slot.SlotNumber)
                {
                    data.SlotNumber = slot.SlotNumber;
                    data.CarLevel = slot.CarLevel;
                    data.IsGarage = slot.InTheGarage;
                    return;
                }
            }
            
            GaragesSlotData.Add(new GarageSlotData(slot.SlotNumber, slot.CarLevel, slot.InTheGarage));
        }

        public int ReadCar(GarageSlot slot)
        {
            foreach (var data in GaragesSlotData)
            {
                if (data.SlotNumber == slot.SlotNumber)
                {
                    return data.CarLevel;
                }
            }

            return 0;
        }

        public void AddPriceCar(string levelCar, int price)
        {
            foreach (PriceCar car in PriceCars)
            {
                if (car.LevelCar == levelCar)
                {
                    car.LevelCar = levelCar;
                    car.CurrentPrise = price;
                    return;
                }
            }
            
            PriceCars.Add(new PriceCar(levelCar, price));
        }

        public int ReadPriceSlot(string levelCar)
        {
            foreach (var data in PriceCars)
            {
                if (data.LevelCar == levelCar)
                {
                    return data.CurrentPrise;
                }
            }

            return 0;
        }
    }
}