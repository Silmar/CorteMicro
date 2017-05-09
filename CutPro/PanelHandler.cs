/*
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
using CutMicro.Properties;

namespace CutMicro
{
    public class PanelHandler
    {
        private readonly DetailPlacer _dp;
        private readonly AutoPlacer _ap;
        private readonly FlowLayoutPanel _f;
        private readonly SplitContainer _cont;
        private readonly ContextMenu _dtcontext;
        //private readonly Options _opts;
        private readonly ToolStripStatusLabel _statusLabel;

        private CutPanel _selectedPanel;
        private int _dtcontextX;
        private int _dtcontextY;

        /// <summary>
        /// Creates panels
        /// </summary>
        /// <param name="opts">Opts class</param>
        /// <param name="cont">Container to place workshop area</param>
        /// <param name="f">Container to place other panels</param>
        /// <param name="panels">List with panels</param>
        /// <param name="details">List with details. if PanelID set - it will place detail on that panel</param>
        /// <param name="statuslabel">Label to show detail info on mouseover</param>
        public PanelHandler(SplitContainer cont, FlowLayoutPanel f, List<CutPanel> panels,
                            List<Detail> details, ToolStripStatusLabel statuslabel)
        {
            cont.Visible = false;
            f.Visible = false;
            _cont = cont;
            _f = f;
            _dp = new DetailPlacer();
            _ap = new AutoPlacer();
           // _opts = opts;
            Plist = panels;
            Dlist = details;
            cont.Panel2.Controls.Clear();
            f.Controls.Clear();
            foreach (CutPanel cp in panels)
            {
                cp.MouseMove += CpMouseMove;
                cp.MouseDown += CpMouseDown;
                cp.DragDrop += CpDragDrop;
                cp.DragEnter += CpDragEnter;
                cp.GiveFeedback += CpGiveFeedback;
                cp.AllowDrop = true;
                cp.MouseEnter += CpMouseEnter;
                if (cp.PanelId == 0)
                {
                    cont.Panel2.Controls.Add(cp);
                }
                else
                {
                    f.Controls.Add(cp);
                }
            }

            foreach (Detail d in details)
            {
                if (d._panelID >= Plist.Count)
                {
                    d._panelID = 0;
                }
                Plist[d._panelID].PlacedDetails.Add(d);
            }
            // Application.DoEvents();
            /////////////////////////////////////////

            _dtcontext = new ContextMenu();
            _dtcontext.MenuItems.Add(Resources.PHRotate);
            _dtcontext.MenuItems[0].Click += RotateClick;
            _dtcontext.MenuItems.Add("-");
            _dtcontext.MenuItems.Add(Resources.Sort_details_id);
            _dtcontext.MenuItems[2].Click += sortByID;
            _dtcontext.MenuItems.Add(Resources.Sort_details_width);
            _dtcontext.MenuItems[3].Click += sortByWidth;
            _dtcontext.MenuItems.Add(Resources.Sort_details_height);
            _dtcontext.MenuItems[4].Click += sortBHeight;
            _dtcontext.MenuItems.Add("-");
            _dtcontext.MenuItems.Add(Resources.Delete_free_areas);
            _dtcontext.MenuItems[6].Click += DeleteFrearea;
            _dtcontext.MenuItems.Add(Resources.Clear_details);
            _dtcontext.MenuItems[7].Click += DeleteDetails;
            _dtcontext.Popup += DtcontextPopup;
            ////////////////////////////////////////
            _statusLabel = statuslabel;
            ApplyOptions();
            cont.Visible = true;
            f.Visible = true;
            Application.DoEvents();
        }

        void DtcontextPopup(object sender, EventArgs e)
        {
            _dtcontext.MenuItems[0].Enabled = false;
            Detail d = _dp.processRightMouseClick(_selectedPanel, _dtcontextX, _dtcontextY);
            if (d != null && d._CanBeRotated)
                    _dtcontext.MenuItems[0].Enabled = true;
        }

        private void DeleteDetails(object sender, EventArgs e)
        {
            if (_selectedPanel.PanelId != 0)
            {
                DeleteFrearea(null, null);
                foreach (Detail d in _selectedPanel.PlacedDetails)
                {
                    d.X = 0; d.Y = 0;
                    Plist[0].PlacedDetails.Add(d);
                }
                _selectedPanel.PlacedDetails.Clear();
                _selectedPanel.Redraw();
                SortWorkShop(SortType.ById);
            }
        }

        void CpMouseEnter(object sender, EventArgs e)
        {
            
            if (((CutPanel)sender).PanelId == 0)
            {
                _cont.Panel2.Focus();
            }
            else
            {
                _f.Focus();
            }
        }

        public List<CutPanel> Plist { get; protected set; }

        public List<Detail> Dlist { get; protected set; }

        private static void CpGiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            if (e.Effect == DragDropEffects.Move)
                e.UseDefaultCursors = false;
        }

        public void DeleteFrearea(object sender, EventArgs e)
        {
            Predicate<Detail> x = IsFreeArea;
            _selectedPanel.PlacedDetails.RemoveAll(x);
            _selectedPanel.Redraw();
        }

        public void DeleteAllFreeArea()
        {
            Predicate<Detail> x = IsFreeArea;
            foreach (CutPanel cp in Plist)
            {
                cp.PlacedDetails.RemoveAll(x);
                cp.Redraw();
            }
        }

        private void sortByWidth(object sender, EventArgs e)
        {
            _dp.arrangeDetails(Plist[0], SortType.ByWidth);
        }

        private void sortBHeight(object sender, EventArgs e)
        {
            _dp.arrangeDetails(Plist[0], SortType.ByHeight);
        }

        private void sortByID(object sender, EventArgs e)
        {
            _dp.arrangeDetails(Plist[0], SortType.ById);
        }

        /// <summary>
        /// Apply options like font, color, zoom etc
        /// </summary>
        public void ApplyOptions()
        {
            foreach (CutPanel panel in Plist)
            {
                panel.PTextFont = Settings.Default.TextFont;
                panel.PTextColor = Settings.Default.FontColor;
                panel.BackColor = Settings.Default.BackColor;
                panel.PFreeAreaHatchColor = Settings.Default.FAHatchColor;
                panel.PRectBorderColor = Settings.Default.LineColor;
                panel.PRectHatchColor = Settings.Default.BackHatchColor;
                panel.PRectColor = Settings.Default.DetailColor;
                panel.SetUp(CurrentZoom.Z);
            }
        }

        public void ReDrawAll()
        {
            foreach (CutPanel panel in Plist)
            {
                panel.Redraw();
            }
        }

        private void CpDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof (int)))
            {
                e.Effect = DragDropEffects.Move;
                Cursor.Current = DrawCursor.getCursor(Plist[(int)e.Data.GetData(typeof(int))], Settings.Default.LineColor,
                                                      Settings.Default.DetailColor);
            }
        }

        public void SortWorkShop(SortType stype)
        {
            if (Plist[0].WorkArea.Width == 0)
            {
                Plist[0].CreateControl();
                Application.DoEvents();
            }
            _dp.arrangeDetails(Plist[0], SortType.ById);
        }

        private void CpDragDrop(object sender, DragEventArgs e)
        {
            if (e.Effect != DragDropEffects.Move)
            {
                return;
            }
            Point clientPoint = ((CutPanel) sender).PointToClient(new Point(e.X, e.Y));
            bool alt = ((Control.ModifierKeys & Keys.Alt) > 0);
            bool shift = ((Control.ModifierKeys & Keys.Shift) > 0);
            bool ctrl = ((Control.ModifierKeys & Keys.Control) > 0);

            var pid = (int) e.Data.GetData(typeof (int));
            _dp.ProcessDrop(Plist[pid], ((CutPanel)sender), clientPoint.X, clientPoint.Y, alt ^ Settings.Default.AltX, shift,
                           Settings.Default.SuperShift, ctrl);
            Plist[pid].Redraw();
            ((CutPanel) sender).Redraw();
        }

        private void CpMouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Right) > 0 || (e.Button & MouseButtons.Middle) > 0)
            {
                RMouseDown(sender, e);
            }
            else
            {
                bool alt = false;
                bool shift = false;
                if ((Control.ModifierKeys & Keys.Shift) > 0)
                {
                    shift = true;
                }
                if ((Control.ModifierKeys & Keys.Alt) > 0)
                {
                    alt = true;
                }
                _dp.processMouseDown((CutPanel)sender, e.X, e.Y, alt ^ Settings.Default.AltX, shift);
            }
        }

        private void CpMouseMove(object sender, MouseEventArgs e)
        {
            //showtooltip
            Detail d = _dp.processMouseOver((CutPanel) sender, e.X, e.Y);
            if (d != null && d.isFreeArea)
                _statusLabel.Text = Resources.FAwidth1 + d.RealWidth + Resources.FAwidth2 + d.RealHeight +
                                    Resources.FAwidth3;
            else if (d != null && !d.isFreeArea)
                _statusLabel.Text = string.Format(Resources._Width_, d.detailID, d.etc, d.RealWidth,
                                                  Resources.FAwidth2, d.RealHeight, Resources.FAwidth3);
            else
                _statusLabel.Text = "";
        }

        private void RMouseDown(object sender, MouseEventArgs e)
        {
            _selectedPanel = (CutPanel)sender;
            _dtcontextX = e.X;
            _dtcontextY = e.Y;
            if ((e.Button & MouseButtons.Right) > 0 )
            {
                _dtcontext.Show((Control)sender, new Point(e.X, e.Y));
            }
            if ((e.Button & MouseButtons.Middle) > 0)
            {
                RotateClick(null, null);
            }

            
        }

        /// <summary>
        /// ROtateDetail
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RotateClick(object sender, EventArgs e)
        {
            Detail d = _dp.processRightMouseClick(_selectedPanel, _dtcontextX, _dtcontextY);
            if (d != null && d._CanBeRotated)
            {
                CPanel.cpMove.RotateDetail(_selectedPanel, d);
                
            }
        }

        /// <summary>
        /// Free RAM when design cancelling
        /// </summary>
        public void Dispose()
        {
            cpMove.ClearUndoRedo();
            _cont.Panel2.Controls.Clear();
            _f.Controls.Clear();
            foreach (CutPanel x in Plist)
            {
                x.Dispose();
            }
            Plist = null;
            Dlist = null;
            //_dp = null;
            //_ap = null;
        }

        /// <summary>
        /// Predicate for deleting all free areas
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public bool IsFreeArea(Detail d)
        {
            return d.isFreeArea;
        }

        public bool Undo()
        {
            if (cpMove.getUndoCount() == 0)
                return false;
            cpMove.undoLast();
                return true;
        }

        public bool Redo()
        {
            if (cpMove.getRedoCount() == 0)
                return false;
            cpMove.redoLast();
            return true;
        }

        public void ShowAutoPlaceWindow()
        {
            _ap.AutoPlaceHorizontalMethod(Plist);
            SortWorkShop(SortType.ById);
        }
    }
}