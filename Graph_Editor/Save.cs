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
        public ControlsData VerteciesData;
    }

    [Serializable]
    public class ControlsData
    {
        public List<ControlData> Controls;
    }

    [Serializable]
    public class ControlData
    {
        public int XLocation;
        public int YLocation;
        public int XSize;
        public int YSize;
        public string Text;

        public ControlData() { }

        public ControlData(Control control)
        {
            XLocation = control.Location.X;
            YLocation = control.Location.Y;
            XSize = control.Size.Width;
            YSize = control.Size.Height;
            Text = control.Text;
        }
    }
}
