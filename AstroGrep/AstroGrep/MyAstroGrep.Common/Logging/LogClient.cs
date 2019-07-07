using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace MyAstroGrep.Common.Logging
{
    public class LogClient : ILogClient
    {
        public string LogFile { get; set; }
        public Logger Logger { get; set; }

        private static LogClient instance;
        /// <summary>
        /// 单例模式
        /// </summary>
        public static LogClient Instance
        {
            get
            {
                if (LogClient.instance == null)
                {
                    LogClient.instance = new LogClient();
                }
                return LogClient.instance;
            }
        }

        /// <summary>
        /// 私有构造函数
        /// </summary>
        private LogClient()
        {
            LoggingConfiguration loggingConfiguration = new LoggingConfiguration();

            FileTarget fileTarget = new FileTarget();
            loggingConfiguration.AddTarget("file", fileTarget);

            LogFile = ApplicationPaths.LogFile;
            fileTarget.FileName = LogFile;
            fileTarget.ArchiveFileName = ApplicationPaths.LogArchiveFile;
            fileTarget.Layout = "${longdate}|${level}|${callsite}|${message}";
            fileTarget.ArchiveAboveSize = 5242880L;
            fileTarget.ArchiveNumbering = ArchiveNumberingMode.Rolling;
            fileTarget.MaxArchiveFiles = 3;

            LoggingRule item = new LoggingRule("*", LogLevel.Debug, fileTarget);
            loggingConfiguration.LoggingRules.Add(item);
            LogManager.Configuration = loggingConfiguration;

            this.Logger = LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// 抓取异常
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string GetAllExceptions(Exception ex)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Exception:");
            stringBuilder.AppendLine(ex.ToString());
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("Stack trace:");
            stringBuilder.AppendLine(ex.StackTrace);
            int num = 0;
            // checked 整数溢出检查
            checked
            {
                while (ex.InnerException != null)
                {
                    num++;
                    stringBuilder.AppendLine("Inner Exception " + num.ToString() + ":");
                    stringBuilder.AppendLine(ex.InnerException.ToString());
                    ex = ex.InnerException;
                }
                return stringBuilder.ToString();
            }
        }
    }
}
