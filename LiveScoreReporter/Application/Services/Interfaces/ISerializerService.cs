using LiveScoreReporter.Application.Models.DTO;

namespace LiveScoreReporter.Application.Services.Interfaces
{
    public interface ISerializerService
    {
        string SerializeObjectToJson<T>(T objectToSerialize);
    }
}
