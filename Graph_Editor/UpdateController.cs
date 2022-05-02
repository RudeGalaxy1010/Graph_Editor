using Graph_Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graph_Editor
{
    public class UpdateController
    {
        private PictureBox _pictureBox;
        private DrawController _drawController;
        private Dictionary<Control, Vertex> _visualVerticies;
        private Dictionary<Control, Connection> VisualWeights;

        public UpdateController(PictureBox pictureBox, DrawController drawController)
        {
            _pictureBox = pictureBox;
            _drawController = drawController;
            _visualVerticies = new Dictionary<Control, Vertex>();
            VisualWeights = new Dictionary<Control, Connection>();

            _drawController.DrawArrowLine(new Point(130, 50), new Point(200, 100), Color.Black);
        }

        public Control GetControlByVertex(Vertex vertex) => _visualVerticies.FirstOrDefault(v => v.Value.Equals(vertex)).Key;
        public Vertex GetVertexByControl(Control control) => _visualVerticies[control];

        public void AddVertexControl(Control control, Vertex vertex)
        {
            var pair = new KeyValuePair<Control, Vertex>(control, vertex);
            if (_visualVerticies.Contains(pair) == false)
            {
                _visualVerticies.Add(control, vertex);
            }
        }

        public void RemoveVertexControl(Control control)
        {
            var pair = _visualVerticies.FirstOrDefault(v => v.Key.Equals(control));
            if (pair.Key != null)
            {
                _visualVerticies.Remove(pair.Key);
            }
        }

        public void UpdateWith(Graph graph)
        {
            _pictureBox.Image = _drawController.ClearCanvas();
            UpdateVerticies(graph);
            DrawConnections(graph);
        }

        private void UpdateVerticies(Graph graph)
        {
            List<Control> extraVerticies = (from pair in _visualVerticies
                                           where graph.Vertices.Contains(pair.Value) == false
                                           select pair.Key).ToList();
            for (int i = 0; i < extraVerticies.Count; i++)
            {
                RemoveVertexControl(extraVerticies[i]);
            }
        }

        private void DrawConnections(Graph graph)
        {
            for (int i = 0; i < graph.Connections.Count; i++)
            {
                Connection connection = graph.Connections[i];
                Control vertex1 = _visualVerticies.FirstOrDefault(v => v.Value.Equals(connection.Vertex1)).Key;
                Control vertex2 = _visualVerticies.FirstOrDefault(v => v.Value.Equals(connection.Vertex2)).Key;

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
    }
}
