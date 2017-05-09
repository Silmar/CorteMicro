/*
This file is part of Cut Micro.

Cut Micro is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.

Cut Micro is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with Cut Micro. If not, see http://www.gnu.org/licenses/.
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CPanel
{
    [Serializable]
    public class PL
    {
        protected int Pid;
        protected int Pcutdown;
        protected int Pcutleft;
        protected int Pcutright;
        protected int Pcutup;
        protected int Pheight;
        protected int Pquantity = 1;
        protected string Ptxt = "";
        protected int Pwidth;

        public PL()
        {
        }

        public PL(int id, int width, int height, int cutleft, int cutright, int cutup, int cutdown, string txt)
        {
            Id = id;
            Width = width;
            Height = height;
            Cutleft = cutleft;
            Cutright = cutright;
            Cutup = cutup;
            Cutdown = cutdown;
            Txt = txt;
        }

        public int Id
        {
            get { return Pid; }
            set { Pid = value; }
        }

        public int Width
        {
            get { return Pwidth; }
            set { Pwidth = value; }
        }

        public int Height
        {
            get { return Pheight; }
            set { Pheight = value; }
        }

        public int Cutleft
        {
            get { return Pcutleft; }
            set { Pcutleft = value; }
        }

        public int Cutright
        {
            get { return Pcutright; }
            set { Pcutright = value; }
        }

        public int Cutup
        {
            get { return Pcutup; }
            set { Pcutup = value; }
        }

        public int Cutdown
        {
            get { return Pcutdown; }
            set { Pcutdown = value; }
        }

        public int Quantity
        {
            get { return Pquantity; }
            set { Pquantity = value; }
        }

        public string Txt
        {
            get { return Ptxt; }
            set { Ptxt = value; }
        }
    }

    public class CutPanel : Panel,IDisposable
    {
        //Pens,Brushes///////////////////////////////////////////////////////////////////////
        private readonly SolidBrush _rectBrush = new SolidBrush(Color.White);
        private readonly Pen _rectanglesPen = new Pen(Color.Black, 4);
        private readonly SolidBrush _textBrush = new SolidBrush(Color.Black);
        private Graphics _drawing;
        private HatchBrush _faBrush;
        public List<Detail> PlacedDetails = new List<Detail>();
        private HatchBrush _panelBrush;
        private Bitmap _backBuffer;
// ReSharper disable UnaccessedField.Local
        private SolidBrush _backTextBrush = new SolidBrush(Color.White);
// ReSharper restore UnaccessedField.Local
        private Graphics _g;
        //Other//////////////////////////////////////////////////////////////////////////////
        public double ZScale;
        public Rectangle WorkArea;

        #region Variables

        /// <summary>
        /// Color of the work area border
        /// </summary>
        protected Color BorderColor = Color.Black;

        /// <summary>
        /// Border of board thet must be cuttet out
        /// </summary>   
        protected int CutDown = 30;

        /// <summary>
        /// Border of board thet must be cuttet out
        /// </summary>   
        protected int CutLeft = 30;

        /// <summary>
        /// Border of board thet must be cuttet out
        /// </summary>   
        protected int CutRight = 30;

        /// <summary>
        /// Border of board thet must be cuttet out
        /// </summary>   
        protected int CutUp = 30;

        /// <summary>
        /// Color of the free area drawed rectangles
        /// </summary>
        protected Color FreeAreaHatchColor = Color.LightCoral;

        /// <summary>
        /// For external use;
        /// </summary>
        public int PanelId;

        /// <summary>
        /// Color of the drawed rectangles(border)
        /// </summary>
        protected Color RectBorderColor = Color.Black;

        /// <summary>
        /// Color of the drawed rectangles(border)
        /// </summary>
        protected Color RectColor = Color.White;

        /// <summary>
        /// Color of the drawed rectangles hatch
        /// </summary>
        protected Color RectHatchColor = Color.DarkSalmon;

        /// <summary>
        /// Color of the text
        /// </summary>
        protected Color TextColor = Color.Black;

        /// <summary>
        /// Font to draw sizes
        /// </summary>
        protected Font TextFont = new Font(FontFamily.GenericMonospace, 6);

        /// <summary>
        /// Total board height
        /// </summary>
        protected int BoardHeight = 1000;

        /// <summary>
        /// Total board width
        /// </summary>
        protected int BoardWidth = 1000;

        /// <summary>
        /// For external use;
        /// </summary>
        public string Txt = "";

        public Font PTextFont
        {
            get { return TextFont; }
            set { TextFont = value; }
        }

        public Color PFreeAreaHatchColor
        {
            get { return FreeAreaHatchColor; }
            set { FreeAreaHatchColor = value; }
        }

        public Color PRectBorderColor
        {
            get { return RectBorderColor; }
            set { RectBorderColor = value; }
        }

        public Color PRectColor
        {
            get { return RectColor; }
            set { RectColor = value; }
        }

        public Color PRectHatchColor
        {
            get { return RectHatchColor; }
            set { RectHatchColor = value; }
        }

        public Color PBorderColor
        {
            get { return BorderColor; }
            set { BorderColor = value; }
        }

        public Color PTextColor
        {
            get { return TextColor; }
            set { TextColor = value; }
        }

        public int PCutUp
        {
            get { return CutUp; }
            set
            {
                if (value >= 0)
                    CutUp = value;
            }
        }

        public int PCutDown
        {
            get { return CutDown; }
            set
            {
                if (value >= 0)
                    CutDown = value;
            }
        }

        public int PCutLeft
        {
            get { return CutLeft; }
            set
            {
                if (value >= 0)
                    CutLeft = value;
            }
        }

        public int PCutRight
        {
            get { return CutRight; }
            set
            {
                if (value >= 0)
                    CutRight = value;
            }
        }

        public int PboardHeight
        {
            get { return BoardHeight; }
            set
            {
                if (value >= 0)
                    BoardHeight = value;
            }
        }

        public int PboardWidth
        {
            get { return BoardWidth; }
            set
            {
                if (value >= 0)
                    BoardWidth = value;
            }
        }

        #endregion

        public CutPanel()
        {
        }

        public CutPanel(int height, int width, int panelId, int cutLeft, int cutRight, int cutUp, int cutDown,
                        string txt)
        {
            PboardHeight = height;
            PboardWidth = width;
            PanelId = panelId;
            PCutLeft = cutLeft;
            PCutRight = cutRight;
            PCutUp = cutUp;
            PCutDown = cutDown;
            Txt = txt;
        }

        public CutPanel(PL p)
        {
            PboardHeight = p.Height;
            PboardWidth = p.Width;
            PanelId = p.Id;
            PCutLeft = p.Cutleft;
            PCutRight = p.Cutright;
            PCutUp = p.Cutup;
            PCutDown = p.Cutdown;
            Txt = p.Txt;
        }

        /// <summary>
        /// Sets size of panel according to zoom
        /// Creates graphics and backbuffer bitmaps
        /// Calculates 
        /// </summary>
        public void SetUp(double zoom)
        {
            if (zoom < 1)
            {
                return;
            }
            Width = (int)(BoardWidth/zoom)+1;
            Height = (int)(BoardHeight / zoom) + ((int)TextFont.GetHeight()*2);
            ZScale = (double) Width/BoardWidth;

            if (_backBuffer != null)
            {
                _backBuffer.Dispose();
            }
            _backBuffer = new Bitmap(Width, Height);

            if (_drawing != null)
            {
                _drawing.Dispose();
            }
            _drawing = Graphics.FromImage(_backBuffer);

            if (_g != null)
            {
                _g.Dispose();
            }
            _g = CreateGraphics();
            int wAwidth = BoardWidth - CutRight - CutLeft;
            int wAheight = BoardHeight - CutUp - CutDown;
            WorkArea = new Rectangle(0, 0, wAwidth, wAheight);
            Redraw();
        }

        private void DrawBorder(Graphics drawing)
        {
            var output = new Rectangle
                             {
                                 X = (int) (CutLeft*ZScale),
                                 Y = (int) (CutUp*ZScale),
                                 Width = (int) ((PboardWidth - CutLeft - CutRight)*ZScale)-1,
                                 Height = (int) ((PboardHeight - CutUp - CutDown)*ZScale)
                             };
            drawing.FillRectangle(_panelBrush, output);
            drawing.DrawRectangle(_rectanglesPen, output);
            _textBrush.Color = TextColor;
            if (PanelId != 0)
            {
                String inf = "#" + PanelId + " " + PboardWidth
                             + "x" + PboardHeight + "mm (" + (PboardWidth - CutLeft
                                                              - CutRight) + "x" +
                             (PboardHeight - CutUp - CutDown)
                             + "mm)" + Txt;
                drawing.DrawString(inf, TextFont, _textBrush, -1, Height - ((int)TextFont.GetHeight() * 2) - 1);
                String inf2 = CalcAreaUsage();
                drawing.DrawString(inf2, TextFont, _textBrush, -1, Height - (int)TextFont.GetHeight() - 1);
            }
        }

        private string CalcAreaUsage()
        {
            long area = 0;
            foreach (Detail D in PlacedDetails)
            {
                if (!D.isFreeArea)
                {
                    area += D.Height * D.Width;
                }
            }

            long totalarea = (PboardWidth - CutLeft - CutRight) * (PboardHeight - CutUp - CutDown);
            return "Área utilizada: " + (100 * area / totalarea).ToString("F0") + "% (" +
                ((double)area / 1000000).ToString("F2") + "//" + ((double)totalarea / 1000000).ToString("F2") + "m²)";
        }

        /// <summary>
        /// Drawes Rectangle on CutPanel
        /// </summary>
        /// <param name="drawing">Graphics that handles backbuffer.</param>
        /// <param name="rect">Rectabgle to draw</param>
        private void DrawRect(Graphics drawing, Detail rect)
        {
            
            if (rect == null || rect.Height == 0 || rect.Width == 0)
                return;
            var output = new Rectangle
                             {
                                 X = (int) ((rect.X + CutLeft)*ZScale),
                                 Y = (int) ((rect.Y + CutUp)*ZScale),
                                 Width = (int) (rect.Width*ZScale - SawSize.S*ZScale),
                                 Height = (int) (rect.Height*ZScale - SawSize.S*ZScale)
                             };
            ////////////////Calculating output rect/////////////////////////////////////////////////////////////
            ///////////////// Creating temporary bitmap/////////////////////////////////////////////////////////
          //  var btmp = new Bitmap(output.Width + 1, output.Height + 1);
          //  Graphics gptmp = 
         //   Graphics gtmp = Graphics.FromImage(btmp);
         //   if (DisableFontSmoothing)
          //  {
         //       gtmp.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel;
         //   }
           // gtmp.Clear(BackColor);
            drawing.DrawRectangle(_rectanglesPen, output.X, output.Y, output.Width, output.Height); //Draw Rectangle on temp bitmap
            if (rect.isFreeArea)
                drawing.FillRectangle(_faBrush, output.X + 1, output.Y + 1, output.Width - 1, output.Height - 1); //Then fill it with hatch
            else
                drawing.FillRectangle(_rectBrush, output.X + 1, output.Y + 1, output.Width - 1, output.Height - 1);
            ///////////////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////Starts text placing code///////////////////////////////////////////////////////  
            SizeF dsize = drawing.MeasureString(rect.detailID.ToString(), TextFont);
            SizeF txTsize = drawing.MeasureString(rect.etc, TextFont);
            bool tdrawed = false;
            SizeF wHsize = drawing.MeasureString((rect.RealWidth).ToString(), TextFont);
            bool wdrawed = false;
            SizeF hTsize = drawing.MeasureString((rect.RealHeight).ToString(), TextFont);
            _backTextBrush = new SolidBrush(RectHatchColor);

            if (!rect.isFreeArea)
            {
                if (dsize.Width < output.Width && dsize.Height < output.Height) //First Draw ID
                {
                    _backTextBrush = new SolidBrush(RectHatchColor);
           //         gtmp.FillRectangle(_backTextBrush, (output.Width - dsize.Width)/2, (output.Height - dsize.Height)/2,
           //                            dsize.Width, dsize.Height);
                    drawing.DrawString(rect.detailID.ToString(), TextFont, _textBrush, output.X + (output.Width - dsize.Width) / 2,
                                    output.Y + (output.Height - dsize.Height) / 2);
                }
                else
                    return; //If we cant draw ID - do not draw nothing
            }
            //Width
            if (wHsize.Width < output.Width &&
                (wHsize.Height*2.2F < output.Height ||
                 (wHsize.Height < output.Height && wHsize.Width + dsize.Width*2 < output.Width))) //Draw width normally
            {
                drawing.DrawString((rect.RealWidth).ToString(), TextFont, _textBrush, output.X + output.Width - wHsize.Width, output.Y+ 1);
                wdrawed = true;
            }
            //TXT
            if (txTsize.Width + hTsize.Height/1.3F < output.Width &&
                (txTsize.Height + dsize.Height + wHsize.Height < output.Height ||
                 (txTsize.Height + wHsize.Height/1.4F < output.Height && txTsize.Width*2 < output.Width)))
            {
                drawing.DrawString(rect.etc, TextFont, _textBrush, output.X + output.Width - txTsize.Width,
                                output.Y+ output.Height - txTsize.Height);
                tdrawed = true;
            }
            //Height
            if (hTsize.Width > output.Height && hTsize.Width + txTsize.Width < output.Width &&
                hTsize.Height < output.Height)
            {
                drawing.DrawString((rect.RealHeight).ToString(), TextFont, _textBrush, output.X, output.Y + output.Height - hTsize.Height);
            }
            //Rotate
            drawing.RotateTransform(-90);
            //Try draw what we cant earler
            if (!wdrawed && wHsize.Width*2 < output.Height && wHsize.Height < output.Width)
            {
                drawing.DrawString((rect.RealWidth).ToString(), TextFont, _textBrush, -output.Y -wHsize.Width,
                                output.X + output.Width - wHsize.Height + 4);
            }
            if (hTsize.Width <= output.Height &&
                (hTsize.Height*1.3F + dsize.Width < output.Width ||
                 (hTsize.Height < output.Width && hTsize.Width*2 < output.Height)))
            {
                drawing.DrawString((rect.RealHeight).ToString(), TextFont, _textBrush, -output.Y -output.Height, output.X);
            }
            if (!tdrawed && txTsize.Width + wHsize.Width < output.Height &&
                txTsize.Height + hTsize.Height/1.4F < output.Width)
            {
                drawing.DrawString(rect.etc, TextFont, _textBrush, -output.Y - output.Height, output.X + output.Width - txTsize.Height + 4);
            }
            drawing.RotateTransform(90);
            ////////////////////End text placing code//////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////    
           // draw:
           // drawing.DrawImageUnscaled(btmp, output.X, output.Y); //Draw temp bitmap on backbuffer
           // btmp.Dispose();
           // gtmp.Dispose();
        }

        public Point GetMouseClickPoint(int x, int y)
        {
            var p = new Point(((int) (x/ZScale) - CutLeft), ((int) (y/ZScale) - CutUp));
            if (p.X < 0)
            {
                p.X = 0;
            }
            if (p.Y < 0)
            {
                p.Y = 0;
            }
            return p;
        }

        // //////////////////////////////////////////////////////////////////////////

        public void Redraw()
        {
            _rectBrush.Color = RectColor;
            _textBrush.Color = TextColor;
            _rectanglesPen.Color = RectBorderColor;
            _rectanglesPen.Width = 1;
            _panelBrush = new HatchBrush(HatchStyle.LightDownwardDiagonal, RectHatchColor, BackColor);
            _faBrush = new HatchBrush(HatchStyle.Percent50, FreeAreaHatchColor, BackColor);
            ////////////////////////////////
            _drawing.Clear(BackColor);
            DrawBorder(_drawing);
            for (int i = 0; i < PlacedDetails.Count; i++)
            {
                DrawRect(_drawing, PlacedDetails[i]);
            }
// ReSharper disable AssignNullToNotNullAttribute
            OnPaint(null);
// ReSharper restore AssignNullToNotNullAttribute
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            _g.DrawImageUnscaled(_backBuffer, 0, 0);
        }

        ////////////////////////////////////////////////////
        public PL ExportToPL()
        {
            var p = new PL(PanelId, PboardWidth, PboardHeight,
                           CutLeft, PCutRight, CutUp, CutDown, Txt);
            return p;
        }

        public new void Dispose()
        {
            base.Dispose();
            Visible = false;
            _g.Dispose();
            _backBuffer.Dispose();
            _drawing.Dispose();
        }

        public Bitmap GetBitmap()
        {
            return _backBuffer;
        }
    }
}