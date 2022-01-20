namespace Editor.Geometry
{
    public class Edge
    {
        public Edge(PolygonVertex v1, PolygonVertex v2)
        {
            V1 = v1;
            V2 = v2;
        }

        public PolygonVertex V1 { get; set; }
        public PolygonVertex V2 { get; set; }
    }
}