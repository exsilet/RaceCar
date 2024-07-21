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

        public void SaveMoney(int money) => _dataBase.AllMoney = money;

        public int ReadMoney() => _dataBase.AllMoney;

        public void SaveMaxLevelCar(int maxLevelCar) => _dataBase.MaxLevelCar = maxLevelCar;

        public int ReadMaxLevelCar() => _dataBase.MaxLevelCar;

        public void SaveGarageSlotData(GarageSlot slot)
        {
            _dataBase.AddGarageSlot(slot);
            Save();
        }

        public void SaveCar(Car car)
        {
            _dataBase.AddSaveCar(car);
            Save();
        }

        public int ReadCar(GarageSlot slot)
        {
            return _dataBase.ReadCar(slot);
        }

        private void Save()
        {
            PlayerPrefs.SetString(ProgressKey, JsonUtility.ToJson(_dataBase));
            PlayerPrefs.Save();
        }
    }
}