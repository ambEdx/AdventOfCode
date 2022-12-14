using System.Numerics;
using Point = System.Drawing.Point;

namespace Days.ViewModels
{
    public partial class Day12ViewModel : ViewModelBase
    {
        #region Constructor
        public Day12ViewModel()
        {
            Day = 12;
        }
        #endregion

        private Dictionary<Point, bool> _closedPoints = new();
        private Dictionary<Point, bool> _openPoints = new ();
        private Dictionary<Point, int> _gScores = new ();
        private Dictionary<Point, int> _fScores = new ();
        private Dictionary<Point, Point> _nodes = new ();

        #region Overrides
        protected async override void HandleProcessCommand()
        {
            var lines = RawInput.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var graph = new int[lines.Length, lines[0].Length];

            var start = new Point();
            var end = new Point();
            var r = 0;
            var c = 0;
            foreach (var line in lines)
            {
                foreach (var chr in line)
                {

                    if (chr == 'S')
                    {
                        graph[r, c] = 1;
                        start.X = r;
                        start.Y = c;
                    }
                    else if (chr == 'E')
                    {
                        graph[r, c] = 26;
                        end.X = r;
                        end.Y = c;
                    }
                    else
                    {
                        graph[r, c] = chr - 96;
                    }
                    c++;
                }
                r++;
                c = 0;
            }

            var results = FindPath(graph, start, end);
            Answer = results.Count.ToString();

            await Shell.Current.GoToAsync("//Day12Pg2");
            base.HandleProcessCommand();
        }
        #endregion

        #region Private Helpers
        public List<Point> FindPath(int[,] graph, Point start, Point end)
        {

            _openPoints[start] = true;
            _gScores[start] = 0;
            _fScores[start] = CalcHeuristic(start, end);

            while (_openPoints.Count > 0)
            {
                var current = BestNode();
                if (current == end)
                {
                    return ReversedPoints(current);
                }

                _openPoints.Remove(current);
                _closedPoints[current] = true;

                var adjacents = ValidAdjacents(graph, current, false);
                foreach (var adjacent in adjacents)
                {
                    if (_closedPoints.ContainsKey(adjacent)) continue;

                    var projectedGScore = CalcGScore(current) + 1;

                    if (!_openPoints.ContainsKey(adjacent))
                    {
                        _openPoints[adjacent] = true;
                    }
                    else if (projectedGScore >= CalcGScore(adjacent))
                    {
                        continue;
                    }

                    _nodes[adjacent] = current;
                    _gScores[adjacent] = projectedGScore;
                    _fScores[adjacent] = projectedGScore + CalcHeuristic(adjacent, end);
                }
            }

            return new List<Point>();
        }

        private List<Point> ValidAdjacents(int[,] graph, Point point, bool useDiagonals)
        {
            var result = new List<Point>();
            var currentValue = graph[point.X, point.Y];
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
                if (adjacentPoint.X >= 0 && adjacentPoint.X < graph.GetLength(0) && adjacentPoint.Y >= 0 && adjacentPoint.Y < graph.GetLength(1))
                {
                    if (graph[adjacentPoint.X, adjacentPoint.Y] <= currentValue + 1)
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
                var score = CalcFScore(node);
                if (score < best)
                {
                    bestPoint = node;
                    best = score;
                }
            }

            return bestPoint;
        }

        private int CalcHeuristic(Point start, Point end)
        {
            var dx = end.X - start.X;
            var dy = end.Y - start.Y;
            return Math.Abs(dx) + Math.Abs(dy);
        }
        private int CalcGScore(Point point)
        {
            if (_gScores.TryGetValue(point, out var score))
            {
                return score;
            }
            return int.MaxValue;
        }

        private int CalcFScore(Point point)
        {
            if (_fScores.TryGetValue(point, out var score))
            {
                return score;
            }
            return int.MaxValue;
        }
        #endregion
    }
}
