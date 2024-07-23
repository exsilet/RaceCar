using System;
using System.Collections.Generic;
using DefaultNamespace;

namespace SaveData
{
    [Serializable]
    public class DataBase
    {
        public int AllMoney;
        public int MaxLevelCar;
        public int OpenCarCount;
        
        public List<GarageSlotData> GaragesSlotData = new();
        public List<CarObject> CarObjectsGarageSlot = new();
        public List<PriceCar> PriceCars = new();
        
        public DataBase()
        {
            AllMoney = 6000;
            MaxLevelCar = 1;
            OpenCarCount = 1;
        }
        
        public void AddGarageSlot(GarageSlot slot)
        {
            foreach (var data in GaragesSlotData)
            {
                if (data.NameSlot == slot.name)
                {
                    data.NameSlot = slot.name;
                    data.CarLevel = slot.CarLevel;
                    data.IsGarage = slot.InTheGarage;
                    return;
                }
            }
            
            GaragesSlotData.Add(new GarageSlotData(slot.name, slot.CarLevel, slot.InTheGarage));
        }

        public void AddPriceCar(string nameSlot, int price)
        {
            foreach (PriceCar car in PriceCars)
            {
                if (car.NameSlot == nameSlot)
                {
                    car.NameSlot = nameSlot;
                    car.CurrentPrise = price;
                    return;
                }
            }
            
            PriceCars.Add(new PriceCar(nameSlot, price));
        }

        public void AddSaveCar(Car car)
        {
            foreach (var data in CarObjectsGarageSlot)
            {
                if (data.NameSlot == car.Slot.name)
                {
                    data.NameSlot = car.Slot.name;
                    data.Level = car.Level;
                    data.Speed = car.Speed;
                    return;
                }
            }
            
            CarObjectsGarageSlot.Add(new CarObject(car.Slot.name, car.Speed, car.Level));
        }

        public int ReadPriceSlot(string nameSlot)
        {
            foreach (var data in PriceCars)
            {
                if (data.NameSlot == nameSlot)
                {
                    return data.CurrentPrise;
                }
            }

            return 0;
        }

        public int ReadCar(GarageSlot slot)
        {
            foreach (var data in CarObjectsGarageSlot)
            {
                if (data.NameSlot == slot.name)
                {
                    return data.Level;
                }
            }

            return 0;
        }
    }
}