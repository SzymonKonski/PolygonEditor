using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Editor.Geometry;
using Editor.Properties;
using Editor.Relations;

namespace Editor
{
    public partial class MainWindow
    {
        public List<TangentCircleRelation> TangentCircleRelations = new();

        private void AddRelationBetweenEdges(PolygonVertex firstEdgeVertex, PolygonVertex secondEdgeVertex = null)
        {
            if (selectedRelationType == typeof(EqualLengthRelation))
                currentPolygon.EqualLengthRelations.Add(new EqualLengthRelation(firstEdgeVertex, secondEdgeVertex));
            else if (selectedRelationType == typeof(ParallelRelation))
                currentPolygon.ParallelRelations.Add(new ParallelRelation(firstEdgeVertex, secondEdgeVertex));
            else if (selectedRelationType == typeof(SetLengthRelation))
                currentPolygon.SetLengthRelations.Add(new SetLengthRelation(firstEdgeVertex));
        }

        private void AddRelationBetweenCircleAndEdge(Circle circle, PolygonVertex firstEdgeVertex)
        {
            if (selectedRelationType == typeof(TangentCircleRelation))
            {
                if (circle.Locked && circle.RadiusLocked)
                    return;

                TangentCircleRelations.Add(new TangentCircleRelation(circle, firstEdgeVertex));
            }
        }

        private void AddPolygonRelation(Type relationType, int edgeCounter)
        {
            selectedRelationType = relationType;
            chooseSecondEdge = false;

            if (selectedRelationEdgeVertex.HasMainRelation())
            {
                MessageBox.Show(Resources.MoreThanOneRelationError, @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else if (edgeCounter == 1)
            {
                AddRelationBetweenEdges(selectedRelationEdgeVertex);
                selectedRelationType = null;
                selectedRelationEdgeVertex = null;
                chooseSecondEdge = false;
            }
            else if (edgeCounter == 2)
            {
                chooseSecondEdge = true;
            }
        }

        private void AddCircleRelation(Type relationType)
        {
            selectedRelationType = relationType;
            chooseSecondEdge = false;
            if (currentCircle.HasRelation())
                MessageBox.Show(Resources.MoreThanOneRelationError, @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            else
                chooseSecondEdge = true;
        }
    }
}