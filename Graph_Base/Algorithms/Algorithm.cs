using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_Base.Algorithms
{
    internal class Algorithm
    {
        public Dejikstra Dejikstra { get; }
        public DeepFirstSearch DeepFirstSearch { get; }

        internal Algorithm()
        {
            Dejikstra = new Dejikstra();
            DeepFirstSearch = new DeepFirstSearch();
        }
    }
}
