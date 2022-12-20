using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Days.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Numerics;
using System.Text;
using System.Text.Json;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;

namespace Days.ViewModels
{
    public partial class Day14ViewModel : ViewModelBase
    {
        private char[,] _matrix;
        private Size _size = new(1000, 0);

        #region Constructor
        public Day14ViewModel()
        {
            Day = 14;
        }
        #endregion

        [ObservableProperty]
        private string additionalAnswer;

        #region Overrides
        protected async override void HandleProcessCommand()
        {
            var dropPoint = await ScanCave();
            var result = 0;
            while (true)
            {
                result++;
                var pt = GetFallPoint(dropPoint);
                //if (pt.Y == int.MaxValue) break;
                if (pt == new Point(500, 0)) break;

                _matrix[pt.X, pt.Y] = 'o';
                if (pt == dropPoint)
                {
                    dropPoint.Y--;
                }

            }

            var sand = new StringBuilder();
            for (var y = 0; y < _matrix.GetLength(1); y++)
            {
                for (var x = 0; x < _matrix.GetLength(0); x++)
                {
                    sand.Append(_matrix[x, y]);
                }
                sand.AppendLine();
            }

            AdditionalAnswer = sand.ToString();
            Answer = result.ToString();

            await Shell.Current.GoToAsync("//Day14Pg2");
            base.HandleProcessCommand();
        }
        #endregion

        #region Private Helpers

        private Task<Point> ScanCave()
        {
            return Task.Run(() =>
            {
                var lines = RawInput.ReplaceLineEndings().Replace(CRCR, CR).Split(CR);

                var rocks = new List<Point>();
                foreach (var line in lines)
                {
                    var vectors = line.Split(" -> ");
                    for (var i = 1; i < vectors.Length; i++)
                    {
                        var current = vectors[i - 1].Split(",").Select(v => int.Parse(v)).ToArray();
                        var end = vectors[i].Split(",").Select(v => int.Parse(v)).ToArray();
                        var d = current[0] == end[0] ? 1 : 0;
                        var inc = end[d] > current[d] ? 1 : -1;
                        while (inc == 1 ? current[d] <= end[d] : current[d] >= end[d])
                        {
                            var pt = new Point(current[0], current[1]);
                            if (pt.Y > _size.Height) _size.Height = pt.Y;
                            rocks.Add(pt);
                            current[d] += inc;
                        }
                    }
                }

                _size.Height += 3;

                _matrix = new char[_size.Width, _size.Height];
                for (var y = 0; y < _size.Height; y++)
                {
                    for (var x = 0; x < _size.Width; x++)
                    {
                        if (y == _size.Height - 1)
                        {
                            _matrix[x, y] = '#';
                        }
                        else
                        {
                            _matrix[x, y] = '.';
                        }
                    }
                }

                var dropPoint = new Point(500, int.MaxValue);
                foreach (var rock in rocks)
                {
                    _matrix[rock.X, rock.Y] = '#';
                    if (rock.X == 500 && rock.Y - 1 < dropPoint.Y) dropPoint.Y = rock.Y - 1;
                }

                return dropPoint;
            });
        }
        private Point GetFallPoint(Point pt)
        {
            if (pt.Y == int.MaxValue) return pt;

            var origin = pt;
            if (_matrix[pt.X - 1, pt.Y + 1] == '.')
            {
                pt.X--;
                pt.Y = LowestYofX(pt);
            }
            else if (_matrix[pt.X + 1, pt.Y + 1] == '.')
            {
                pt.X++;
                pt.Y = LowestYofX(pt);
            }
            if (pt != origin)
            {
                return GetFallPoint(pt);
            }

            return pt;
        }
        private int LowestYofX(Point current)
        {
            for (var y = current.Y + 1; y <= _size.Height; y++)
            {
                if (_matrix[current.X, y] != '.')
                {
                    return y - 1;
                }
            }

            return int.MaxValue;
        }
        #endregion
    }
}
