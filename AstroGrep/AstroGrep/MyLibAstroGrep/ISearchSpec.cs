using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibAstroGrep
{
    public interface ISearchSpec
    {
        /// <summary>
        /// 搜索文件
        /// </summary>
        string[] StartFilePaths { get; set; }
        /// <summary>
        /// 搜索目录
        /// </summary>
        string[] StartDirectories { get; }
        /// <summary>
        /// 文件类型
        /// </summary>
        string FileFilter { get; }
        /// <summary>
        /// 搜索内容
        /// </summary>
        string SearchText { get; }
    }
}
