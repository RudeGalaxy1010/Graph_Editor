using Graph_Base;
using Graph_Base.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
        private UpdateController _updateController;
        private DrawController _drawController;
        EditorController _editorController;
        Bitmap _canvas;

        public MainForm()
        {
            InitializeComponent();

            _canvas = new Bitmap(MainPictureBox.Width, MainPictureBox.Height);
            _drawController = new DrawController(_canvas);
            _updateController = new UpdateController(MainPictureBox, _drawController);
            _editorController = new EditorController(_graph, _updateController);

            ModeButton.Text = _editorController.Mode.ToString();
            MainPictureBox.Image = _canvas;
            Subscribe();
        }

        private void Subscribe()
        {
            _editorController.ModeChanged += (mode) => ModeButton.Text = mode.ToString();
            _editorController.VertexDeleted += (vertex) => Controls.Remove(vertex);
        }

        #region Actions
        private void editToolStripMenuItem_Click(object sender, EventArgs e) => _editorController.OnChangeMode(Mode.Edit);
        private void connectToolStripMenuItem_Click(object sender, EventArgs e) => _editorController.OnChangeMode(Mode.Connect);
        private void ModeButton_Click(object sender, EventArgs e) => ModeContextMenu.Show(ModeButton, 0, ModeButton.Height);
        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e) => _editorController.OnChangeMode(Mode.Disconnect);
        private void VertexContextMenu_Opened(object sender, EventArgs e) => _editorController.OnContextMenuSelectVertex((sender as ContextMenuStrip).SourceControl);
        private void deleteVertexToolStripMenuItem_Click(object sender, EventArgs e) => _editorController.OnVertexRemove();
        #endregion

        #region Vertex Control
        private void AddVertexButton_Click(object sender, EventArgs e)
        {
            if (_graph.Vertices.Count >= Graph.Max_Verticies_Count)
            {
                return;
            }

            Vertex vertex = _graph.CreateVertex();

            Button button = new Button()
            {
                Size = new Size(Vertex_Width, Vertex_Height),
                Parent = MainPictureBox,
                Location = new Point(ActiveForm.Width / 2, ActiveForm.Height / 2),
                Text = $"{vertex.Id}",
                ContextMenuStrip = VertexContextMenu,
            };

            button.MouseDown += (s, e) => _editorController.OnMouseDown(button);
            button.MouseUp += (s, e) => _editorController.OnMouseUp(button);
            button.MouseMove += (s, e) => _editorController.OnMouseMove(MainPictureBox);

            _updateController.AddVertexControl(button, vertex);

            Controls.Add(button);
            button.BringToFront();
        }
        #endregion

        #region Weight
        private bool TryCreateWeight(Control visualVertex1, Control visualVertex2)
        {
            //Vertex vertex1 = _visualVerticies[visualVertex1];
            //Vertex vertex2 = _visualVerticies[visualVertex2];
            //Connection connection1 = _graph.FindExactConnection(vertex1, vertex2);
            //Connection connection2 = _graph.FindExactConnection(vertex2, vertex1);
            //Connection connection;

            //if (connection1 != null)
            //{
            //    connection = connection1;
            //}
            //else if (connection2 != null)
            //{
            //    connection = connection2;
            //}
            //else
            //{
            //    return false;
            //}

            int xDelta = (visualVertex2.Location.X - visualVertex1.Location.X) / 2;
            int yDelta = (visualVertex2.Location.Y - visualVertex1.Location.Y) / 2;
            int xPosition = visualVertex1.Location.X + xDelta;
            int yPosition = visualVertex1.Location.Y + yDelta;

            TextBox weightText = new TextBox()
            {
                Size = new Size(Weight_Text_Width, Weight_Text_Height),
                Location = new Point(xPosition, yPosition),
                TextAlign = HorizontalAlignment.Center,
                //Text = connection.Weight.ToString(),
            };

            weightText.TextChanged += (s, e) => ChangeWeight(weightText);

            Controls.Add(weightText);
            weightText.BringToFront();

            //_visualWeights.Add(weightText, connection);

            return true;
        }

        private void ChangeWeight(Control weight)
        {
            //Connection connection = _visualWeights[weight];
            //float newWeight = connection.Weight;
            //if (float.TryParse(weight.Text, out newWeight))
            //{
            //    connection.Weight = newWeight;
            //}
            //else
            //{
            //    newWeight = connection.Weight;
            //}

            //weight.Text = newWeight.ToString();
        }

        private void RemoveExtraWeights()
        {
            // TODO: check if need to iterate throw each weight
            //var weightConnectionPair = _visualWeights.FirstOrDefault(w => _graph.FindExactConnection(w.Value.Vertex1, w.Value.Vertex2) == null);
            //var con = _graph.FindExactConnection(weightConnectionPair.Value.Vertex1, weightConnectionPair.Value.Vertex2);
            //if (weightConnectionPair.Key != null)
            //{
            //    Controls.Remove(weightConnectionPair.Key);
            //    _visualWeights.Remove(weightConnectionPair.Key);
            //}
        }

        private void DrawWeights()
        {
            for (int i = 0; i < _graph.Connections.Count; i++)
            {
                //Connection connection = _graph.Connections[i];
                //Control visualVertex1 = _visualVerticies.FirstOrDefault(v => v.Value.Equals(connection.Vertex1)).Key;
                //Control visualVertex2 = _visualVerticies.FirstOrDefault(v => v.Value.Equals(connection.Vertex2)).Key;

                //int xDelta = (visualVertex2.Location.X - visualVertex1.Location.X) / 2;
                //int yDelta = (visualVertex2.Location.Y - visualVertex1.Location.Y) / 2;
                //int xPosition = visualVertex1.Location.X + xDelta;
                //int yPosition = visualVertex1.Location.Y + yDelta;

                //Control weight = _visualWeights.FirstOrDefault(w => w.Value.Equals(connection)).Key;
                //if (weight != null)
                //{
                //    weight.Location = new Point(xPosition, yPosition);
                //}
            }
        }
        #endregion

        #region Graph
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
