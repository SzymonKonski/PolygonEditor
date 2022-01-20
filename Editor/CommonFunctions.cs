using System;
using System.Collections.Generic;
using System.Drawing;
using Editor.Geometry;
using Editor.Properties;
using Editor.Relations;

namespace Editor
{
    public partial class MainWindow
    {
        private void ClearSelectedObjects()
        {
            currentPolygon = null;
            currentCircle = null;
            selectedEdgeFirstVertex = null;
            selectedRelationEdgeVertex = null;
            selectedVertex = null;
            selectedRelationType = null;
        }

        private void DisableMouseDown()
        {
            pictureBox.MouseDown -= pictureBox_MouseDown;
        }

        private void EnableMouseDown()
        {
            pictureBox.MouseDown -= pictureBox_MouseDown;
            pictureBox.MouseDown += pictureBox_MouseDown;
        }

        private void DrawIconOnEdge(Edge edge, int iconSize, Graphics graphics, Icon icon, int relationNumb)
        {
            var p1 = edge.V1.Point;
            var p2 = edge.V2.Point;
            var p = new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
            var rect = new Rectangle(p.X - iconSize / 2, p.Y - iconSize / 2, iconSize, iconSize);
            graphics.DrawIcon(icon, rect);
            graphics.DrawString(relationNumb.ToString(), Font, Brushes.Navy, rect.X + rect.Width, rect.Y + rect.Height);
        }

        private void DrawIcons(Graphics graphics)
        {
            const int iconSize = 12;

            if (addingTangent)
            {
                var rect = new Rectangle(addTangent.X - iconSize / 2, addTangent.Y - iconSize / 2, iconSize,
                    iconSize);
                graphics.DrawIcon(Resources.tangent, rect);
            }

            foreach (var relation in TangentCircleRelations)
            {
                var tangentPoint = relation.CalculateTangentPoint();
                var rect = new Rectangle(tangentPoint.X - iconSize / 2, tangentPoint.Y - iconSize / 2, iconSize,
                    iconSize);
                graphics.DrawIcon(Resources.tangent, rect);
            }

            foreach (var polygon in polygons)
            {
                var i = 0;
                foreach (var relation in polygon.ParallelRelations)
                {
                    DrawIconOnEdge(relation.FirstEdge, iconSize, graphics, Resources.parallel_lines_icon, i);
                    DrawIconOnEdge(relation.SecondEdge, iconSize, graphics, Resources.parallel_lines_icon, i);
                    i++;
                }

                foreach (var relation in polygon.EqualLengthRelations)
                {
                    DrawIconOnEdge(relation.FirstEdge, iconSize, graphics, Resources.equal_symbol_icon, i);
                    DrawIconOnEdge(relation.SecondEdge, iconSize, graphics, Resources.equal_symbol_icon, i);
                    i++;
                }

                foreach (var relation in polygon.SetLengthRelations)
                {
                    DrawIconOnEdge(relation.Edge, iconSize, graphics, Resources.anchor_icon, i);
                    i++;
                }
            }
        }


        private void MakeTangetMovingEdge(PolygonVertex vertex)
        {
            if (stycznaCheckBox.Checked)
            {
                if (addingTangent)
                {
                    addingTangent = false;
                    addTangentPoint = null;
                    tangentCircle = null;
                }

                foreach (var circle in circles)
                {
                    var x1 = vertex.Point.X;
                    var y1 = vertex.Point.Y;
                    var x2 = vertex.Next.Point.X;
                    var y2 = vertex.Next.Point.Y;
                    var x3 = circle.Point.X;
                    var y3 = circle.Point.Y;

                    var k = (double)((y2 - y1) * (x3 - x1) - (x2 - x1) * (y3 - y1)) /
                            ((y2 - y1) * (y2 - y1) + (x2 - x1) * (x2 - x1));
                    var x4 = x3 - k * (y2 - y1);
                    var y4 = y3 + k * (x2 - x1);

                    Point tangentPoint = new Point((int)x4, (int)y4);

                    if (Library.DistanceBetweenPoints(circle.Point, new Point(Convert.ToInt32(x4), Convert.ToInt32(y4))) > circle.Radius - 1
                        && Library.DistanceBetweenPoints(circle.Point, new Point(Convert.ToInt32(x4), Convert.ToInt32(y4))) < circle.Radius + 1)
                    {
                        addingTangent = true;
                        addTangentPoint = vertex;
                        addTangent = tangentPoint;
                        tangentCircle = circle;
                    }
                }
            }
        }

        public Point CalculateTangentPoint(PolygonVertex vertex, PolygonVertex vertex2, Circle circle)
        {
            var x1 = vertex.Point.X;
            var y1 = vertex.Point.Y;
            var x2 = vertex2.Point.X;
            var y2 = vertex2.Point.Y;
            var x3 = circle.Point.X;
            var y3 = circle.Point.Y;

            var k = (double)((y2 - y1) * (x3 - x1) - (x2 - x1) * (y3 - y1)) /
                    ((y2 - y1) * (y2 - y1) + (x2 - x1) * (x2 - x1));
            var x4 = x3 - k * (y2 - y1);
            var y4 = y3 + k * (x2 - x1);

            return new Point((int)x4, (int)y4);
        }

        private void MakeTangetMovingVertex(PolygonVertex vertex)
        {
            if (stycznaCheckBox.Checked)
            {
                if (addingTangent)
                {
                    addingTangent = false;
                    addTangentPoint = null;
                    tangentCircle = null;
                }

                foreach (var circle in circles)
                {
                    Point tangentPoint1 = CalculateTangentPoint(vertex, vertex.Next, circle);
                    Point tangentPoint2 = CalculateTangentPoint(vertex.Prev, vertex, circle);

                    if (Library.DistanceBetweenPoints(circle.Point, new Point(Convert.ToInt32(tangentPoint1.X), Convert.ToInt32(tangentPoint1.Y))) > circle.Radius - 1
                        && Library.DistanceBetweenPoints(circle.Point, new Point(Convert.ToInt32(tangentPoint1.X), Convert.ToInt32(tangentPoint1.Y))) < circle.Radius + 1)
                    {
                        addingTangent = true;
                        addTangentPoint = vertex;
                        addTangent = tangentPoint1;
                        tangentCircle = circle;
                        break;
                    }
                    else if (Library.DistanceBetweenPoints(circle.Point, new Point(Convert.ToInt32(tangentPoint2.X), Convert.ToInt32(tangentPoint2.Y))) > circle.Radius - 1
                             && Library.DistanceBetweenPoints(circle.Point, new Point(Convert.ToInt32(tangentPoint2.X), Convert.ToInt32(tangentPoint2.Y))) < circle.Radius + 1)
                    {
                        addingTangent = true;
                        addTangentPoint = vertex.Prev;
                        addTangent = tangentPoint2;
                        tangentCircle = circle;
                        break;
                    }
                }
            }
        }

        private void AddFirstPolygon()
        {
            var pol1 = new Polygon();
            InitPolygon(pol1, new Point(100, 100), new Point(300, 100), new Point(300, 400), new Point(100, 400));
            var circle1 = new Circle(new Point(300, 150), 50);
            circles.Add(circle1);

            TangentCircleRelations.Add(new TangentCircleRelation(circle1, pol1.Vertices.First.Next.Value));
            circle1.Locked = true;
        }

        private void AddSecondPolygon()
        {
            var pol2 = new Polygon();
            InitPolygon(pol2, new Point(600, 50), new Point(800, 50), new Point(800, 200), new Point(600, 200));
            selectedRelationType = typeof(SetLengthRelation);
            AddRelationBetweenEdges(pol2.Vertices.First.Value);
            AddRelationBetweenEdges(pol2.Vertices.First.Next.Next.Next.Value);
        }

        private void AddThirdPolygon()
        {
            var pol3 = new Polygon();
            InitPolygon(pol3, new Point(600, 300), new Point(900, 300), new Point(900, 450), new Point(600, 450));
            selectedRelationType = typeof(ParallelRelation);
            AddRelationBetweenEdges(pol3.Vertices.First.Value, pol3.Vertices.First.Next.Next.Value);
        }

        private void AddFourthPolygon()
        {
            var pol4 = new Polygon();
            InitPolygon(pol4, new Point(100, 500), new Point(300, 500), new Point(300, 700), new Point(100, 700));
            var circle1 = new Circle(new Point(300, 600), 50);
            circles.Add(circle1);
            TangentCircleRelations.Add(new TangentCircleRelation(circle1, pol4.Vertices.First.Next.Value));
        }

        private void AddFifthPolygon()
        {
            var pol4 = new Polygon();
            InitPolygon(pol4, new Point(600, 550), new Point(900, 550), new Point(900, 700), new Point(600, 700));
            selectedRelationType = typeof(EqualLengthRelation);
            AddRelationBetweenEdges(pol4.Vertices.First.Value, pol4.Vertices.First.Next.Next.Value);
        }

        private void InitPolygon(Polygon pol, Point p1, Point p2, Point p3, Point p4)
        {
            var vertices = new List<PolygonVertex>();

            vertices.Add(new PolygonVertex(p1));
            vertices.Add(new PolygonVertex(p2));
            vertices.Add(new PolygonVertex(p3));
            vertices.Add(new PolygonVertex(p4));

            var lastInd = vertices.Count - 1;
            vertices[0].Next = vertices[1];
            vertices[0].Prev = vertices[lastInd];
            vertices[0].Polygon = pol;
            vertices[lastInd].Next = vertices[0];
            vertices[lastInd].Prev = vertices[lastInd - 2];
            vertices[lastInd].Polygon = pol;
            for (var i = 1; i < lastInd; i++)
            {
                vertices[i].Prev = vertices[i - 1];
                vertices[i].Next = vertices[i + 1];
                vertices[i].Polygon = pol;
            }

            pol.Vertices = new LinkedList<PolygonVertex>(vertices);
            currentPolygon = pol;
            polygons.Add(pol);
        }
    }
}