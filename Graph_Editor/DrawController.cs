using Graph_Base;
using System.Collections.Generic;
using System.Drawing;

namespace Graph_Editor
{
    public class DrawController
    {
        public Bitmap DrawLine(Bitmap canvas, Point start, Point end, Color color, int lineWidth = 2)
        {
            Graphics graphics = Graphics.FromImage(canvas);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Pen pen = new Pen(color, lineWidth);

            graphics.DrawLine(pen, start, end);
            return canvas;
        }

        public Bitmap DrawArrowLine(Bitmap canvas, Point start, Point end, Color color, int lineWidth = 2)
        {
            Graphics graphics = Graphics.FromImage(canvas);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Brush brush = new SolidBrush(color);

            Point leftAngle = new Point(end.X - 10, end.Y + 5);
            Point rightAngle = new Point(end.X - 10, end.Y - 5);
            Point topAngle = end;

            graphics.FillPolygon(brush, new Point[3] { leftAngle, topAngle, rightAngle });
            DrawLine(canvas, start, new Point(end.X - 3, end.Y), color, lineWidth);
            return canvas;
        }

        public Bitmap ClearCanvas(Bitmap canvas)
        {
            Graphics graphics = Graphics.FromImage(canvas);
            graphics.Clear(Color.White);
            return canvas;
        }
    }
}
