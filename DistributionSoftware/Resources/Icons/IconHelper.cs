using System;
using System.Drawing;
using System.Windows.Forms;

namespace DistributionSoftware.Resources.Icons
{
    public static class IconHelper
    {
        public static Bitmap CreateEmailIcon()
        {
            Bitmap icon = new Bitmap(24, 24);
            using (Graphics g = Graphics.FromImage(icon))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                
                // Email envelope
                using (Pen pen = new Pen(Color.FromArgb(100, 149, 237), 2))
                {
                    g.DrawRectangle(pen, 2, 6, 20, 14);
                    g.DrawLine(pen, 2, 6, 12, 16);
                    g.DrawLine(pen, 12, 16, 22, 6);
                }
            }
            return icon;
        }

        public static Bitmap CreatePasswordIcon()
        {
            Bitmap icon = new Bitmap(24, 24);
            using (Graphics g = Graphics.FromImage(icon))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                
                // Lock icon
                using (Pen pen = new Pen(Color.FromArgb(100, 149, 237), 2))
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(100, 149, 237)))
                {
                    g.FillRectangle(brush, 8, 12, 8, 10);
                    g.DrawRectangle(pen, 6, 10, 12, 14);
                    g.DrawArc(pen, 8, 4, 8, 8, 0, 180);
                }
            }
            return icon;
        }

        public static Bitmap CreateUserIcon()
        {
            Bitmap icon = new Bitmap(24, 24);
            using (Graphics g = Graphics.FromImage(icon))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                
                // User icon
                using (Pen pen = new Pen(Color.FromArgb(100, 149, 237), 2))
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(100, 149, 237)))
                {
                    g.FillEllipse(brush, 8, 4, 8, 8);
                    g.DrawArc(pen, 4, 10, 16, 12, 0, 180);
                }
            }
            return icon;
        }
    }
}
