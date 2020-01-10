using System.Threading.Tasks;

namespace ComProvis.AV.Core.Repositories
{
    public interface ILogDataRepository : IRepository<LogData>
    {
        Task InsertLogoRecordAsync(string source, string level, string message, string externalId, string data);
    }
}
