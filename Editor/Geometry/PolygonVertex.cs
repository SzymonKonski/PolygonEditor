using System;
using System.Drawing;
using Editor.Relations;

namespace Editor.Geometry
{
    public class PolygonVertex : IComparable<PolygonVertex>
    {
        public const double Eps = 1e-6;
        public static readonly int VertexRadius = 17;
        private Point point;

        public PolygonVertex(Point point, Polygon polygon = null, PolygonVertex prev = null, PolygonVertex next = null)
        {
            this.point = point;
            Polygon = polygon;
            Point = point;
            Prev = prev;
            Next = next;
        }

        public PolygonVertex Prev { get; set; }
        public PolygonVertex Next { get; set; }
        public Polygon Polygon { get; set; }
        public IRelation MinorRelation { get; set; }
        public IRelation MainRelation { get; set; }
        public bool Moved { get; set; }


        public Point Point
        {
            get => point;
            set => point = value;
        }

        public int CompareTo(PolygonVertex otherVertex)
        {
            if (otherVertex == null)
                return 1;

            if (Point.X - otherVertex.Point.X < Eps)
                return Point.Y.CompareTo(otherVertex.Point.Y);

            return Point.X.CompareTo(otherVertex.Point.X);
        }

        public void Offset(int x, int y)
        {
            point.Offset(x, y);
        }

        public void MoveEdge(Size offset)
        {
            var old1 = point;
            var old2 = Next.point;
            var cantMove = false;
            point.Offset(offset.Width, offset.Height);
            Moved = true;
            Next.Offset(offset.Width, offset.Height);
            Next.Moved = true;

            if (HasMainRelation())
            {
                if (MainRelation.MaintainRelationEdgeMoved(new Edge(this, Next), offset))
                {
                }
                else
                {
                    point = old1;
                    Moved = false;
                    Next.point = old2;
                    Next.Moved = false;
                    cantMove = true;
                }
            }

            if (Next.HasMainRelation())
            {
                if (Next.MainRelation.MaintainRelationVertexMoved(Next, offset))
                {
                }
                else
                {
                    point = old1;
                    Moved = false;
                    Next.point = old2;
                    Next.Moved = false;
                    cantMove = true;
                }
            }

            if (HasMinorRelation())
            {
                if (MinorRelation.MaintainRelationVertexMoved(this, offset))
                {
                }
                else
                {
                    point = old1;
                    Moved = false;
                    Next.point = old2;
                    Next.Moved = false;
                    cantMove = true;
                }
            }

            if (cantMove) Polygon.Offset(offset);
        }

        public void Move(Size offset)
        {
            Moved = true;
            var old = point;
            point.Offset(offset.Width, offset.Height);
            var cantMove = false;

            if (HasMainRelation())
            {
                if (MainRelation.MaintainRelationVertexMoved(this, offset))
                {
                }
                else
                {
                    Moved = false;
                    point = old;
                    cantMove = true;
                }
            }

            if (HasMinorRelation())
            {
                if (MinorRelation.MaintainRelationVertexMoved(this, offset))
                {
                }
                else
                {
                    Moved = false;
                    point = old;
                    cantMove = true;
                }
            }

            if (cantMove) Polygon.Offset(offset);
        }

        public bool MoveMain(Size offset)
        {
            if (offset.Height == 0 && offset.Width == 0)
                return true;

            if (Moved)
                return false;

            Moved = true;
            var old = point;
            point.Offset(offset.Width, offset.Height);

            if (HasMainRelation())
            {
                if (MainRelation.MaintainRelationVertexMoved(this, offset)) return true;

                Moved = false;
                point = old;
                return false;
            }

            return true;
        }

        public bool MoveMinor(Size offset)
        {
            if (offset.Height == 0 && offset.Width == 0)
                return true;

            if (Moved)
                return false;

            Moved = true;
            var old = point;
            point.Offset(offset.Width, offset.Height);

            if (HasMinorRelation())
            {
                if (MinorRelation.MaintainRelationVertexMoved(this, offset)) return true;

                Moved = false;
                point = old;
                return false;
            }

            return true;
        }

        public bool HasMainRelation()
        {
            return MainRelation != null;
        }

        public bool HasMinorRelation()
        {
            return MinorRelation != null;
        }
    }
}