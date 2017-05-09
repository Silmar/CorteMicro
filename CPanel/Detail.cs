/*
This file is part of Cut Micro.

Cut Micro is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.

Cut Micro is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with Cut Micro. If not, see http://www.gnu.org/licenses/.
*/
using System;

namespace CPanel
{
    [Serializable]
    public class DT
    {
        protected int pDetID;
        protected bool pcanberotated;
        protected int pheidth;
        protected int ppanelid;
        protected int pquantity = 1;
        protected string ptxt = "";
        protected int pwidth;
        protected int px;
        protected int py;

        public DT()
        {
        }

        public DT(int DetID, string txt, int width, int heidth, int x, int y, int panelid, bool canberotated)
        {
            _DetID = DetID;
            _txt = txt;
            _width = width;
            _height = heidth;
            _x = x;
            _y = y;
            _panelid = panelid;
            _canberotated = canberotated;
        }

        public int _DetID
        {
            get { return pDetID; }
            set { pDetID = value; }
        }

        public string _txt
        {
            get { return ptxt; }
            set { ptxt = value; }
        }

        public int _width
        {
            get { return pwidth; }
            set { pwidth = value; }
        }

        public int _height
        {
            get { return pheidth; }
            set { pheidth = value; }
        }

        public int _x
        {
            get { return px; }
            set { px = value; }
        }

        public int _y
        {
            get { return py; }
            set { py = value; }
        }

        public int _panelid
        {
            get { return ppanelid; }
            set { ppanelid = value; }
        }

        public bool _canberotated
        {
            get { return pcanberotated; }
            set { pcanberotated = value; }
        }

        /////////////////////////

        public int _quantity
        {
            get { return pquantity; }
            set { pquantity = value; }
        }
    }

    public class Detail
    {
        protected string Etc = "";
        public bool _CanBeRotated;

        /// <summary>
        /// External use only
        /// </summary>
        public int _panelID;

        public int detailID;
        protected int height;
        public bool isFreeArea;
        public bool isSelected;
        protected int width;
        protected int x;
        protected int y;

        public Detail()
        {
        }

        public Detail(int x, int y, int width, int height, int DETAILID, string ETC, bool CanBeRotated, int panelID)
        {
            X = x;
            Y = y;
            Width = width + SawSize.S;
            Height = height + SawSize.S;
            detailID = DETAILID;
            etc = ETC;
            _CanBeRotated = CanBeRotated;
            _panelID = panelID;
        }

        public Detail(DT D)
        {
            X = D._x;
            Y = D._y;
            width = D._width + SawSize.S;
            height = D._height + SawSize.S;
            detailID = D._DetID;
            etc = D._txt;
            _CanBeRotated = D._canberotated;
            _panelID = D._panelid;
        }

        /// <summary>
        /// FREEAREA ONLY!!!
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="SawSize.S"></param>
        public Detail(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width + SawSize.S;
            Height = height + SawSize.S;
            detailID = 0;
            etc = "";
            isFreeArea = true;
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public int RealWidth
        {
            get { return width - SawSize.S; }
        }

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public int RealHeight
        {
            get { return height - SawSize.S; }
        }

        public string etc
        {
            get { return Etc; }
            set
            {
                if (value != null)
                    Etc = value;
                else
                    Etc = "";
            }
        }

        //////////////////////////////////////////////////////

        public void Rotate()
        {
            if (_CanBeRotated)
            {
                int tw = width;
                width = height;
                height = tw;
            }
        }

        public DT ExportToDT()
        {
            if (isFreeArea)
            {
                throw new Exception("Freearea cannot be exported");
            }
            var D = new DT(detailID, etc, Width - SawSize.S,
                           Height - SawSize.S, X, Y, _panelID, _CanBeRotated);
            return D;
        }
    }
}