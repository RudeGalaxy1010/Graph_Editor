using Graph_Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Graph_Editor
{
    public enum Mode
    {
        Edit,
        Connect,
        Disconnect,
    }

    public class EditorController
    {
        private Graph _graph;
        private UpdateController _updateController;

        private Control _selectedVertex1;
        private Control _selectedVertex2;
        private Control _heldVertex;
        private Control _contextMenuVertex;
        private Point _delta;

        #region Events
        public delegate void ModeChangedDelegate (Mode mode);
        public event ModeChangedDelegate ModeChanged;

        public delegate void BothVerticiesSelectedDelegate(Control vertexControl1, Control vertexControl2);
        public event BothVerticiesSelectedDelegate BothVerticiesSelected;

        public delegate void VertexDeletedDelegate(Control vertex);
        public event VertexDeletedDelegate VertexDeleted;
        #endregion

        public EditorController(Graph graph, UpdateController updateController)
        {
            _graph = graph;
            _updateController = updateController;
            Mode = Mode.Edit;

            BothVerticiesSelected += (v1, v2) => CreateConnection();
            BothVerticiesSelected += (v1, v2) => RemoveConnection();
        }

        public Mode Mode { get; private set; }

        #region Handlers
        public void OnChangeMode(Mode mode)
        {
            Mode = mode;
            _selectedVertex1 = null;
            _selectedVertex2 = null;
            ModeChanged?.Invoke(Mode);
        }

        public void OnMouseDown(Control handler)
        {
            switch (Mode)
            {
                case Mode.Edit:
                    SelectMovable(handler);
                    break;
                case Mode.Connect:
                case Mode.Disconnect:
                    SelectVertex(handler);
                    break;
            }
        }

        public void OnMouseUp(Control handler)
        {
            switch (Mode)
            {
                case Mode.Edit:
                    DeselectElement();
                    break;
                case Mode.Connect:
                case Mode.Disconnect:
                    break;
            }

            _updateController.UpdateWith(_graph);
        }

        public void OnMouseMove(Control box)
        {
            if (_heldVertex == null)
            {
                return;
            }

            int xPosition = Cursor.Position.X + _heldVertex.Width - _delta.X;
            int yPosition = Cursor.Position.Y + (int)(_heldVertex.Height * 1.5f) - _delta.Y;

            int leftBorber = box.Location.X;
            int rightBorder = box.Location.X + box.Width - _heldVertex.Width;
            int topBorber = box.Location.Y;
            int bottomBorder = box.Location.Y + box.Height - _heldVertex.Height;

            xPosition = xPosition < leftBorber ? leftBorber : xPosition;
            xPosition = xPosition > rightBorder ? rightBorder : xPosition;
            yPosition = yPosition < topBorber ? topBorber : yPosition;
            yPosition = yPosition > bottomBorder ? bottomBorder : yPosition;

            _heldVertex.Location = new Point(xPosition, yPosition);
            _updateController.UpdateWith(_graph);
        }

        public void OnContextMenuSelectVertex(Control vertex)
        {
            _contextMenuVertex = vertex;
        }

        public void OnVertexRemove()
        {
            Vertex vertex = _updateController.GetVertexByControl(_contextMenuVertex);
            _graph.RemoveVertex(vertex);
            _updateController.UpdateWith(_graph);
            VertexDeleted?.Invoke(_contextMenuVertex);
            _contextMenuVertex = null;
        }
        #endregion

        private void SelectVertex(Control handler)
        {
            if (_selectedVertex1 == null)
            {
                _selectedVertex1 = handler;
                return;
            }
            else if (_selectedVertex2 == null)
            {
                _selectedVertex2 = handler;
                BothVerticiesSelected?.Invoke(_selectedVertex1, _selectedVertex2);
                _selectedVertex1 = null;
                _selectedVertex2 = null;
                return;
            }
        }

        private void SelectMovable(Control handler)
        {
            _heldVertex = handler;
            var center = new Point(_heldVertex.Location.X - _heldVertex.Width, _heldVertex.Location.Y - (int)(_heldVertex.Height * 1.5f));
            _delta = new Point(Cursor.Position.X - center.X, Cursor.Position.Y - center.Y);
        }

        private void DeselectElement()
        {
            _heldVertex = null;
        }

        private void CreateConnection()
        {
            if (Mode != Mode.Connect)
            {
                return;
            }

            Vertex vertex1 = _updateController.GetVertexByControl(_selectedVertex1);
            Vertex vertex2 = _updateController.GetVertexByControl(_selectedVertex2);
            _graph.TryCreateConnection(vertex1, vertex2);
            _updateController.UpdateWith(_graph);
        }

        private void RemoveConnection()
        {
            if (Mode != Mode.Disconnect)
            {
                return;
            }

            Vertex vertex1 = _updateController.GetVertexByControl(_selectedVertex1);
            Vertex vertex2 = _updateController.GetVertexByControl(_selectedVertex2);
            _graph.TryRemoveConnection(vertex1, vertex2);

            _updateController.UpdateWith(_graph);
        }
    }
}
