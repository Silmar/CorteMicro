/*
This file is part of Cut Micro.

Cut Micro is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.

Cut Micro is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with Cut Micro. If not, see http://www.gnu.org/licenses/.
*/
using System.Drawing;
using System.Collections.Generic;

namespace CPanel
{
    public class cpMove
    {
        private static Stack<MoveHistory> _mhUndo = new Stack<MoveHistory>();
        private static Stack<MoveHistory> _mhRedo = new Stack<MoveHistory>();


        /// <summary>
        /// Move detail ferom Panel moveFrom at coordinates pFrom to Panel moveTo to coordinates pTo
        /// </summary>
        /// <param name="moveFrom"></param>
        /// <param name="moveTo"></param>
        /// <param name="pFrom"></param>
        /// <param name="pTo"></param>
        public static void MoveDetail(CutPanel moveFrom, CutPanel moveTo, Point pFrom, Point pTo)
        {
            MoveDetail(moveFrom, moveTo, pFrom, pTo, false);
        }

        /// <summary>
        /// Rotate detail rotateDet at panel rotateOn
        /// </summary>
        /// <param name="rotateOn"></param>
        /// <param name="rotateDet"></param>
        public static void RotateDetail(CutPanel rotateOn, Detail rotateDet)
        {
            RotateDetail(rotateOn, rotateDet, false);
        }

        private static void MoveDetail(CutPanel moveFrom, CutPanel moveTo, Point pFrom, Point pTo, bool isUndo)
        {
            foreach (Detail det in moveFrom.PlacedDetails)
            {
                if (det.X == pFrom.X && det.Y == pFrom.Y)
                {
                    det.X = pTo.X;
                    det.Y = pTo.Y;
                    if (moveFrom.PanelId != moveTo.PanelId)
                    {
                        moveFrom.PlacedDetails.Remove(det);
                        moveTo.PlacedDetails.Add(det);
                    }
                    moveFrom.PlacedDetails.Sort(new sortDetailByY());
                    moveTo.PlacedDetails.Sort(new sortDetailByY());
                    moveFrom.Redraw();
                    moveTo.Redraw();

                    if (!isUndo)
                    {
                        _mhUndo.Push(new MoveHistory(moveFrom, moveTo, pFrom, pTo));
                        _mhRedo.Clear();
                    }

                    break;
                }
            }
        }

        private static void RotateDetail(CutPanel rotateOn, Detail rotateDet, bool isUndo)
        {
            rotateDet.Rotate();
            rotateOn.Redraw();
            if (!isUndo)
            {
                _mhUndo.Push(new MoveHistory(rotateOn, rotateDet));
                _mhRedo.Clear();
            }

        }

        public static void undoLast()
        {
            if (_mhUndo.Count > 0)
            {
                MoveHistory last = _mhUndo.Pop();

                if (last._rotated == null)
                {
                    MoveDetail(last._moveTo, last._moveFrom, last._newDetailPlace, last._oldDetailPlace, true);
                }
                else
                {
                    RotateDetail(last._moveFrom, last._rotated, true);
                }

                _mhRedo.Push(last);
            }
        }

        public static void redoLast()
        {
            if (_mhRedo.Count > 0)
            {
                MoveHistory last = _mhRedo.Pop();
                if (last._rotated == null)
                {
                    MoveDetail(last._moveFrom, last._moveTo, last._oldDetailPlace, last._newDetailPlace, true);
                }
                else
                {
                    RotateDetail(last._moveFrom, last._rotated, true);
                }
                _mhUndo.Push(last);
            }
        }

        public static int getUndoCount()
        {
            return _mhUndo.Count;
        }

        public static void ClearUndoRedo()
        {
            _mhUndo.Clear();
            _mhRedo.Clear();

        }

        public static int getRedoCount()
        {
            return _mhRedo.Count;
        }
    }

    public class MoveHistory
    {
        public CutPanel _moveFrom;
        public CutPanel _moveTo;
        public Point _newDetailPlace;
        public Point _oldDetailPlace;
        public Detail _rotated;

        /// <summary>
        /// One moved detail info
        /// </summary>
        /// <param name="moveFrom">Panel that give detail</param>
        /// <param name="moveTo">Panel that take detail</param>
        /// <param name="pFrom">Old coord</param>
        /// <param name="pTo">New Coord</param>         
        public MoveHistory(CutPanel moveFrom, CutPanel moveTo, Point pFrom, Point pTo)
        {
            _moveFrom = moveFrom; _moveTo = moveTo;
            _newDetailPlace = pTo;
            _oldDetailPlace = pFrom;
            _rotated = null;
        }

        public MoveHistory(CutPanel rotatedOn, Detail rotateDet)
        {
            _rotated = rotateDet;
            _moveFrom = rotatedOn;
        }
    }
}