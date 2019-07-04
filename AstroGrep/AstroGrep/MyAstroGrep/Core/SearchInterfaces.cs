using MyLibAstroGrep;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAstroGrep.Core
{
    public class SearchInterfaces
    {
        public class SearchSpec : ISearchSpec
        {
            /// <summary>
            /// 搜索文件
            /// </summary>
            public string[] StartFilePaths { get; set; }
            /// <summary>
            /// 搜索目录
            /// </summary>
            public string[] StartDirectories { get; set; }
            /// <summary>
            /// 文件类型
            /// </summary>

            public string FileFilter { get; set; }
            /// <summary>
            /// 搜索内容
            /// </summary>

            public string SearchText { get; set; }
        }
    }
}
