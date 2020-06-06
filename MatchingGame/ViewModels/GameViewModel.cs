using MatchingGame.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using MatchingGame.Extension;
using static MatchingGame.Enums.MatchingGameEnums;
using System.Windows.Controls;
using MatchingGame.Interface;
using MatchingGame.UserControlls;
using System.Windows.Controls.Primitives;
using System.Threading.Tasks;

namespace MatchingGame.ViewModels
{
    public class GameViewModel
    {
        #region Properties & Members
        private const string fieldItemFile = "FieldItems.csv";
        private const string fieldPairFile = "FieldPairs.csv";
        private bool isFirstPick = true;
        private int FirstPick = -1;
        private int SecondPick = -1;
        private GameContent Content { get; set; }
        private List<FieldItem> AllFieldItems { get; set; }
        public List<FieldItem> UsedFieldItems { get; set; }
        private List<FieldPairs> FieldPairs { get; set; }
        private GameConfig GameConfig { get; set; }
        FieldPairs LastPair;

        private GameField28 View;

        #endregion



        public GameViewModel(GameContent content, GameConfig config, GameField28 view)
        {
            this.Content = content;
            this.GameConfig = config;
            this.View = view;

            InitGame();
            
            TurnOverAllFields(false);

        }

        public void ClickField(int pickId)
        {
            
            TurnOverField(pickId, true);
            
            
            if (isFirstPick)            
            {
                //TurnOverField(pickId, true);
                FirstPick = pickId;                
                isFirstPick = false;
                return;
            }
            else
            {
                //TurnOverField(pickId, true);
                SecondPick = pickId;
                CheckPics(FirstPick, pickId);
                isFirstPick = true;
            }
        }

        private void CheckPics(int idA, int idB)
        {            

            if(IsPickCorrect(idA, idB))
            {
                if(this.GameConfig.WithBonusGame)
                {
                    ShowBonusGame();
                }                

            }
            else
            {
                Task.Delay(2000).ContinueWith(_ =>
                {
                    TurnOverField(idA, false);
                    TurnOverField(idB, false);
                });
                
            }
        }

        private void ShowBonusGame()
        {
            this.View.MainGrid.Visibility = Visibility.Hidden;
            this.View.BonusGrid.Visibility = Visibility.Visible;
            this.View.BonusGrid.DataContext = LastPair;
        }

        public void CloseBonusGame()       
        {            
            this.View.BonusGrid.Visibility = Visibility.Hidden;
            this.View.MainGrid.Visibility = Visibility.Visible;
            View.PairHeadline.Text = "What is the headline / topic of both pictures?";
            View.PicA.Text = "What is Picture A?";
            View.PicB.Text = "What is Picutre B?";
        }

        public void ShowBonusResult()
        {
            View.PairHeadline.Text = LastPair.Description;
            View.PicA.Text = LastPair.FieldA.Description;
            View.PicB.Text = LastPair.FieldB.Description;
        }

        private bool IsPickCorrect(int idA, int idB)
        {
            LastPair = null;
            bool res = false;

            var pair = FieldPairs.First(p => p.FieldA.Id == idA || p.FieldB.Id == idA);

            bool a = pair.FieldA.Id == idA || pair.FieldA.Id == idB;
            bool b = pair.FieldB.Id == idA || pair.FieldB.Id == idB;

            res = a == true && b == true;

            if (res)
            {
                LastPair = pair;
            }

            return res;
        }

        private void TurnOverField(int itemId, bool showSolution)
        {
            if (itemId == -1)
                return;

            var field = UsedFieldItems.First(i => i.Id == itemId);

            if(showSolution)
            {
                field.CurrentDisplayPath = field.PicturePath;
            }
            else
            {
                field.CurrentDisplayPath = this.Content.CoverPath;
            }
        }

        public void TurnOverAllFields(bool ShowSolution)
        {
            foreach(var item in UsedFieldItems)
            {
                item.CurrentDisplayPath = ShowSolution ? item.PicturePath : this.Content.CoverPath;
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

                int takeAmount = (int)this.GameConfig.FieldSize/ 2;
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
