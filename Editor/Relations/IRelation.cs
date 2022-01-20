using System.Drawing;
using Editor.Geometry;

namespace Editor.Relations
{
    public interface IRelation
    {
        bool MaintainRelationEdgeMoved(Edge movedEdge, Size offset);
        bool MaintainRelationVertexMoved(PolygonVertex movedVertex, Size offset);
        void RemoveRelation();
    }
}