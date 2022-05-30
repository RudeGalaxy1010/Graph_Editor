using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_Base.Helpers
{
    public class MatrixHelper
    {
        public float[,] GetEmptyMatrix(int sizeX, int sizeY)
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
    }
}
