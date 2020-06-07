using System.ComponentModel;

namespace MatchingGame.Models
{
    public class BonusGame : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Members

        private const string defaultPictureAText = "What is Picture A?";
        private const string defaultPictureBText = "What is Picture B?";
        private const string defaultHeadlineText = "What is the headline / topic of both pictures?";

        private bool isMainGridVisible;
        private bool isBonusGameVisible;
        private FieldPairs currentPair;

        private string headline;
        private string pictureAText;
        private string pictureBText;

        #endregion

        #region Properties
        public bool IsMainGridVisible
        {
            get
            {
                return isMainGridVisible;
            }
            set
            {
                isMainGridVisible = value;
                OnPropertyChanged("IsMainGridVisible");
            }
        }
        public bool IsBonusGameVisible
        {
            get
            {
                return isBonusGameVisible;
            }
            set
            {
                isBonusGameVisible = value;
                OnPropertyChanged("IsBonusGameVisible");
            }
        }
        public FieldPairs CurrentPair
        {
            get
            {
                return currentPair;
            }
            set
            {
                currentPair = value;
                OnPropertyChanged("CurrentPair");
            }
        }

        public string Headline
        {
            get
            {
                return headline;
            }
            set
            {
                headline = value;
                OnPropertyChanged("Headline");
            }
        }

        public string PictureAText
        {
            get
            {
                return pictureAText;
            }
            set
            {
                pictureAText = value;
                OnPropertyChanged("PictureAText");
            }
        }

        public string PictureBText
        {
            get
            {
                return pictureBText;
            }
            set
            {
                pictureBText = value;
                OnPropertyChanged("PictureBText");
            }
        }


        #endregion

        public BonusGame()
        {
            IsMainGridVisible = true;
            ResetBonusGame();
        }

        #region Methods

        public void ShowBonusGame()
        {
            IsMainGridVisible = false;
            IsBonusGameVisible = true;
        }

        public void CloseBonusGame()
        {
            IsMainGridVisible = true;
            IsBonusGameVisible = false;
            ResetBonusGame();
        }

        public void ResetBonusGame()
        {
            Headline = defaultHeadlineText;
            PictureAText = defaultPictureAText;
            PictureBText = defaultPictureBText;
        }

        public void ShowResult()
        {
            Headline = CurrentPair.Description;
            PictureAText = CurrentPair.FieldA.Description;
            PictureBText = CurrentPair.FieldB.Description;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
