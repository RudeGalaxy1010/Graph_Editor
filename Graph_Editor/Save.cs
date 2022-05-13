using Graph_Base;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Graph_Editor
{
    [Serializable]
    public class Save
    {
        public Graph Graph;
        public List<Control> Verticies;
    }
}
