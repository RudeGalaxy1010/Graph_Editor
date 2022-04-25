using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Graph_Base;

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

        Control? _selectedElement;
        Point _delta;
        Mode _mode;

        Control? _selectedVertex1;
        Control? _selectedVertex2;
        Dictionary<Control, Vertex> _visualVerticies;

        Bitmap _canvas;

        public MainForm()
        {
            InitializeComponent();

            _selectedElement = null;
            _delta = new Point(0, 0);
            _canvas = new Bitmap(MainPictureBox.Width, MainPictureBox.Height);
            _mode = Mode.Edit;
            _visualVerticies = new Dictionary<Control, Vertex>();

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
                Text = $"{vertex.Id + 1}",
            };

            button.MouseDown += (s, e) => SelectElement(button);
            button.MouseUp += (s, e) => DeselectElement();
            button.MouseMove += (s, e) => MoveElement(button);

            Controls.Add(button);
            button.BringToFront();

            _visualVerticies.Add(button, vertex);
        }

        private void SelectElement(Control element)
        {
            if (_mode == Mode.Edit)
            {
                _selectedElement = element;
                var center = new Point(element.Location.X - element.Width, element.Location.Y - (int)(element.Height * 1.5f));
                _delta = new Point(Cursor.Position.X - center.X, Cursor.Position.Y - center.Y);
            }
            else if (_mode == Mode.Connect)
            {
                if (_selectedVertex1 == null)
                {
                    _selectedVertex1 = element;
                }
                else if (_selectedVertex2 == null)
                {
                    _selectedVertex2 = element;
                }
            }
        }

        private void DeselectElement()
        {
            if (_mode == Mode.Edit)
            {
                _selectedElement = null;
                CoordsText.Text = "";
            }
            else if (_mode == Mode.Connect)
            {
                if (_selectedVertex1 != null && _selectedVertex2 != null)
                {
                    Vertex vertex1 = _visualVerticies[_selectedVertex1];
                    Vertex vertex2 = _visualVerticies[_selectedVertex2];
                    Connection connection = new Connection(vertex1, vertex2, 0);
                    _graph.AddConnection(connection);

                    _selectedVertex1 = null;
                    _selectedVertex2 = null;

                    DrawConnections();
                }
            }
        }

        private void MoveElement(Control element)
        {
            if (element == null || element != _selectedElement)
            {
                return;
            }

            int xPosition = Cursor.Position.X + element.Width - _delta.X;
            int yPosition = Cursor.Position.Y + (int)(element.Height * 1.5f) - _delta.Y;

            int leftBorber = MainPictureBox.Location.X;
            int rightBorder = MainPictureBox.Location.X + MainPictureBox.Width - element.Width;
            int topBorber = MainPictureBox.Location.Y;
            int bottomBorder = MainPictureBox.Location.Y + MainPictureBox.Height - element.Height;

            xPosition = xPosition < leftBorber ? leftBorber : xPosition;
            xPosition = xPosition > rightBorder ? rightBorder : xPosition;
            yPosition = yPosition < topBorber ? topBorber : yPosition;
            yPosition = yPosition > bottomBorder ? bottomBorder : yPosition;

            element.Location = new Point(xPosition, yPosition);
            CoordsText.Text = element.Location.ToString();

            DrawConnections();
        }
        #endregion

        #region Connections
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

                Control vertexControl1 = _visualVerticies.First(v => v.Value == vertex1).Key;
                Control vertexControl2 = _visualVerticies.First(v => v.Value == vertex2).Key;
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
        #endregion

        private void ClearCanvas()
        {
            Graphics graphics = Graphics.FromImage(_canvas);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            graphics.Clear(Color.White);
            MainPictureBox.Image = _canvas;
        }
    }
}
