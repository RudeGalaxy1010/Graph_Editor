using System.Collections.Generic;
using System.Linq;

namespace Graph_Base
{
    public class Graph
    {
        public IReadOnlyList<Vertex> Vertices => _vertices;
        public IReadOnlyList<Connection> Connections => _connections;

        private List<Vertex> _vertices;
        private List<Connection> _connections;
        private List<int> _ids;

        public Graph()
        {
            _vertices = new List<Vertex>();
            _connections = new List<Connection>();
            _ids = new List<int>();
        }

        public void AddVertex(Vertex vertex)
        {
            if (vertex != null)
            {
                vertex.Id = GetId();
                _vertices.Add(vertex);
            }
        }

        public void RemoveVertex(Vertex vertex)
        {
            if (_vertices.Contains(vertex))
            {
                _ids.Remove(vertex.Id);
                _vertices.Remove(vertex);
                List<Connection> connections = FindConnections(vertex).ToList();
                _connections = _connections.Except(connections).ToList();
            }
        }

        public bool TryCreateConnection(Vertex vertex1, Vertex vertex2, float weight = 1)
        {
            // Create not directed connection
            if (_connections.FirstOrDefault(c => 
                c.Vertex1.Equals(vertex2) 
                && c.Vertex2.Equals(vertex1)
                && c.IsDirected) != null)
            {
                if (TryRemoveConnection(vertex2, vertex1))
                {
                    Connection connection = new Connection(vertex1, vertex2, weight);
                    _connections.Add(connection);
                    return true;
                }
            }
            // if connection already exists
            else if (_connections.FirstOrDefault(c =>
                c.Vertex1.Equals(vertex1)
                && c.Vertex2.Equals(vertex2)) == null)
            {
                Connection connection = new Connection(vertex1, vertex2, weight, true);
                _connections.Add(connection);
                return true;
            }

            return false;
        }

        public bool TryRemoveConnection(Vertex vertex1, Vertex vertex2)
        {
            Connection connection1 = TryGetConnection(vertex1, vertex2);
            Connection connection2 = TryGetConnection(vertex2, vertex1);

            if (connection1 != null && connection2 != null)
            {
                _connections.Remove(connection1);
                _connections.Remove(connection2);
                return true;
            }

            if (connection1 != null)
            {
                _connections.Remove(connection1);
                return true;
            }

            if (connection2 != null)
            {
                _connections.Remove(connection2);
                return true;
            }

            return false;
        }

        public IEnumerable<Connection> FindConnections(Vertex vertex)
        {
            return from connection in _connections
                   where connection.StartsWith(vertex) || connection.EndsWith(vertex)
                   select connection;
        }

        public Connection TryGetConnection(Vertex vertex1, Vertex vertex2)
        {
            return _connections.FirstOrDefault(c => c.StartsWith(vertex1) && c.EndsWith(vertex2));
        }

        public float[,] GetAdjacencyMatrix()
        {
            float[,] matrix = GetEmptyMatrix(_vertices.Count, _vertices.Count);
            for (int i = 0; i < _vertices.Count; i++)
            {
                // TODO: fix weights
                Connection connection = Connections.FirstOrDefault(c => c.StartsWith(_vertices[i]));

                if (connection == null)
                {
                    continue;
                }

                matrix[i, _vertices.IndexOf(connection.Vertex2)] = connection.Weight;

                if (connection.IsDirected == false)
                {
                    matrix[_vertices.IndexOf(connection.Vertex2), i] = connection.Weight;
                }
            }

            return matrix;
        }

        private float[,] GetEmptyMatrix(int sizeX, int sizeY)
        {
            float[,] matrix = new float[sizeX, sizeY];
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    matrix[i, j] = 0;
                }
            }

            return matrix;
        }

        private int GetId()
        {
            for (int i = 0; i < _ids.Count + 1; i++)
            {
                int id = i + 1;
                if (_ids.Contains(id) == false)
                {
                    _ids.Add(id);
                    return id;
                }
            }

            return -1;
        }
    }
}
