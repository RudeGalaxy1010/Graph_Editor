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

        // TODO: Пересчитывать при start > end
        public Bitmap DrawArrowLine(Point start, Point end, Color color, int lineWidth = 2)
        {
            int xArrowOffset = 15;
            int yArrowOffset = 0;

            float vectorLength = (float)Math.Sqrt(Math.Pow(end.X - start.X, 2) + Math.Pow(end.Y - start.Y, 2));
            float angle = (float)Math.Asin((end.Y - start.Y) / vectorLength);

            int topX = (int)(start.X + (end.X - start.X) * Math.Cos(angle));
            int topY = (int)(start.Y + (end.X - start.X) * Math.Sin(angle));

            int xCornersOffset = 20;
            int yCornersOffset = 6;

            Graphics graphics = Graphics.FromImage(_canvas);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Brush brush = new SolidBrush(color);

            int leftX = (int)(start.X + (end.X - start.X - xCornersOffset) * Math.Cos(angle) - (yCornersOffset) * Math.Sin(angle));
            int leftY = (int)(start.Y + (end.X - start.X - xCornersOffset) * Math.Sin(angle) + (yCornersOffset) * Math.Cos(angle));

            int rightX = (int)(start.X + (end.X - start.X - xCornersOffset) * Math.Cos(angle) - (-yCornersOffset) * Math.Sin(angle));
            int rightY = (int)(start.Y + (end.X - start.X - xCornersOffset) * Math.Sin(angle) + (-yCornersOffset) * Math.Cos(angle));

            int deltaX = end.X - topX - xArrowOffset;
            int deltaY = end.Y - topY - yArrowOffset;

            Point leftAngle = new Point(leftX + deltaX, leftY + deltaY);
            Point rightAngle = new Point(rightX + deltaX, rightY + deltaY);
            Point topAngle = new Point(topX + deltaX, topY + deltaY);

            graphics.FillPolygon(brush, new Point[3] { leftAngle, topAngle, rightAngle });
            DrawLine(start, new Point(end.X, end.Y), color, lineWidth);
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
