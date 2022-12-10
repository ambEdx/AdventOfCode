using Point = System.Drawing.Point;

namespace Days.ViewModels
{
    public partial class Day10ViewModel : ViewModelBase
    {
        #region Constructor
        public Day10ViewModel()
        {
            Day = 10;
        }
        #endregion

        #region Overrides
        protected async override void HandleProcessCommand()
        {
            var commandCycleMap = new Dictionary<string, int>
            {
                { "noop", 1 },
                { "addx", 2 },
            };

            var x = 1;
            var pixel = new Point();
            var screen = string.Empty;

            var lines = RawInput.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                var code = line.Split(" ");
                var commandCycle = commandCycleMap[code[0]];
                for (var c = 0; c < commandCycle; c++)
                {
                    var isSpriteOnPixel = pixel.X >= x - 1 && pixel.X <= x + 1;
                    screen += isSpriteOnPixel ? "#" : ".";
                    pixel.X++;
                    if (pixel.X == 40)
                    {
                        pixel.X = 0;
                        pixel.Y++;
                        screen += "\r\n";
                    }
                }

                if (code[0] == "addx")
                {
                    x += int.Parse(code[1]);
                }
            }

            Answer = screen;

            await Shell.Current.GoToAsync("//Day10Pg2");
            base.HandleProcessCommand();
        }
        #endregion

        #region Private Helpers
        #endregion
    }
}
