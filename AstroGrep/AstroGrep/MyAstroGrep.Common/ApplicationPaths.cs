using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAstroGrep.Common
{
    public sealed class ApplicationPaths
    {
        /// <summary>
        /// 程序运行路径
        /// </summary>
        public static string DataFolder
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ProductInformation.ApplicationName);
            }
        }
        /// <summary>
        /// 日志文件夹
        /// </summary>
        public static string LogDirectory = Path.Combine(DataFolder, "Log");
        /// <summary>
        /// 日志文件路径
        /// </summary>
        public static string LogFile = Path.Combine(LogDirectory, ProductInformation.ApplicationName + ".log");
        /// <summary>
        /// 日志架构文件
        /// </summary>
        public static string LogArchiveFile = Path.Combine(LogDirectory, ProductInformation.ApplicationName + ".{#}.log");
    }
}
