using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euchre.Services
{
    public class JsonService
    {
        /// <summary>
        /// Serializes an object or list of objects to a JSON file.
        /// </summary>
        public static void SaveToJsonFile<T>(T data, string filePath)
        {
            try
            {
                string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(filePath, json);
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);    
            }
        }

        /// <summary>
        /// Deserializes a JSON file back into an object or list of objects.
        /// </summary>
        public static T LoadFromJsonFile<T>(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    throw new FileNotFoundException("File not found", filePath);

                string json = File.ReadAllText(filePath);
                T data = JsonConvert.DeserializeObject<T>(json);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
