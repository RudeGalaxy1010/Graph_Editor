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
    public partial class MainForm : Form
    {
        private const int Vertex_Width = 40;
        private const int Vertex_Height = 40;

        private const int Weight_Text_Width = 50;
        private const int Weight_Text_Height = 20;

        private Graph _graph = new Graph();
        private Serializer _serializer;
        private UpdateController _updateController;
        private DrawController _drawController;
        private EditorController _editorController;
        private Bitmap _canvas;

        public MainForm()
        {
            InitializeComponent();

            _canvas = new Bitmap(MainPictureBox.Width, MainPictureBox.Height);
            _drawController = new DrawController(_canvas);
            _updateController = new UpdateController(MainPictureBox, _drawController);
            _editorController = new EditorController(_graph, _updateController);
            _serializer = new Serializer();

            modeToolStripMenuItem.Text = _editorController.Mode.ToString();
            MainPictureBox.SizeMode = PictureBoxSizeMode.Normal;
            MainPictureBox.Image = _canvas;
            MainPictureBox.ContextMenuStrip = PictureBoxContextMenuStrip;
            Subscribe();
        }

        private void Subscribe()
        {
            _editorController.ModeChanged += (mode) => modeToolStripMenuItem.Text = mode.ToString();
            _updateController.VertexDeleted += (vertex) => Controls.Remove(vertex);
            _updateController.WeightDeleted += (weight) => Controls.Remove(weight);
        }

        //private void ResizeCanvas()
        //{
        //    MainPictureBox.Size = new Size(Size.Width - 118, Size.Height - 48);
        //    _canvas = new Bitmap(Size.Width - 118, Size.Height - 48);
        //    MainPictureBox.Image = _canvas;
        //    _updateController.UpdateWith(_graph);
        //}

        #region Actions
        private void editModeToolstripItem_Click(object sender, EventArgs e) => _editorController.OnChangeMode(Mode.Edit);
        private void connectToolstripItem_Click(object sender, EventArgs e) => _editorController.OnChangeMode(Mode.Connect);
        private void disconnectToolstripItem_Click(object sender, EventArgs e) => _editorController.OnChangeMode(Mode.Disconnect);
        private void VertexContextMenu_Opened(object sender, EventArgs e) => _editorController.OnContextMenuSelectVertex((sender as ContextMenuStrip).SourceControl);
        private void deleteVertexToolStripMenuItem_Click(object sender, EventArgs e) => _editorController.OnVertexRemove();
        #endregion

        #region Factory
        private void addVertexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateControl();
        }

        private void addVertexPictureBoxToolstripMenu_Click(object sender, EventArgs e)
        {
            CreateControl();
        }

        public Control CreateControl()
        {
            if (_graph.Vertices.Count >= Graph.Max_Verticies_Count)
            {
                return null;
            }

            Vertex newVertex = _graph.CreateVertex();
            Control vertex = CreateVertex(newVertex);

            vertex.MouseDown += (s, e) => _editorController.OnMouseDown(vertex);
            vertex.MouseUp += (s, e) => _editorController.OnMouseUp(vertex);
            vertex.MouseMove += (s, e) => _editorController.OnMouseMove(MainPictureBox);

            _updateController.AddVertexControl(vertex, newVertex);

            Controls.Add(vertex);
            vertex.BringToFront();

            return vertex;
        }

        public Control CreateControl(Vertex vertex, ControlData control)
        {
            if (_graph.Vertices.Count >= Graph.Max_Verticies_Count)
            {
                return null;
            }

            Control result = CreateVertex(vertex);

            result.MouseDown += (s, e) => _editorController.OnMouseDown(result);
            result.MouseUp += (s, e) => _editorController.OnMouseUp(result);
            result.MouseMove += (s, e) => _editorController.OnMouseMove(MainPictureBox);

            Controls.Add(result);
            result.BringToFront();

            result.Location = new Point(control.XLocation, control.YLocation);
            result.Text = control.Text;
            result.Size = new Size(control.XSize, control.YSize);

            return result;
        }

        public Control CreateVertex(Vertex newVertex)
        {
            Bitmap bitmap = null;
            string path = @$"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName}\Assets\VertexBackgroundImage.png";

            if (File.Exists(path))
            {
                bitmap = new Bitmap(path);
            }

            Button button = new Button()
            {
                Size = new Size(Vertex_Width, Vertex_Height),
                Parent = MainPictureBox,
                Location = new Point(Width / 2, Height / 2),
                Text = $"{newVertex.Id}",
                ContextMenuStrip = VertexContextMenu,
                BackgroundImageLayout = ImageLayout.Stretch,
            };

            if (bitmap != null)
            {
                button.BackgroundImage = bitmap;
            }

            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseDownBackColor = Color.White;
            button.FlatAppearance.MouseOverBackColor = Color.White;
            button.BackColor = Color.White;

            return button;
        }

        public Control CreateWeight()
        {
            TextBox weightText = new TextBox()
            {
                Size = new Size(Weight_Text_Width, Weight_Text_Height),
                TextAlign = HorizontalAlignment.Center,
            };

            weightText.BackColor = Color.White;

            Controls.Add(weightText);
            weightText.BringToFront();

            return weightText;
        }
        #endregion

        #region Graph

        private void adjacencyMatrixToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new Form()
            {
                Width = 500,
                Height = 500,
                MaximumSize = new Size(500, 500),
                MinimumSize = new Size(500, 500),
            };

            var text = new TextBox()
            {
                Text = _graph.GetAdjacencyMatrix().GetTableFormat(),
                Size = new Size(form.Width - 25, form.Height - 80),
                Location = new Point(5, 35),
                Multiline = true,
                TextAlign = HorizontalAlignment.Center,
            };

            form.Controls.Add(text);
            text.BringToFront();
            form.ShowDialog();
        }

        private void shortestDistanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new Form()
            {
                Width = 500,
                Height = 500,
                MaximumSize = new Size(500, 500),
                MinimumSize = new Size(500, 500),
            };

            var text = new TextBox()
            {
                Text = _graph.GetShortestDistanceMatrix().GetTableFormat(),
                Size = new Size(form.Width - 25, form.Height - 80),
                Location = new Point(5, 35),
                Multiline = true,
                TextAlign = HorizontalAlignment.Center,
            };

            form.Controls.Add(text);
            text.BringToFront();
            form.ShowDialog();
        }

        private void shortestRouteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new Form()
            {
                Width = 200,
                Height = 125,
                MaximumSize = new Size(200, 125),
                MinimumSize = new Size(200, 125),
            };

            var label1 = new Label() 
            { 
                Text = "From",
                Location = new Point(5, 5),
            };

            var text1 = new TextBox()
            {
                Size = new Size(form.Width / 2 - 25, form.Height - 80),
                Location = new Point(5, 25),
                Multiline = false,
                TextAlign = HorizontalAlignment.Center,
            };

            var label2 = new Label()
            {
                Text = "To",
                Location = new Point(form.Width / 2 + 5, 5),
            };

            var text2 = new TextBox()
            {
                Size = new Size(form.Width / 2 - 25, form.Height - 80),
                Location = new Point(form.Width / 2 + 5, 25),
                Multiline = false,
                TextAlign = HorizontalAlignment.Center,
            };

            var confirmButton = new Button()
            {
                Text = "Ok",
                Size = new Size(form.Width - 25, 25),
                Location = new Point(5, 55),
            };

            confirmButton.Click += (s, e) =>
            {
                if (ValidatePointIndex(text1.Text) && ValidatePointIndex(text2.Text))
                {
                    int startPointIndex = int.Parse(text1.Text) - 1;
                    int endPointIndex = int.Parse(text2.Text) - 1;
                    ShowShortestRoute(startPointIndex, endPointIndex);
                    form.Close();
                }
            };

            form.Controls.Add(label1);
            form.Controls.Add(label2);
            form.Controls.Add(text1);
            form.Controls.Add(text2);
            form.Controls.Add(confirmButton);
            text1.BringToFront();
            text2.BringToFront();
            form.ShowDialog();
        }

        private void ShowShortestRoute(int startPointIndex, int endPointIndex)
        {
            var form = new Form()
            {
                Width = 500,
                Height = 500,
                MaximumSize = new Size(500, 500),
                MinimumSize = new Size(500, 500),
            };

            var text = new TextBox()
            {
                Text = _graph.GetShortestRoute(startPointIndex, endPointIndex).GetTableFormat(),
                Size = new Size(form.Width - 25, form.Height - 80),
                Location = new Point(5, 35),
                Multiline = true,
                TextAlign = HorizontalAlignment.Center,
            };

            form.Controls.Add(text);
            text.BringToFront();
            form.ShowDialog();
        }

        private bool ValidatePointIndex(string text)
        {
            int index = -1;
            bool isValid = true;

            if (string.IsNullOrEmpty(text) == true 
                || int.TryParse(text, out index) == false)
            {
                isValid = false;
                MessageBox.Show($"Can't parse '{text}' vertex index", "Incorrect input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (index < 1 || index > _graph.Vertices.Count)
                {
                    isValid = false;
                    MessageBox.Show($"Too big '{index}' vertex index", "Incorrect input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return isValid;
        }
        #endregion

        #region Saving
        private void saveScreenshotToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                InitialDirectory = @"c:\Documents",
                Filter = "Json (*.json)|*.json|All files (*.*)|*.*",
                FileName = "Graph",
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var save = new Save();
                save.Graph = _graph;
                save.VerteciesData = new ControlsData()
                {
                    Controls = (from controlPair in _updateController.VisualVerticies
                                select new ControlData(controlPair.Key)).ToList()
                };
                _serializer.Serialize(save, saveFileDialog.FileName);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                InitialDirectory = @"c:\Documents",
                Filter = "Json (*.json)|*.json|All files (*.*)|*.*",
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ClearControls();

                Dictionary<Control, Vertex> visualVerticies = new Dictionary<Control, Vertex>();
                Save save = _serializer.Deserialize<Save>(openFileDialog.FileName);
                _graph = save.Graph;

                for (int i = 0; i < _graph.Vertices.Count; i++)
                {
                    Control control = CreateControl(_graph.Vertices[i], save.VerteciesData.Controls[i]);
                    visualVerticies.Add(control, _graph.Vertices.First(v => v.Id == int.Parse(save.VerteciesData.Controls[i].Text)));
                }

                _updateController = new UpdateController(MainPictureBox, _drawController, visualVerticies);
                _editorController = new EditorController(_graph, _updateController);
                _updateController.UpdateWith(_graph);
                Subscribe();

                // Connections and weights
                for (int i = 0; i < _graph.Connections.Count; i++)
                {
                    Connection connection = _graph.Connections[i];
                    Control control1 = _updateController.GetControlByVertex(connection.Vertex1);
                    Control control2 = _updateController.GetControlByVertex(connection.Vertex2);

                    _editorController.OnChangeMode(Mode.Connect);
                    _editorController.OnMouseDown(control1);
                    _editorController.OnMouseDown(control2);

                    if (connection.IsDirected == false)
                    {
                        _editorController.OnMouseDown(control2);
                        _editorController.OnMouseDown(control1);
                    }

                    _editorController.OnChangeMode(Mode.Edit);
                }
            }
        }
        #endregion

        private void ClearControls()
        {
            foreach (var vertex in _updateController.VisualVerticies)
            {
                Controls.Remove(vertex.Key);
            }

            foreach (var weight in _updateController.VisualWeights)
            {
                Controls.Remove(weight.Key);
            }

            for (int i = 0; i < _updateController.VisualWeights.Count; i++)
            {
                Controls.RemoveAt(i);
            }
        }
    }
}
