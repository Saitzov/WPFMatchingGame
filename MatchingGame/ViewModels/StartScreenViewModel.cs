using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MatchingGame.ViewModels
{
    public class StartScreenViewModel
    {
        public string SelectedFolder { get; set; }
        public List<string> FolderList { get; set; }

        public string SelectedFieldSize { get; set; }
        public List<string> FieldSizeList { get; set; }

        public StartScreenViewModel()
        {
            FillFolderList();
            FillFieldSizeList();
        }


        private void FillFieldSizeList()
        {
            FieldSizeList = new List<string>();
            FieldSizeList.Add("4x4 (8 Pairs)");
        }
            
        private void FillFolderList()
        {
            FolderList = new List<string>();
            string contentDirectory = Environment.CurrentDirectory + "\\Content" ;

            var contentEntries = Directory.GetDirectories(contentDirectory)
                            .Select(p => new {
                                Path = p,
                                Name = Path.GetFileName(p)
                            }).ToArray();

            foreach(var entry in contentEntries)
            {
                var picCount = Directory.EnumerateFiles(entry.Path, "*.png", SearchOption.TopDirectoryOnly).Count();

                FolderList.Add($"{entry.Name} [{picCount / 2} Pairs]");

            }

        }
    }
}
