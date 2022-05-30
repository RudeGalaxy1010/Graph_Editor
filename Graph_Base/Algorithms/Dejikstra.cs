namespace Graph_Base.Algorithms
{
    internal class Vertex
    {
        public float Weight;
        public bool IsPassed;

        internal Vertex()
        {
            Weight = float.MaxValue;
            IsPassed = false;
        }
    }

    public class Dejikstra
    {
        public void Test()
        {
            float[,] graph =    {{0, 1, 4, 0, 2, 0},
                                {0, 0, 0, 9, 0, 0},
                                {4, 0, 0, 7, 0, 0},
                                {0, 9, 7, 0, 0, 2},
                                {0, 0, 0, 0, 0, 8},
                                {0, 0, 0, 0, 0, 0}};

            float[] shortestPath = GetShortestPaths(graph, 0);
        }

        public float[] GetShortestPaths(float[,] adjacencyMatrix, int startPointIndex)
        {
            if (adjacencyMatrix.GetLength(0) != adjacencyMatrix.GetLength(1))
            {
                return new float[0];
            }

            Vertex[] vertices = new Vertex[adjacencyMatrix.GetLength(0)];
            float temp, minWeight;
            int minVertexIndex;

            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = new Vertex();
            }

            vertices[startPointIndex].Weight = 0;

            do
            {
                minVertexIndex = int.MaxValue;
                minWeight = float.MaxValue;

                for (int i = 0; i < vertices.Length; i++)
                {
                    if (vertices[i].IsPassed == false && vertices[i].Weight < minWeight)
                    {
                        minWeight = vertices[i].Weight;
                        minVertexIndex = i;
                    }
                }

                if (minVertexIndex < int.MaxValue)
                {
                    for (int i = 0; i < vertices.Length; i++)
                    {
                        if (adjacencyMatrix[minVertexIndex, i] > 0)
                        {
                            temp = minWeight + adjacencyMatrix[minVertexIndex, i];
                            if (temp < vertices[i].Weight)
                            {
                                vertices[i].Weight = temp;
                            }
                        }
                    }
                    vertices[minVertexIndex].IsPassed = true;
                }
            }
            while (minVertexIndex < int.MaxValue);

            float[] result = new float[vertices.Length];

            for (int i = 0; i < vertices.Length; i++)
            {
                result[i] = vertices[i].Weight;
            }

            return result;
        }
    }
}
