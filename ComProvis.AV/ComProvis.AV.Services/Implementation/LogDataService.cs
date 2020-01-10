using ComProvis.AV.Core;
using ComProvis.AV.Services.Interfaces;
using System.Threading.Tasks;

namespace ComProvis.AV.Services.Implementation
{
    public class LogDataService : Service<LogData>, ILogDataService
    {
        public LogDataService(IUnitOfWork uow) : base(uow)
        {

        }

        public async Task InsertLogoRecordAsync(string source, string level, string message, string externalId, string data) =>  await UnitOfWork.LogDataRepository.InsertLogoRecordAsync(source, level, message, externalId, data);
    }
}
