using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibAstroGrep
{
    public class MatchResultLine
    {
        public string Line { get; set; }

        public int LineNumber { get; set; }

        public int ColumnNumber { get; set; }

        public bool HasMatch { get; set; }

        public List<MatchResultLineMatch> Matches { get; set; }

        public MatchResultLine()
        {
            Line = string.Empty;
            LineNumber = 1;
            ColumnNumber = 1;
            HasMatch = false;
            Matches = new List<MatchResultLineMatch>();
        }
    }
}
