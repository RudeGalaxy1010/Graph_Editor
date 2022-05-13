using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_Base.Helpers
{
    public class Serializer
    {
        public bool Serialize<T>(T data, string path)
            where T : class
        {
            try
            {
                string result = JsonConvert.SerializeObject(data);
                File.WriteAllText(path, result);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public T Deserialize<T>(string path)
            where T : class
        {
            try
            {
                string result = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<T>(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
