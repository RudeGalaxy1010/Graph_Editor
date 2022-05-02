using System;

namespace Graph_Base
{
    public class Vertex
    {
        public int Id { get; private set; }

        public Vertex(int id)
        {
            Id = id;
        }

        public Vertex(Vertex vertex)
        {
            Id = vertex.Id;
        }

        public override bool Equals(object obj)
        {
            Vertex vertex = obj as Vertex;

            if (vertex != null)
            {
                return Id == vertex.Id;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
