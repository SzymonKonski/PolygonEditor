using System;
using System.Drawing;
using Editor.Geometry;

namespace Editor.Relations
{
    public class ParallelRelation : IRelation
    {
        public Edge FirstEdge;
        public Edge SecondEdge;

        public ParallelRelation(PolygonVertex v1, PolygonVertex v3)
        {
            FirstEdge = new Edge(v1, v1.Next);
            SecondEdge = new Edge(v3, v3.Next);

            FirstEdge.V1.MainRelation = this;
            FirstEdge.V2.MinorRelation = this;
            SecondEdge.V1.MainRelation = this;
            SecondEdge.V2.MinorRelation = this;
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

            var slope = Library.GetSlope(movedEdge.V1.Point, movedEdge.V2.Point);

            if (movedVertex == movedEdge.V1)
            {
                if (otherEdge.V1.Moved == false)
                {
                    var p = SetOtherEdgeFirstVertexSlope(slope, otherEdge);
                    var off = new Size(p.X - otherEdge.V1.Point.X, p.Y - otherEdge.V1.Point.Y);
                    return otherEdge.V1.MoveMinor(off);
                }

                if (otherEdge.V2.Moved == false)
                {
                    var p = SetOtherEdgeSecondVertexSlope(slope, otherEdge);
                    var off = new Size(p.X - otherEdge.V2.Point.X, p.Y - otherEdge.V2.Point.Y);
                    return otherEdge.V2.MoveMain(off);
                }

                if (movedEdge.V2.Moved == false) return movedEdge.V2.MoveMain(offset);

                return false;
            }

            if (movedVertex == movedEdge.V2)
            {
                if (otherEdge.V2.Moved == false)
                {
                    var p = SetOtherEdgeSecondVertexSlope(slope, otherEdge);
                    var off = new Size(p.X - otherEdge.V2.Point.X, p.Y - otherEdge.V2.Point.Y);
                    return otherEdge.V2.MoveMain(off);
                }

                if (otherEdge.V1.Moved == false)
                {
                    var p = SetOtherEdgeFirstVertexSlope(slope, otherEdge);
                    var off = new Size(p.X - otherEdge.V1.Point.X, p.Y - otherEdge.V1.Point.Y);
                    return otherEdge.V1.MoveMinor(off);
                }

                if (movedEdge.V1.Moved == false) return movedEdge.V1.MoveMinor(offset);

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

        public Point SetOtherEdgeFirstVertexSlope(float movedEdgeSlope, Edge otherEdge)
        {
            if (otherEdge.V2.Point.X - otherEdge.V1.Point.X == 0) movedEdgeSlope = 10;
            var prevEdge = new Edge(otherEdge.V1.Prev, otherEdge.V1);
            var prevEdgeSlope = Library.GetSlope(prevEdge.V1.Point, prevEdge.V2.Point);
            if (prevEdge.V2.Point.X - prevEdge.V1.Point.X == 0) prevEdgeSlope = 10;
            var k2 = prevEdge.V2.Point.Y - prevEdge.V2.Point.X * prevEdgeSlope;
            var k1 = otherEdge.V2.Point.Y - otherEdge.V2.Point.X * movedEdgeSlope;
            var x = (k1 - k2) / (prevEdgeSlope - movedEdgeSlope);
            var y = x * prevEdgeSlope + k2;
            var p = new Point((int) Math.Round(x), (int) Math.Round(y));

            return p;
        }

        public Point SetOtherEdgeSecondVertexSlope(float movedEdgeSlope, Edge otherEdge)
        {
            if (otherEdge.V1.Point.X - otherEdge.V2.Point.X == 0) movedEdgeSlope = 10;
            var nextEdge = new Edge(otherEdge.V2, otherEdge.V2.Next);
            var nextEdgeSlope = Library.GetSlope(nextEdge.V1.Point, nextEdge.V2.Point);
            if (nextEdge.V1.Point.X - nextEdge.V2.Point.X == 0) nextEdgeSlope = 10;
            var k2 = nextEdge.V1.Point.Y - nextEdge.V1.Point.X * nextEdgeSlope;
            var k1 = otherEdge.V1.Point.Y - otherEdge.V1.Point.X * movedEdgeSlope;
            var x = (k1 - k2) / (nextEdgeSlope - movedEdgeSlope);
            var y = x * nextEdgeSlope + k2;
            var p = new Point((int) Math.Round(x), (int) Math.Round(y));

            return p;
        }
    }
}