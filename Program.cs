using System;
using LightInject;
using Logging.Core;

namespace Logging
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            //var ld = GetLogDetail("Hello Elastic!", null);
            //Logger.WriteError(ld);
            
            var container = new ServiceContainer();
            container.Register<ILogDetailRetriever, LogDetailRetriever>();
            container.Register<Core.ILogger, Logger>(new PerContainerLifetime());

            var logger = container.GetInstance<Core.ILogger>();

            using (LogContextWrapper.PushProperty("Test", 1))
            {
                var ex1 = new Exception("The bottom!");
                
                var ex2 = new Exception("The middle", ex1);
                
                var ex3 = new Exception("The top", ex2);
                
                logger.WriteError(ex3, "Test123");
            }
            
            Console.ReadLine();
        }
    }
}