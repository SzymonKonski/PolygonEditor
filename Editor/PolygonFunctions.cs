using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Editor.Geometry;
using Editor.Relations;

namespace Editor
{
    public partial class MainWindow : Form
    {
        private void SelectPolygon(Point mouseClickPoint)
        {
            selectedEdgeFirstVertex = null;
            selectedVertex = null;
            selectedVertex = GetVertex(mouseClickPoint);

            if (selectedVertex == null)
            {
                selectedEdgeFirstVertex = GetEdgeFirstVertexFromPolygon(mouseClickPoint, currentPolygon)?.Value;
                if (selectedEdgeFirstVertex == null)
                {
                    foreach (var polygon in polygons)
                    {
                        if (polygon.IsPointInside(mouseClickPoint))
                        {
                            currentPolygon = polygon;
                            currentCircle = null;
                            return;
                        }
                    }

                    currentPolygon = null;
                    currentCircle = null;
                }
                else
                {
                    currentCircle = null;
                }
            }
            else
            {
                currentCircle = null;
            }
        }

        private void DrawNewPolygon(Graphics canvasGraphics)
        {
            var mousePosition = pictureBox.PointToClient(MousePosition);
            foreach (var vertex in currentPolygon.Vertices)
            {
                bitmap.DrawLine(vertex.Point, vertex.Next != null ? vertex.Next.Point : mousePosition, Color.Black);
                var offset = new Size(-PolygonVertex.VertexRadius / 2, -PolygonVertex.VertexRadius / 2);
                vertexRect.Location = vertex.Point + offset;
                canvasGraphics.FillEllipse(Brushes.Black, vertexRect);
            }
        }

        private void DrawPolygons(Graphics canvasGraphics)
        {
            foreach (var polygon in polygons)
            {
                var color = polygon == currentPolygon ? Brushes.PowderBlue : Brushes.White;
                canvasGraphics.FillPolygon(color, polygon.Vertices.Select(v => v.Point).ToArray());
                var i = 0;
                foreach (var vertex in polygon.Vertices)
                {
                    bitmap.DrawLine(vertex.Point, vertex.Next.Point, Equals(vertex, selectedVertex) ? Color.Red : Color.Black);

                    var offset = new Size(-PolygonVertex.VertexRadius / 2, -PolygonVertex.VertexRadius / 2);
                    vertexRect.Location = vertex.Point + offset;
                    canvasGraphics.FillEllipse(Equals(vertex, selectedVertex) ? Brushes.Red : Brushes.Black,
                        vertexRect);
                    canvasGraphics.DrawString(i.ToString(), Font, Brushes.Black,
                        vertexRect.Location.X + PolygonVertex.VertexRadius,
                        vertexRect.Location.Y + PolygonVertex.VertexRadius);
                    i++;
                }
            }
        }

        private LinkedListNode<PolygonVertex> GetEdgeFirstVertexFromPolygon(Point mouseClickPoint, Polygon polygon)
        {
            const double eps = 0.5;
            if (polygon == null) return null;

            for (var currentNode = polygon.Vertices.First; currentNode != null; currentNode = currentNode.Next)
            {
                var vertex = currentNode.Value;
                var dist1 = Library.DistanceBetweenPoints(vertex.Point, mouseClickPoint);
                var dist2 = Library.DistanceBetweenPoints(mouseClickPoint, vertex.Next.Point);
                var edgeLength = Library.DistanceBetweenPoints(vertex.Point, vertex.Next.Point);
                var dist = Math.Abs(dist1 + dist2 - edgeLength);
                if (dist < eps) return currentNode;
            }

            return null;
        }

        private PolygonVertex GetVertex(Point mouseClickPoint)
        {
            PolygonVertex polygonVertex = null;
            var min = double.PositiveInfinity;

            if (currentPolygon == null)
                return null;

            foreach (var vertex in currentPolygon.Vertices)
            {
                if (Library.AreVerticesIntersecting(vertex.Point, mouseClickPoint, PolygonVertex.VertexRadius) ||
                    vertex.Point.Equals(mouseClickPoint))
                {
                    var currentDist = Library.DistanceBetweenPoints(vertex.Point, mouseClickPoint);
                    if (currentDist < min)
                    {
                        polygonVertex = vertex;
                        min = currentDist;
                    }
                }
            }

            return polygonVertex;
        }

        private void AddNewVertex(Point mouseClickPoint)
        {
            var edgeFirstVertex = GetEdgeFirstVertexFromPolygon(mouseClickPoint, currentPolygon);

            if (edgeFirstVertex == null)
                return;

            var prevVertex = edgeFirstVertex.Value;
            var nextVertex = prevVertex.Next;

            RemoveRelations(edgeFirstVertex.Value.MainRelation);

            if (edgeFirstVertex.Value.HasMainRelation())
                edgeFirstVertex.Value.MainRelation.RemoveRelation();

            var newVertex = new PolygonVertex(mouseClickPoint, currentPolygon, prevVertex, nextVertex);
            prevVertex.Next = newVertex;
            nextVertex.Prev = newVertex;
            currentPolygon.Vertices.AddAfter(edgeFirstVertex, newVertex);
            pictureBox.Invalidate();
        }

        private void RemoveRelations(IRelation relation)
        {
            if (relation == null)
                return;

            if (relation.GetType() == typeof(ParallelRelation))
                currentPolygon.ParallelRelations.Remove((ParallelRelation) relation);
            else if (relation.GetType() == typeof(EqualLengthRelation))
                currentPolygon.EqualLengthRelations.Remove((EqualLengthRelation) relation);
            else if (relation.GetType() == typeof(SetLengthRelation))
                currentPolygon.SetLengthRelations.Remove((SetLengthRelation) relation);
            else if (relation.GetType() == typeof(TangentCircleRelation))
                TangentCircleRelations.Remove((TangentCircleRelation) relation);
        }
    }
}