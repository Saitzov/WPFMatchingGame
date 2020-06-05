using MatchingGame.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MatchingGame.ViewModels
{
    public class StartScreenViewModel
    {
        private string contentDirectory = Environment.CurrentDirectory + "\\Content";
        public GameContent SelectedFolder { get; set; }
        public List<GameContent> FolderList { get; set; }

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
            FolderList = new List<GameContent>();            

            var contentEntries = Directory.GetDirectories(contentDirectory)
                            .Select(p => new {
                                Path = p,
                                Name = Path.GetFileName(p)
                            }).ToArray();

            foreach(var entry in contentEntries)
            {
                GameContent game = new GameContent();
                game.FolderName = entry.Name;
                game.FolderPath = entry.Path;
                game.AssetsPath = $"{entry.Path}\\Assets";
                game.AmountOfPictures = this.CountPictureInFolder(game.AssetsPath);

                FolderList.Add(game);
            }

        }

        private int CountPictureInFolder(string path)
        {
            return Directory.Exists(path)
                ? Directory.EnumerateFiles(path, "*.png", SearchOption.TopDirectoryOnly).Count()
                : 0;
        }
    }
}
