using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibAstroGrep
{
    public class MatchResult
    {
        public FileInfo File { get; set; } = null;

        public int HitCount { get; private set; }

        public int Index { get; set; }

        public MatchResult(FileInfo file)
        {
            File = file;
            HitCount = 0;
        }
    }
}
