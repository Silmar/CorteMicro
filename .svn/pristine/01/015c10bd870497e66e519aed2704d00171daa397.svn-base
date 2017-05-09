/*
This file is part of Cut Micro.

Cut Micro is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.

Cut Micro is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with Cut Micro. If not, see http://www.gnu.org/licenses/.
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using CPanel;
using CutMicro.Properties;

namespace CutMicro
{
    public partial class MainForm
    {
        private readonly BindingList<PL> _cPanelsList = new BindingList<PL>();
        ///////////////////////////////////////////////////////////////////////////////
        ////////PANELS AND DETAILS CONVERTING CODE//////////////////////////////////////
        private bool CheckPL()
        {
            if (PanelsTable.Rows.Count == 1) { return false; }
            PanelsTable.EndEdit();
            bool ok = true;
            for (int i = 0; i < PanelsTable.Rows.Count; i++)
            {
                PanelsTable.Rows[i].ErrorText = null;
                if (!PanelsTable.Rows[i].IsNewRow)
                {
                    PL panel = _cPanelsList[i];
                    if (panel.Cutdown < 0 || panel.Cutleft < 0 || panel.Cutright < 0 || panel.Cutup < 0
                    || panel.Height < 10 || panel.Width < 10 || panel.Quantity < 1)
                    {
                        PanelsTable.Rows[i].ErrorText = Resources.Invalid_value; ok = false;
                    }
                    if (panel.Cutleft + panel.Cutright > panel.Width || panel.Cutup + panel.Cutdown > panel.Height)
                    {
                        PanelsTable.Rows[i].ErrorText = Resources.Width_or_height_error; ok = false;
                    }
                }
                PanelsTable.UpdateRowErrorText(i);
            }       
            return ok;
        }

        private bool CheckDt()
        {
            if (DetailsTable.Rows.Count == 1) { return false; }
            DetailsTable.EndEdit();
            bool ok = true;
            for (int i = 0; i < DetailsTable.Rows.Count; i++)
            {
                DetailsTable.Rows[i].ErrorText = null;
                if (!DetailsTable.Rows[i].IsNewRow)
                {
                    DT det = _detailsList[i];
                    if (det._height < 1 || det._width <1 || det._quantity < 1)
                    {
                        DetailsTable.Rows[i].ErrorText = Resources.Invalid_value; ok = false;
                    }
                }
                DetailsTable.UpdateRowErrorText(i);
            }
            return ok;
        }        

        /// <summary>
        /// Start placing details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreatePlanesButtonClick(object sender, EventArgs e)
        {
            CancelEdit();
            if (CheckPL() && CheckDt())
            {
                _panelsCreated = true;
                CreatePanels();
                MainTabControl.SelectedIndex = 1;
            }
        }
      
        private List<Detail> DetailsTableToList()
        {
        	var dlist = new List<Detail>();
            DetailsTable.EndEdit();
            Application.DoEvents();
        	foreach(DT det in _detailsList)
        	{
        		for (int i = 0; i<det._quantity; i++)
        		{
                    dlist.Add(new Detail(0, 0, det._width, det._height, det._DetID, det._txt, det._canberotated, 0));
        		}
        	}
            if (DetailsTable.CurrentRow != null)
                if (DetailsTable.CurrentRow.IsNewRow)
                {
                    dlist.RemoveAt(dlist.Count - 1);
                }
            return dlist;
        } 	//Tables -> data
        												//On panels creation
        private List<CutPanel> PanelsTableToList()
        {
            int j = 1;
        	var plist = new List<CutPanel>();
            PanelsTable.EndEdit();
            Application.DoEvents();
        	foreach(PL pan in _cPanelsList)
        	{
        		for ( int i = 0 ; i<pan.Quantity; i++)
        		{
        			plist.Add(new CutPanel(pan.Height, pan.Width, j, pan.Cutleft, pan.Cutright, pan.Cutup, pan.Cutdown, pan.Txt));
                    j++;
        		}
        	}
            if (PanelsTable.CurrentRow != null && PanelsTable.CurrentRow.IsNewRow)
                    plist.RemoveAt(plist.Count - 1);
            return plist;
        }
        
        private List<Detail> PanelstoDetailList()
        {
            List<CutPanel> panels = PanelsToPanelsList();
            UpdatePanelId(panels);
            var l = new List<Detail>();
            foreach (var c in panels)
            {
                foreach (var d in c.PlacedDetails)
                {
                    if (!d.isFreeArea)
                    {
                        l.Add(d);
                    }
                }
            }
            return l;
        }	//Panels -> data
														//On save started proj
        private List<CutPanel> PanelsToPanelsList()
        {
            var l = new List<CutPanel>();
            foreach (Control c in splitContainer1.Panel2.Controls)
            {
                if (c.GetType() == typeof(CutPanel))
                {
                    l.Add((CutPanel)c); break;
                }
            }
            foreach (Control c in FlowPanel.Controls)
            {
                if (c.GetType() == typeof(CutPanel))
                {
                    l.Add((CutPanel)c);
                }
            }
            return l;
        }

        private void CancelEdit()
        {
            PanelsTable.CancelEdit();
            DetailsTable.CancelEdit();
        }

        private PL[] PanelsTableToPL()
        {
        	//PanelsTable.CancelEdit();
        	var pl = new PL[_cPanelsList.Count]; 
        	_cPanelsList.CopyTo(pl,0);
        	return pl;
        }				//Tables -> PL/DT
        												//On save any proj		        
        private DT[] DetailsTabletoDt()
        {
        	//DetailsTable.CancelEdit();
        	var dt = new DT[_detailsList.Count];
        	_detailsList.CopyTo(dt, 0);
        	return dt;
        }
        
        private void PLDTToTables(IEnumerable<PL> pl, IEnumerable<DT> dt)
        {
        	_cPanelsList.Clear();
        	foreach(var p in pl)
        	{
        		_cPanelsList.Add(p);
        	}
        	_detailsList.Clear();
        	foreach(var d in dt)
        	{
        		_detailsList.Add(d);
        	}
			//DetailsTable.DataSource = dt;
			//PanelsTable.DataSource = pl;			
        }	//PL/DT -> Tables
        												//On load any proj
        
     	/// <summary>
     	/// Create panels from scratch
     	/// </summary>
        public void CreatePanels()
     	{
     	    CurrentZoom.Z = zoomComboBox.SelectedIndex + 2;
        	SawSize.S = (int)swidthUpDown.Value;
            List<CutPanel> plist = PanelsTableToList();
            plist.Insert(0, new CutPanel(getFreePanelHeight(_detailsList), getFreePanelWidth(_detailsList), 0, 0, 0, 0, 0, Resources.FREE));
            
            _ph = new PanelHandler(splitContainer1, FlowPanel, plist, DetailsTableToList(), statusLabel);
            _ph.SortWorkShop(SortType.ById);
            SealGui(false);
        }


        /// <summary>
        /// Create panels from loaded data
        /// </summary>
        /// <param name="panels"></param>
        /// <param name="details"></param>
        /// <param name="sawsize"></param>
        public void CreatePanels(List<CutPanel> panels, List<Detail> details, int sawsize)
        {
            CurrentZoom.Z = zoomComboBox.SelectedIndex + 2;
        	SawSize.S = sawsize;            
            _ph = new PanelHandler(splitContainer1, FlowPanel, panels, details, statusLabel);
           // _ph.SortWorkShop();
            SealGui(false);
        }

        /////
        //////END//////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////


        private int getFreePanelWidth(BindingList<DT> details)
        {
            int _calculatedWidth = 100;
            List<int> usedids = new List<int>();
            foreach (DT det in details)
            {
                if (!usedids.Contains(det._DetID))
                {
                    usedids.Add(det._DetID);
                    _calculatedWidth += Math.Max(det._height, det._width);
                }
            }
            return (int)(_calculatedWidth * 3);
        }

        private int getFreePanelHeight(BindingList<DT> details)
        {
            int _calculatedHeight = 100;
            foreach (DT det in details)
            {
                if (_calculatedHeight < Math.Max(det._height, det._width))
                    _calculatedHeight = Math.Max(det._height, det._width);
            }
            return (int)(_calculatedHeight * 2);
        }

        /// <summary>
        /// Update panelID for every detail. 
        /// </summary>
        /// <param name="panels"></param>
        private static void UpdatePanelId(List<CutPanel> panels)
        {
            for(var i = 0; i< panels.Count; i++)
            {
                foreach (var d in panels[i].PlacedDetails)
                {
                    d._panelID = i;
                }
            }
        }
    }
}
