using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;
using Editor.Geometry;
using Editor.Properties;
using Editor.Relations;

namespace Editor
{
    public partial class MainWindow
    {
        private readonly Bitmap bitmap;
        private readonly List<Circle> circles = new();
        private readonly List<Polygon> polygons = new();
        private bool chooseSecondEdge;
        private Circle currentCircle;
        private Polygon currentPolygon;
        private bool drawingNewCircleEnabled;
        private bool drawingNewPolygonEnabled;
        private bool drawingNewRadiusEnabled;
        private Point prevMousePosition;
        private PolygonVertex selectedEdgeFirstVertex;
        private PolygonVertex selectedRelationEdgeVertex;
        private Type selectedRelationType;
        private PolygonVertex selectedVertex;
        private Rectangle vertexRect = new() {Size = new Size(PolygonVertex.VertexRadius, PolygonVertex.VertexRadius)};

        private bool addingTangent;
        private PolygonVertex addTangentPoint;
        private Point addTangent;
        private Circle tangentCircle;


        public MainWindow()
        {
            InitializeComponent();

            AddFirstPolygon();
            AddSecondPolygon();
            AddThirdPolygon();
            AddFourthPolygon();
            AddFifthPolygon();
            bitmap = new Bitmap(1920, 1024);
        }


        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            var canvasGraphics = Graphics.FromImage(bitmap);
            using (canvasGraphics)
            {
                canvasGraphics.Clear(Color.White);
                DrawPolygons(canvasGraphics);
                DrawCircles(canvasGraphics);
                DrawIcons(canvasGraphics);

                if (drawingNewPolygonEnabled)
                    DrawNewPolygon(canvasGraphics);
                else if (drawingNewCircleEnabled)
                    DrawNewCircle(canvasGraphics, currentCircle);
                else if (drawingNewRadiusEnabled) DrawNewCircle(canvasGraphics, currentCircle);
            }

            e.Graphics.DrawImage(bitmap, 0, 0);
        }


        // Mouse
        private void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (addingTangent && tangentCircle != null && addTangentPoint != null)
                {
                    TangentCircleRelations.Add(new TangentCircleRelation(tangentCircle, addTangentPoint));
                    stycznaCheckBox.Checked = false;
                    addTangentPoint = null;
                    addingTangent = false;
                    tangentCircle = null;
                }
                else  if (addPolygonRadioButton.Checked)
                {
                    var vertices = new LinkedList<PolygonVertex>();
                    ClearSelectedObjects();
                    currentPolygon = new Polygon(vertices);
                    currentPolygon.Vertices.AddLast(new PolygonVertex(e.Location, currentPolygon));
                    addPolygonRadioButton.Checked = false;
                    drawingNewPolygonEnabled = true;
                    DisableMouseDown();
                }
                else if (drawingNewPolygonEnabled)
                {
                    var vertex = GetVertex(e.Location);
                    if (vertex == null)
                    {
                        var newVertex = new PolygonVertex(e.Location, currentPolygon,
                            currentPolygon.Vertices.Last.Value);

                        currentPolygon.Vertices.Last.Value.Next = newVertex;
                        currentPolygon.Vertices.AddLast(newVertex);
                    }
                    else if (currentPolygon.Vertices.Count >= 3 && vertex.Prev == null)
                    {
                        vertex.Prev = currentPolygon.Vertices.Last.Value;
                        currentPolygon.Vertices.Last.Value.Next = vertex;
                        currentPolygon.ReversePolygon();
                        polygons.Add(currentPolygon);
                        drawingNewPolygonEnabled = false;
                        EnableMouseDown();
                    }
                }
                else if (addCircleRadioButton.Checked)
                {
                    ClearSelectedObjects();
                    currentCircle = new Circle(e.Location, 30);
                    addCircleRadioButton.Checked = false;
                    drawingNewCircleEnabled = true;
                    DisableMouseDown();
                }
                else if (drawingNewCircleEnabled)
                {
                    currentCircle.Radius = (float) Library.DistanceBetweenPoints(e.Location, currentCircle.Point);
                    drawingNewCircleEnabled = false;
                    circles.Add(currentCircle);
                    EnableMouseDown();
                }
                else if (GetVertex(e.Location) == null && divideEdgeRadioButton.Checked)
                {
                    AddNewVertex(e.Location);
                }
                else if (deleteRelationRadioButton.Checked)
                {
                    if (currentPolygon != null)
                    {
                        selectedRelationEdgeVertex = GetEdgeFirstVertexFromPolygon(e.Location, currentPolygon)?.Value;
                        if (selectedRelationEdgeVertex != null && selectedRelationEdgeVertex.HasMainRelation())
                        {
                            RemoveRelations(selectedRelationEdgeVertex.MainRelation);
                            selectedRelationEdgeVertex.MainRelation.RemoveRelation();
                            selectedRelationEdgeVertex = null;
                            chooseSecondEdge = false;
                        }
                    }
                    else if (currentCircle != null && currentCircle.HasRelation())
                    {
                        currentCircle.Relation.RemoveRelation();
                        TangentCircleRelations.RemoveAll(x => x.Circle.Equals(currentCircle));
                    }
                }
                else if (deleteCircleRadioButton.Checked)
                {
                    foreach (var circle in circles)
                    {
                        if (circle.IsPointInside(e.Location))
                        {
                            if (circle.HasRelation()) circle.Relation.RemoveRelation();
                            TangentCircleRelations.RemoveAll(x => x.Circle.Equals(circle));
                            circles.Remove(circle);
                            ClearSelectedObjects();
                            break;
                        }
                    }
                }
                else if (deletePolygonRadioButton.Checked)
                {
                    foreach (var polygon in polygons)
                    {
                        if (polygon.IsPointInside(e.Location))
                        {
                            foreach (var v in polygon.Vertices)
                            {
                                if (v.HasMainRelation()) v.MainRelation.RemoveRelation();

                                if (v.HasMinorRelation()) v.MinorRelation.RemoveRelation();
                            }

                            TangentCircleRelations.RemoveAll(x => x.Edge.V1.Polygon == polygon);
                            polygons.Remove(polygon);
                            ClearSelectedObjects();
                            break;
                        }
                    }
                }
                else if (deleteVertexRadioButton.Checked)
                {
                    Polygon curPolygon = null;
                    foreach (var polygon in polygons)
                    {
                        if (polygon.Vertices.Count <= 3) continue;

                        var min = double.PositiveInfinity;

                        foreach (var vertex in polygon.Vertices)
                        {
                            if (vertex.Point.Equals(e.Location) ||
                                Library.AreVerticesIntersecting(vertex.Point, e.Location, PolygonVertex.VertexRadius))
                            {
                                var currentDist = Library.DistanceBetweenPoints(vertex.Point, e.Location);
                                if (currentDist < min)
                                {
                                    selectedVertex = vertex;
                                    min = currentDist;
                                    curPolygon = polygon;
                                }
                            }
                        }
                    }

                    if (curPolygon != null)
                    {
                        currentPolygon = curPolygon;
                        currentPolygon.Vertices.Remove(selectedVertex);
                        RemoveRelations(selectedVertex.MainRelation);
                        RemoveRelations(selectedVertex.Prev.MainRelation);

                        if (selectedVertex.HasMainRelation()) selectedVertex.MainRelation.RemoveRelation();

                        if (selectedVertex.Prev.HasMainRelation()) selectedVertex.Prev.MainRelation.RemoveRelation();

                        selectedVertex.Next.Prev = selectedVertex.Prev;
                        selectedVertex.Prev.Next = selectedVertex.Next;
                        selectedVertex = null;
                    }
                }
                else if (changeRadiusRadioButton.Checked && currentCircle is {RadiusLocked: false})
                {
                    drawingNewRadiusEnabled = true;
                    changeRadiusRadioButton.Checked = false;
                    DisableMouseDown();
                }
                else if (drawingNewRadiusEnabled && currentCircle != null)
                {
                    currentCircle.ChangeRadius((float) Library.DistanceBetweenPoints(e.Location, currentCircle.Point));
                    drawingNewRadiusEnabled = false;
                    EnableMouseDown();
                }
            }
            else if (e.Button == MouseButtons.Right && ModifierKeys == Keys.Shift && !drawingNewPolygonEnabled &&
                     !drawingNewCircleEnabled
                     && !drawingNewRadiusEnabled)
            {
                if (currentCircle != null)
                {
                    LinkedListNode<PolygonVertex> vertex = null;
                    foreach (var polygon in polygons)
                    {
                        vertex = GetEdgeFirstVertexFromPolygon(e.Location, polygon);
                        if (vertex != null)
                            break;
                    }

                    var edgeVertex = vertex?.Value;
                    var circleAndEdge = currentCircle != null && edgeVertex != null;

                    if (circleAndEdge && chooseSecondEdge && !edgeVertex.HasMainRelation())
                    {
                        AddRelationBetweenCircleAndEdge(currentCircle, edgeVertex);
                        currentCircle = null;
                        chooseSecondEdge = false;
                    }
                    else if (circleAndEdge && chooseSecondEdge)
                    {
                        MessageBox.Show(Resources.MoreThanOneRelationError, @"Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (currentCircle != null && currentCircle.IsPointInside(e.Location))
                        {
                            contextMenuStrip3.Items[0].Enabled = !currentCircle.Locked && !currentCircle.RadiusLocked;
                            contextMenuStrip3.Items[1].Enabled = !currentCircle.RadiusLocked && !currentCircle.Locked;

                            contextMenuStrip3.Items[2].Enabled = currentCircle.Locked;
                            contextMenuStrip3.Items[3].Enabled = currentCircle.RadiusLocked;

                            if (TangentCircleRelations.SingleOrDefault(x => x.Circle.Equals(currentCircle)) == null)
                                contextMenuStrip3.Items[4].Enabled = true;
                            else
                                contextMenuStrip3.Items[4].Enabled = false;

                            contextMenuStrip3.Show(pictureBox, e.Location);
                            chooseSecondEdge = false;
                        }
                    }
                }
                else if (currentPolygon != null)
                {
                    var edgeVertex = GetEdgeFirstVertexFromPolygon(e.Location, currentPolygon)?.Value;
                    var differentEdges = selectedRelationEdgeVertex != null && edgeVertex != null &&
                                         selectedRelationEdgeVertex != edgeVertex;

                    if (differentEdges && chooseSecondEdge && !edgeVertex.HasMainRelation())
                    {
                        AddRelationBetweenEdges(selectedRelationEdgeVertex, edgeVertex);
                        selectedRelationEdgeVertex = null;
                        chooseSecondEdge = false;
                    }
                    else if (differentEdges && chooseSecondEdge)
                    {
                        MessageBox.Show(Resources.MoreThanOneRelationError, @"Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    else
                    {
                        selectedRelationEdgeVertex = edgeVertex;
                        if (selectedRelationEdgeVertex != null)
                        {
                            if (selectedRelationEdgeVertex.HasMainRelation())
                            {
                                MessageBox.Show(Resources.MoreThanOneRelationError, @"Error", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                            }
                            else
                            {
                                chooseSecondEdge = false;
                                contextMenuStrip2.Show(pictureBox, e.Location);
                            }
                        }
                    }
                }
            }
            else if (e.Button == MouseButtons.Right && ModifierKeys == Keys.None)
            {
                drawingNewPolygonEnabled = false;
                drawingNewCircleEnabled = false;
                drawingNewRadiusEnabled = false;
                chooseSecondEdge = false;
                ClearSelectedObjects();
                EnableMouseDown();
            }

            pictureBox.Invalidate();
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && ModifierKeys == Keys.None)
            {
                prevMousePosition = e.Location;
                SelectPolygon(e.Location);

                if (currentPolygon == null && selectedEdgeFirstVertex == null && selectedVertex == null)
                    SelectCircle(e.Location);

                pictureBox.Invalidate();
            }
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if ((moveRadioButton.Checked && drawingNewPolygonEnabled == false && drawingNewCircleEnabled == false && MouseButtons == MouseButtons.Left)){
                var dx = e.Location.X - prevMousePosition.X;
                var dy = e.Location.Y - prevMousePosition.Y;
                var offset = new Size(dx, dy);

                if (selectedEdgeFirstVertex != null)
                {
                    foreach (var vertex in currentPolygon.Vertices) vertex.Moved = false;
                    prevMousePosition = e.Location;
                    selectedEdgeFirstVertex.MoveEdge(offset);

                    MakeTangetMovingEdge(selectedEdgeFirstVertex);

                    pictureBox.Invalidate();
                }

                else if (selectedVertex != null)
                {
                    foreach (var vertex in currentPolygon.Vertices) vertex.Moved = false;
                    prevMousePosition = e.Location;
                    selectedVertex.Move(offset);

                    MakeTangetMovingVertex(selectedVertex);

                    pictureBox.Invalidate();
                }

                else if (currentPolygon != null)
                {
                    prevMousePosition = e.Location;

                    var tangentCircleRelations = TangentCircleRelations.Where(x =>
                        x.Edge.V1.Polygon == currentPolygon && x.Circle.Locked && x.Circle.RadiusLocked);

                    if (!tangentCircleRelations.Any())
                    {
                        currentPolygon.Offset(offset);

                        foreach (var relation in TangentCircleRelations)
                        {
                            if (relation.Edge.V1.Polygon == currentPolygon)
                            {
                                if (relation.Circle.Locked)
                                    relation.Circle.MaintainRelationWithRadiusChangeAndLockedPoint();
                                else
                                    relation.Circle.Offset(offset.Width, offset.Height);
                            }
                        }
                    }
                }

                else if (currentCircle != null)
                {
                    var oldLocation = currentCircle.Point;
                    prevMousePosition = e.Location;

                    try
                    {
                        currentCircle.Move(offset);

                        if (stycznaCheckBox.Checked)
                        {
                            if (addingTangent)
                            {
                                addingTangent = false;
                                addTangentPoint = null;
                                tangentCircle = null;
                            }

                            foreach (var polygon in polygons)
                            {
                                foreach (var vertex in polygon.Vertices)
                                {
                                    var next = vertex.Next;
                                    var x1 = vertex.Point.X;
                                    var y1 = vertex.Point.Y;
                                    var x2 = next.Point.X;
                                    var y2 = next.Point.Y;
                                    var x3 = currentCircle.Point.X;
                                    var y3 = currentCircle.Point.Y;

                                    var k = (double)((y2 - y1) * (x3 - x1) - (x2 - x1) * (y3 - y1)) /
                                            ((y2 - y1) * (y2 - y1) + (x2 - x1) * (x2 - x1));
                                    var x4 = x3 - k * (y2 - y1);
                                    var y4 = y3 + k * (x2 - x1);

                                    Point tangentPoint =  new Point((int)x4, (int)y4);

                                    if (Library.DistanceBetweenPoints(currentCircle.Point, new Point(Convert.ToInt32(x4), Convert.ToInt32(y4))) > currentCircle.Radius - 1
                                        && Library.DistanceBetweenPoints(currentCircle.Point, new Point(Convert.ToInt32(x4), Convert.ToInt32(y4))) < currentCircle.Radius + 1)
                                    {
                                        addingTangent = true;
                                        addTangentPoint = vertex;
                                        addTangent = tangentPoint;
                                        tangentCircle = currentCircle;
                                    }
                                }
                            }
                        }
                    }
                    catch (InvalidOperationException)
                    {
                        currentCircle.Point = oldLocation;
                    }
                }
            }

            pictureBox.Invalidate();
        }


        // ToolStripMenu
        private void lockCircleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (currentCircle != null)
            {
                currentCircle.Locked = true;
                pictureBox.Invalidate();
            }
        }

        private void lockRadiusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentCircle != null)
            {
                currentCircle.RadiusLocked = true;
                pictureBox.Invalidate();
            }
        }

        private void equalLengthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddPolygonRelation(typeof(EqualLengthRelation), 2);
            pictureBox.Invalidate();
        }

        private void setLengthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var newDialog = new Form2((int) Library.DistanceBetweenPoints(selectedRelationEdgeVertex.Point,
                selectedRelationEdgeVertex.Next.Point));

            float xDiff = selectedRelationEdgeVertex.Next.Point.X - selectedRelationEdgeVertex.Point.X;
            float yDiff = selectedRelationEdgeVertex.Next.Point.Y - selectedRelationEdgeVertex.Point.Y;
            var vec = new Vector2(xDiff, yDiff);
            var normVector = Vector2.Normalize(vec);

            if (newDialog.ShowDialog(this) == DialogResult.OK)
            {
                foreach (var vertex in currentPolygon.Vertices) vertex.Moved = false;
                var newLength = newDialog.Length;

                var x2 = selectedRelationEdgeVertex.Point.X + newLength * normVector.X;
                var y2 = selectedRelationEdgeVertex.Point.Y + newLength * normVector.Y;

                var offset = new Size((int) x2 - selectedRelationEdgeVertex.Next.Point.X,
                    (int) y2 - selectedRelationEdgeVertex.Next.Point.Y);
                selectedRelationEdgeVertex.Next.MoveMain(offset);

                newDialog.Dispose();
                AddPolygonRelation(typeof(SetLengthRelation), 1);
                pictureBox.Invalidate();
            }
        }

        private void parallelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentPolygon.Vertices.Count == 3)
                return;

            AddPolygonRelation(typeof(ParallelRelation), 2);
            pictureBox.Invalidate();
        }

        private void tangentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddCircleRelation(typeof(TangentCircleRelation));
            pictureBox.Invalidate();
        }

        private void unlockCircleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentCircle != null)
            {
                currentCircle.Locked = false;
                pictureBox.Invalidate();
            }
        }

        private void unlockRadiusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentCircle != null)
            {
                currentCircle.RadiusLocked = false;
                pictureBox.Invalidate();
            }
        }

        private void stycznaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (stycznaCheckBox.Checked)
                moveRadioButton.Checked = true;
            else
            {
                moveRadioButton.Checked = false;
            }
        }
    }
}