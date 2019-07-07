using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAstroGrep.Core
{
    public class Constants
    {
        // ListView column index constants
        /// <summary>File Index</summary>
        public const int COLUMN_INDEX_FILE = 0;
        /// <summary>Directory Index</summary>
        public const int COLUMN_INDEX_DIRECTORY = 1;
        /// <summary>File Extension Index</summary>
        public const int COLUMN_INDEX_FILE_EXT = 2;
        /// <summary>Date Index</summary>
        public const int COLUMN_INDEX_DATE = 3;
        /// <summary>Size Index</summary>
        public const int COLUMN_INDEX_SIZE = 4;
        /// <summary>Count Index</summary>
        public const int COLUMN_INDEX_COUNT = 5;

        // ListView column width constants
        /// <summary>File Column Width</summary>
        public const int COLUMN_WIDTH_FILE = 100;
        /// <summary>Directory Column Width</summary>
        public const int COLUMN_WIDTH_DIRECTORY = 200;
        /// <summary>File Extension Column Width</summary>
        public const int COLUMN_WIDTH_FILE_EXT = 110;
        /// <summary>Date Column Width</summary>
        public const int COLUMN_WIDTH_DATE = 140;
        /// <summary>Size Column Width</summary>
        public const int COLUMN_WIDTH_SIZE = 80;
        /// <summary>Count Column Width</summary>
        public const int COLUMN_WIDTH_COUNT = 60;

        /// <summary>Grep Index Index</summary>
        public const int COLUMN_INDEX_GREP_INDEX = 6;

        /// <summary>Default language</summary>
        public static string DEFAULT_LANGUAGE = "zh-cn";
    }
}
