using System;
using System.Drawing;

namespace Graph_Editor
{
    public class DrawController
    {
        private Bitmap _canvas;

        public DrawController(Bitmap canvas)
        {
            _canvas = canvas;
            ClearCanvas();
        }

        public Bitmap DrawLine(Point start, Point end, Color color, int lineWidth = 2)
        {
            Graphics graphics = Graphics.FromImage(_canvas);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Pen pen = new Pen(color, lineWidth);

            graphics.DrawLine(pen, start, end);
            return _canvas;
        }

        public Bitmap DrawArrowLine(Point start, Point end, Color color, int lineWidth = 2)
        {
            Graphics graphics = Graphics.FromImage(_canvas);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Brush brush = new SolidBrush(color);

            float yOffset = 5;
            float xOffset = 15;

            float vectorLength = (float)Math.Sqrt(Math.Pow(end.X - start.X, 2) + Math.Pow(end.Y - start.Y, 2));
            float angle = (float)Math.Asin((end.Y - start.Y) / vectorLength);

            int leftX = (int)(start.X + (end.X - start.X - xOffset) * Math.Cos(angle) - (yOffset) * Math.Sin(angle));
            int leftY = (int)(start.Y + (end.X - start.X - xOffset) * Math.Sin(angle) + (yOffset) * Math.Cos(angle));

            int rightX = (int)(start.X + (end.X - start.X - xOffset) * Math.Cos(angle) - (-yOffset) * Math.Sin(angle));
            int rightY = (int)(start.Y + (end.X - start.X - xOffset) * Math.Sin(angle) + (-yOffset) * Math.Cos(angle));

            int topX = (int)(start.X + (end.X - start.X) * Math.Cos(angle));
            int topY = (int)(start.Y + (end.X - start.X) * Math.Sin(angle));

            int deltaX = end.X - topX;
            int deltaY = end.Y - topY;

            Point leftAngle = new Point(leftX + deltaX, leftY + deltaY);
            Point rightAngle = new Point(rightX + deltaX, rightY + deltaY);
            Point topAngle = new Point(topX + deltaX, topY + deltaY);

            graphics.FillPolygon(brush, new Point[3] { leftAngle, topAngle, rightAngle });
            DrawLine(start, new Point(end.X - 5, end.Y - 5), color, lineWidth);
            return _canvas;
        }

        public Bitmap ClearCanvas()
        {
            Graphics graphics = Graphics.FromImage(_canvas);
            graphics.Clear(Color.White);
            return _canvas;
        }
    }
}
