namespace Graph_Base
{
    public class Connection
    {
        public float Weight { get; private set; }
        public bool IsDirected { get; private set; }

        public Vertex Vertex1 => new Vertex(_vertex1);
        public Vertex Vertex2 => new Vertex(_vertex2);

        private Vertex _vertex1;
        private Vertex _vertex2;

        public Connection(Vertex vertex1, Vertex vertex2, float weight, bool isDirected = false)
        {
            _vertex1 = vertex1;
            _vertex2 = vertex2;
            Weight = weight;
            IsDirected = isDirected;
        }

        public bool StartsWith(Vertex vertex)
        {
            return _vertex1.Equals(vertex);
        }

        public bool EndsWith(Vertex vertex)
        {
            return _vertex2.Equals(vertex);
        }

        public override bool Equals(object obj)
        {
            Connection connection = obj as Connection;

            if (connection != null)
            {
                return Weight == connection.Weight &&
                       ((_vertex1.Equals(connection.Vertex1) &&
                       _vertex2.Equals(connection.Vertex2)) ||
                       (_vertex1.Equals(connection.Vertex2) &&
                       _vertex2.Equals(connection.Vertex1)));
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
