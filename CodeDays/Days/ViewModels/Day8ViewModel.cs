using Point = System.Drawing.Point;

namespace Days.ViewModels
{
    public partial class Day8ViewModel : ViewModelBase
    {
        #region Constructor
        public Day8ViewModel()
        {
            Day = 8;
        }
        #endregion

        private const int FROM_LEFT = 0;
        private const int FROM_RIGHT = 1;
        private const int FROM_TOP = 2;
        private const int FROM_BOTTOM = 3;

        #region Overrides
        protected async override void HandleProcessCommand()
        {
            var lines = RawInput.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            int gridSize = lines[0].Length;

            // Initialise arrays.
            var viewPoints = new int[4][][];
            for (int a2 = 0; a2 < 4; a2++)
            {
                viewPoints[a2] = new int[gridSize][];
                for (int a3 = 0; a3 < gridSize; a3++)
                {
                    viewPoints[a2][a3] = new int[gridSize];
                }
            }

            // Populate arrays.
            for (int i = 0; i < 4; i++)
            {
                for (int r = 0; r < gridSize; r++)
                {
                    viewPoints[i][r] = new int[gridSize];
                    for (int c = 0; c < gridSize; c++)
                    {
                        switch (i)
                        {
                            case FROM_LEFT: viewPoints[i][r][c] = lines[r][c] - '0'; break;
                            case FROM_RIGHT: viewPoints[i][r][gridSize - 1 - c] = lines[r][c] - '0'; break;
                            case FROM_TOP: viewPoints[i][r][c] = lines[c][r] - '0'; break;
                            case FROM_BOTTOM: viewPoints[i][r][gridSize - 1 - c] = lines[c][r] - '0'; break;
                        }
                    }
                }
            }

            // Get trees.
            var coordsOfHighestTrees = new List<Point>();
            var v = 0;
            foreach (var viewPoint in viewPoints)
            {
                int tl = 0;
                foreach (var treeline in viewPoint)
                {
                    var highestTrees = FindHighestTrees(treeline);
                    foreach (var tree in highestTrees)
                    {
                        var coords = new Point();
                        switch (v)
                        {
                            case FROM_LEFT: coords.X = tl; coords.Y = tree; break;
                            case FROM_RIGHT: coords.X = tl; coords.Y = gridSize - 1 - tree; break;
                            case FROM_TOP: coords.X = tree; coords.Y = tl; break;
                            case FROM_BOTTOM: coords.X = gridSize - 1 - tree; coords.Y = tl; break;
                        }

                        if (!coordsOfHighestTrees.Contains(coords))
                        {
                            coordsOfHighestTrees.Add(coords);
                        }
                    }
                    tl++;
                }
                v++;
            }

            // Get visible distances
            var maxViewingDistance = 0;
            foreach (var coords in coordsOfHighestTrees)
            {
                var intersectingViewPoints = new int[4][];
                intersectingViewPoints[FROM_LEFT] = viewPoints[FROM_LEFT][coords.X];
                intersectingViewPoints[FROM_RIGHT] = viewPoints[FROM_RIGHT][coords.X];
                intersectingViewPoints[FROM_TOP] = viewPoints[FROM_TOP][coords.Y];
                intersectingViewPoints[FROM_BOTTOM] = viewPoints[FROM_BOTTOM][coords.Y];

                var viewingDistance = CalculateVisibleDistance(intersectingViewPoints, coords);
                if (viewingDistance > maxViewingDistance)
                {
                    maxViewingDistance = viewingDistance;
                }
            }
            var result = maxViewingDistance;

            Answer = result.ToString();

            await Shell.Current.GoToAsync("//Day8Pg2");
            base.HandleProcessCommand();
        }
        #endregion

        #region Private Helpers

        private List<int> FindHighestTrees(int[] trees)
        {
            var result = new List<int>();
            var maxHeight = -1;
            for (int t = 0; t < trees.Length; t++)
            {
                if (trees[t] > maxHeight)
                {
                    result.Add(t);
                    maxHeight = trees[t];
                }
            }
            return result;
        }

        private int CalculateVisibleDistance(int[][] intersectingViewPoints, Point treeCoords)
        {
            var totalViewingDistance = 1;
            var treeHeight = intersectingViewPoints[FROM_LEFT][treeCoords.Y];
            var v = 0;
            foreach (var intersectingViewPoint in intersectingViewPoints)
            {
                var tree = 0;
                switch (v)
                {
                    case FROM_LEFT: tree = treeCoords.Y; break;
                    case FROM_RIGHT: tree = intersectingViewPoint.Length - 1 - treeCoords.Y; break;
                    case FROM_TOP: tree = treeCoords.X; break;
                    case FROM_BOTTOM: tree = intersectingViewPoint.Length - 1 - treeCoords.X; break;
                }

                var viewingDistance = tree;
                for (int t = tree - 1; t >= 0; t--)
                {
                    if (intersectingViewPoint[t] >= treeHeight)
                    {
                        viewingDistance = tree - t;
                        break;
                    }
                }

                totalViewingDistance *= viewingDistance;

                v++;
            }
            return totalViewingDistance;
        }
        #endregion
    }
}
