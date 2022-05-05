using Graph_Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Graph_Editor
{
    public class MouseActions
    {
        Graph _graph;
        Dictionary<Control, Vertex> _visualVerticies;

        Control? _selectedMovable = null;
        Control? _selectedVertex1 = null;
        Control? _selectedVertex2 = null;
        Point _delta = new Point();

        public bool BothVertexSelected => _selectedVertex1 != null && _selectedVertex2 != null;
        public Control SelectedVertex1 => _selectedVertex1;
        public Control SelectedVertex2 => _selectedVertex2;

        public MouseActions(Graph graph, Dictionary<Control, Vertex> visualVerticies)
        {
            _graph = graph;
            _visualVerticies = visualVerticies;
            ClearSelectedVerticies();
        }

        public void SelectMovable(Control element)
        {
            _selectedMovable = element;
            var center = new Point(element.Location.X - element.Width, element.Location.Y - (int)(element.Height * 1.5f));
            _delta = new Point(Cursor.Position.X - center.X, Cursor.Position.Y - center.Y);
        }

        public void SelectVertex(Control element)
        {
            if (_selectedVertex1 == null)
            {
                _selectedVertex1 = element;
                _selectedVertex2 = null;
            }
            else if (_selectedVertex2 == null)
            {
                _selectedVertex2 = element;
            }
        }

        public void DeselectElement(Label? coordsText = null)
        {
            _selectedMovable = null;
            if (coordsText != null)
            {
                coordsText.Text = "";
            }
        }

        public bool TryCreateConnection()
        {
            if (_selectedVertex1 != null && _selectedVertex2 != null)
            {
                Vertex vertex1 = _visualVerticies[_selectedVertex1];
                Vertex vertex2 = _visualVerticies[_selectedVertex2];
                if (_graph.TryCreateConnection(vertex1, vertex2) != null)
                {
                    ClearSelectedVerticies();
                    return true;
                }
            }

            return false;
        }

        public bool TryRemoveConnection()
        {
            if (_selectedVertex1 != null && _selectedVertex2 != null)
            {
                Vertex vertex1 = _visualVerticies[_selectedVertex1];
                Vertex vertex2 = _visualVerticies[_selectedVertex2];

                _graph.TryRemoveConnection(vertex1, vertex2);
                ClearSelectedVerticies();

                return true;
            }

            return false;
        }

        public void ClearSelectedVerticies()
        {
            _selectedVertex1 = null;
            _selectedVertex2 = null;
        }

        public void MoveElement(Control element, PictureBox mainPictureBox, Label? coordsText = null)
        {
            if (element == null || element != _selectedMovable)
            {
                return;
            }

            int xPosition = Cursor.Position.X + element.Width - _delta.X;
            int yPosition = Cursor.Position.Y + (int)(element.Height * 1.5f) - _delta.Y;

            int leftBorber = mainPictureBox.Location.X;
            int rightBorder = mainPictureBox.Location.X + mainPictureBox.Width - element.Width;
            int topBorber = mainPictureBox.Location.Y;
            int bottomBorder = mainPictureBox.Location.Y + mainPictureBox.Height - element.Height;

            xPosition = xPosition < leftBorber ? leftBorber : xPosition;
            xPosition = xPosition > rightBorder ? rightBorder : xPosition;
            yPosition = yPosition < topBorber ? topBorber : yPosition;
            yPosition = yPosition > bottomBorder ? bottomBorder : yPosition;

            element.Location = new Point(xPosition, yPosition);
            if (coordsText != null)
            {
                coordsText.Text = element.Location.ToString();
            }
        }
    }
}
