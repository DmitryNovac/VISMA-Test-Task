using System;
using System.Collections;
using System.IO;
using System.Xml;
using log4net;

namespace VISMA.TestTask.Core.Logger
{
    public class LoggingService
    {
        public static ICollection Configure()
        {
            return log4net.Config.XmlConfigurator.Configure();
        }

        public static ICollection Configure(XmlElement element)
        {
            return log4net.Config.XmlConfigurator.Configure(element);
        }

        public static ICollection Configure(FileInfo configFile)
        {
            return log4net.Config.XmlConfigurator.Configure(configFile);
        }

        public static ICollection Configure(Uri configUri)
        {
            return log4net.Config.XmlConfigurator.Configure(configUri);
        }

        public static ICollection Configure(Stream configStream)
        {
            return log4net.Config.XmlConfigurator.Configure(configStream);
        }

        public static ICollection ConfigureAndWatch(FileInfo configFile)
        {
            return log4net.Config.XmlConfigurator.Configure(configFile);
        }

        public static ILogger GetLogger()
        {
            return new Logger(LogManager.GetLogger(LoggerNames.Default));
        }

        public static ILogger GetLogger(string name)
        {
            return new Logger(LogManager.GetLogger(name));
        }

        public static ILogger GetLogger(Type type)
        {
            return new Logger(LogManager.GetLogger(type));
        }
    }
}
