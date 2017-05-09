/*
This file is part of Cut Micro.

Cut Micro is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.

Cut Micro is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with Cut Micro. If not, see http://www.gnu.org/licenses/.
*/
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CPanel
{
    public class DrawCursor
    {
        [DllImport("user32.dll")]
        public static extern IntPtr CreateIconIndirect(ref IconInfo icon);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);

        public static Cursor CreateCursor(Bitmap bmp,
            int xHotSpot, int yHotSpot)
        {
            IconInfo tmp = new IconInfo();
            GetIconInfo(bmp.GetHicon(), ref tmp);
            tmp.xHotspot = xHotSpot;
            tmp.yHotspot = yHotSpot;
            tmp.fIcon = false;
            return new Cursor(CreateIconIndirect(ref tmp));
        }

        public static Cursor getCursor(CutPanel draggedfrom, Color LineColor, Color BackColor)
        {
            foreach (Detail D in draggedfrom.PlacedDetails)
            {
                if (D.isSelected)
                {
                    int wbmp;
                    int hbmp;
                    var w = (int) (D.Width*draggedfrom.ZScale);
                    var h = (int) (D.Height*draggedfrom.ZScale);

                    if (w > 254) //Preserve of creating too large cursor
                    { wbmp = 254; }
                    else
                    {wbmp = w + 1;}

                    if (h > 254)
                    { hbmp = 254; }
                    else
                    { hbmp = h + 1; }

                    var bmp = new Bitmap(wbmp + 1, hbmp + 1);
                    Graphics gbmp = Graphics.FromImage(bmp);
                    var SemiTransp = new SolidBrush(Color.FromArgb(200, BackColor.R, BackColor.G, BackColor.B));
                    var p = new Pen(LineColor);
                    gbmp.Clear(Color.Transparent);
                    gbmp.FillRectangle(SemiTransp, 0, 0, w, h);
                    gbmp.DrawRectangle(p, 0, 0, w, h);
                    Cursor cur = CreateCursor(bmp, 0, 0);
                    gbmp.Dispose();
                    bmp.Dispose();
                    return cur;
                }
            }
            return Cursors.No;
        }

        public struct IconInfo
        {
            public bool fIcon;
            public int xHotspot;
            public int yHotspot;
            public IntPtr hbmMask;
            public IntPtr hbmColor;
        }
    }
}