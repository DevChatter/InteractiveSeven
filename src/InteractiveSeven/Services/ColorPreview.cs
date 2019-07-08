using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Color = System.Drawing.Color;

namespace InteractiveSeven.Services
{
    public static class ColorPreview
    {
        public static ImageSource MakeBmp(params Color[] colorArray)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, 200, 100);
            graphicsPath.AddRectangle(rect);

            using var bmp = new Bitmap(200, 100);
            using Graphics graphics = Graphics.FromImage(bmp);
            using PathGradientBrush pathGradientBrush = new PathGradientBrush(graphicsPath)
            {
                CenterColor = Color.FromArgb((int)colorArray.Average(a => a.R), (int)colorArray.Average(a => a.G), (int)colorArray.Average(a => a.B)),
                SurroundColors = colorArray
            };
            graphics.FillPath(pathGradientBrush, graphicsPath);
            graphics.DrawImage(bmp, rect);

            return Imaging.CreateBitmapSourceFromHBitmap(
                bmp.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromWidthAndHeight(200, 100));
        }
    }
}
