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
            int xCornersOffset = 20;
            int yCornersOffset = 6;
            int arrowOffset = 20;

            float vectorLength = (float)Math.Sqrt(Math.Pow(end.X - start.X, 2) + Math.Pow(end.Y - start.Y, 2));
            float angle = (float)Math.Asin((end.Y - start.Y) / vectorLength);

            float vectorLengthWithOffset = vectorLength - arrowOffset;

            if (end.X < start.X)
            {
                angle = -angle;
                xCornersOffset = -xCornersOffset;
                yCornersOffset = -yCornersOffset;
                vectorLengthWithOffset = -vectorLengthWithOffset;
            }

            int topX = (int)(start.X + (end.X - start.X) * Math.Cos(angle));
            int topY = (int)(start.Y + (end.X - start.X) * Math.Sin(angle));

            Graphics graphics = Graphics.FromImage(_canvas);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Brush brush = new SolidBrush(color);

            int leftX = (int)(start.X + (end.X - start.X - xCornersOffset) * Math.Cos(angle) - yCornersOffset * Math.Sin(angle));
            int leftY = (int)(start.Y + (end.X - start.X - xCornersOffset) * Math.Sin(angle) + yCornersOffset * Math.Cos(angle));

            int rightX = (int)(start.X + (end.X - start.X - xCornersOffset) * Math.Cos(angle) + yCornersOffset * Math.Sin(angle));
            int rightY = (int)(start.Y + (end.X - start.X - xCornersOffset) * Math.Sin(angle) - yCornersOffset * Math.Cos(angle));

            int newEndX = (int)(start.X + (vectorLengthWithOffset) * Math.Cos(angle));
            int newEndY = (int)(start.Y + (vectorLengthWithOffset) * Math.Sin(angle));

            int deltaX = newEndX - topX;
            int deltaY = newEndY - topY;

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
