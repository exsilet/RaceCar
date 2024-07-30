using System;

namespace SaveData
{
    [Serializable]
    public class PriceCar
    {
        public string LevelCar;
        public int CurrentPrise;
        
        public PriceCar(string levelCar, int currentPrise)
        {
            LevelCar = levelCar;
            CurrentPrise = currentPrise;
        }
    }
}