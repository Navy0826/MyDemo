using System;
using System.IO;

using AstroGrep.Common;

namespace AstroGrep.Core
{
   /// <summary>
   /// Used to access the general settings.
   /// </summary>
   /// <remarks>
   ///   AstroGrep File Searching Utility. Written by Theodore L. Ward
   ///   Copyright (C) 2002 AstroComma Incorporated.
   ///   
   ///   This program is free software; you can redistribute it and/or
   ///   modify it under the terms of the GNU General public License
   ///   as published by the Free Software Foundation; either version 2
   ///   of the License, or (at your option) any later version.
   ///   
   ///   This program is distributed in the hope that it will be useful,
   ///   but WITHOUT ANY WARRANTY; without even the implied warranty of
   ///   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   ///   GNU General public License for more details.
   ///   
   ///   You should have received a copy of the GNU General public License
   ///   along with this program; if not, write to the Free Software
   ///   Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
   /// 
   ///   The author may be contacted at:
   ///   ted@astrocomma.com or curtismbeard@gmail.com
   /// </remarks>
   /// <history>
   /// [Curtis_Beard]      11/02/2006  Created
   /// [Curtis_Beard]      04/14/2007  FIX: 1677004, make sure search panel width is at least default
   /// [Curtis_Beard]      04/25/2007  FIX: 1700029, always get correct config path
   /// [Curtis_Beard]      08/10/2007  CHG: use 800x600 for default size
   /// [Curtis_Beard]	   01/31/2012	ADD: size column width
   /// [Curtis_Beard]	   01/31/2012	CHG: 1947760, update default exclude list to exclude images (bmp,gif,jpg,jpeg,png)
   /// [Curtis_Beard]	   02/24/2012	CHG: 3489693, save state of search options
   /// [Curtis_Beard]	   02/24/2012	CHG: 3488321, ability to change results font
   /// [Curtis_Beard]	   03/07/2012	ADD: 3131609, exclusions, empty out exclude list to use search options value instead
   /// [Curtis_Beard]	   10/05/2012	ADD: 1741935, option to show/hide exclusion/error dialog message to user
   /// [Curtis_Beard]	   10/16/2012	CHG: Save search settings on exit
   /// [Curtis_Beard]	   10/28/2012	ADD: 3575509, results word wrap
   /// [Curtis_Beard]	   10/28/2012	ADD: 3479503, ability to change file list font
   /// [Curtis_Beard]      10/27/2014	CHG: 88, add file extension column
   /// [Curtis_Beard]      11/11/2014	CHG: 85, remove leading white space
   /// [Curtis_Beard]      03/06/2015	CHG: default texteditor is created instead of using delimeter which is now private
   /// [Curtis_Beard]	   04/08/2015	ADD: 20, option to show entire file contents
   /// [Curtis_Beard]	   05/26/2015	FIX: 69, add performance setting for file detection
   /// [LinkNet]           04/24/2017	CHG: Changed default main form window width	
   /// [LinkNet]           04/24/2017	CHG: Increased default file panel font size from 8.25 to 9 points
   /// [LinkNet]           04/27/2017	ADD: Added save and restore exclusions window position settings
   /// [Curtis_Beard]	   05/19/2017	CHG: 120, add option to use accent color
   /// </history>
   public sealed class GeneralSettings
   {
      // This class is fully static.
      private GeneralSettings()  {}

      /// <summary>Default file panel height</summary>
      public const int DEFAULT_FILE_PANEL_HEIGHT = 195;

      #region Declarations
      private static GeneralSettings __MySettings = null;
      
      private const string VERSION = "1.0";

      private string resultsForeColor = string.Format("0{0}0{0}0{0}255", Constants.COLOR_SEPARATOR);
      private string resultsBackColor = string.Format("255{0}255{0}255{0}255", Constants.COLOR_SEPARATOR);
      private string resultsContextForeColor = string.Format("192{0}192{0}192{0}255", Constants.COLOR_SEPARATOR);
      private string matchForeColor = string.Format("255{0}255{0}255{0}255", Constants.COLOR_SEPARATOR);
      private string matchBackColor = string.Format("251{0}127{0}6{0}255", Constants.COLOR_SEPARATOR);
      private string resultsFont = string.Format("Lucida Console{0}9.75{0}Regular", Constants.FONT_SEPARATOR);
      private string resultsFontDefault = string.Format("Lucida Console{0}9.75{0}Regular", Constants.FONT_SEPARATOR);
      private int mruListCount = 15;

      private string language = Constants.DEFAULT_LANGUAGE;
      private string extExcludeList = string.Empty;

      private int windowsDPIPerCentSetting = 100;
      private bool setDefaultFonts = true;

      private int windowLeft = -1;
      private int windowTop = -1;
      private int windowWidth = -1;
      private int windowHeight = -1;
      private int windowState = -1;
      
      private int searchPanelWidth = Constants.DEFAULT_SEARCH_PANEL_WIDTH;
      private int filePanelHeight = -1;
      private int columnFile = -1;
      private int columnLocation = -1;
      private int columnDate = -1;
      private int columnCount = -1;
      private int columnSize = -1;
      private int columnFileExt = -1;

      private bool logDisplaySaveWindowPosition = true;
      private int columnLogDisplayDate = -1;
      private int columnLogDisplayType = -1;
      private int columnLogDisplayValue = -1;
      private int columnLogDisplayDetails = -1;
      private int logDisplayTop = -1;
      private int logDisplayLeft = -1;
      private int logDisplayWidth = -1;
      private int logDisplayHeight = -1;

      private bool exclusionsDisplaySaveWindowPosition = true;
      private int columnExclusionsDisplayEnabled = -1;
      private int columnExclusionsDisplayCategory = -1;
      private int columnExclusionsDisplayType = -1;
      private int columnExclusionsDisplayValue = -1;
      private int columnExclusionsDisplayOption = -1;
      private int exclusionsDisplayTop = -1;
      private int exclusionsDisplayLeft = -1;
      private int exclusionsDisplayWidth = -1;
      private int exclusionsDisplayHeight = -1;

      private string searchStartPaths = string.Empty;
      private string searchFilters = string.Format("*.*{0}*.txt{0}*.java{0}*.htm, *.html{0}*.jsp, *.asp{0}*.js, *.inc{0}*.htm, *.html, *.jsp, *.asp{0}*.sql{0}*.bas, *.cls, *.vb{0}*.cs{0}*.cpp, *.c, *.h{0}*.asm", Constants.SEARCH_ENTRIES_SEPARATOR);
      private string searchTexts = string.Empty;

      private string textEditors = (new TextEditor("*", "notepad", "%1", 0)).ToString();

      private bool showExclusionErrorMessage = true;
        
      private bool saveSearchOptionsOnExit = true;

      private bool resultsWordWrap = false;

      private string filePanelFont = string.Format("Microsoft Sans Serif{0}9{0}Regular", Constants.FONT_SEPARATOR);
      private string filePanelFontDefault = string.Format("Microsoft Sans Serif{0}9{0}Regular", Constants.FONT_SEPARATOR);

      private bool removeLeadingWhiteSpace = false;

      private bool detectFileEncoding = true;
      private string fileEncodings = string.Empty;
      private int encodingPerformance = 2;
      private bool useEncodingCache = true;

      private bool showEntireFile = false;

      private bool useAstroGrepAccentColor = true;
      #endregion
      
      /// <summary>
      /// Contains the static reference of this class.
      /// </summary>
      private static GeneralSettings MySettings
      {
         get
         {
            if (__MySettings == null)
            {
               __MySettings = new GeneralSettings();
               SettingsIO.Load(__MySettings, Location, VERSION);
            }
            return __MySettings;
         }
      }

      /// <summary>
      /// Gets the full location to the config file.
      /// </summary>
      static public string Location
      {
         get
         {
            return Path.Combine(ApplicationPaths.DataFolder, "AstroGrep.general.config");
         }
      }

      /// <summary>
      /// Save the search options.
      /// </summary>
      /// <returns>Returns true on success, false otherwise</returns>
      static public bool Save()
      {
         return SettingsIO.Save(MySettings, Location, VERSION);
      }

      /// <summary>
      /// Gets/Sets the result fore color.
      /// </summary>
      static public string ResultsForeColor
      {
         get { return MySettings.resultsForeColor; }
         set { MySettings.resultsForeColor = value; }
      }

      /// <summary>
      /// Gets/Sets the result back color.
      /// </summary>
      static public string ResultsBackColor
      {
         get { return MySettings.resultsBackColor; }
         set { MySettings.resultsBackColor = value; }
      }

      /// <summary>
      /// Gets/Sets the result context line fore color.
      /// </summary>
      static public string ResultsContextForeColor
      {
         get { return MySettings.resultsContextForeColor; }
         set { MySettings.resultsContextForeColor = value; }
      }

      /// <summary>
      /// Gets/Sets the highlight fore color.
      /// </summary>
      static public string HighlightForeColor
      {
         get { return MySettings.matchForeColor; }
         set { MySettings.matchForeColor = value; }
      }

      /// <summary>
      /// Gets/Sets the highlight back color.
      /// </summary>
      static public string HighlightBackColor
      {
         get { return MySettings.matchBackColor; }
         set { MySettings.matchBackColor = value; }
      }

      /// <summary>
      /// Gets/Sets the maximum number of mru path to save.
      /// </summary>
      static public int MaximumMRUPaths
      {
         get { return MySettings.mruListCount; }
         set { MySettings.mruListCount = value; }
      }

      /// <summary>
      /// Gets/Sets the application language.
      /// </summary>
      static public string Language
      {
         get { return MySettings.language; }
         set { MySettings.language = value; }
      }

      /// <summary>
      /// Gets/Sets the extension exclusion list (NO LONGER USED).
      /// </summary>
      static public string ExtensionExcludeList
      {
         get { return MySettings.extExcludeList; }
         set { MySettings.extExcludeList = value; }
      }

      /// <summary>
      /// Gets/Sets the window's left value.
      /// </summary>
      static public int WindowLeft
      {
         get { return MySettings.windowLeft; }
         set { MySettings.windowLeft = value; }
      }

      /// <summary>
      /// Gets/Sets the window's top value.
      /// </summary>
      static public int WindowTop
      {
         get { return MySettings.windowTop; }
         set { MySettings.windowTop = value; }
      }

      /// <summary>
      /// Gets/Sets the window's height value.
      /// </summary>
      static public int WindowHeight
      {
         get { return MySettings.windowHeight; }
         set { MySettings.windowHeight = value; }
      }

      /// <summary>
      /// Gets/Sets the window's width value.
      /// </summary>
      static public int WindowWidth
      {
         get { return MySettings.windowWidth; }
         set { MySettings.windowWidth = value; }
      }

      /// <summary>
      /// Gets/Sets the window's WindowState value.
      /// </summary>
      static public int WindowState
      {
         get { return MySettings.windowState; }
         set { MySettings.windowState = value; }
      }

      /// <summary>
      /// Gets/Sets the window's search panel width value.
      /// </summary>
      static public int WindowSearchPanelWidth
      {
         get { return MySettings.searchPanelWidth; }
         set 
         { 
            if (value < Constants.DEFAULT_SEARCH_PANEL_WIDTH)
               value = Constants.DEFAULT_SEARCH_PANEL_WIDTH;

            MySettings.searchPanelWidth = value; 
         }
      }

      /// <summary>
      /// Gets/Sets the window's file panel height value.
      /// </summary>
      static public int WindowFilePanelHeight
      {
         get { return MySettings.filePanelHeight; }
         set { MySettings.filePanelHeight = value; }
      }

      /// <summary>
      /// Gets/Sets the window's file list name column value.
      /// </summary>
      static public int WindowFileColumnNameWidth
      {
         get { return MySettings.columnFile; }
         set { MySettings.columnFile = value; }
      }

      /// <summary>
      /// Gets/Sets the window's file list location column value.
      /// </summary>
      static public int WindowFileColumnLocationWidth
      {
         get { return MySettings.columnLocation; }
         set { MySettings.columnLocation = value; }
      }

      /// <summary>
      /// Gets/Sets the window's file list date column value.
      /// </summary>
      static public int WindowFileColumnDateWidth
      {
         get { return MySettings.columnDate; }
         set { MySettings.columnDate = value; }
      }

      /// <summary>
      /// Gets/Sets the window's file list count column value.
      /// </summary>
      static public int WindowFileColumnCountWidth
      {
         get { return MySettings.columnCount; }
         set { MySettings.columnCount = value; }
      }

      /// <summary>
      /// Gets/Sets the window's file list size column value.
      /// </summary>
      static public int WindowFileColumnSizeWidth
      {
         get { return MySettings.columnSize; }
         set { MySettings.columnSize = value; }
      }

      /// <summary>
      /// Gets/Sets the window's file list file extension column value.
      /// </summary>
      static public int WindowFileColumnFileExtWidth
      {
         get { return MySettings.columnFileExt; }
         set { MySettings.columnFileExt = value; }
      }

      /// <summary>
      /// Gets/Sets the LogDisplay's date column width value.
      /// </summary>
      static public int LogDisplayColumnDateWidth
      {
         get { return MySettings.columnLogDisplayDate; }
         set { MySettings.columnLogDisplayDate = value; }
      }

      /// <summary>
      /// Gets/Sets the LogDisplay's type column width value.
      /// </summary>
      static public int LogDisplayColumnTypeWidth
      {
         get { return MySettings.columnLogDisplayType; }
         set { MySettings.columnLogDisplayType = value; }
      }

      /// <summary>
      /// Gets/Sets the LogDisplay's value column width value.
      /// </summary>
      static public int LogDisplayColumnValueWidth
      {
         get { return MySettings.columnLogDisplayValue; }
         set { MySettings.columnLogDisplayValue = value; }
      }

      /// <summary>
      /// Gets/Sets the LogDisplay's details column width value.
      /// </summary>
      static public int LogDisplayColumnDetailsWidth
      {
         get { return MySettings.columnLogDisplayDetails; }
         set { MySettings.columnLogDisplayDetails = value; }
      }

      /// <summary>
      /// Gets/Sets the LogDisplay's top value.
      /// </summary>
      static public int LogDisplayTop
      {
         get { return MySettings.logDisplayTop; }
         set { MySettings.logDisplayTop = value; }
      }

      /// <summary>
      /// Gets/Sets the LogDisplay's left value.
      /// </summary>
      static public int LogDisplayLeft
      {
         get { return MySettings.logDisplayLeft; }
         set { MySettings.logDisplayLeft = value; }
      }

      /// <summary>
      /// Gets/Sets the LogDisplay's width value.
      /// </summary>
      static public int LogDisplayWidth
      {
         get { return MySettings.logDisplayWidth; }
         set { MySettings.logDisplayWidth = value; }
      }

      /// <summary>
      /// Gets/Sets the LogDisplay's height value.
      /// </summary>
      static public int LogDisplayHeight
      {
         get { return MySettings.logDisplayHeight; }
         set { MySettings.logDisplayHeight = value; }
      }

      /// <summary>
      /// Gets/Sets whether to save the log display form's window position.
      /// </summary>
      static public bool LogDisplaySavePosition
      {
         get { return MySettings.logDisplaySaveWindowPosition; }
         set { MySettings.logDisplaySaveWindowPosition = value; }
      }

      /// <summary>
      /// Gets/Sets the exclusions display form's enabled column width value.
      /// </summary>
      static public int ExclusionsDisplayColumnEnabledWidth
      {
         get { return MySettings.columnExclusionsDisplayEnabled; }
         set { MySettings.columnExclusionsDisplayEnabled = value; }
      }

      /// <summary>
      /// Gets/Sets the exclusions display form's category column width value.
      /// </summary>
      static public int ExclusionsDisplayColumnCategoryWidth
      {
         get { return MySettings.columnExclusionsDisplayCategory; }
         set { MySettings.columnExclusionsDisplayCategory = value; }
      }

      /// <summary>
      /// Gets/Sets the exclusions display form's type column width value.
      /// </summary>
      static public int ExclusionsDisplayColumnTypeWidth
      {
         get { return MySettings.columnExclusionsDisplayType; }
         set { MySettings.columnExclusionsDisplayType = value; }
      }

      /// <summary>
      /// Gets/Sets the exclusions display form's value column width value.
      /// </summary>
      static public int ExclusionsDisplayColumnValueWidth
      {
         get { return MySettings.columnExclusionsDisplayValue; }
         set { MySettings.columnExclusionsDisplayValue = value; }
      }

      /// <summary>
      /// Gets/Sets the exclusions display form's option column width value.
      /// </summary>
      static public int ExclusionsDisplayColumnOptionWidth
      {
         get { return MySettings.columnExclusionsDisplayOption; }
         set { MySettings.columnExclusionsDisplayOption = value; }
      }

      /// <summary>
      /// Gets/Sets the exclusions display form's top value.
      /// </summary>
      static public int ExclusionsDisplayTop
      {
         get { return MySettings.exclusionsDisplayTop; }
         set { MySettings.exclusionsDisplayTop = value; }
      }

      /// <summary>
      /// Gets/Sets the exclusions display form's left value.
      /// </summary>
      static public int ExclusionsDisplayLeft
      {
         get { return MySettings.exclusionsDisplayLeft; }
         set { MySettings.exclusionsDisplayLeft = value; }
      }

      /// <summary>
      /// Gets/Sets the exclusions display form's width value.
      /// </summary>
      static public int ExclusionsDisplayWidth
      {
         get { return MySettings.exclusionsDisplayWidth; }
         set { MySettings.exclusionsDisplayWidth = value; }
      }

      /// <summary>
      /// Gets/Sets the exclusions display form's height value.
      /// </summary>
      static public int ExclusionsDisplayHeight
      {
         get { return MySettings.exclusionsDisplayHeight; }
         set { MySettings.exclusionsDisplayHeight = value; }
      }

      /// <summary>
      /// Gets/Sets whether to save the exclusions display form's window position.
      /// </summary>
      static public bool ExclusionsDisplaySavePosition
      {
         get { return MySettings.exclusionsDisplaySaveWindowPosition; }
         set { MySettings.exclusionsDisplaySaveWindowPosition = value; }
      }

      /// <summary>
      /// Gets/Sets the search starting paths.
      /// </summary>
      static public string SearchStarts
      {
         get { return MySettings.searchStartPaths; }
         set { MySettings.searchStartPaths = value; }
      }

      /// <summary>
      /// Gets/Sets the search file filters.
      /// </summary>
      static public string SearchFilters
      {
         get { return MySettings.searchFilters; }
         set { MySettings.searchFilters = value; }
      }

      /// <summary>
      /// Gets/Sets the search's search texts.
      /// </summary>
      static public string SearchTexts
      {
         get { return MySettings.searchTexts; }
         set { MySettings.searchTexts = value; }
      }

      /// <summary>
      /// Gets/Sets the text editors.
      /// </summary>
      static public string TextEditors
      {
         get { return MySettings.textEditors; }
         set { MySettings.textEditors = value; }
      }

      /// <summary>
      /// Gets/Sets results font.
      /// </summary>
      static public string ResultsFont
      {
         get { return MySettings.resultsFont; }
         set { MySettings.resultsFont = value; }
      }

      /// <summary>
      /// Gets default results font.
      /// </summary>
      static public string ResultsFontDefault
      {
         get { return MySettings.resultsFontDefault; }
      }

      /// <summary>
      /// Gets/sets whether to show the exclusion/error message.
      /// </summary>
      static public bool ShowExclusionErrorMessage
      {
         get { return MySettings.showExclusionErrorMessage; }
         set { MySettings.showExclusionErrorMessage = value; }
      }

      /// <summary>
      /// Gets/sets whether to save search options on exit.
      /// </summary>
      static public bool SaveSearchOptionsOnExit
      {
         get { return MySettings.saveSearchOptionsOnExit; }
         set { MySettings.saveSearchOptionsOnExit = value; }
      }

      /// <summary>
      /// Gets/sets whether to enable word wrap for the results area.
      /// </summary>
      static public bool ResultsWordWrap
      {
         get { return MySettings.resultsWordWrap; }
         set { MySettings.resultsWordWrap = value; }
      }

      /// <summary>
      /// Gets/Sets the file panel font.
      /// </summary>
      static public string FilePanelFont
      {
         get { return MySettings.filePanelFont; }
         set { MySettings.filePanelFont = value; }
      }

      /// <summary>
      /// Gets the default file panel font.
      /// </summary>
      static public string FilePanelFontDefault
      {
         get { return MySettings.filePanelFontDefault; }
      }

      /// <summary>
      /// Gets/sets whether to remove leading white space from lines in file output area.
      /// </summary>
      static public bool RemoveLeadingWhiteSpace
      {
         get { return MySettings.removeLeadingWhiteSpace; }
         set { MySettings.removeLeadingWhiteSpace = value; }
      }

      /// <summary>
      /// Show entire file contents.
      /// </summary>
      static public bool ShowEntireFile
      {
         get { return MySettings.showEntireFile; }
         set { MySettings.showEntireFile = value; }
      }

      /// <summary>
      /// Gets/sets whether to detect file encodings.
      /// </summary>
      static public bool DetectFileEncoding
      {
         get { return MySettings.detectFileEncoding; }
         set { MySettings.detectFileEncoding = value; }
      }

      /// <summary>
      /// Gets/Sets the user specified file encodings.
      /// </summary>
      static public string FileEncodings
      {
         get { return MySettings.fileEncodings; }
         set { MySettings.fileEncodings = value; }
      }

      /// <summary>
      /// Gets/Sets the user specified file encoding detection performance level.
      /// </summary>
      static public int EncodingPerformance
      {
         get { return MySettings.encodingPerformance; }
         set { MySettings.encodingPerformance = value; }
      }

      /// <summary>
      /// Gets/Sets to enable encoding cache.
      /// </summary>
      static public bool UseEncodingCache
      {
         get { return MySettings.useEncodingCache; }
         set { MySettings.useEncodingCache = value; }
      }

      /// <summary>
      /// Gets/Sets Windows DPI percent scale setting.
      /// </summary>
      static public int WindowsDPIPerCentSetting
      {
         get { return MySettings.windowsDPIPerCentSetting; }
         set { MySettings.windowsDPIPerCentSetting = value; }
      }

      /// <summary>
      /// Gets/sets whether to set the default fonts.
      /// </summary>
      static public bool SetDefaultFonts
      {
         get { return MySettings.setDefaultFonts; }
         set { MySettings.setDefaultFonts = value; }
      }

      /// <summary>
      /// Gets/sets whether to use the AstroGrep accent color.
      /// </summary>
      static public bool UseAstroGrepAccentColor
      {
         get { return MySettings.useAstroGrepAccentColor; }
         set { MySettings.useAstroGrepAccentColor = value; }
      }
   }
}
