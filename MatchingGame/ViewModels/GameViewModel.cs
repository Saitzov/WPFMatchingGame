using MatchingGame.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using MatchingGame.Extension;
using static MatchingGame.Enums.MatchingGameEnums;

namespace MatchingGame.ViewModels
{
    public class GameViewModel
    {
        #region Properties & Members
        private const string fieldItemFile = "FieldItems.csv";
        private const string fieldPairFile = "FieldPairs.csv";
        private bool isFirstPick = true;
        private int FirstPick = -1;

        private string coverPath = "cover.png";
        private GameContent Content { get; set; }
        private List<FieldItem> AllFieldItems { get; set; }
        public List<FieldItem> UsedFieldItems { get; set; }
        private List<FieldPairs> FieldPairs { get; set; }
        private FieldSize FieldSize { get; set; }

        #endregion

        

        public GameViewModel(GameContent content, FieldSize fieldSize)
        {
            this.Content = content;
            this.FieldSize = fieldSize;
            InitGame();
            TurnOverAllFields(false);
        }

        public void ClickField(int pickId)
        {
            TurnOverField(pickId, true);
            
            if (isFirstPick)            
            {
                FirstPick = pickId;                
                isFirstPick = false;
                return;
            }
            else
            {
                CheckPics(FirstPick, pickId);
                isFirstPick = true;
            }
        }


        private void CheckPics(int idA, int idB)
        {
            

            if(IsPickCorrect(idA, idB, out FieldPairs pair))
            {

            }
            else
            {
                TurnOverField(idA, false);
                TurnOverField(idB, false);
            }
        }

        private bool IsPickCorrect(int idA, int idB, out FieldPairs pairs)
        {
            pairs = null;
            bool res = false;

            var pair = FieldPairs.First(p => p.FieldA.Id == idA || p.FieldB.Id == idA);



            return res;
        }

        private void TurnOverField(int itemId, bool showSolution)
        {
            var field = UsedFieldItems.First(i => i.Id == itemId);

            if(showSolution)
            {
                field.CurrentDisplayPath = field.PicturePath;
            }
        }

        private void TurnOverAllFields(bool ShowSolution)
        {
            foreach(var item in UsedFieldItems)
            {
                item.CurrentDisplayPath = ShowSolution ? item.PicturePath : this.Content.FolderPath + "\\Cover.png";
            }
        }

        private void InitGame()
        {
            try
            {
                this.AllFieldItems = this.LoadFieldItems();
                this.FieldPairs = this.LoadFieldPairs();

                this.UsedFieldItems = new List<FieldItem>();
                foreach (var pair in this.FieldPairs)
                {
                    this.UsedFieldItems.Add(pair.FieldA);
                    this.UsedFieldItems.Add(pair.FieldB);
                }
                UsedFieldItems.Shuffle();

                

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        private List<FieldPairs> LoadFieldPairs()
        {
            List<FieldPairs> res = new List<FieldPairs>();

            var path = this.Content.FolderPath + "\\" + fieldPairFile;
            using (var reader = new StreamReader(path))
            {
                var headLine = reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    FieldPairs pair = new FieldPairs();

                    pair.FieldA = this.AllFieldItems.First(i => i.Id == Convert.ToInt32(values[0]));
                    pair.FieldB = this.AllFieldItems.First(i => i.Id == Convert.ToInt32(values[1]));
                    pair.Description = values[2];

                    res.Add(pair);
                }

                res.Shuffle();

                int takeAmount = (int)this.FieldSize/ 2;
                res = res.Take(takeAmount).ToList();

                return res;
            }
        }

        private List<FieldItem> LoadFieldItems()
        {            
            List<FieldItem> res = new List<FieldItem>();

            var path = this.Content.FolderPath + "\\" + fieldItemFile;

            using (var reader = new StreamReader(path))
            {
                var headLine = reader.ReadLine();

                while (!reader.EndOfStream)
                {                    
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    FieldItem item = new FieldItem();
                    item.Id = Convert.ToInt32(values[0]);
                    item.FileName = values[1];
                    item.Description = values[2];
                    item.PicturePath = this.Content.AssetsPath + "\\" + item.FileName+ ".png";

                    res.Add(item);
                }
            }

            return res;
        }

        
    }
}
