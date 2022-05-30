using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Graph_Base.Helpers
{
    public static class Formats
    {
        public static string GetTableFormat<T>(this T[,] matrix)
        {
            string result = "";

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    result += $"{matrix[i, j]}" + (j == matrix.GetLength(1) - 1 ? "" : "\t");
                }
                result += Environment.NewLine;
            }

            return result;
        }
    }
}
