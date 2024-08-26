using LiveScoreReporter.Application.Models.DTO;
using LiveScoreReporter.Application.Services.Interfaces;
using Newtonsoft.Json;

namespace LiveScoreReporter.Application.Services
{
    public class SerializerService : ISerializerService
    {
        public string SerializeObjectToJson<T>(T objectToSerialize)
        {
            return JsonConvert.SerializeObject(objectToSerialize);
        }
    }
}
