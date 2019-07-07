using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MyAstroGrep.Windows
{
    public class API
    {
        private API() { }

        [DllImport("Shlwapi.dll", CharSet = CharSet.Auto)]
        private static extern long StrFormatByteSize(
              long fileSize
              , [MarshalAs(UnmanagedType.LPTStr)] System.Text.StringBuilder buffer
              , int bufferSize);

        public static string StrFormatByteSize(long filesize)
        {
            StringBuilder sb = new StringBuilder(11);
            StrFormatByteSize(filesize, sb, sb.Capacity);
            return sb.ToString();
        }
    }
}
