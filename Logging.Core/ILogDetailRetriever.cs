namespace Logging.Core
{
    public interface ILogDetailRetriever
    {
        LogDetail GetLogDetail(string message);
    }
}