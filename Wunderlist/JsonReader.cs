using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Wunderlist
{
    public abstract class JsonReader<T>
    {
        protected abstract void Configure(JsonSerializer serializer);

        public T Read(Stream stream)
        {
            using (StreamReader file = new StreamReader(stream, Encoding.UTF8))
            {
                JsonSerializer serializer = new JsonSerializer();
                Configure(serializer);
                serializer.ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };

                return (T)serializer.Deserialize(file, typeof(T));
            }
        }
    }
}