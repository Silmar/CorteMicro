using System;
using System.Collections.Generic;
using System.Text;

namespace CutPro
{
    [Serializable]
    public class PL
    {
        public PL(int id, int width, int height, int cutleft, int cutright, int cutup, int cutdown)
        {
            _id = id; _width = width; _height = height; _cutleft = cutleft;
            _cutright = cutright; _cutup = cutup; _cutdown = cutdown;
        }
        public int _id;
        public int _width;
        public int _height;
        public int _cutleft;
        public int _cutright;
        public int _cutup;
        public int _cutdown;
    }

    [Serializable]
    public class DT
    {
        public DT(int DetID, string txt, int width, int heidth, int x, int y, int panelid, bool canberotated, int sawSize)
        {
            _DetID = DetID; _txt = txt; _width = width;
            _heidth = heidth; _x = x; _y = y; _panelid = panelid;
            _canberotated = canberotated; _sawSize = sawSize;
        }
        public int _DetID;
        public string _txt;
        public int _width;
        public int _heidth;
        public int _x;
        public int _y;
        public int _panelid;
        public bool _canberotated;
        public int _sawSize;
    }
}
