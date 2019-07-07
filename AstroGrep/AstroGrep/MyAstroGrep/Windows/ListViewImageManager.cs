using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyAstroGrep.Windows
{
    public sealed class ListViewImageManager
    {
        private static Hashtable ExtensionTable = new Hashtable();
        private enum IconSize { Large, Small, Shell };

        /// <summary>
		/// API Constants
		/// </summary>
		private const uint SHGFI_ICON = 0x100;  // get icon
        private const uint SHGFI_LINKOVERLAY = 0x8000;  // put a link overlay on icon
        private const uint SHGFI_SELECTED = 0x10000;    // show icon in selected state
        private const uint SHGFI_LARGEICON = 0x0;       // get large icon
        private const uint SHGFI_SMALLICON = 0x1;       // get small icon
        private const uint SHGFI_OPENICON = 0x2;        // get open icon
        private const uint SHGFI_SHELLICONSIZE = 0x4;       // get shell size icon
        private const uint SHGFI_USEFILEATTRIBUTES = 0x10;      // use passed dwFileAttribute
        private const uint SHGFI_TYPENAME = 0x400;	// get file type name

        public static int GetImageIndex(FileInfo file, ImageList list)
        {
            string ext = file.Extension.ToLower();

            switch (ext)
            {
                case ".ico":
                case ".exe":
                    ext = file.Name.ToLower();
                    break;
            }

            if (ExtensionTable.Contains(ext))
            {
                return int.Parse(ExtensionTable[ext].ToString());
            }
            else
            {
                AddIconOfFile(file.FullName, IconSize.Small, false, false, false, list);
                ExtensionTable.Add(ext, list.Images.Count - 1);

                return list.Images.Count - 1;
            }
        }

        private static void AddIconOfFile(string fileName, IconSize iconSize, bool selectedState, bool openState, bool linkOverlay, ImageList destinationImagelist)
        {
            uint uFlags = ((iconSize == IconSize.Large) ? SHGFI_LARGEICON : 0) |
                            ((iconSize == IconSize.Small) ? SHGFI_SMALLICON : 0) |
                            ((iconSize == IconSize.Shell) ? SHGFI_SHELLICONSIZE : 0) |
                            ((selectedState) ? SHGFI_SELECTED : 0) |
                            ((openState) ? SHGFI_OPENICON : 0) |
                            ((linkOverlay) ? SHGFI_LINKOVERLAY : 0);

            Add(fileName, destinationImagelist, uFlags);
        }
    }
}
