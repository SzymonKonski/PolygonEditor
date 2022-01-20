using System.Drawing;
using Editor.Geometry;
using Editor.Properties;

namespace Editor
{
    public partial class MainWindow
    {
        private void SelectCircle(Point location)
        {
            foreach (var circle in circles)
            {
                if (circle.IsPointInside(location))
                {
                    currentCircle = circle;
                    break;
                }
            }
        }

        private void DrawNewCircle(Graphics canvasGraphics, Circle circle)
        {
            var mousePosition = pictureBox.PointToClient(MousePosition);
            var radius = Library.DistanceBetweenPoints(mousePosition, circle.Point);
            canvasGraphics.DrawEllipse(Pens.Red, circle.Point.X - (float) radius, circle.Point.Y - (float) radius,
                (float) (radius + radius), (float) (radius + radius));
        }

        private void DrawCircles(Graphics canvasGraphics)
        {
            var pen = new Pen(Brushes.Black);

            foreach (var circle in circles)
            {
                var color = circle == currentCircle ? Brushes.PowderBlue : Brushes.White;
                canvasGraphics.DrawEllipse(pen, circle.Point.X - circle.Radius, circle.Point.Y - circle.Radius,
                    circle.Radius + circle.Radius, circle.Radius + circle.Radius);
                canvasGraphics.FillEllipse(color, circle.Point.X - circle.Radius, circle.Point.Y - circle.Radius,
                    circle.Radius + circle.Radius, circle.Radius + circle.Radius);

                if (circle.RadiusLocked)
                {
                    var p = new Point(circle.Point.X + (int) circle.Radius, circle.Point.Y);
                    canvasGraphics.DrawLine(pen, circle.Point, p);
                    var iconSize = 17;
                    var p2 = new Point((circle.Point.X + p.X) / 2, (circle.Point.Y + p.Y) / 2);
                    var rect = new Rectangle(p2.X - iconSize / 2, p2.Y - iconSize / 2, iconSize, iconSize);
                    canvasGraphics.DrawIcon(Resources.anchor_icon, rect);
                }
                else if (circle.Locked)
                {
                    var iconSize = 17;
                    var rect = new Rectangle(circle.Point.X - iconSize / 2, circle.Point.Y - iconSize / 2, iconSize,
                        iconSize);
                    canvasGraphics.DrawIcon(Resources.anchor_icon, rect);
                }
            }
        }
    }
}