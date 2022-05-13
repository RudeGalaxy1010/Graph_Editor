using Graph_Base.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph_Base
{
    [Serializable]
    public class Graph
    {
        public const int Max_Verticies_Count = 100;

        private List<Vertex> _vertices;
        private List<Connection> _connections;
        private IdController _idController;
        private Algorithms _algorithms;

        public Graph()
        {
            _vertices = new List<Vertex>();
            _connections = new List<Connection>();
            _idController = new IdController();
            _algorithms = new Algorithms();
        }

        public IReadOnlyList<Vertex> Vertices => _vertices;
        public IReadOnlyList<Connection> Connections => _connections;

        public Vertex CreateVertex()
        {
            if (_vertices.Count == Max_Verticies_Count)
            {
                throw new Exception("Max verticies count");
            }

            var newVertex = new Vertex(_idController.GetId());
            _vertices.Add(newVertex);
            return newVertex;
        }

        public void RemoveVertex(Vertex vertex)
        {
            if (_vertices.Contains(vertex))
            {
                _idController.TryRemoveId(vertex.Id);
                _vertices.Remove(vertex);
                List<Connection> connections = FindAnyConnections(vertex).ToList();

                for (int i = 0; i < connections.Count; i++)
                {
                    TryRemoveConnection(connections[i].Vertex1, connections[i].Vertex2);
                }
            }
        }

        public Connection TryCreateConnection(Vertex vertex1, Vertex vertex2, float weight = 1)
        {
            // Create not directed connection
            if (_connections.FirstOrDefault(c => 
                c.Vertex1.Equals(vertex2) 
                && c.Vertex2.Equals(vertex1)
                && c.IsDirected) != null)
            {
                if (TryRemoveConnection(vertex2, vertex1) != null)
                {
                    Connection connection = new Connection(vertex1, vertex2, weight);
                    _connections.Add(connection);
                    return connection;
                }
            }
            // if connection already exists
            else if (_connections.FirstOrDefault(c =>
                c.Vertex1.Equals(vertex1)
                && c.Vertex2.Equals(vertex2)) == null)
            {
                Connection connection = new Connection(vertex1, vertex2, weight, true);
                _connections.Add(connection);
                return connection;
            }

            return null;
        }

        public Connection TryRemoveConnection(Vertex vertex1, Vertex vertex2)
        {
            Connection connection1 = FindExactConnection(vertex1, vertex2);
            Connection connection2 = FindExactConnection(vertex2, vertex1);

            if (connection1 != null)
            {
                _connections.Remove(connection1);
                return connection1;
            }

            if (connection2 != null)
            {
                _connections.Remove(connection2);
                return connection2;
            }

            return null;
        }

        #region Find connections
        public IEnumerable<Connection> FindAnyConnections(Vertex vertex)
        {
            return from connection in _connections
                   where connection.StartsWith(vertex) || connection.EndsWith(vertex)
                   select connection;
        }

        public IEnumerable<Connection> FindExactConnections(Vertex vertex)
        {
            return from connection in _connections
                   where connection.StartsWith(vertex)
                   select connection;
        }

        public Connection FindExactConnection(Vertex vertex1)
        {
            return _connections.FirstOrDefault(c => c.StartsWith(vertex1));
        }

        public Connection FindExactConnection(Vertex vertex1, Vertex vertex2)
        {
            return _connections.FirstOrDefault(c => c.StartsWith(vertex1) && c.EndsWith(vertex2));
        }

        public Connection FindAnyConnection(Vertex vertex1)
        {
            return _connections.FirstOrDefault(c => c.StartsWith(vertex1) || c.EndsWith(vertex1));
        }
        #endregion

        public float[,] GetAdjacencyMatrix()
        {
            float[,] matrix = _algorithms.GetEmptyMatrix(_vertices.Count, _vertices.Count);
            for (int i = 0; i < _vertices.Count; i++)
            {
                List<Connection> connections = FindExactConnections(_vertices[i]).ToList();
                
                for (int j = 0; j < connections.Count; j++)
                {
                    Connection connection = connections[j];
                    matrix[i, _vertices.IndexOf(connection.Vertex2)] = connection.Weight;
                    
                    if (connection.IsDirected == false)
                    {
                        matrix[_vertices.IndexOf(connection.Vertex2), i] = connection.Weight;
                    }
                }
            }

            return matrix;
        }
    }
}
