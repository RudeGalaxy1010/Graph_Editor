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
        private PictureBox _pictureBox;
        private Bitmap _canvas;
        private DrawController _drawController;
        private Control _selectedVertex1;
        private Control _selectedVertex2;
        private Control _heldVertex;
        private Point _delta;

        #region Events
        public delegate void ModeChangedDelegate (Mode mode);
        public event ModeChangedDelegate ModeChanged;

        public delegate void BothVerticiesSelectedDelegate(Control vertexControl1, Control vertexControl2);
        public event BothVerticiesSelectedDelegate BothVerticiesSelected;
        #endregion

        public EditorController(PictureBox pictureBox)
        {
            _pictureBox = pictureBox;
            _canvas = new Bitmap(_pictureBox.Width, _pictureBox.Height);
            _drawController = new DrawController();
            Mode = Mode.Edit;

            _pictureBox.Image = _drawController.ClearCanvas(_canvas);
            // TODO: create connection
            BothVerticiesSelected += DrawConnection;
        }

        public Mode Mode { get; private set; }

        #region Handlers
        public void OnChangeMode(Mode mode)
        {
            Mode = mode;
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
                    // Redraw connections
                    break;
                case Mode.Disconnect:
                    DisconnectSelectedVerticies();
                    break;
            }
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

            //DrawConnections();
            //DrawWeights();
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

        private void DrawConnection(Control vertex1, Control vertex2)
        {
            var vertexCenter1 = new Point(
                vertex1.Location.X - _pictureBox.Location.X + vertex1.Width / 2,
                vertex1.Location.Y - _pictureBox.Location.Y + vertex1.Height / 2);
            var vertexCenter2 = new Point(
                vertex2.Location.X - _pictureBox.Location.X + vertex2.Width / 2,
                vertex2.Location.Y - _pictureBox.Location.Y + vertex2.Height / 2);

            _pictureBox.Image = _drawController.DrawLine(_canvas, vertexCenter1, vertexCenter2, Color.Black);
        }

        private void DisconnectSelectedVerticies()
        {
            // TODO: replace
            //_mouseActions.TryCreateConnection();
            // Remove extra weights
        }
    }
}
