using System.Threading.Tasks;

namespace ComProvis.AV.Services.Interfaces
{
    public interface ILogDataService : IService<LogData>
    {
        Task InsertLogoRecordAsync(string source, string level, string message, string externalId, string data);
    }
}
