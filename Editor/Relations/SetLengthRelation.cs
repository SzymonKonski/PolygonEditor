using System.Drawing;
using Editor.Geometry;

namespace Editor.Relations
{
    public class SetLengthRelation : IRelation
    {
        public Edge Edge;

        public SetLengthRelation(PolygonVertex v1)
        {
            Edge = new Edge(v1, v1.Next);
            Edge.V1.MainRelation = this;
            Edge.V2.MinorRelation = this;
        }

        public bool MaintainRelationEdgeMoved(Edge movedEdge, Size offset)
        {
            return true;
        }

        public bool MaintainRelationVertexMoved(PolygonVertex movedVertex, Size offset)
        {
            if (offset.Height == 0 && offset.Width == 0)
                return true;

            if (Edge.V1 == movedVertex)
            {
                if (Edge.V2.Moved == false) return Edge.V2.MoveMain(offset);

                return false;
            }

            if (Edge.V2 == movedVertex)
            {
                if (Edge.V1.Moved == false) return Edge.V1.MoveMinor(offset);

                return false;
            }

            return false;
        }

        public void RemoveRelation()
        {
            Edge.V1.MainRelation = null;
            Edge.V2.MinorRelation = null;
        }
    }
}