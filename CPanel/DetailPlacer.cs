﻿/*
This file is part of Cut Micro.

Cut Micro is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.

Cut Micro is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with Cut Micro. If not, see http://www.gnu.org/licenses/.
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CPanel;

namespace CPanel
{
    public class DetailPlacer
    {
       //rivate readonly Stack<MoveHistory> _moveHistory = new Stack<MoveHistory>(100);

        /// <summary>
        /// Finds left-top point to snap detail to
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="mouseX"></param>
        /// <param name="mouseY"></param>
        /// <param name="alternativeSnap"></param>
        /// <returns></returns>
        private static Point FindSnapPoint(CutPanel panel, int mouseX, int mouseY, bool alternativeSnap)
        {
            List<Detail> D = panel.PlacedDetails;
            Point dropPoint = panel.GetMouseClickPoint(mouseX, mouseY);
            int snapX = 0;
            int snapY = 0;
            bool gotOne = false;
            ///////First find left line to snap/////////            
            var dtmp = new Detail(0, 0, 0, 0, 0, "", false, -1);
            foreach (Detail Det in D)
            {
                if (Det.Y <= dropPoint.Y && (Det.Y + Det.Height) > dropPoint.Y)
                {
                    if ((Det.X + Det.Width) < dropPoint.X && Det.X >= dtmp.X)
                    {
                        dtmp = Det;
                        gotOne = true;
                    }
                }
            }
            if (!gotOne)
            {
                snapX = 0;
            }
            else
            {
                snapX = dtmp.X + dtmp.Width;
            }
            //////Find top line to snap////////
            gotOne = false;
            foreach (Detail Det in D)
            {
                if (Det.X <= snapX && (Det.X + Det.Width) > snapX && dropPoint.Y > (Det.Y + Det.Height) &&
                    (Det.Y + Det.Height) > snapY)
                {
                    dtmp = Det;
                    gotOne = true;
                    snapY = dtmp.Y + dtmp.Height;
                }
                if (!alternativeSnap && (Det.X + Det.Width) == snapX && snapY < Det.Y && Det.Y < dropPoint.Y)
                {
                    dtmp = Det;
                    gotOne = true;
                    snapY = dtmp.Y;
                }
            }
            if (!gotOne)
            {
                snapY = 0;
            }
            return new Point(snapX, snapY);
        }

        /// <summary>
        /// This function searches for free space using start point
        /// </summary>
        /// <param name="panel">CutPanel</param>
        /// <param name="Snap">Start point</param>
        /// <returns>Detail marked as "isFreeArea"</returns>
        private static Detail findFreeSpace(CutPanel panel, Point Snap, bool altSnap)
        {
            List<Detail> D = panel.PlacedDetails;
            bool GotOne = false;
            int Fwidth = 99999999;
            int Fheidth = 99999999;
            //First find maximum width
            foreach (Detail Det in D)
            {
                if (Det.Y <= Snap.Y && (Det.Y + Det.Height) > Snap.Y && Det.X > Snap.X &&
                    Fwidth > (Det.X - Snap.X - SawSize.S))
                {
                    Fwidth = Det.X - Snap.X - SawSize.S;
                    GotOne = true;
                }
            }
            if (!GotOne)
            {
                Fwidth = panel.WorkArea.Width - Snap.X;
            }
            //Then fing maximum height
            GotOne = false;
            foreach (Detail Det in D)
            {
                //Do not try to understand this.
                if (!(Det.X >= (Snap.X + Fwidth) || (Det.X + Det.Width) <= Snap.X) && Det.Y > Snap.Y &&
                    Fheidth > (Det.Y - Snap.Y - SawSize.S))
                {
                    Fheidth = Det.Y - Snap.Y - SawSize.S;
                    GotOne = true;
                }
                if (!altSnap && Det.X + Det.Width == Snap.X && Det.Y <= Snap.Y && Det.Y + Det.Height > Snap.Y &&
                    Fheidth > Det.Y + Det.Height - Snap.Y - SawSize.S)
                {
                    Fheidth = Det.Y + Det.Height - Snap.Y - SawSize.S;
                    GotOne = true;
                }
                if (!altSnap && Det.X == Snap.X + Fwidth + SawSize.S && Det.Y <= Snap.Y && Det.Y + Det.Height > Snap.Y &&
                    Fheidth > Det.Y + Det.Height - Snap.Y - SawSize.S)
                {
                    Fheidth = Det.Y + Det.Height - Snap.Y - SawSize.S;
                    GotOne = true;
                }
            }

            if (!GotOne)
            {
                Fheidth = panel.WorkArea.Height - Snap.Y;
            }
            if (Fwidth == 0 || Fheidth == 0 || Fwidth == 99999999 || Fheidth == 99999999)
            {
                throw new Exception("Não pôde alocar espaço livre");
            }
            return new Detail(Snap.X, Snap.Y, Fwidth, Fheidth);
        }

        /// <summary>
        /// Returns number of selected detail or -1 if nothing selected. Does not mark detail as "isSelected"
        /// </summary>
        /// <param name="panel">Cut panel object</param>
        /// <param name="MouseX"></param>
        /// <param name="MouseY"></param>
        /// <returns></returns>
        public int findSelectedDetail(CutPanel panel, int MouseX, int MouseY)
        {
            Point Click = panel.GetMouseClickPoint(MouseX, MouseY);
            List<Detail> D = panel.PlacedDetails;
            int indx = -1;
            for (int i = 0; i < D.Count; i++)
            {
                if (D[i].X <= Click.X && D[i].X + D[i].Width >= Click.X && D[i].Y <= Click.Y &&
                    (D[i].Y + D[i].Height) >= Click.Y)
                {
                    indx = i;
                }
            }
            return indx;
        }

        /// <summary>
        /// Places Detail on CutPanel
        /// </summary>
        /// <param name="placedOn">Panel to place</param>
        /// <param name="Det">Detail to place</param>
        /// <param name="MouseX"></param>
        /// <param name="MouseY"></param>
        /// <param name="ignoreBorder">Ignore right and down cut borders</param>
        /// <param name="alternativeSnap">Snaps to Up-left corner, not a detail</param>
        /// <returns>Point of new detail</returns>
        public Point placeDetail(CutPanel PlacedFrom, CutPanel placedOn, Detail Det, int MouseX, int MouseY, bool ignoreBorder,
                                 bool alternativeSnap, bool ignoreAll)
        {
            Point P = FindSnapPoint(placedOn, MouseX, MouseY, alternativeSnap);
            var dtmp = new Rectangle(P.X, P.Y, Det.RealWidth, Det.RealHeight);
            var SavePoint = new Point(Det.X, Det.Y);
            //Move detail away
            Det.X = -9999;
            Det.Y = -9999;
            List<Detail> D = placedOn.PlacedDetails;
            bool fail = false;
            foreach (Detail detail in D)
            {
                if (detail.X >= dtmp.X && detail.X < dtmp.X + Det.Width)
                {
                    if (!(detail.Y >= dtmp.Y + dtmp.Height || detail.Y + detail.Height <= dtmp.Y))
                    {
                        fail = true;
                        break;
                    }
                }
                if (detail.Y >= dtmp.Y && detail.Y < dtmp.Y + dtmp.Height)
                {
                    if (!(detail.X >= dtmp.X + dtmp.Width || detail.X + detail.Width <= dtmp.X))
                    {
                        fail = true;
                        break;
                    }
                }
            }

            Det.X = SavePoint.X;
            Det.Y = SavePoint.Y;

            if (fail)
            {                
                throw new Exception();
            } //Cant place...
            if (!ignoreBorder &&
                (dtmp.X + dtmp.Width > placedOn.WorkArea.Width || dtmp.Y + dtmp.Height > placedOn.WorkArea.Height))
            {
               throw new Exception();
            }
            if (ignoreBorder && !ignoreAll &&
                (dtmp.X + dtmp.Width > placedOn.WorkArea.Width + placedOn.PCutRight ||
                 dtmp.Y + dtmp.Height > placedOn.WorkArea.Height + placedOn.PCutDown))
            {
                throw new Exception();
            }
            //Finally....
            cpMove.MoveDetail(PlacedFrom, placedOn, SavePoint, new Point(dtmp.X, dtmp.Y));  
            //Det.X = dtmp.X;
            //Det.Y = dtmp.Y;
            //placedOn.PlacedDetails.Add(Det);
            return new Point(Det.X, Det.Y);
        }

        /// <summary>
        /// Processes mouse click according to click position.
        /// </summary>
        /// <param name="panel">Cut panel object</param>
        /// <param name="MouseX"></param>
        /// <param name="MouseY"></param>
        /// <returns></returns>
        public void processMouseDown(CutPanel panel, int MouseX, int MouseY, bool AltPressed, bool ShiftPressed)
        {
            try
            {
                ////First deselect all details
                foreach (Detail D in panel.PlacedDetails)
                {
                    D.isSelected = false;
                }
                ////Check if we select smth
                int sel = findSelectedDetail(panel, MouseX, MouseY);
                if (sel != -1 && !panel.PlacedDetails[sel].isFreeArea)
                {
                    panel.PlacedDetails[sel].isSelected = true;
                    ////START DRAGDROP!
                    panel.DoDragDrop(panel.PanelId, DragDropEffects.All);
                }
                else if (sel != -1 && panel.PlacedDetails[sel].isFreeArea)
                {
                    panel.PlacedDetails.RemoveAt(sel);
                    panel.Redraw();
                }
                else if (panel.PanelId != 0)
                {
                    ////Else Create FreeArea
                    Point F = FindSnapPoint(panel, MouseX, MouseY, AltPressed);
                    Detail Dt = findFreeSpace(panel, F, AltPressed);
                    if (Dt.RealWidth != 0 && Dt.RealHeight != 0)
                    {
                        panel.PlacedDetails.Add(Dt);
                        panel.Redraw();
                    }
                }
            }
            catch (Exception)
            {
            }
            return;
        }

        /// <summary>
        /// Processes rigth mouse click
        /// </summary>
        /// <param name="panel">Cut panel object</param>
        /// <param name="MouseX"></param>
        /// <param name="MouseY"></param>
        /// <returns>Selected detail or null</returns>
        public Detail processRightMouseClick(CutPanel panel, int MouseX, int MouseY)
        {
            int sel = findSelectedDetail(panel, MouseX, MouseY);
            if (sel != -1)
            {
                panel.PlacedDetails[sel].isSelected = true;
                panel.Redraw();
                return panel.PlacedDetails[sel];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Returns detail to show info about
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="MouseX"></param>
        /// <param name="MouseY"></param>
        /// <returns></returns>
        public Detail processMouseOver(CutPanel panel, int MouseX, int MouseY)
        {
            int sel = findSelectedDetail(panel, MouseX, MouseY);
            if (sel != -1)
            {
                return panel.PlacedDetails[sel];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Arranges detail in free panel. Panel Height must be enougth...
        /// </summary>
        /// <param name="panel"></param>
        public void arrangeDetails(CutPanel panel, SortType stype)
        {
            int space = 8;
            int detailspace = 8;
            
            switch (stype)
            {
                case SortType.ByHeight:
                    {
                        AlternativeSort.SortDetailsByHeight(panel.PlacedDetails);
                        break;
                    }
                case SortType.ByWidth:
                    {
                        AlternativeSort.SortDetailsByWidth(panel.PlacedDetails);
                        break;
                    }
                case SortType.ById:
                    {
                        AlternativeSort.SortDetailsByID(panel.PlacedDetails);
                        break;
                    }
            }

            int prevW = -1;
            int prevH = -1;
            var cluster = new Rectangle(space, space, 0, 0);
            int numofdetails = 0;
            int Yline = 0;

                foreach (Detail D in panel.PlacedDetails)
                {
                    if (D.Width != prevW || D.Height != prevH)
                    {
                        //Create new cluster
                        prevW = D.Width;
                        prevH = D.Height;
                        //See if we can place new cluster in this line
                        if (panel.WorkArea.Width > cluster.X + cluster.Width + D.Width + space)
                        {
                            cluster.X = cluster.X + cluster.Width + space; //Create new cluster
                            cluster.Height = D.Height;
                            cluster.Width = D.Width;
                            numofdetails = 0;
                        }
                        else
                        {
                            //Place cluster down
                            cluster.Y = Yline;
                            cluster.X = space;
                            cluster.Height = D.Height;
                            cluster.Width = D.Width;
                            numofdetails = 0;
                        }
                    }
                    //Finally place detail
                   // cpMove.MoveDetail(panel, panel, new Point(D.X, D.Y), new Point(cluster.X, cluster.Y + numofdetails * detailspace));

                    

                    D.X = cluster.X;
                    D.Y = cluster.Y + numofdetails*detailspace;
                    cluster.Height += detailspace;
                    numofdetails++;

                    if (cluster.Y + cluster.Height + space > Yline)
                    {
                        Yline = cluster.Y + cluster.Height + space;
                    }
                }
            
            //  panel.PlacedDetails.Reverse();
                panel.Redraw();
                cpMove.ClearUndoRedo();
        }

        /// <summary>
        /// Places detail rigth here
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="MouseX"></param>
        /// <param name="MouseY"></param>
        /// <param name="Det"></param>
        public void placeHere(CutPanel moveFrom, CutPanel moveOn, int MouseX, int MouseY, Detail Det)
        {
            Point pNew = moveOn.GetMouseClickPoint(MouseX, MouseY);
            cpMove.MoveDetail(moveFrom, moveOn, new Point(Det.X, Det.Y), pNew);
        }

        public void ProcessDrop(CutPanel droppedFrom, CutPanel droppedOn, int MouseX, int MouseY, bool AltPressed,
                                bool ShiftPressed, bool IgnoreAll, bool PlaceHere)
        {
            int i = 0;
            for (; i < droppedFrom.PlacedDetails.Count; i++)
            {
                if (droppedFrom.PlacedDetails[i].isSelected)
                {
                    droppedFrom.PlacedDetails[i].isSelected = false;
                    break;
                }
            }

            if (droppedOn.PanelId == 0 || PlaceHere)
            {
                placeHere(droppedFrom, droppedOn, MouseX, MouseY, droppedFrom.PlacedDetails[i]);                
            }
            else
            {
                try
                {
                    placeDetail(droppedFrom, droppedOn, droppedFrom.PlacedDetails[i], MouseX, MouseY, ShiftPressed, AltPressed, IgnoreAll);
                }
                catch { }
            }
        }

            /*  {
            var moveRec = new MoveHistory();
            moveRec.moveFrom = droppedFrom;
            moveRec.moveTo = droppedOn;
            
            //First find selected detail
            
            moveRec.oldDetailPlace = new Point(droppedFrom.PlacedDetails[i].X, droppedFrom.PlacedDetails[i].Y);
            //Second try to place those detail
            
            try
            {
                moveRec.newDetailPlace = placeDetail(droppedOn, droppedFrom.PlacedDetails[i], MouseX, MouseY,
                                                     
                droppedFrom.PlacedDetails.RemoveAt(i);
                //  _moveHistory.Push(moveRec);
                return moveRec;
            }
            catch
            {
            }
            return null;
        } */

   /*     public void ClearHistory()
        {
            _moveHistory.Clear();
        }

        public bool UndoLastStep()
        {
            if (_moveHistory.Count > 0)
            {
                MoveHistory last = _moveHistory.Pop();
                if (last.rotated == null)
                {
                    Detail Dt = null;
                    for (int i = 0; i < last.moveTo.PlacedDetails.Count; i++)
                    {
                        if (last.moveTo.PlacedDetails[i].X == last.newDetailPlace.X &&
                            last.moveTo.PlacedDetails[i].Y == last.newDetailPlace.Y)
                        {
                            Dt = last.moveTo.PlacedDetails[i];
                            last.moveTo.PlacedDetails.RemoveAt(i);
                            break;
                        }
                    }
                    if (Dt == null)
                    {
                        ClearHistory();
                        return false;
                    }
                    Dt.X = last.oldDetailPlace.X;
                    Dt.Y = last.oldDetailPlace.Y;
                    last.moveFrom.PlacedDetails.Add(Dt);
                    last.moveTo.Redraw();
                    last.moveFrom.Redraw();
                    return true;
                }
                else
                {
                    last.rotated.Rotate();
                    last.moveFrom.Redraw(); return true;
                }
            }
            else
            {
                return false;
            }
        }

       public void RecordStep(CutPanel movedFrom, CutPanel movedOn, Point oldCoord, Point newCoord)
        {
            _moveHistory.Push(new MoveHistory(movedFrom, movedOn, oldCoord, newCoord));
        }

        public void RecordStep(CutPanel rotatedOn, Detail rotated)
        {
            _moveHistory.Push(new MoveHistory(rotatedOn, rotated));
        } */

      /*  public void RecordStep(MoveHistory mh)
        {
            _moveHistory.Push(mh);
        } */
    }
}