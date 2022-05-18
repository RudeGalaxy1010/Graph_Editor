using Graph_Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Graph_Editor
{
    public class UpdateController
    {
        private PictureBox _pictureBox;
        private DrawController _drawController;
        public Dictionary<Control, Vertex> VisualVerticies;
        public Dictionary<Control, Connection> VisualWeights;

        #region Events
        public delegate void VertexDeletedDelegate(Control vertex);
        public event VertexDeletedDelegate VertexDeleted;

        public delegate void WeightDeleteDelegate(Control weight);
        public event WeightDeleteDelegate WeightDeleted;
        #endregion

        public UpdateController(PictureBox pictureBox, DrawController drawController)
        {
            _pictureBox = pictureBox;
            _drawController = drawController;
            VisualVerticies = new Dictionary<Control, Vertex>();
            VisualWeights = new Dictionary<Control, Connection>();
        }

        public UpdateController(PictureBox pictureBox, DrawController drawController, Dictionary<Control, Vertex> visualVerticies)
        {
            _pictureBox = pictureBox;
            _drawController = drawController;
            VisualVerticies = visualVerticies;
            VisualWeights = new Dictionary<Control, Connection>();
        }

        public Control GetControlByVertex(Vertex vertex) => VisualVerticies.FirstOrDefault(v => v.Value.Equals(vertex)).Key;
        public Vertex GetVertexByControl(Control control) => VisualVerticies.FirstOrDefault(v => v.Key.Equals(control)).Value;
        public Control GetControlByConnection(Connection connection) => VisualWeights.FirstOrDefault(w => w.Value.Equals(connection)).Key;
        public Connection GetConnectionByControl(Control control) => VisualWeights.FirstOrDefault(w => w.Key.Equals(control)).Value;

        public void AddVertexControl(Control control, Vertex vertex)
        {
            var pair = new KeyValuePair<Control, Vertex>(control, vertex);
            if (VisualVerticies.Contains(pair) == false)
            {
                VisualVerticies.Add(control, vertex);
            }
        }

        public void TryRemoveVertexControl(Control control)
        {
            if (GetVertexByControl(control) == null)
            {
                return;
            }

            VisualVerticies.Remove(control);
            VertexDeleted?.Invoke(control);
        }

        public void AddWeightControl(Control control, Connection connection)
        {
            Control existingControl = GetControlByConnection(connection);

            if (existingControl != null)
            {
                TryRemoveWeightControl(existingControl);
            }

            VisualWeights.Add(control, connection);
        }

        public void TryRemoveWeightControl(Control control)
        {
            if (GetConnectionByControl(control) == null)
            {
                return;
            }

            VisualWeights.Remove(control);
            WeightDeleted?.Invoke(control);
        }

        public void UpdateWith(Graph graph)
        {
            _pictureBox.Image = _drawController.ClearCanvas();
            UpdateVerticies(graph);
            UpdateWeights(graph);
            DrawConnections(graph);
            DrawWeights(graph);
        }

        private void UpdateVerticies(Graph graph)
        {
            List<Control> extraVerticies = (from pair in VisualVerticies
                                            where graph.Vertices.Contains(pair.Value) == false
                                            select pair.Key).ToList();
            for (int i = 0; i < extraVerticies.Count; i++)
            {
                TryRemoveVertexControl(extraVerticies[i]);
            }
        }

        private void UpdateWeights(Graph graph)
        {
            List<Control> extraWeights = (from pair in VisualWeights
                                          where graph.Connections.Contains(pair.Value) == false
                                          select pair.Key).ToList();
            for (int i = 0; i < extraWeights.Count; i++)
            {
                TryRemoveWeightControl(extraWeights[i]);
            }
        }

        private void DrawConnections(Graph graph)
        {
            for (int i = 0; i < graph.Connections.Count; i++)
            {
                Connection connection = graph.Connections[i];
                Control vertex1 = VisualVerticies.FirstOrDefault(v => v.Value.Equals(connection.Vertex1)).Key;
                Control vertex2 = VisualVerticies.FirstOrDefault(v => v.Value.Equals(connection.Vertex2)).Key;

                DrawConnection(vertex1, vertex2, connection.IsDirected);
            }
        }

        private void DrawConnection(Control vertex1, Control vertex2, bool isDirected)
        {
            var vertexCenter1 = new Point(
                vertex1.Location.X - _pictureBox.Location.X + vertex1.Width / 2,
                vertex1.Location.Y - _pictureBox.Location.Y + vertex1.Height / 2);
            var vertexCenter2 = new Point(
                vertex2.Location.X - _pictureBox.Location.X + vertex2.Width / 2,
                vertex2.Location.Y - _pictureBox.Location.Y + vertex2.Height / 2);

            if (isDirected)
            {
                _pictureBox.Image = _drawController.DrawArrowLine(vertexCenter1, vertexCenter2, Color.Black);
            }
            else
            {
                _pictureBox.Image = _drawController.DrawLine(vertexCenter1, vertexCenter2, Color.Black);
            }
        }

        private void DrawWeights(Graph graph)
        {
            for (int i = 0; i < graph.Connections.Count; i++)
            {
                Connection connection = graph.Connections[i];
                Control visualVertex1 = VisualVerticies.FirstOrDefault(v => v.Value.Equals(connection.Vertex1)).Key;
                Control visualVertex2 = VisualVerticies.FirstOrDefault(v => v.Value.Equals(connection.Vertex2)).Key;

                int xDelta = (visualVertex2.Location.X - visualVertex1.Location.X) / 2;
                int yDelta = (visualVertex2.Location.Y - visualVertex1.Location.Y) / 2;
                int xPosition = visualVertex1.Location.X + xDelta;
                int yPosition = visualVertex1.Location.Y + yDelta;

                Control weight = VisualWeights.FirstOrDefault(w => w.Value.Equals(connection)).Key;
                if (weight != null)
                {
                    weight.Text = connection.Weight.ToString();
                    weight.Location = new Point(xPosition, yPosition);
                }
            }
        }
    }
}
