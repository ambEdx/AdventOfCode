namespace Days.ViewModels
{
    public partial class Day3ViewModel : ViewModelBase
    {
        #region Constructor
        public Day3ViewModel()
        {
            Day = 3;
        }
        #endregion

        #region Overrides
        protected async override void HandleProcessCommand()
        {
            var lines = RawInput.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var rucksacks = new string[3];
            var trioCount = 0;
            var score = 0;
            foreach (var line in lines)
            {
                rucksacks[trioCount] = line;
                trioCount++;
                if (trioCount == 3)
                {
                    var matchingItem = rucksacks[0].FirstOrDefault(i => rucksacks[1].Contains(i) && rucksacks[2].Contains(i));
                    var priority = GetPriority(matchingItem);
                    score += priority;
                    trioCount = 0;
                }
            }

            Answer = score.ToString();

            await Shell.Current.GoToAsync("//Day3Pg2");
            base.HandleProcessCommand();
        }
        #endregion

        #region Private Methods
        private int GetPriority(char c)
        {
            int asciiValue = c;
            if (asciiValue >= 97 && asciiValue <= 122)
            {
                return asciiValue - 96;
            }
            if (asciiValue >= 65 && asciiValue <= 90)
            {
                return asciiValue - 38;
            }
            else
            {
                throw new ArgumentOutOfRangeException(c.ToString() + " is out of bounds.");
            }
        }
        #endregion

    }
}
