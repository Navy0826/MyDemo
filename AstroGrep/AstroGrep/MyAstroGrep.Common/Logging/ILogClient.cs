using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAstroGrep.Common.Logging
{
    public interface ILogClient
    {
        string LogFile { get; set; }

        Logger Logger { get; set; }
    }
}
