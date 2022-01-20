using System.Drawing;
using static System.Math;

namespace Editor.Geometry
{
    public static class Library
    {
        public const double Eps = 1e-6;

        public static float GetSlope(Point p1, Point p2)
        {
            if (p2.X - p1.X == 0)
                return 100;

            return -(float) (p1.Y - p2.Y) / (p2.X - p1.X);
        }

        public static bool AreVerticesIntersecting(Point p1, Point p2, int radius)
        {
            var a = (p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y);

            return a <= radius * radius;
        }

        public static double DistanceBetweenPoints(Point p1, Point p2)
        {
            return Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }

        public static Point GetEqualLinesPoint(Point p1, Point p2, double length1, double length2)
        {
            var rate = length1 / length2;
            var x = (int) ((1 - rate) * p1.X + rate * p2.X);
            var y = (int) ((1 - rate) * p1.Y + rate * p2.Y);
            return new Point(x, y);
        }

        public static void DrawLine(this Bitmap canvas, Point p1, Point p2, Color color)
        {
            var width = p2.X - p1.X;
            var height = p2.Y - p1.Y;

            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (width < 0) dx1 = -1;
            else if (width > 0) dx1 = 1;
            if (height < 0) dy1 = -1;
            else if (height > 0) dy1 = 1;
            if (width < 0) dx2 = -1;
            else if (width > 0) dx2 = 1;

            var longest = Abs(width);
            var shortest = Abs(height);
            if (!(longest > shortest))
            {
                longest = Abs(height);
                shortest = Abs(width);
                if (height < 0) dy2 = -1;
                else if (height > 0) dy2 = 1;
                dx2 = 0;
            }

            var numerator = longest >> 1;
            for (var i = 0; i <= longest; ++i)
            {
                if (p1.X > 0 && p1.Y > 0 && p1.X < canvas.Width && p1.Y < canvas.Height)
                    canvas.SetPixel(p1.X, p1.Y, color);

                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    p1.Offset(dx1, dy1);
                }
                else
                {
                    p1.Offset(dx2, dy2);
                }
            }
        }
    }
}