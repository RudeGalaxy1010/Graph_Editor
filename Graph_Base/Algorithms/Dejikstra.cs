using System.Collections.Generic;

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
        public float[] GetShortestPaths(float[,] adjacencyMatrix, int startPointIndex)
        {
            if (adjacencyMatrix.GetLength(0) == 0)
            {
                return new float[0];
            }

            if (adjacencyMatrix.GetLength(0) != adjacencyMatrix.GetLength(1))
            {
                return new float[0];
            }

            Vertex[] vertices = new Vertex[adjacencyMatrix.GetLength(0)];
            float[] result = new float[vertices.Length];
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

            for (int i = 0; i < vertices.Length; i++)
            {
                result[i] = vertices[i].Weight;
            }

            return result;
        }

        public int[] GetShortestRoute(float[,] adjacencyMatrix, int startPointIndex, int endPointIndex)
        {
            if (adjacencyMatrix.GetLength(0) == 0)
            {
                return new int[0];
            }

            if (adjacencyMatrix.GetLength(0) != adjacencyMatrix.GetLength(1))
            {
                return new int[0];
            }

            float[] shortestDistance = GetShortestPaths(adjacencyMatrix, startPointIndex);
            List<int> result = new List<int>();

            int size = adjacencyMatrix.GetLength(0);
            int[] backwardsRoute = new int[size];
            backwardsRoute[0] = endPointIndex + 1;
            float currentWeight = shortestDistance[endPointIndex];
            int currentVertexIndex = 1;

            int initialEndPointIndex = endPointIndex;
            int steps = 0;

            while (endPointIndex != startPointIndex)
            {
                for (int i = 0; i < size; i++)
                {
                    if (steps >= size && endPointIndex == initialEndPointIndex)
                    {
                        return new int[0];
                    }

                    if (adjacencyMatrix[i, endPointIndex] != 0)
                    {
                        float temp = currentWeight - adjacencyMatrix[i, endPointIndex];
                        if (temp == shortestDistance[i])
                        {
                            currentWeight = temp;
                            endPointIndex = i;
                            backwardsRoute[currentVertexIndex] = i + 1;
                            currentVertexIndex++;
                        }
                    }
                    else
                    {
                        steps++;
                    }
                }
            }

            for (int i = currentVertexIndex - 1; i >= 0; i--)
            {
                if (backwardsRoute[i] != 0)
                {
                    result.Add(backwardsRoute[i]);
                }
            }

            return result.ToArray();
        }
    }
}
