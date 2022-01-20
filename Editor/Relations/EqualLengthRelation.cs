using System;
using System.Drawing;
using Editor.Geometry;

namespace Editor.Relations
{
    public class EqualLengthRelation : IRelation
    {
        public Edge FirstEdge;
        public Edge SecondEdge;

        public EqualLengthRelation(PolygonVertex v1, PolygonVertex v3)
        {
            FirstEdge = new Edge(v1, v1.Next);
            SecondEdge = new Edge(v3, v3.Next);

            FirstEdge.V1.MainRelation = this;
            FirstEdge.V2.MinorRelation = this;
            SecondEdge.V1.MainRelation = this;
            SecondEdge.V2.MinorRelation = this;
            PutRelation();
        }

        public bool MaintainRelationEdgeMoved(Edge movedEdge, Size offset)
        {
            return true;
        }

        public bool MaintainRelationVertexMoved(PolygonVertex movedVertex, Size offset)
        {
            Edge movedEdge;
            Edge otherEdge;

            if (offset.Height == 0 && offset.Width == 0)
                return true;

            if (movedVertex == FirstEdge.V1 || movedVertex == FirstEdge.V2)
            {
                movedEdge = FirstEdge;
                otherEdge = SecondEdge;
            }
            else
            {
                movedEdge = SecondEdge;
                otherEdge = FirstEdge;
            }

            var len1 = Library.DistanceBetweenPoints(movedEdge.V1.Point, movedEdge.V2.Point);
            var len2 = Library.DistanceBetweenPoints(otherEdge.V1.Point, otherEdge.V2.Point);

            if (movedVertex == movedEdge.V1)
            {
                if (otherEdge.V1.Moved == false)
                {
                    var p = Library.GetEqualLinesPoint(otherEdge.V2.Point, otherEdge.V1.Point, len1, len2);
                    var off = new Size(p.X - otherEdge.V1.Point.X, p.Y - otherEdge.V1.Point.Y);

                    return otherEdge.V1.MoveMinor(off);
                }

                if (otherEdge.V2.Moved == false)
                {
                    var p = Library.GetEqualLinesPoint(otherEdge.V1.Point, otherEdge.V2.Point, len1, len2);
                    var off = new Size(p.X - otherEdge.V2.Point.X, p.Y - otherEdge.V2.Point.Y);

                    return otherEdge.V2.MoveMain(off);
                }

                if (movedEdge.V2.Moved == false)
                {
                    var p = Library.GetEqualLinesPoint(otherEdge.V2.Point, otherEdge.V1.Point, len1, len2);
                    var off = new Size(-p.X + otherEdge.V1.Point.X, -p.Y + otherEdge.V1.Point.Y);

                    return movedEdge.V2.MoveMain(off);
                }

                return false;
            }

            if (movedVertex == movedEdge.V2)
            {
                if (otherEdge.V1.Moved == false)
                {
                    var p = Library.GetEqualLinesPoint(otherEdge.V2.Point, otherEdge.V1.Point, len1, len2);
                    var off = new Size(p.X - otherEdge.V1.Point.X, p.Y - otherEdge.V1.Point.Y);

                    return otherEdge.V1.MoveMinor(off);
                }

                if (otherEdge.V2.Moved == false)
                {
                    var p = Library.GetEqualLinesPoint(otherEdge.V1.Point, otherEdge.V2.Point, len1, len2);
                    var off = new Size(p.X - otherEdge.V2.Point.X, p.Y - otherEdge.V2.Point.Y);

                    return otherEdge.V2.MoveMain(off);
                }

                if (movedEdge.V1.Moved == false)
                {
                    var p = Library.GetEqualLinesPoint(otherEdge.V1.Point, otherEdge.V2.Point, len1, len2);
                    var off = new Size(-p.X + otherEdge.V2.Point.X, -p.Y + otherEdge.V2.Point.Y);

                    return movedEdge.V1.MoveMinor(off);
                }

                return false;
            }

            return false;
        }

        public void RemoveRelation()
        {
            FirstEdge.V1.MainRelation = null;
            FirstEdge.V2.MinorRelation = null;
            SecondEdge.V1.MainRelation = null;
            SecondEdge.V2.MinorRelation = null;
        }

        public void PutRelation()
        {
            var len1 = Library.DistanceBetweenPoints(FirstEdge.V1.Point, FirstEdge.V2.Point);
            var len2 = Library.DistanceBetweenPoints(SecondEdge.V1.Point, SecondEdge.V2.Point);

            var newLength = (len1 + len2) / 2;
            if (Math.Abs(newLength - len1) > Library.Eps)
            {
                var p1 = Library.GetEqualLinesPoint(FirstEdge.V1.Point, FirstEdge.V2.Point, newLength, len1);
                var off1 = new Size(p1.X - FirstEdge.V2.Point.X, p1.Y - FirstEdge.V2.Point.Y);
                FirstEdge.V2.MoveMain(off1);

                var p2 = Library.GetEqualLinesPoint(SecondEdge.V1.Point, SecondEdge.V2.Point, newLength, len2);
                var off2 = new Size(p2.X - SecondEdge.V2.Point.X, p2.Y - SecondEdge.V2.Point.Y);
                SecondEdge.V2.MoveMain(off2);
            }
        }
    }
}