using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day24
{
    class Grid
    {
        private const int OpenSpace = -1;

        public int Step { get; private set; }
        readonly Point[,] _points;
        public IEnumerable<Point> Positions { get => _points.Cast<Point>().Distinct().Where(x => char.IsNumber(x?.Position ?? 'x')).OrderBy(x => x.Position); }
        public Grid(List<Point> points)
        {
            var width = points.Max(x => x.X) + 2;
            var height = points.Max(x => x.Y) + 2;

            _points = new Point[width, height];

            foreach (var point in points)
                _points[point.X, point.Y] = point;

            Reset();
        }

        private void Reset()
        {
            Step = 0;
            foreach (var point in _points)
                if (point != null)
                    point.Value = OpenSpace;
        }
        public List<Distance> GetDistances()
        {
            var distances = new List<Distance>();
            foreach (var from in Positions)
            {
                BuildFrom(from);
                foreach (var to in Positions.Where(x => x.Position > from.Position))
                {
                    distances.Add(new Distance
                    {
                        From = from.Position,
                        To = to.Position,
                        Length = to.Value
                    });
                    distances.Add(new Distance
                    {
                        From = to.Position,
                        To = from.Position,
                        Length = to.Value
                    });
                }
            }
            return distances;
        }
        public Point GetPoint(int x, int y) => _points[x, y];
        public void BuildFrom(Point position)
        {
            Reset();
            position.Value = 0;

            List<Point> points;
            do
            {
                Step++;
                points = GetPossibleSteps();

                foreach (var point in points)
                    point.Value = Step;

            } while (points.Any());
        }
        List<Point> GetPossibleSteps()
        {
            Func<Point, int, int, bool> offsetAvailable = (point, x, y) => (GetPoint(point.X + x, point.Y + y)?.Value ?? OpenSpace) > OpenSpace;
            var points = new List<Point>();


            foreach (var point in _points.Cast<Point>().Where(x => x != null && x.Value == OpenSpace))
            {
                if (offsetAvailable(point, -1, 0) || offsetAvailable(point, 1, 0) || offsetAvailable(point, 0, -1) || offsetAvailable(point, 0, 1))
                    points.Add(point);
            }

            return points.Where(x => x != null).ToList();
        }
    }
}
