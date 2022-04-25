using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_Base
{
    public class Vertex
    {
        public int Id { get; private set; }

        public static int IDs = 0;

        public Vertex()
        {
            Id = IDs++;
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
