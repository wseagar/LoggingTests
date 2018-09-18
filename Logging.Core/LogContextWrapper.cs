using System;
using Serilog.Context;

namespace Logging.Core
{
    public static class LogContextWrapper
    {
        public static IDisposable PushProperty(string name, object value, bool destructureObjects = false)
        {
            return LogContext.PushProperty(name, value, destructureObjects);
        }
    }
}