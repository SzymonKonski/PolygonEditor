using System;
using System.Drawing;
using Editor.Relations;

namespace Editor.Geometry
{
    public class Circle : IComparable<Circle>
    {
        public const double Eps = 1e-6;
        private Point point;

        public Circle(Point point, float radius)
        {
            Point = point;
            Radius = radius;
        }

        public float Radius { get; set; }

        public bool RadiusLocked { get; set; }

        public bool Locked { get; set; }

        public TangentCircleRelation Relation { get; set; }

        public Point Point
        {
            get => point;
            set => point = value;
        }

        public int CompareTo(Circle otherCircle)
        {
            if (otherCircle == null)
                return 1;

            if (Point.X - otherCircle.Point.X < Eps)
                return Point.Y.CompareTo(otherCircle.Point.Y);

            return Point.X.CompareTo(otherCircle.Point.X);
        }

        public bool IsPointInside(Point p)
        {
            if ((p.X - point.X) * (p.X - point.X) + (p.Y - point.Y) * (p.Y - point.Y) <= Radius * Radius)
                return true;

            return false;
        }

        public void ChangeRadius(float radius)
        {
            Radius = radius;

            if (HasRelation() && Locked == false) Relation.ChangeRadius();
        }

        public void MaintainRelationWithRadiusChangeAndLockedPoint()
        {
            Relation.MaintainRelationWithRadiusChangeAndLockedPoint();
        }

        public void Move(Size offset)
        {
            if (Locked)
                return;

            point.Offset(offset.Width, offset.Height);
            if (HasRelation()) Relation.MaintainRelationVertexMoved(null, offset);
        }

        public void Offset(int x, int y)
        {
            point.Offset(x, y);
        }

        public bool HasRelation()
        {
            return Relation != null;
        }
    }
}