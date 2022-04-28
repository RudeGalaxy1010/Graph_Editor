using Graph_Base;
using Graph_Base.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Graph_Editor
{
    enum Mode
    {
        Edit,
        Connect,
        Disconnect,
    }

    public partial class MainForm : Form
    {
        Graph _graph = new Graph();
        Dictionary<Control, Vertex> _visualVerticies;
        MouseActions _mouseActions;
        Bitmap _canvas;
        Mode _mode;

        Control? _selectedInContextVertex;

        public MainForm()
        {
            InitializeComponent();

            _visualVerticies = new Dictionary<Control, Vertex>();
            _mouseActions = new MouseActions(_graph, _visualVerticies);
            _canvas = new Bitmap(MainPictureBox.Width, MainPictureBox.Height);
            _mode = Mode.Edit;
            editToolStripMenuItem_Click(null, null);

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
                else if (_mode == Mode.Disconnect)
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
                else if (_mode == Mode.Disconnect)
                {
                    if (_mouseActions.TryRemoveConnection())
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
        private void ModeButton_Click(object sender, EventArgs e)
        {
            _mouseActions.ClearSelectedVerticies();
            ModeContextMenu.Show(ModeButton, 0, ModeButton.Height);
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

        #region Removing
        private void deleteVertexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_selectedInContextVertex != null)
            {
                Controls.Remove(_selectedInContextVertex);
                Vertex vertex = _visualVerticies[_selectedInContextVertex];
                _visualVerticies.Remove(_selectedInContextVertex);
                _graph.RemoveVertex(vertex);
            }

            DrawConnections();
        }

        private void VertexContextMenu_Opened(object sender, EventArgs e)
        {
            ContextMenuStrip contextMenuStrip = sender as ContextMenuStrip;
            if (contextMenuStrip != null)
            {
                _selectedInContextVertex = contextMenuStrip.SourceControl;
            }
        }
        #endregion

        #region Mode
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeMode(Mode.Edit);
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeMode(Mode.Connect);
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeMode(Mode.Disconnect);
        }

        private void ChangeMode(Mode mode)
        {
            _mode = mode;
            ModeButton.Text = _mode.ToString();
        }
        #endregion

        #region Graph works
        private void AdjacencyMatrixButton_Click(object sender, EventArgs e)
        {
            var form = new Form()
            {
                Width = 500,
                Height = 500,
                MaximumSize = new Size(500, 500),
                MinimumSize = new Size(500, 500),
            };

            var saveButton = new Button()
            {
                Text = "Save",
                Size = new Size(form.Width - 25, 25),
                Location = new Point(5, 5),
            };

            var text = new TextBox()
            {
                Text = _graph.GetAdjacencyMatrix().GetTableFormat(),
                Size = new Size(form.Width - 25, form.Height - 80),
                Location = new Point(5, 35),
                Multiline = true,
                TextAlign = HorizontalAlignment.Center,
            };

            form.Controls.Add(saveButton);
            form.Controls.Add(text);
            text.BringToFront();
            form.ShowDialog();
        }
        #endregion

        #region Saving
        private void ScreenshotButton_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(MainPictureBox.Width, MainPictureBox.Height);
            Graphics graphics = Graphics.FromImage(image);
            // TODO: change capture size
            graphics.CopyFromScreen(ActiveForm.Location, new Point(-(MainPictureBox.Location.X + 8), -30), ActiveForm.Size);
            
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                InitialDirectory = @"c:\Documents",
                Filter = "Image (*.png)|*.png|All files (*.*)|*.*",
                FileName = "Graph"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = saveFileDialog.FileName;
                image.Save(path, ImageFormat.Png);
            }
        }
        #endregion
    }
}
