using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Library.Wunderlist
{
    public class Reader : JsonReader<Backup>, IReader<Backup>
    {
        protected override void Configure(JsonSerializer serializer)
        {
            serializer
                .ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };
        }
    }
}