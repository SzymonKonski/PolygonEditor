using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Editor.Relations;

namespace Editor.Geometry
{
    public class Polygon
    {
        public List<EqualLengthRelation> EqualLengthRelations = new();
        public List<ParallelRelation> ParallelRelations = new();
        public List<SetLengthRelation> SetLengthRelations = new();

        public Polygon(LinkedList<PolygonVertex> polygonVertices = null)
        {
            Vertices = polygonVertices;
        }

        public LinkedList<PolygonVertex> Vertices { get; set; }

        public void Offset(Size offset)
        {
            foreach (var vertex in Vertices) vertex.Point += offset;
        }

        public void ReversePolygon()
        {
            var sum = 0;
            foreach (var v in Vertices) sum += (v.Next.Point.X - v.Point.X) * (v.Next.Point.Y + v.Point.Y);

            if (sum > 0)
            {
                var reversedPolygon = Vertices.Reverse();
                Vertices = new LinkedList<PolygonVertex>(reversedPolygon);

                foreach (var vertex in Vertices) (vertex.Next, vertex.Prev) = (vertex.Prev, vertex.Next);
            }
        }

        public bool IsPointInside(Point point)
        {
            var points = Vertices.Select(v => v.Point).ToList();
            var pointInside = false;
            var j = points.Count - 1;

            for (var i = 0; i < points.Count; i++)
            {
                if (points[i].Y >= point.Y && points[j].Y < point.Y ||
                    points[j].Y >= point.Y && points[i].Y < point.Y)
                {
                    var ax = points[i].X + (point.Y - points[i].Y) * (points[j].X - points[i].X)
                        / (double) (points[j].Y - points[i].Y);

                    if (ax < point.X)
                        pointInside = !pointInside;
                }

                j = i;
            }

            return pointInside;
        }
    }
}