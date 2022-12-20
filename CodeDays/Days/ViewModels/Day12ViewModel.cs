using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;

namespace Days.ViewModels
{
    public partial class Day12ViewModel : ViewModelBase
    {
        private Dictionary<Point, bool> _closedPoints;
        private Dictionary<Point, bool> _openPoints;
        private Dictionary<Point, int> _gScores;
        private Dictionary<Point, int> _fScores;
        private Dictionary<Point, Point> _nodes;
        private int[,] _graph;
        private char[,] _graphChar;
        private char[,] _animatedGraph;
        private Size _graphSize;
        private Point _start;
        private Point _end;
        private bool _doAnimation;

        #region Constructor
        public Day12ViewModel()
        {
            Day = 12;
            AnimateCommand = new RelayCommand(HandleAnimateCommand, () => !string.IsNullOrWhiteSpace(RawInput));
            RouteCommand = new RelayCommand(HandleRouteCommand, () => !string.IsNullOrWhiteSpace(RawInput));
        }
        #endregion

        [ObservableProperty]
        private string animatedAnswer;

        [ObservableProperty]
        private RelayCommand animateCommand;

        [ObservableProperty]
        private RelayCommand routeCommand;

        private async void HandleAnimateCommand()
        {
            _doAnimation = true;
            InitialiseGraph();
            await RefreshAnimated();
            await FindPath();
        }
        private async void HandleRouteCommand()
        {
            _doAnimation = false;
            InitialiseGraph();
            var points = await FindPath();
            var result = string.Empty;
            for (int r = 0; r < _graphSize.Height; r++)
            {
                for (int c = 0; c < _graphSize.Width; c++)
                {
                    var chr = ' ';
                    if (points.Any(p => p.X == c && p.Y == r))
                    {
                        var point = points.FirstOrDefault(p => p.X == c && p.Y == r);
                        chr = _graphChar[point.Y, point.X];
                    }
                    result += chr.ToString();
                }
                result += "\r\n";
            }

            MainThread.BeginInvokeOnMainThread(() =>
            {
                AnimatedAnswer = result;
            });
        }

        #region Overrides
        protected override void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.ViewModel_PropertyChanged(sender, e);
            AnimateCommand?.NotifyCanExecuteChanged();
            RouteCommand?.NotifyCanExecuteChanged();
        }
        protected async override void HandleProcessCommand()
        {
            InitialiseGraph();
            var results = await FindPath();
            Answer = results.Count.ToString();

            await Shell.Current.GoToAsync("//Day12Pg2");
            base.HandleProcessCommand();
        }
        #endregion

        #region Private Helpers
        private void InitialiseGraph()
        {
            _closedPoints = new();
            _openPoints = new();
            _gScores = new();
            _fScores = new();
            _nodes = new();
            _start = new();
            _end = new();

            var lines = RawInput.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            _graphSize = new Size(lines[0].Length, lines.Length);
            _graph = new int[_graphSize.Height, _graphSize.Width];
            _graphChar = new char[_graphSize.Height, _graphSize.Width];
            _animatedGraph = new char[_graphSize.Height, _graphSize.Width];

            var col = 0;
            var row = 0;
            foreach (var line in lines)
            {
                foreach (var chr in line)
                {

                    if (chr == 'S')
                    {
                        _graph[row, col] = 1;
                        _start.X = col;
                        _start.Y = row;
                    }
                    else if (chr == 'E')
                    {
                        _graph[row, col] = 26;
                        _end.X = col;
                        _end.Y = row;
                    }
                    else
                    {
                        _graph[row, col] = chr - 96;
                    }
                    _graphChar[row, col] = chr;
                    _animatedGraph[row, col] = ' ';
                    col++;
                }
                row++;
                col = 0;
            }
        }

        private async Task<List<Point>> FindPath()
        {
            _openPoints[_start] = true;
            _gScores[_start] = 0;
            _fScores[_start] = CalcHeuristic(_start);

            while (_openPoints.Count > 0)
            {
                var current = BestNode();
                if (current == _end)
                {
                    return ReversedPoints(current);
                }

                _openPoints.Remove(current);
                _closedPoints[current] = true;

                var adjacents = ValidAdjacents(current, false);
                foreach (var adjacent in adjacents)
                {
                    if (_closedPoints.ContainsKey(adjacent)) continue;

                    var projectedGScore = GetGScore(current) + 1;

                    if (!_openPoints.ContainsKey(adjacent))
                    {
                        _openPoints[adjacent] = true;
                    }
                    else if (projectedGScore >= GetGScore(adjacent))
                    {
                        continue;
                    }

                    _nodes[adjacent] = current;
                    _gScores[adjacent] = projectedGScore;
                    _fScores[adjacent] = projectedGScore + CalcHeuristic(adjacent);
                    if (_doAnimation)
                    {
                        await RefreshAnimated();
                    }
                }
            }

            return new List<Point>();
        }

        private List<Point> ValidAdjacents(Point point, bool useDiagonals)
        {
            var result = new List<Point>();
            var currentValue = _graph[point.Y, point.X];
            var maxI = useDiagonals ? 5 : 3;
            for (var i = 0; i <= maxI; i++)
            {
                var dx = 0;
                var dy = 0;
                switch (i)
                {
                    case 0: dx = 1; dy = 0; break;
                    case 1: dx = -1; dy = 0; break;
                    case 2: dx = 0; dy = 1; break;
                    case 3: dx = 0; dy = -1; break;
                    case 4: dx = 1; dy = 1; break;
                    case 5: dx = -1; dy = -1; break;
                }
                var adjacentPoint = new Point(point.X + dx, point.Y + dy);
                if (adjacentPoint.X >= 0 && adjacentPoint.X < _graphSize.Width && adjacentPoint.Y >= 0 && adjacentPoint.Y < _graphSize.Height)
                {
                    if (_graph[adjacentPoint.Y, adjacentPoint.X] <= currentValue + 1)
                    {
                        result.Add(adjacentPoint);
                    }

                }
            }
            return result;
        }
        private List<Point> ReversedPoints(Point point)
        {
            List<Point> path = new List<Point>();
            while (_nodes.ContainsKey(point))
            {
                path.Add(point);
                point = _nodes[point];
            }

            path.Reverse();
            return path;
        }
        private Point BestNode()
        {
            int best = int.MaxValue;
            var bestPoint = new Point();
            foreach (var node in _openPoints.Keys)
            {
                var score = GetFScore(node);
                if (score < best)
                {
                    bestPoint = node;
                    best = score;
                }
            }

            return bestPoint;
        }

        private int CalcHeuristic(Point current)
        {
            var dx = _end.X - current.X;
            var dy = _end.Y - current.Y;
            return Math.Abs(dx) + Math.Abs(dy);
        }
        private int GetGScore(Point point)
        {
            if (_gScores.TryGetValue(point, out var score))
            {
                return score;
            }
            return int.MaxValue;
        }

        private int GetFScore(Point point)
        {
            if (_fScores.TryGetValue(point, out var score))
            {
                return score;
            }
            return int.MaxValue;
        }

        private Task RefreshAnimated()
        {
            return Task.Run(() =>
            {
                foreach (var point in _nodes.Values)
                {
                    _animatedGraph[point.Y, point.X] = _graphChar[point.Y, point.X];
                }

                var result = string.Empty;
                for (int r = 0; r < _graphSize.Height; r++)
                {
                    for (int c = 0; c < _graphSize.Width; c++)
                    {
                        result += _animatedGraph[r, c].ToString();
                    }
                    result += "\r\n";
                }

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    AnimatedAnswer = result;
                });
            });

        }
        #endregion
    }
}
