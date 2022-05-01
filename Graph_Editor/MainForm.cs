﻿using Graph_Base;
using Graph_Base.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

namespace Graph_Editor
{
    enum Mode
    {
        Edit,
        Connect,
        Disconnect,
        Weight
    }

    public partial class MainForm : Form
    {
        Graph _graph = new Graph();
        Dictionary<Control, Vertex> _visualVerticies;
        Dictionary<Control, Connection> _visualWeights;
        MouseActions _mouseActions;
        Bitmap _canvas;
        Mode _mode;

        Control? _selectedInContextVertex;

        public MainForm()
        {
            InitializeComponent();

            _visualVerticies = new Dictionary<Control, Vertex>();
            _visualWeights = new Dictionary<Control, Connection>();
            _mouseActions = new MouseActions(_graph, _visualVerticies);
            _canvas = new Bitmap(MainPictureBox.Width, MainPictureBox.Height);

            ChangeMode(Mode.Edit);
            ClearCanvas();
        }

        #region Vertex Control
        private void AddVertexButton_Click(object sender, EventArgs e)
        {
            Vertex vertex = new Vertex();
            _graph.AddVertex(vertex);

            int xSize = 40;
            int ySize = 40;

            Button button = new Button()
            {
                Size = new Size(xSize, ySize),
                Parent = MainPictureBox,
                Location = new Point(ActiveForm.Width / 2, ActiveForm.Height / 2),
                Text = $"{vertex.Id}",
                ContextMenuStrip = VertexContextMenu,
            };

            button.MouseDown += (s, e) => {
                if (_mode == Mode.Edit)
                {
                    _mouseActions.SelectMovable(button);
                }
                else if (_mode == Mode.Connect)
                {
                    _mouseActions.SelectVertex(button);
                }
                else if (_mode == Mode.Disconnect)
                {
                    _mouseActions.SelectVertex(button);
                }
                else if (_mode == Mode.Weight)
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
                        RemoveExtraWeights();
                    }
                }
                else if (_mode == Mode.Weight)
                {
                    if (_mouseActions.BothVertexSelected)
                    {
                        TryCreateWeight(_mouseActions.SelectedVertex1, _mouseActions.SelectedVertex2);
                        _mouseActions.ClearSelectedVerticies();
                    }
                }
            };

            button.MouseMove += (s, e) =>
            {
                _mouseActions.MoveElement(button, MainPictureBox, CoordsText);
                DrawConnections();
                DrawWeights();
            };

            Controls.Add(button);
            button.BringToFront();

            _visualVerticies.Add(button, vertex);
        }
        #endregion

        #region Weight
        private bool TryCreateWeight(Control visualVertex1, Control visualVertex2)
        {
            Vertex vertex1 = _visualVerticies[visualVertex1];
            Vertex vertex2 = _visualVerticies[visualVertex2];
            Connection connection1 = _graph.TryGetConnection(vertex1, vertex2);
            Connection connection2 = _graph.TryGetConnection(vertex1, vertex2);
            Connection connection;

            if (connection1 != null)
            {
                connection = connection1;
            }
            else if (connection2 != null)
            {
                connection = connection2;
            }
            else
            {
                return false;
            }


            int xSize = 50;
            int ySize = 20;

            int xDelta = (visualVertex2.Location.X - visualVertex1.Location.X) / 2;
            int yDelta = (visualVertex2.Location.Y - visualVertex1.Location.Y) / 2;
            int xPosition = visualVertex1.Location.X + xDelta;
            int yPosition = visualVertex1.Location.Y + yDelta;

            TextBox weightText = new TextBox()
            {
                Size = new Size(xSize, ySize),
                Location = new Point(xPosition, yPosition),
                TextAlign = HorizontalAlignment.Center,
                Text = connection.Weight.ToString(),
            };

            weightText.TextChanged += (s, e) => ChangeWeight(weightText);

            Controls.Add(weightText);
            weightText.BringToFront();

            _visualWeights.Add(weightText, connection);

            return true;
        }

        private void ChangeWeight(Control weight)
        {
            Connection connection = _visualWeights[weight];
            float newWeight = connection.Weight;
            if (float.TryParse(weight.Text, out newWeight))
            {
                connection.Weight = newWeight;
            }
            else
            {
                newWeight = connection.Weight;
            }

            weight.Text = newWeight.ToString();
        }

        private void RemoveExtraWeights()
        {
            // TODO: check if need to iterate throw each weight
            var weightConnectionPair = _visualWeights.FirstOrDefault(w => _graph.TryGetConnection(w.Value.Vertex1, w.Value.Vertex2) == null);
            if (weightConnectionPair.Key != null)
            {
                Controls.Remove(weightConnectionPair.Key);
                _visualWeights.Remove(weightConnectionPair.Key);
            }
        }

        private void DrawWeights()
        {
            for (int i = 0; i < _graph.Connections.Count; i++)
            {
                Connection connection = _graph.Connections[i];
                Control visualVertex1 = _visualVerticies.FirstOrDefault(v => v.Value.Equals(connection.Vertex1)).Key;
                Control visualVertex2 = _visualVerticies.FirstOrDefault(v => v.Value.Equals(connection.Vertex2)).Key;

                int xDelta = (visualVertex2.Location.X - visualVertex1.Location.X) / 2;
                int yDelta = (visualVertex2.Location.Y - visualVertex1.Location.Y) / 2;
                int xPosition = visualVertex1.Location.X + xDelta;
                int yPosition = visualVertex1.Location.Y + yDelta;

                Control weight = _visualWeights.FirstOrDefault(w => w.Value.Equals(connection)).Key;
                if (weight != null)
                {
                    weight.Location = new Point(xPosition, yPosition);
                }
            }
        }
        #endregion

        private void ModeButton_Click(object sender, EventArgs e)
        {
            _mouseActions.ClearSelectedVerticies();
            ModeContextMenu.Show(ModeButton, 0, ModeButton.Height);
        }

        #region Drawing
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
                DrawConnection(vertexControl1, vertexControl2, connection.IsDirected);
            }
        }

        private void DrawConnection(Control vertexControl1, Control vertexControl2, bool isDirected)
        {
            Graphics graphics = Graphics.FromImage(_canvas);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            var color = isDirected ? Color.Blue : Color.Black;
            Pen pen = new Pen(color, 2);
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
            RemoveExtraWeights();
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

        private void weightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeMode(Mode.Weight);
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
