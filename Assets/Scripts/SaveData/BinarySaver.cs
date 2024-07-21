using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using SaveData.Surrogate;
using UnityEngine;

namespace SaveData
{
    public class BinarySaver
    {
        private string _saveDirectory;
        private BinaryFormatter _formatter;

        public BinarySaver()
        {
            var directory = Application.persistentDataPath + "/save";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            
            _saveDirectory = directory + "/GameData.save";
            _formatter =  GetFormatter();
        }
        
        public void Save(object saveData)
        {
            var file = File.Create(_saveDirectory);
            _formatter.Serialize(file, saveData);
            file.Close();
        }
        
        public object Load(object saveDataByDefault)
        {
            if (!File.Exists(_saveDirectory))
            {
                if (saveDataByDefault != null) 
                    Save(saveDataByDefault);

                return saveDataByDefault;
            }
            
            var file = File.Open(_saveDirectory, FileMode.Open);
            var saveData = _formatter.Deserialize(file);
            file.Close();
            return saveData;
        }

        // public void Save(DataBase data)
        // {
        //     using (FileStream file = File.Create(_saveDirectory))
        //     {
        //         new BinaryFormatter().Serialize(file, data);
        //     }
        // }
        
        // public DataBase Load()
        // {
        //     DataBase dataBase;
        //
        //     using (FileStream file = File.Open(_saveDirectory, FileMode.Open))
        //     {
        //         object loadedData = new BinaryFormatter().Deserialize(file);
        //         dataBase = (DataBase)loadedData;  
        //     }
        //     
        //     return dataBase;
        // }
        
        private BinaryFormatter GetFormatter()
        {
            var formatter = new BinaryFormatter();
            var surrogateSelector = new SurrogateSelector();
            surrogateSelector.AddSurrogate(typeof(Vector3), new StreamingContext(
                StreamingContextStates.All), new Vector3Serializer());
            surrogateSelector.AddSurrogate(typeof(Quaternion), new StreamingContext(
                StreamingContextStates.All), new QuaternionSerializer());
            formatter.SurrogateSelector = surrogateSelector;
            return formatter;
        }
    }
}