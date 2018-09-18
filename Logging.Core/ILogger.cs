using System;

namespace Logging.Core
{
    public interface ILogger
    {
        void WriteError(Exception ex, string error);
    }
}