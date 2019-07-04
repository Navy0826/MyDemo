using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyLibAstroGrep
{
    public class Grep
    {
        public IList<MatchResult> MatchResults { get; private set; }
        public ISearchSpec SearchSpec { get; private set; }

        public Grep(ISearchSpec searchSpec)
        {
            SearchSpec = searchSpec;
            MatchResults = new List<MatchResult>();
        }

        public void Execute()
        {
            if (SearchSpec.StartFilePaths != null && SearchSpec.StartFilePaths.Length > 0)
            {
                foreach (string path in SearchSpec.StartFilePaths)
                {
                    //SearchFile(new FileInfo(path));
                }
            }
            else
            {
                if (string.IsNullOrEmpty(SearchSpec.FileFilter))
                {
                    foreach (var dir in SearchSpec.StartDirectories)
                    {
                        Execute(new DirectoryInfo(dir), null, null);
                    }
                }
                else
                {
                    List<string> filters = SearchSpec.FileFilter.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries).Distinct(StringComparer.InvariantCultureIgnoreCase).ToList();

                    foreach (var filter in filters)
                    {
                        foreach (var dir in SearchSpec.StartDirectories)
                        {
                            Execute(new DirectoryInfo(dir), null, filter);
                        }
                    }
                }
            }
        }

        private void Execute(DirectoryInfo sourceDirectory, string sourceDirectoryFilter, string sourceFileFilter)
        {
            string filePattern = "*";
            if (sourceFileFilter != null)
                filePattern = sourceFileFilter.Trim();

            string dirPattern = "*";
            if (sourceDirectoryFilter != null)
                dirPattern = sourceDirectoryFilter.Trim();

            foreach (FileInfo SourceFile in sourceDirectory.EnumerateFiles(filePattern))
            {
                bool processFile = true;
                if (sourceFileFilter != null && !StriktMatch(SourceFile.Extension, sourceFileFilter.Trim()))
                {
                    processFile = false;

                    //string filterValue = SourceFile.Extension;
                    //FilterItem filterItem = new FilterItem(new FilterType(FilterType.Categories.File, FilterType.SubCategories.Extension), string.Empty, FilterType.ValueOptions.None, false, true);
                    //OnFileFiltered(SourceFile, filterItem, filterValue);
                }

                if (processFile)
                {
                    SearchFile(SourceFile);
                }
            }
        }

        private void SearchFile(FileInfo sourceFile)
        {
            try
            {
                //FilterItem filterItem = null;
                //string filterValue = string.Empty;
                //if (ShouldFilterOut(SourceFile, SearchSpec, out filterItem, out filterValue))
                //{
                //    OnFileFiltered(SourceFile, filterItem, filterValue);
                //}
                //else 
                if (string.IsNullOrEmpty(SearchSpec.SearchText))
                {
                    //// return a 'file hit' if the search text is empty
                    //var match = new MatchResult(sourceFile) { Index = MatchResults.Count };
                    //var matchLine = new MatchResultLine();
                    //match.Matches.Add(matchLine);
                    //MatchResults.Add(match);

                    //OnFileHit(sourceFile, match.Index);
                }
                else
                {
                    SearchFileContents(sourceFile);
                }
            }
            catch (ThreadAbortException)
            {
                //UnloadPlugins();
            }
            catch (Exception ex)
            {
                //OnSearchError(SourceFile, ex);
            }
        }

        private void SearchFileContents(FileInfo file)
        {
            int lineNumber = 0;
            MatchResult match = null;
            using (Stream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    do
                    {
                        lineNumber++;
                        string textLine = reader.ReadLine();
                        if (string.IsNullOrEmpty(textLine))
                        {
                            break;
                        }

                        if (textLine.Contains(SearchSpec.SearchText))
                        {
                            match = new MatchResult(file) { Index = MatchResults.Count };
                            MatchResults.Add(match);
                        }
                    }
                    while (true);
                }
            }
        }

        private bool StriktMatch(string fileExtension, string searchPattern)
        {
            bool isStriktMatch = false;

            int index = searchPattern.LastIndexOf('.');
            string extension = index > -1 ? searchPattern.Substring(index) : searchPattern;

            if (String.IsNullOrEmpty(extension))
            {
                isStriktMatch = true;
            }
            else if (extension.IndexOfAny(new char[] { '*', '?' }) != -1)
            {
                isStriktMatch = true;
            }
            else if (String.Compare(fileExtension, extension, true) == 0)
            {
                isStriktMatch = true;
            }
            else
            {
                isStriktMatch = false;
            }

            return isStriktMatch;
        }
    }
}
