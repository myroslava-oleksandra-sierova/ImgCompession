using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace ImgCompession
{
    public interface IChartPointMapper
    {
        void Map(object obj, out double x, out double y);
    }
    /*
    public static class SimplifyNet
    {

        // square distance between 2 points
        private static double GetSqDist(Point p1, Point p2)
        {
            double dx = p1.X - p2.X;
            double dy = p1.Y - p2.Y;

            return (dx * dx) + (dy * dy);
        }

        // square distance from a point to a segment
        private static double GetSqSegDist(Point p, Point p1, Point p2)
        {
            double x = p1.X;
            double y = p1.Y;
            double dx = p2.X - x;
            double dy = p2.Y - y;

            if (Math.Abs(dx) > 0 || Math.Abs(dy) > 0)
            {
                double t = ((p.X - x) * dx + (p.Y - y) * dy) / (dx * dx + dy * dy);

                if (t > 1)
                {
                    x = p2.X;
                    y = p2.Y;
                }
                else if (t > 0)
                {
                    x += dx * t;
                    y += dy * t;
                }
            }

            dx = p.X - x;
            dy = p.Y - y;

            return dx * dx + dy * dy;
        }

        // basic distance-based simplification
        private static List<Point> SimplifyRadialDist(List<Point> points, double sqTolerance)
        {
            Point point = null;
            Point prevPoint = points[0];
            var newPoints = new List<Point>() { prevPoint };

            for (int i = 1, len = points.Count; i < len; i++)
            {
                point = points[i];

                if (GetSqDist(point, prevPoint) > sqTolerance)
                {
                    newPoints.Add(point);
                    prevPoint = point;
                }
            }

            if (prevPoint != point)
            {
                newPoints.Add(point);
            }

            return newPoints;
        }

        private static void SimplifyRadialDist(ICollection points, bool[] marks, IChartPointMapper mapper, double sqTolerance)
        {
            var cnt = 0;

            double x, y;
            double lastX, lastY;


            foreach (var obj in points)
            {
                mapper.Map(obj, out x, out y);


                lastX = x;
                lastY = y;
                cnt++;
            }

            Point point = null;
            Point prevPoint = points[0];




            var newPoints = new List<Point>() { prevPoint };
            
            for (int i = 1, len = points.Count; i < len; i++)
            {
                point = points[i];

                if (GetSqDist(point, prevPoint) > sqTolerance)
                {
                    newPoints.Add(point);
                    prevPoint = point;
                }
            }

            if (prevPoint != point)
            {
                newPoints.Add(point);
            }

            return newPoints;
        }

        private static void SimplifyDpStep(List<Point> points, int first, int last, double sqTolerance, List<Point> simplified)
        {
            double maxSqDist = sqTolerance;
            var index = 0;

            for (int i = first + 1; i < last; i++)
            {
                double sqDist = GetSqSegDist(points[i], points[first], points[last]);

                if (sqDist > maxSqDist)
                {
                    index = i;
                    maxSqDist = sqDist;
                }
            }

            if (maxSqDist > sqTolerance)
            {
                if (index - first > 1)
                {
                    SimplifyDpStep(points, first, index, sqTolerance, simplified);

                }

                simplified.Add(points[index]);

                if (last - index > 1)
                {
                    SimplifyDpStep(points, index, last, sqTolerance, simplified);
                }
            }
        }

        // simplification using Ramer-Douglas-Peucker algorithm
        private static List<Point> SimplifyDouglasPeucker(List<Point> points, double sqTolerance)
        {
            int last = points.Count - 1;

            var simplified = new List<Point>() { points[0] };
            SimplifyDpStep(points, 0, last, sqTolerance, simplified);
            simplified.Add(points[last]);

            return simplified;
        }


        // both algorithms combined for awesome performance
        public static List<Point> Simplify(List<Point> points, double tolerance = 1, bool highestQuality = false)
        {
            if (points.Count <= 2)
            {
                return points;
            }

            double sqTolerance = tolerance * tolerance;

            points = highestQuality ? points : SimplifyRadialDist(points, sqTolerance);
            points = SimplifyDouglasPeucker(points, sqTolerance);

            return points;
        }


        public static void Simplify(ICollection points, ref bool[] marks, double tolerance = 1, bool highestQuality = false)
        {

            for (var i = 0; i < marks.Length; i++)
                marks[i] = false;

            if (marks.Length < points.Count)
                Array.Resize(ref marks, points.Count * 2);

            if (points.Count <= 2)
            {
                return;
            }

            double sqTolerance = tolerance * tolerance;

            points = highestQuality ? points : SimplifyRadialDist(points, sqTolerance);
            points = SimplifyDouglasPeucker(points, sqTolerance);

            return points;
        }

    }
    */
    public interface ISimplifyUtility
    {
        /// <summary>
        /// Simplifies a list of points to a shorter list of points.
        /// </summary>
        /// <param name="points">Points original list of points</param>
        /// <param name="tolerance">Tolerance tolerance in the same measurement as the point coordinates</param>
        /// <param name="highestQuality">Enable highest quality for using Douglas-Peucker, set false for Radial-Distance algorithm</param>
        /// <returns>Simplified list of points</returns>
        List<Point> Simplify(Point[] points, double tolerance = 0.3, bool highestQuality = false);
    }
    public class SimplifyUtility : ISimplifyUtility
    {
        // square distance between 2 points
        private double GetSquareDistance(Point p1, Point p2)
        {
            double dx = p1.X - p2.X,
                dy = p1.Y - p2.Y;

            return (dx * dx) + (dy * dy);
        }

        // square distance from a point to a segment
        private double GetSquareSegmentDistance(Point p, Point p1, Point p2)
        {
            var x = p1.X;
            var y = p1.Y;
            var dx = p2.X - x;
            var dy = p2.Y - y;

            if (!dx.Equals(0.0) || !dy.Equals(0.0))
            {
                var t = ((p.X - x) * dx + (p.Y - y) * dy) / (dx * dx + dy * dy);

                if (t > 1)
                {
                    x = p2.X;
                    y = p2.Y;
                }
                else if (t > 0)
                {
                    x += dx * t;
                    y += dy * t;
                }
            }

            dx = p.X - x;
            dy = p.Y - y;

            return (dx * dx) + (dy * dy);
        }

        // rest of the code doesn't care about point format

        // basic distance-based simplification
        private List<Point> SimplifyRadialDistance(Point[] points, double sqTolerance)
        {
            var prevPoint = points[0];
            var newPoints = new List<Point> { prevPoint };
            Point point = null;

            for (var i = 1; i < points.Length; i++)
            {
                point = points[i];

                if (GetSquareDistance(point, prevPoint) > sqTolerance)
                {
                    newPoints.Add(point);
                    prevPoint = point;
                }
            }

            if (point != null && !prevPoint.Equals(point))
                newPoints.Add(point);

            return newPoints;
        }

        // simplification using optimized Douglas-Peucker algorithm with recursion elimination
        private List<Point> SimplifyDouglasPeucker(Point[] points, double sqTolerance)
        {
            var len = points.Length;
            var markers = new int?[len];
            int? first = 0;
            int? last = len - 1;
            int? index = 0;
            var stack = new List<int?>();
            var newPoints = new List<Point>();

            markers[first.Value] = markers[last.Value] = 1;

            while (last != null)
            {
                var maxSqDist = 0.0d;

                for (int? i = first + 1; i < last; i++)
                {
                    var sqDist = GetSquareSegmentDistance(points[i.Value], points[first.Value], points[last.Value]);

                    if (sqDist > maxSqDist)
                    {
                        index = i;
                        maxSqDist = sqDist;
                    }
                }

                if (maxSqDist > sqTolerance)
                {
                    markers[index.Value] = 1;
                    stack.AddRange(new[] { first, index, index, last });
                }


                if (stack.Count > 0)
                {
                    last = stack[stack.Count - 1];
                    stack.RemoveAt(stack.Count - 1);
                }
                else
                    last = null;

                if (stack.Count > 0)
                {
                    first = stack[stack.Count - 1];
                    stack.RemoveAt(stack.Count - 1);
                }
                else
                    first = null;
            }

            for (var i = 0; i < len; i++)
            {
                if (markers[i] != null)
                    newPoints.Add(points[i]);
            }

            return newPoints;
        }

        /// <summary>
        /// Simplifies a list of points to a shorter list of points.
        /// </summary>
        /// <param name="points">Points original list of points</param>
        /// <param name="tolerance">Tolerance tolerance in the same measurement as the point coordinates</param>
        /// <param name="highestQuality">Enable highest quality for using Douglas-Peucker, set false for Radial-Distance algorithm</param>
        /// <returns>Simplified list of points</returns>
        public List<Point> Simplify(Point[] points, double tolerance = 0.3, bool highestQuality = false)
        {
            if (points == null || points.Length == 0)
                return new List<Point>();

            var sqTolerance = tolerance * tolerance;

            if (highestQuality)
                return SimplifyDouglasPeucker(points, sqTolerance);

            List<Point> points2 = SimplifyRadialDistance(points, sqTolerance);
            return SimplifyDouglasPeucker(points2.ToArray(), sqTolerance);
        }

        /// <summary>
        /// Simplifies a list of points to a shorter list of points.
        /// </summary>
        /// <param name="points">Points original list of points</param>
        /// <param name="tolerance">Tolerance tolerance in the same measurement as the point coordinates</param>
        /// <param name="highestQuality">Enable highest quality for using Douglas-Peucker, set false for Radial-Distance algorithm</param>
        /// <returns>Simplified list of points</returns>
        public static List<Point> SimplifyArray(Point[] points, double tolerance = 0.3, bool highestQuality = false)
        {
            return new SimplifyUtility().Simplify(points, tolerance, highestQuality);
        }
    }
    public class Point
    {
        public double X;
        public double Y;

        public Point(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
