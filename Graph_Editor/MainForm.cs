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
            _updateController.VertexDeleted += (vertex) => Controls.Remove(vertex);
            _updateController.WeightDeleted += (weight) => Controls.Remove(weight);
        }

        #region Actions
        private void editToolStripMenuItem_Click(object sender, EventArgs e) => _editorController.OnChangeMode(Mode.Edit);
        private void connectToolStripMenuItem_Click(object sender, EventArgs e) => _editorController.OnChangeMode(Mode.Connect);
        private void ModeButton_Click(object sender, EventArgs e) => ModeContextMenu.Show(ModeButton, 0, ModeButton.Height);
        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e) => _editorController.OnChangeMode(Mode.Disconnect);
        private void VertexContextMenu_Opened(object sender, EventArgs e) => _editorController.OnContextMenuSelectVertex((sender as ContextMenuStrip).SourceControl);
        private void deleteVertexToolStripMenuItem_Click(object sender, EventArgs e) => _editorController.OnVertexRemove();
        #endregion

        private void AddVertexButton_Click(object sender, EventArgs e)
        {
            if (_graph.Vertices.Count >= Graph.Max_Verticies_Count)
            {
                return;
            }

            Vertex newVertex;
            Control vertex = CreateVertex(out newVertex);

            vertex.MouseDown += (s, e) => _editorController.OnMouseDown(vertex);
            vertex.MouseUp += (s, e) => _editorController.OnMouseUp(vertex);
            vertex.MouseMove += (s, e) => _editorController.OnMouseMove(MainPictureBox);

            _updateController.AddVertexControl(vertex, newVertex);

            Controls.Add(vertex);
            vertex.BringToFront();
        }

        public Control CreateVertex(out Vertex newVertex)
        {
            Vertex vertex = _graph.CreateVertex();

            Button button = new Button()
            {
                Size = new Size(Vertex_Width, Vertex_Height),
                Parent = MainPictureBox,
                Location = new Point(ActiveForm.Width / 2, ActiveForm.Height / 2),
                Text = $"{vertex.Id}",
                ContextMenuStrip = VertexContextMenu,
            };

            newVertex = vertex;
            return button;
        }

        public Control CreateWeight()
        {
            TextBox weightText = new TextBox()
            {
                Size = new Size(Weight_Text_Width, Weight_Text_Height),
                TextAlign = HorizontalAlignment.Center,
            };

            Controls.Add(weightText);
            weightText.BringToFront();

            return weightText;
        }
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
