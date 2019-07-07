using MyAstroGrep.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAstroGrep.Core
{
    public class GeneralSettings
    {
        private static GeneralSettings mySettings = null;
        private int columnFile = -1;
        private int columnLocation = -1;
        private int columnDate = -1;
        private int columnCount = -1;
        private int columnSize = -1;
        private int columnFileExt = -1;
        private const string VERSION = "1.0";
        private string language = Constants.DEFAULT_LANGUAGE;
        private int windowsDPIPerCentSetting = 100;

        private static GeneralSettings MySettings
        {
            get
            {
                if (mySettings == null)
                {
                    mySettings = new GeneralSettings();
                    SettingsIO.Load(MySettings, Location, VERSION);
                }
                return mySettings;
            }
        }

        public static string Location
        {
            get
            {
                return Path.Combine(ApplicationPaths.DataFolder, "AstroGrep.general.config");
            }
        }

        public static int WindowFileColumnNameWidth
        {
            get { return MySettings.columnFile; }
            set { MySettings.columnFile = value; }
        }

        public static int WindowFileColumnLocationWidth
        {
            get { return MySettings.columnLocation; }
            set { MySettings.columnLocation = value; }
        }

        public static int WindowFileColumnFileExtWidth
        {
            get { return MySettings.columnFileExt; }
            set { MySettings.columnFileExt = value; }
        }

        public static int WindowFileColumnDateWidth
        {
            get { return MySettings.columnDate; }
            set { MySettings.columnDate = value; }
        }

        public static int WindowFileColumnSizeWidth
        {
            get { return MySettings.columnSize; }
            set { MySettings.columnSize = value; }
        }

        public static int WindowFileColumnCountWidth
        {
            get { return MySettings.columnCount; }
            set { MySettings.columnCount = value; }
        }

        public static string Language
        {
            get { return MySettings.language; }
            set { MySettings.language = value; }
        }

        public static int WindowsDPIPerCentSetting
        {
            get { return MySettings.windowsDPIPerCentSetting; }
            set { MySettings.windowsDPIPerCentSetting = value; }
        }
    }
}
