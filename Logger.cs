using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using log4net;

namespace ECommerceCMS
{
    public static class Logger
    {
        private static readonly string LOG_CONFIG_FILE = @"log4net.config";

        private static readonly ILog _log = GetLogger(typeof(Logger));
        //private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static ILog GetLogger(Type type)
        {
            return LogManager.GetLogger(type);
        }

        public static void Debug(object message)
        {
            SetLog4NetConfiguration();
            _log.Debug(message);
        }

        public static void Info(object message)
        {
            SetLog4NetConfiguration();
            _log.Info(message);
        }

        public static void Warn(object message)
        {
            SetLog4NetConfiguration();
            _log.Warn(message);
        }

        public static void Error(object message)
        {
            SetLog4NetConfiguration();
            _log.Error(message);
        }

        public static void Error(object message, Exception e)
        {
            SetLog4NetConfiguration();
            _log.Error(message, e);
        }

        public static void Fatal(object message)
        {
            SetLog4NetConfiguration();
            _log.Fatal(message);
        }

        public static void Fatal(object message, Exception e)
        {
            SetLog4NetConfiguration();
            _log.Fatal(message, e);
        }

        private static void SetLog4NetConfiguration()
        {
            XmlDocument log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead(LOG_CONFIG_FILE));
            var repo = LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));
            log4net.Config.XmlConfigurator.Configure(repo, log4netConfig["log4net"]);
        }
    }
}