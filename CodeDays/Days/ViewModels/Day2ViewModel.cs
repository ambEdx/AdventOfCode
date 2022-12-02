using CommunityToolkit.Mvvm.ComponentModel;
using Days.Models;
using System.ComponentModel;

namespace Days.ViewModels
{
    [ObservableObject]
    public partial class Day2ViewModel
    {
        private Dictionary<string, string> _opponentCodeShapeMap;
        private Dictionary<string, int> _playerCodeResultMap;
        private List<RockPaperScissorsWinLose> _winLoseMap;
        private Dictionary<string, int> _shapePlayedScoreMap;
        private Dictionary<int, int> __resultScoreMap;
        public Day2ViewModel()
        {
            SaveCommand = new Command(HandleSaveCommand);
            ClearCommand = new Command(HandleClearCommand);
            BuildWinLoseMap();
            BuildCodeShapeMaps();
            BuildShapePlayedScoreMap();
            BuildResultScoreMap();
            PropertyChanged += Day2ViewModel_PropertyChanged;
            Task.Run(async () => await Initialise());
            StatusMessage = "Ready";
        }

        private void Day2ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Plays))
            {
                TotalScore = 0;
                if (Plays == null) return;

                StatusMessage = "Calculating scores ...";
                Task.Run(() =>
                {
                    var aggregate = 0;
                    foreach (var play in Plays)
                    {
                        play.RoundScore = CalculateRoundScore(play);
                        aggregate += play.RoundScore;
                    }
                    TotalScore = aggregate;
                });
                StatusMessage = "Scores ready";
            }
        }

        private async Task Initialise()
        {
            var playList = await App.LocalDataService.GetAllAsync<RockPaperScissorsPlay>();
            Plays = playList.OrderBy(e => e.Seq).ToList();
            if (Plays.Count == 0)
            {
                return;
            }
            var result = string.Empty;
            foreach (var play in Plays)
            {
                result += play.OpponentCode + " " + play.PlayerCode + "\r\n";

            }
            result = result.TrimEnd();
            RawInput = result;
        }

        private void BuildCodeShapeMaps()
        {
            _opponentCodeShapeMap = new Dictionary<string, string>
            {
                { "A", "Rock" },
                { "B", "Paper" },
                { "C", "Scissors" }
            };
            _playerCodeResultMap = new Dictionary<string, int>
            {
                { "X", -1 },
                { "Y", 0 },
                { "Z", 1 }
            };
        }

        private void BuildWinLoseMap()
        {
            _winLoseMap = new List<RockPaperScissorsWinLose>
            {
                new RockPaperScissorsWinLose { Opponent = "Rock", Player = "Rock", Result = 0 },
                new RockPaperScissorsWinLose { Opponent = "Rock", Player = "Paper", Result = 1 },
                new RockPaperScissorsWinLose { Opponent = "Rock", Player = "Scissors", Result = -1 },
                new RockPaperScissorsWinLose { Opponent = "Paper", Player = "Rock", Result = -1 },
                new RockPaperScissorsWinLose { Opponent = "Paper", Player = "Paper", Result = 0 },
                new RockPaperScissorsWinLose { Opponent = "Paper", Player = "Scissors", Result = 1 },
                new RockPaperScissorsWinLose { Opponent = "Scissors", Player = "Rock", Result = 1 },
                new RockPaperScissorsWinLose { Opponent = "Scissors", Player = "Paper", Result = -1 },
                new RockPaperScissorsWinLose { Opponent = "Scissors", Player = "Scissors", Result = 0 },
            };
        }

        private void BuildShapePlayedScoreMap()
        {
            _shapePlayedScoreMap = new Dictionary<string, int>
            {
                {"Rock", 1 },
                {"Paper", 2 },
                {"Scissors", 3 }
            };
        }

        private void BuildResultScoreMap()
        {
            __resultScoreMap = new Dictionary<int, int>
            {
                { 1, 6 },
                { 0, 3 },
                { -1, 0 }
            };
        }

        [ObservableProperty]
        private string statusMessage;

        [ObservableProperty]
        private Command saveCommand;

        [ObservableProperty]
        private Command clearCommand;

        [ObservableProperty]
        private string rawInput;

        [ObservableProperty]
        private List<RockPaperScissorsPlay> plays;

        [ObservableProperty]
        private int totalScore;

        private void HandleSaveCommand(object obj)
        {
            Task.Run(async () => await PopulateGameStrategyAsync());
        }

        private void HandleClearCommand(object obj)
        {
            Task.Run(async () => await ClearInputAsync());
        }
        private int CalculateRoundScore(RockPaperScissorsPlay play)
        {
            var opponentShape = _opponentCodeShapeMap[play.OpponentCode];
            var playerResult = _playerCodeResultMap[play.PlayerCode];
            var playerShape = _winLoseMap.FirstOrDefault(e => e.Opponent == opponentShape && e.Result == playerResult).Player;
            var shapePlayedScore = _shapePlayedScoreMap[playerShape];
            var resultScore = __resultScoreMap[playerResult];
            return shapePlayedScore + resultScore;
        }
        private async Task ClearInputAsync()
        {
            await App.LocalDataService.ClearAllAsync<RockPaperScissorsPlay>();
            MainThread.BeginInvokeOnMainThread(() =>
            {
                RawInput = string.Empty;
                Plays = null;
                StatusMessage = "Cleared";
            });
        }

        private async Task PopulateGameStrategyAsync()
        {
            MainThread.BeginInvokeOnMainThread(() => StatusMessage = "Processing ...");
            await App.LocalDataService.ClearAllAsync<RockPaperScissorsPlay>();
            var rpsPlays = RawInput.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var playList = new List<RockPaperScissorsPlay>();
            var round = 1;
            foreach (var rpsPlay in rpsPlays)
            {

                var map = rpsPlay.Split(" ", StringSplitOptions.None);
                var play = new RockPaperScissorsPlay()
                {
                    OpponentCode = map[0].Trim(),
                    PlayerCode = map[1].Trim(),
                    Seq = round++
                };
                playList.Add(play);
                await App.LocalDataService.InsertAsync(play);
            }
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Plays = playList;
                StatusMessage = "Saved";
            });
        }

    }
}
