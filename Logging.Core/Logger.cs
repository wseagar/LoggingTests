using System;
using Elasticsearch.Net;
using Serilog;
using Serilog.Context;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.File;
using Serilog.Sinks.SystemConsole.Themes;

namespace Logging.Core
{
    public class Logger : ILogger
    {
        private readonly Serilog.ILogger _errorLogger;

        private readonly ILogDetailRetriever _logDetailRetriever;
        
        public Logger(ILogDetailRetriever logDetailRetriever)
        {
            _errorLogger = new LoggerConfiguration()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://127.0.0.1:9200"))
                {
                    AutoRegisterTemplate = true,
                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv5,
                    CustomFormatter = new LoggerJsonFormatter("LogDetail"),
                    FailureCallback = e => FailureCallback(e),
                    EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog |
                                       EmitEventFailureHandling.RaiseCallback |
                                       EmitEventFailureHandling.ThrowException,
                    IndexFormat = "api-error-{0:yyyy.MM.dd}",
                    InlineFields = true
                })
                .WriteTo.Console(theme: AnsiConsoleTheme.Code,
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}{Properties}")
                .WriteTo.File(path: "C:\\logging\\error.txt")
                .Enrich.FromLogContext()
                .CreateLogger();

            _logDetailRetriever = logDetailRetriever;

        }

        public static void FailureCallback(LogEvent e)
        {
            Console.WriteLine("Unable to sumbit event " + e.MessageTemplate);
        }

        public void WriteError(Exception ex, string error)
        {
            var ld = _logDetailRetriever.GetLogDetail(error);
            _errorLogger.Error(ex, "{@LogDetail}", ld);
        }
        
        private static string GetMessageFromException(Exception ex)
        {
            if (ex == null) 
                return "";
            if (ex.InnerException != null)
            {
                return GetMessageFromException(ex.InnerException);
            }
            return ex.Message;
        }
    }
}