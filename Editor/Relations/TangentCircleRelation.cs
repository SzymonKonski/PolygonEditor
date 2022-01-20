using System;
using System.Drawing;
using System.Numerics;
using Editor.Geometry;

namespace Editor.Relations
{
    public class TangentCircleRelation : IRelation
    {
        public Circle Circle;
        public Edge Edge;

        public TangentCircleRelation(Circle circle, PolygonVertex v1)
        {
            Circle = circle;
            Edge = new Edge(v1, v1.Next);

            Circle.Relation = this;
            Edge.V1.MainRelation = this;
            Edge.V2.MinorRelation = this;

            PutRelation();
        }

        public bool MaintainRelationEdgeMoved(Edge movedEdge, Size offset)
        {
            if (Circle.Locked)
                MaintainRelationWithRadiusChangeAndLockedPoint();
            else
                Circle.Move(offset);

            return true;
        }

        public bool MaintainRelationVertexMoved(PolygonVertex movedVertex, Size offset)
        {
            var tangentPoint = CalculateTangentPoint();

            if (movedVertex != null)
            {
                if (Circle.Locked)
                {
                    MaintainRelationWithRadiusChangeAndLockedPoint();
                }
                else
                {
                    var vec = new Vector2(Edge.V1.Point.Y - Edge.V2.Point.Y, Edge.V2.Point.X - Edge.V1.Point.X);
                    var normalized = Circle.Radius * Vector2.Normalize(vec);
                    Circle.Point = new Point(Convert.ToInt32(tangentPoint.X - normalized.X),
                        Convert.ToInt32(tangentPoint.Y - normalized.Y));
                }
            }
            else
            {
                if (Circle.RadiusLocked == false)
                {
                    var newRadius = Library.DistanceBetweenPoints(tangentPoint, Circle.Point);
                    Circle.Radius = (float) newRadius;
                }

                var vec = new Vector2(Edge.V1.Point.Y - Edge.V2.Point.Y, Edge.V2.Point.X - Edge.V1.Point.X);
                var normalized = Circle.Radius * Vector2.Normalize(vec);
                Circle.Point = new Point(Convert.ToInt32(tangentPoint.X - normalized.X),
                    Convert.ToInt32(tangentPoint.Y - normalized.Y));
            }

            return true;
        }

        public void RemoveRelation()
        {
            Edge.V1.MainRelation = null;
            Edge.V2.MinorRelation = null;
            Circle.Relation = null;
        }

        public void ChangeRadius()
        {
            var tangentPoint = CalculateTangentPoint();
            var vec = new Vector2(Edge.V1.Point.Y - Edge.V2.Point.Y, Edge.V2.Point.X - Edge.V1.Point.X);
            var normalized = Circle.Radius * Vector2.Normalize(vec);
            Circle.Point = new Point(Convert.ToInt32(tangentPoint.X - normalized.X),
                Convert.ToInt32(tangentPoint.Y - normalized.Y));
        }

        public void PutRelation()
        {
            if (Circle.Locked && Circle.RadiusLocked == false)
            {
                var tangentPoint = CalculateTangentPoint();

                if (Circle.RadiusLocked == false)
                {
                    var newRadius = Library.DistanceBetweenPoints(tangentPoint, Circle.Point);
                    Circle.Radius = (float) newRadius;
                }
            }
            else if (Circle.Locked == false)
            {

                var centerPoint = CalculateTangentPoint();

                var vec = new Vector2(Edge.V1.Point.Y - Edge.V2.Point.Y, Edge.V2.Point.X - Edge.V1.Point.X);
                var normalized = Circle.Radius * Vector2.Normalize(vec);
                Circle.Point = new Point(Convert.ToInt32(centerPoint.X - normalized.X),
                    Convert.ToInt32(centerPoint.Y - normalized.Y));
            }
        }

        public void MaintainRelationWithRadiusChangeAndLockedPoint()
        {
            var tangentPoint = CalculateTangentPoint();

            if (Circle.RadiusLocked == false)
            {
                var newRadius = Library.DistanceBetweenPoints(tangentPoint, Circle.Point);
                Circle.Radius = (float) newRadius;
            }
        }

        public Point CalculateTangentPoint()
        {
            var x1 = Edge.V1.Point.X;
            var y1 = Edge.V1.Point.Y;
            var x2 = Edge.V2.Point.X;
            var y2 = Edge.V2.Point.Y;
            var x3 = Circle.Point.X;
            var y3 = Circle.Point.Y;

            var k = (double) ((y2 - y1) * (x3 - x1) - (x2 - x1) * (y3 - y1)) /
                    ((y2 - y1) * (y2 - y1) + (x2 - x1) * (x2 - x1));
            var x4 = x3 - k * (y2 - y1);
            var y4 = y3 + k * (x2 - x1);

            return new Point((int) x4, (int) y4);
        }
    }
}