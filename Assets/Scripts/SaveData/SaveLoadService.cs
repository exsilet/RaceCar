using DefaultNamespace;
using UnityEngine;

namespace SaveData
{
    public class SaveLoadService : MonoBehaviour
    {
        private const string ProgressKey = "Progress";
        
        private DataBase _dataBase;

        private void OnEnable()
        {
            _dataBase = PlayerPrefs.HasKey(ProgressKey)
                ? JsonUtility.FromJson<DataBase>(PlayerPrefs.GetString(ProgressKey))
                : new DataBase();
        }

        private void OnDisable()
        {
            Save();
        }

        public void SaveMoney(long money) => _dataBase.AllMoney = money;

        public long ReadMoney() => _dataBase.AllMoney;

        public void SaveMaxLevelCar(int maxLevelCar) => _dataBase.MaxLevelCar = maxLevelCar;

        public int ReadMaxLevelCar() => _dataBase.MaxLevelCar;
        
        public void SaveOpenLevelCar(int openLevelCar) => _dataBase.OpenCarCount = openLevelCar;

        public int ReadOpenLevelCar() => _dataBase.OpenCarCount;

        public void SaveGarageSlotData(GarageSlot slot)
        {
            _dataBase.AddGarageSlot(slot);
            Save();
        }

        public int ReadCar(GarageSlot slot)
        {
            return _dataBase.ReadCar(slot);
        }

        public void SavePriceCar(string nameSlot, int priceCar)
        {
            _dataBase.AddPriceCar(nameSlot, priceCar);
            Save();
        }

        public int ReadPriceCar(string levelCar)
        {
            return _dataBase.ReadPriceSlot(levelCar);
        }

        private void Save()
        {
            PlayerPrefs.SetString(ProgressKey, JsonUtility.ToJson(_dataBase));
            PlayerPrefs.Save();
        }
    }
}