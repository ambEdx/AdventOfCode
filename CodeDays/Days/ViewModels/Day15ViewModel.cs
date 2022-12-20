using Point = System.Drawing.Point;

namespace Days.ViewModels
{
    public partial class Day15ViewModel : ViewModelBase
    {
        private Dictionary<int, Impossible> _impossibles = new();
        private List<Point> _beacons = new();
        private Point _min = new(int.MaxValue, int.MaxValue);
        private Point _max = new(int.MinValue, int.MinValue);

        #region Constructor
        public Day15ViewModel()
        {
            Day = 15;
        }
        #endregion

        #region Overrides
        protected async override void HandleProcessCommand()
        {
            var lines = RawInput
                .Replace("Sensor at", "")
                .Replace(" closest beacon is at", "")
                .Replace(" x=", "")
                .Replace(" y=", "")
                .Split(ALL_CR, StringSplitOptions.None);

            var sbPairs = new List<Point[]>();
            for (var i = 0; i < lines.Length; i++)
            {
                var pair = lines[i].Split(":");
                var s = pair[0].Split(",").Select(v => int.Parse(v)).ToArray();
                var b = pair[1].Split(",").Select(v => int.Parse(v)).ToArray();
                var sbp = new Point[2] { new Point(s[0], s[1]), new Point(b[0], b[1]) };
                sbPairs.Add(sbp);

                if (!_beacons.Contains(sbp[1]))
                {
                    _beacons.Add(sbp[1]);
                }

                var radius = ManhattenDistance(sbp[0], sbp[1]);
                for (var p = 0; p <= 1; p++)
                {
                    var r = p == 0 ? radius : 0;
                    if (sbp[p].X - r < _min.X) _min.X = sbp[p].X - r;
                    if (sbp[p].X + r > _max.X) _max.X = sbp[p].X + r;
                    if (sbp[p].Y - r < _min.Y) _min.Y = sbp[p].Y - r;
                    if (sbp[p].Y + r > _max.Y) _max.Y = sbp[p].Y + r;
                }
            }

            foreach (var sbp in sbPairs)
            {
                var radius = ManhattenDistance(sbp[0], sbp[1]);
                await BuildImpossibles(sbp[0], radius);
            }

            await IntersectImpossibles();


            //var result = GetBeaconCountInY(2000000);
            var result = FindFrequency(4000000);

            Answer = result.ToString();

            await Shell.Current.GoToAsync("//Day15Pg2");
            base.HandleProcessCommand();
        }
        #endregion

        #region Private Helpers
        private ulong FindFrequency(int maxXY)
        {
            var sole = _impossibles.Values.First(i =>
                i.Y > 0 &&
                i.Y <= maxXY
                && i.ContiguousStartAndEnds.Count == 2
                && i.ContiguousStartAndEnds.First().End + 1 >= 0
                && i.ContiguousStartAndEnds.First().End + 1 <= maxXY);

            return (ulong)(sole.ContiguousStartAndEnds.First().End + 1) * (ulong)4000000 + (ulong)sole.Y;

        }

        private int GetBeaconCountInY(int y)
        {
            var impossibleXs = _impossibles[y].ContiguousStartAndEnds.Sum(c => c.End - c.Start + 1);
            var existingBeacons = _beacons.Count(b => b.Y == y);
            return impossibleXs - existingBeacons;
        }

        private Task BuildImpossibles(Point origin, int radius)
        {
            return Task.Run(() =>
            {
                var min = new Point(origin.X - radius, origin.Y - radius);
                var max = new Point(origin.X + radius, origin.Y + radius);
                var dx = 0;
                for (var y = min.Y; y <= max.Y; y++)
                {
                    var startX = Math.Max(_min.X, origin.X - dx);
                    var endX = Math.Min(_max.X, origin.X + dx);
                    dx += (y < origin.Y ? 1 : -1);
                    if (_impossibles.TryGetValue(y, out var existing))
                    {
                        existing.StartAndEnds.Add(new StartAndEnd { Start = startX, End = endX });
                    }
                    else
                    {
                        var impossible = new Impossible { Y = y };
                        impossible.StartAndEnds.Add(new StartAndEnd { Start = startX, End = endX });
                        _impossibles.Add(y, impossible);
                    }
                }
            });
        }

        private Task IntersectImpossibles()
        {
            return Task.Run(() =>
            {
                foreach (var impossible in _impossibles.Values)
                {
                    if (impossible.Y == 22)
                    {

                    }
                    var ordered = impossible.StartAndEnds.OrderBy(i => i.Start).ThenBy(i => i.End).ToList();
                    StartAndEnd last = null;
                    foreach (var o in ordered)
                    {
                        if (last == null || o.Start > last.End + 1)
                        {
                            last = new StartAndEnd { Start = o.Start, End = o.End };
                            impossible.ContiguousStartAndEnds.Add(last);
                            continue;
                        }

                        if (o.End > last.End) last.End = o.End;
                    }
                }
            });
        }

        private int ManhattenDistance(Point pt1, Point pt2)
        {
            var dx = Math.Abs(pt2.X - pt1.X);
            var dy = Math.Abs(pt2.Y - pt1.Y);
            return dx + dy;
        }

        private class Impossible
        {
            public int Y;
            public List<StartAndEnd> StartAndEnds = new();
            public List<StartAndEnd> ContiguousStartAndEnds = new();
        }

        private class StartAndEnd
        {
            public int Start;
            public int End;
        }
        #endregion
    }
}
