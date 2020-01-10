using ComProvis.AV.Core;
using System;

namespace ComProvis.AV
{
    public class LogData : IPrimaryKey
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Source { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string ExternalId { get; set; }
        public string Data { get; set; }
    }
}
