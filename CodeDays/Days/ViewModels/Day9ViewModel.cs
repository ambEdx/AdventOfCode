using Point = System.Drawing.Point;

namespace Days.ViewModels
{
    public partial class Day9ViewModel : ViewModelBase
    {
        #region Constructor
        public Day9ViewModel()
        {
            Day = 9;
        }
        #endregion

        #region Overrides
        protected async override void HandleProcessCommand()
        {
            var lines = RawInput.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var knotCount = 9;
            var targetKnot = 9;
            var knotPos = new int[knotCount + 1, 2];
            var visitedPositions = new List<string>(new string[] { "0,0" });

            foreach (var line in lines)
            {
                var move = line.Split(" ");
                for (int i = 0; i < int.Parse(move[1]); i++)
                {
                    switch (move[0])
                    {
                        case "R": knotPos[0, 0]++; break;
                        case "L": knotPos[0, 0]--; break;
                        case "U": knotPos[0, 1]--; break;
                        case "D": knotPos[0, 1]++; break;
                    }

                    for (var knot = 0; knot < knotCount; knot++)
                    {
                        for (int d = 0; d <= 1; d++)
                        {
                            var notd = 1 - d;
                            if (Math.Abs(knotPos[knot, d] - knotPos[knot + 1, d]) == 2)
                            {
                                if (knotPos[knot, notd] != knotPos[knot + 1, notd])
                                {
                                    knotPos[knot + 1, notd] += knotPos[knot, notd] > knotPos[knot + 1, notd] ? 1 : -1;
                                }
                                knotPos[knot + 1, d] += knotPos[knot, d] > knotPos[knot + 1, d] ? 1 : -1;
                                var key = $"{knotPos[knot + 1, 0]},{knotPos[knot + 1, 1]}";
                                if (knot + 1 == targetKnot && !visitedPositions.Contains(key))
                                {
                                    visitedPositions.Add(key);
                                }
                                break;
                            }
                        }
                    }

                }
            }
            var result = visitedPositions.Count();
            Answer = result.ToString();

            await Shell.Current.GoToAsync("//Day9Pg2");
            base.HandleProcessCommand();
        }
        #endregion

        #region Private Helpers
        #endregion
    }
}
