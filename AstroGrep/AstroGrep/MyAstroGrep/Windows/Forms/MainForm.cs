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
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CboFilePath.Text = @"D:\MyWork\MyDemo\Test";
            CboFileFilter.Text = "*.*";
            CboSearchText.Text = "666666";
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            var searchSpec = GetSearchSpecFromUI();

            Grep grep = new Grep(searchSpec);

            grep.Execute();
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
    }
}
