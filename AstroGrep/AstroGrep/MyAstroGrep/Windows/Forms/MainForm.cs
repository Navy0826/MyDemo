using MyAstroGrep.Core;
using MyLibAstroGrep;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyAstroGrep.Windows.Forms
{
    public partial class MainForm : BaseForm
    {
        Grep grep = null;
        private delegate void AddToListCallBack(FileInfo file, int index);

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CboFilePath.Text = @"D:\MyWork\MyDemo\Test";
            CboFileFilter.Text = "*.*";
            CboSearchText.Text = "666666";

            LoadSettings();
        }

        private void LoadSettings()
        {
            SetColumnsText();

            LoadWindowSettings();
        }

        private void LoadWindowSettings()
        {
            if (GeneralSettings.WindowFileColumnNameWidth != -1)
                LstFileNames.Columns[Constants.COLUMN_INDEX_FILE].Width = GeneralSettings.WindowFileColumnNameWidth;
            else
                LstFileNames.Columns[Constants.COLUMN_INDEX_FILE].Width = Constants.COLUMN_WIDTH_FILE * GeneralSettings.WindowsDPIPerCentSetting / 100;

            if (GeneralSettings.WindowFileColumnLocationWidth != -1)
                LstFileNames.Columns[Constants.COLUMN_INDEX_DIRECTORY].Width = GeneralSettings.WindowFileColumnLocationWidth;
            else
                LstFileNames.Columns[Constants.COLUMN_INDEX_DIRECTORY].Width = Constants.COLUMN_WIDTH_DIRECTORY * GeneralSettings.WindowsDPIPerCentSetting / 100;

            if (GeneralSettings.WindowFileColumnFileExtWidth != -1)
                LstFileNames.Columns[Constants.COLUMN_INDEX_FILE_EXT].Width = GeneralSettings.WindowFileColumnFileExtWidth;
            else
                LstFileNames.Columns[Constants.COLUMN_INDEX_FILE_EXT].Width = Constants.COLUMN_WIDTH_FILE_EXT * GeneralSettings.WindowsDPIPerCentSetting / 100;

            if (GeneralSettings.WindowFileColumnDateWidth != -1)
                LstFileNames.Columns[Constants.COLUMN_INDEX_DATE].Width = GeneralSettings.WindowFileColumnDateWidth;
            else
                LstFileNames.Columns[Constants.COLUMN_INDEX_DATE].Width = Constants.COLUMN_WIDTH_DATE * GeneralSettings.WindowsDPIPerCentSetting / 100;

            if (GeneralSettings.WindowFileColumnSizeWidth != -1)
                LstFileNames.Columns[Constants.COLUMN_INDEX_SIZE].Width = GeneralSettings.WindowFileColumnSizeWidth;
            else
                LstFileNames.Columns[Constants.COLUMN_INDEX_SIZE].Width = Constants.COLUMN_WIDTH_SIZE * GeneralSettings.WindowsDPIPerCentSetting / 100;

            if (GeneralSettings.WindowFileColumnCountWidth != -1)
                LstFileNames.Columns[Constants.COLUMN_INDEX_COUNT].Width = GeneralSettings.WindowFileColumnCountWidth;
            else
                LstFileNames.Columns[Constants.COLUMN_INDEX_COUNT].Width = Constants.COLUMN_WIDTH_COUNT * GeneralSettings.WindowsDPIPerCentSetting / 100;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            var searchSpec = GetSearchSpecFromUI();

            grep = new Grep(searchSpec);
            grep.FileHit += ReceiveFileHit;

            grep.Execute();
        }

        private void ReceiveFileHit(FileInfo file, int index)
        {
            AddHitToList(file, index);
        }

        private void AddHitToList(FileInfo file, int index)
        {
            if (LstFileNames.InvokeRequired)
            {
                AddToListCallBack _delegate = AddHitToList;
                LstFileNames.Invoke(_delegate, new object[2] { file, index });
                return;
            }

            foreach (ListViewItem item in LstFileNames.Items)
            {
                MatchResult hit = grep.RetrieveMatchResult(int.Parse(item.SubItems[Constants.COLUMN_INDEX_GREP_INDEX].Text));
                if (hit.File.FullName.Equals(file.FullName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return;
                }
            }

            var listItem = new ListViewItem(file.Name);
            listItem.Name = index.ToString();
            listItem.ImageIndex = ListViewImageManager.GetImageIndex(file, ListViewImageList);
            listItem.SubItems.Add(file.DirectoryName);
            listItem.SubItems.Add(file.Extension);
            listItem.SubItems.Add(file.LastWriteTime.ToString());

            ListViewItem.ListViewSubItem subItem = new ListViewItem.ListViewSubItem(listItem, API.StrFormatByteSize(file.Length));
            subItem.Tag = file.Length;
            listItem.SubItems.Add(subItem);

            listItem.SubItems.Add("0");

            listItem.SubItems.Add(index.ToString());

            LstFileNames.Items.Add(listItem);

            listItem = null;
        }

        private ISearchSpec GetSearchSpecFromUI()
        {
            string path = CboFilePath.Text.Trim();
            List<string> filePaths = new List<string>();

            var spec = new SearchInterfaces.SearchSpec
            {
                SearchText = CboSearchText.Text
            };

            string fileFiler = CboFileFilter.Text;
            int slashPos = fileFiler.LastIndexOf(Path.DirectorySeparatorChar.ToString());
            if (slashPos > -1)
            {
                fileFiler = fileFiler.Substring(slashPos + 1);
            }
            spec.FileFilter = fileFiler;

            if (filePaths != null && filePaths.Count > 0)
            {
            }
            else
            {
                string[] paths = path.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                slashPos = spec.FileFilter.LastIndexOf(Path.DirectorySeparatorChar.ToString());
                if (slashPos > -1)
                {
                    for (int i = 0; i < paths.Length; i++)
                    {
                        paths[i] += spec.FileFilter.Substring(0, slashPos);
                    }
                }
                spec.StartDirectories = paths;
                spec.StartFilePaths = null;
            }

            return spec;
        }

        private void SetColumnsText()
        {
            if (LstFileNames.Columns.Count == 0)
            {
                //LstFileNames.Columns.Add(Language.GetGenericText("ResultsColumnFile"), GeneralSettings.WindowFileColumnNameWidth, HorizontalAlignment.Left);
                //LstFileNames.Columns.Add(Language.GetGenericText("ResultsColumnLocation"), GeneralSettings.WindowFileColumnLocationWidth, HorizontalAlignment.Left);
                //LstFileNames.Columns.Add(Language.GetGenericText("ResultsColumnFileExt"), GeneralSettings.WindowFileColumnFileExtWidth, HorizontalAlignment.Left);
                //LstFileNames.Columns.Add(Language.GetGenericText("ResultsColumnDate"), GeneralSettings.WindowFileColumnDateWidth, HorizontalAlignment.Left);
                //LstFileNames.Columns.Add(Language.GetGenericText("ResultsColumnSize"), GeneralSettings.WindowFileColumnSizeWidth, HorizontalAlignment.Left);
                //LstFileNames.Columns.Add(Language.GetGenericText("ResultsColumnCount"), GeneralSettings.WindowFileColumnCountWidth, HorizontalAlignment.Left);
                LstFileNames.Columns.Add("名称", GeneralSettings.WindowFileColumnNameWidth, HorizontalAlignment.Left);
                LstFileNames.Columns.Add("路径", GeneralSettings.WindowFileColumnLocationWidth, HorizontalAlignment.Left);
                LstFileNames.Columns.Add("类型", GeneralSettings.WindowFileColumnFileExtWidth, HorizontalAlignment.Left);
                LstFileNames.Columns.Add("修改日期", GeneralSettings.WindowFileColumnDateWidth, HorizontalAlignment.Left);
                LstFileNames.Columns.Add("大小", GeneralSettings.WindowFileColumnSizeWidth, HorizontalAlignment.Left);
                LstFileNames.Columns.Add("统计", GeneralSettings.WindowFileColumnCountWidth, HorizontalAlignment.Left);
            }
            else
            {
                LstFileNames.Columns[Constants.COLUMN_INDEX_FILE].Text = "名称";
                LstFileNames.Columns[Constants.COLUMN_INDEX_DIRECTORY].Text = "路径";
                LstFileNames.Columns[Constants.COLUMN_INDEX_FILE_EXT].Text = "类型";
                LstFileNames.Columns[Constants.COLUMN_INDEX_SIZE].Text = "修改日期";
                LstFileNames.Columns[Constants.COLUMN_INDEX_DATE].Text = "大小";
                LstFileNames.Columns[Constants.COLUMN_INDEX_COUNT].Text = "统计";
            }

            AddContextMenuForResults();
        }

        private void AddContextMenuForResults()
        {
            // todo
        }
    }
}
