namespace Days.ViewModels
{
    public partial class Day2ViewModel : ViewModelBase
    {
        #region Constructor
        public Day2ViewModel()
        {
            Day = 2;
        }
        #endregion

        #region Overrides
        protected async override void HandleProcessCommand()
        {
            var opponentCodeShapeMap = new Dictionary<string, string>
            {
                { "A", "R" },
                { "B", "P" },
                { "C", "S" }
            };

            var playerCodeResultMap = new Dictionary<string, string>
            {
                { "X", "L" },
                { "Y", "D" },
                { "Z", "W" }
            };

            var shapePlayedScoreMap = new Dictionary<string, int>
            {
                {"R", 1 },
                {"P", 2 },
                {"S", 3 }
            };

            var resultScoreMap = new Dictionary<string, int>
            {
                { "W", 6 },
                { "D", 3 },
                { "L", 0 }
            };

            var winloseMap = new List<string[]>
            {
                { new string[] { "R", "R", "D" } },
                { new string[] { "R", "P", "W" } },
                { new string[] { "R", "S", "L" } },
                { new string[] { "P", "R", "L" } },
                { new string[] { "P", "P", "D" } },
                { new string[] { "P", "S", "W" } },
                { new string[] { "S", "R", "W" } },
                { new string[] { "S", "P", "L" } },
                { new string[] { "S", "S", "D" } }
            };

            var plays = RawInput.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var score = 0;
            foreach (var rpsPlay in plays)
            {
                var map = rpsPlay.Split(" ");
                var opponentShape = opponentCodeShapeMap[map[0].Trim()];
                var playerResult = playerCodeResultMap[map[1].Trim()];
                var playerShape = winloseMap.FirstOrDefault(map => map[0] == opponentShape && map[2] == playerResult)[1];
                var shapePlayedScore = shapePlayedScoreMap[playerShape];
                var resultScore = resultScoreMap[playerResult];
                var roundScore = shapePlayedScore + resultScore;
                score += roundScore;
            }

            Answer = score.ToString();

            await Shell.Current.GoToAsync("//Day2Pg2");
            base.HandleProcessCommand();
        }
        #endregion


    }
}
