using Graph_Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Graph_Editor
{
    enum Mode
    {
        Edit,
        Connect,
    }

    public partial class MainForm : Form
    {
        Graph _graph = new Graph();
        Dictionary<Control, Vertex> _visualVerticies;
        MouseActions _mouseActions;
        Bitmap _canvas;
        Mode _mode;

        Control? _selectedVertex;

        public MainForm()
        {
            InitializeComponent();

            _visualVerticies = new Dictionary<Control, Vertex>();
            _mouseActions = new MouseActions(_graph, _visualVerticies);
            _canvas = new Bitmap(MainPictureBox.Width, MainPictureBox.Height);
            _mode = Mode.Edit;

            ClearCanvas();
        }

        #region Vertex Control
        private void AddVertexButton_Click(object sender, EventArgs e)
        {
            Vertex vertex = new Vertex();
            _graph.AddVertex(vertex);

            Button button = new Button()
            {
                Width = 40,
                Height = 40,
                Parent = MainPictureBox,
                Location = new Point(ActiveForm.Width / 2, ActiveForm.Height / 2),
                Text = $"{vertex.Id}",
                ContextMenuStrip = VertexContextMenu,
            };

            button.MouseDown += (s, e) => {
                if (_mode == Mode.Edit)
                {
                    _mouseActions.SelectElement(button);
                }
                else if (_mode == Mode.Connect)
                {
                    _mouseActions.SelectVertex(button);
                }
            };
            button.MouseUp += (s, e) =>
            {
                if (_mode == Mode.Edit)
                {
                    _mouseActions.DeselectElement(CoordsText);
                }
                else if (_mode == Mode.Connect)
                {
                    if (_mouseActions.TryCreateConnection())
                    {
                        DrawConnections();
                    }
                }
            };
            button.MouseMove += (s, e) =>
            {
                _mouseActions.MoveElement(button, MainPictureBox, CoordsText);
                DrawConnections();
            };

            Controls.Add(button);
            button.BringToFront();

            _visualVerticies.Add(button, vertex);
        }
        #endregion

        #region Connections and drawing
        private void ConnectButton_Click(object sender, EventArgs e)
        {
            ConnectButton.Text = _mode.ToString();

            if (_mode == Mode.Edit)
            {
                _mode = Mode.Connect;
            }
            else if (_mode == Mode.Connect)
            {
                _mode = Mode.Edit;
            }
        }

        private void DrawConnections()
        {
            ClearCanvas();

            for (int i = 0; i < _graph.Connections.Count; i++)
            {
                label1.Text = "Cons: " + (i + 1);
                Connection connection = _graph.Connections[i];
                Vertex vertex1 = connection.Vertex1;
                Vertex vertex2 = connection.Vertex2;

                Control vertexControl1 = _visualVerticies.First(v => v.Value.Equals(vertex1)).Key;
                Control vertexControl2 = _visualVerticies.First(v => v.Value.Equals(vertex2)).Key;
                DrawConnection(vertexControl1, vertexControl2);
            }
        }

        private void DrawConnection(Control vertexControl1, Control vertexControl2)
        {
            Graphics graphics = Graphics.FromImage(_canvas);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            Pen pen = new Pen(Color.Black, 2);
            var vertexCenter1 = new Point(
                vertexControl1.Location.X - MainPictureBox.Location.X + vertexControl1.Width / 2,
                vertexControl1.Location.Y - MainPictureBox.Location.Y + vertexControl1.Height / 2);
            var vertexCenter2 = new Point(
                vertexControl2.Location.X - MainPictureBox.Location.X + vertexControl2.Width / 2,
                vertexControl2.Location.Y - MainPictureBox.Location.Y + vertexControl2.Height / 2);

            graphics.DrawLine(pen, vertexCenter1, vertexCenter2);
            MainPictureBox.Image = _canvas;
        }

        private void ClearCanvas()
        {
            Graphics graphics = Graphics.FromImage(_canvas);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            graphics.Clear(Color.White);
            MainPictureBox.Image = _canvas;
        }
        #endregion

        private void deleteVertexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedVertex != null)
            {
                Controls.Remove(_selectedVertex);
                Vertex vertex = _visualVerticies[_selectedVertex];
                _visualVerticies.Remove(_selectedVertex);
                _graph.RemoveVertex(vertex);
            }

            DrawConnections();
        }

        private void VertexContextMenu_Opened(object sender, EventArgs e)
        {
            ContextMenuStrip contextMenuStrip = sender as ContextMenuStrip;
            _selectedVertex = contextMenuStrip.SourceControl;
        }
    }
}
