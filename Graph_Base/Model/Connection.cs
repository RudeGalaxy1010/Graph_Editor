using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return _vertex1 == vertex;
        }

        public bool EndsWith(Vertex vertex)
        {
            return _vertex2 == vertex;
        }

        public override bool Equals(object obj)
        {
            Connection connection = obj as Connection;

            if (connection != null)
            {
                return connection.Vertex1 == connection.Vertex1 &&
                       connection.Vertex2 == connection.Vertex2 &&
                       connection.Weight == connection.Weight;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
