using ComProvis.AV.Core;
using ComProvis.AV.Core.Repositories;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ComProvis.AV.Data.Repositories
{
    public class LogDataRepository : Repository<LogData>, ILogDataRepository
    {
        public LogDataRepository(IComProvisAvDbContext context) : base(context)
        {

        }

        public async Task InsertLogoRecordAsync(string source, string level, string message, string externalId, string data)
        {
            await _context.ExecuteSqlCommandAsync("EXECUTE [dbo].[InsertLogoRecord] @p0, @p1, @p2, @p3, @p4", new SqlParameter("p0", source ?? (object)DBNull.Value),
                                                                                     new SqlParameter("p1", level ?? (object)DBNull.Value),
                                                                                     new SqlParameter("p2", message ?? (object)DBNull.Value),
                                                                                     new SqlParameter("p3", externalId ?? (object)DBNull.Value),
                                                                                     new SqlParameter("p4", data ?? (object)DBNull.Value));
        }
    }

}
