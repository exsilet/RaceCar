using System.Collections.Generic;

namespace DefaultNamespace.SaveLoad
{
    public class Data
    {
        public int Money;
        public int MaxLevelCar;
        public int Score;

        public List<CarStaticData> CarStaticData = new();
    }
}