using System;
using Logging.Core;

namespace Logging
{
    public class LogDetailRetriever : ILogDetailRetriever
    {
        LogDetail ILogDetailRetriever.GetLogDetail(string message)
        {
            return GetLogDetail(message);
        }

        private static LogDetail GetLogDetail(string message)
        {
            return new LogDetail {
                Hostname = Environment.MachineName,
                OrgId = new Guid().ToString(),
                Message = message,
                Layer = "API",
                Location = "/api/SalesOrder/1",
                Product = "Unleashed",
                User = "w.seagar@gmail.com",
                CorrelationId = new Guid().ToString()
            };
        }
    }
}