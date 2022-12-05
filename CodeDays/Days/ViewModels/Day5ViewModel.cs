namespace Days.ViewModels
{
    public partial class Day5ViewModel : ViewModelBase
    {
        #region Constructor
        public Day5ViewModel()
        {
            Day = 5;
        }
        #endregion

        #region Overrides
        protected async override void HandleProcessCommand()
        {
            var lines = RawInput.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            var stackRowTop = 0;
            var stackRowBottom = 0;
            var moveRowTop = 0;
            var moveRowBottom = lines.Length - 1;
            var stackCount = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                if (line.Contains('1'))
                {
                    stackRowBottom = i - 1;
                    moveRowTop = i + 2;
                    stackCount = int.Parse(line.Substring(line.Length - 2, 1));
                    break;
                }
            }

            var stacks = new Dictionary<int, Stack<string>>();
            for (int i = 1; i <= stackCount; i++)
            {
                stacks.Add(i, new Stack<string>());
            }

            for (int i = stackRowBottom; i >= stackRowTop; i--)
            {
                for (int j = 1; j <= stackCount; j++)
                {
                    var stockNameIndex = ((j - 1) * 4) + 1;
                    var stockName = lines[i].Substring(stockNameIndex, 1);

                    if (!string.IsNullOrWhiteSpace(stockName))
                    {
                        stacks[j].Push(stockName);
                    }
                }
            }

            for (int i = moveRowTop; i <= moveRowBottom; i++)
            {
                var csv = lines[i].Replace("move ", "").Replace(" from ", ",").Replace(" to ", ",");
                var cmds = Array.ConvertAll(csv.Split(","), int.Parse);
                var moveItemsCount = cmds[0];
                var fromStack = cmds[1];
                var toStack = cmds[2];

                var crane = new Stack<string>();
                for (int j = 1; j <= moveItemsCount; j++)
                {
                    var movingItem = stacks[fromStack].Pop();
                    crane.Push(movingItem);
                }

                while(crane.Count > 0)
                {
                    var movingItem = crane.Pop();
                    stacks[toStack].Push(movingItem);
                }
            }

            var result = "";
            foreach (var stack in stacks.Values)
            {
                result += stack.Pop();
            }

            Answer = result;

            await Shell.Current.GoToAsync("//Day5Pg2");
            base.HandleProcessCommand();
        }
        #endregion
    }
}
