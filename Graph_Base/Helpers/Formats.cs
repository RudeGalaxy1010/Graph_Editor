using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_Base.Helpers
{
    public static class Formats
    {
        public static string GetTableFormat<T>(this T[,] array)
        {
            string result = "";

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    result += $"{array[i, j]}\t";
                }
                result += "\n";
            }

            return result;
        }
    }
}
