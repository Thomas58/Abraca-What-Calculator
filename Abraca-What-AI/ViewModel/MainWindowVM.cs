using Abraca_What_AI.Model;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Media;

namespace Abraca_What_AI.ViewModel
{
    class MainWindowVM : ViewModel
    {
        public AI AI = new AI();

        public string HandSizeText => AI.HandSize.ToString();
        private string[] knownText = { "0", "0", "0", "0", "0", "0", "0", "0" };
        public string[] KnownText => knownText;
        public string UnknownText => AI.Unknown.CountAll().ToString();
        private string[] probabilityText = { "0", "0", "0", "0", "0", "0", "0", "0" };
        public string[] ProbabilityText => probabilityText;
        private Brush[] probabilityColor = 
            {
                new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)),
                new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)),
                new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)),
                new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)),
                new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)),
                new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)),
                new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)),
                new SolidColorBrush(Color.FromArgb(255, 0, 0, 0))
            };
        public Brush[] ProbabilityColor => probabilityColor;
        public bool[] NotInHandRef => AI.NotInHand;

        public RelayCommand IncHandSizeCommand => new RelayCommand(()=> { if (AI.HandSize < 5) EditHandSize(1); });
        public RelayCommand DecHandSizeCommand => new RelayCommand(() => { if (1 < AI.HandSize) EditHandSize(-1); });
        public RelayCommand NewRoundCommand => new RelayCommand(NewRound);
        public RelayCommand<Tiles> AddKnownCommand => new RelayCommand<Tiles>(AddKnown);
        public RelayCommand<Tiles> RemoveKnownCommand => new RelayCommand<Tiles>(RemoveKnown);
        public RelayCommand<Tiles> NotInHandCommand => new RelayCommand<Tiles>(NotInHand);
        public RelayCommand<Tiles> InHandCommand => new RelayCommand<Tiles>(InHand);

        public MainWindowVM() => NewRound();

        private void CalcProb()
        {
            double[] ProbTemp = new double[8];
            for (int i = 0; i < 8; i++)
            {
                ProbTemp[i] = AI.CalculateProbability(TilesMethods.GetTileByNumber(i + 1)) * 100.0;
            }
            for (int i = 0; i < 8; i++)
            {
                probabilityColor[i] = new SolidColorBrush(Color.FromArgb((byte)(155 + ProbTemp[i]), 0, 0, 0));
                probabilityText[i] = ProbTemp[i].ToString("0.00") + "%";
            }
            OnPropertyChanged("ProbabilityText");
            OnPropertyChanged("ProbabilityColor");
        }

        private void NewRound()
        {
            AI.Clear();
            knownText = new string[] { "0", "0", "0", "0", "0", "0", "0", "0" };
            OnPropertyChanged("HandSizeText");
            OnPropertyChanged("KnownText");
            OnPropertyChanged("UnknownText");
            CalcProb();
        }

        private void EditHandSize(int num)
        {
            AI.HandSize += num;
            OnPropertyChanged("HandSizeText");
            CalcProb();
        }

        private void NotInHand(Tiles tile)
        {
            AI.NotInHand[tile.GetNumber() - 1] = true;
            CalcProb();
            OnPropertyChanged("NotInHandRef");
        }

        private void InHand(Tiles tile)
        {
            AI.NotInHand[tile.GetNumber() - 1] = false;
            CalcProb();
            OnPropertyChanged("NotInHandRef");
        }

        private void RemoveKnown(Tiles tile)
        {
            Tiles newTile = AI.Known.Remove(tile);
            AI.Unknown.Add(newTile);
            knownText[tile.GetNumber() - 1] = AI.Known.Count(tile).ToString();
            OnPropertyChanged("KnownText");
            OnPropertyChanged("UnknownText");
            CalcProb();
        }

        private void AddKnown(Tiles tile)
        {
            Tiles newTile = AI.Unknown.Remove(tile);
            AI.Known.Add(newTile);
            knownText[tile.GetNumber() - 1] = AI.Known.Count(tile).ToString();
            OnPropertyChanged("KnownText");
            OnPropertyChanged("UnknownText");
            CalcProb();
        }
    }
}
