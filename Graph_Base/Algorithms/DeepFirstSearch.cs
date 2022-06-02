using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_Base.Algorithms
{
    public class DeepFirstSearch
    {
        public int[] Pass(float[,] adjacencyMatrix, int startPointIndex)
        {
            List<int> result = new List<int>();
            Queue<int> queue = new Queue<int>();
            int[] nodes = new int[adjacencyMatrix.GetLength(0)];

            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i] = 0;
            }

            queue.Enqueue(startPointIndex);

            while (queue.Count != 0)
            {
                int node = queue.Dequeue();
                nodes[node] = 2;
                for (int j = 0; j < nodes.Length; j++)
                {
                    if (adjacencyMatrix[node, j] != 0 && nodes[j] == 0)
                    {
                        queue.Enqueue(j);
                        nodes[j] = 1;
                    }
                }
                result.Add(node + 1);
            }

            return result.ToArray();
        }
    }
}
