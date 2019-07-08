using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibAstroGrep
{
    public class MatchResultLineMatch
    {
        public int StartPosition { get; set; }

        public int Length { get; set; }

        public MatchResultLineMatch()
        {
            StartPosition = -1;
            Length = 0;
        }

        public MatchResultLineMatch(int startPosition, int length)
        {
            StartPosition = startPosition;
            Length = length;
        }
    }
}
