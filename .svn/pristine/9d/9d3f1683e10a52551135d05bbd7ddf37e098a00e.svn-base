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
using System.IO;
using CPanel;
using CutMicro.Properties;

namespace CutMicro
{
    public partial class MainForm
    {
        PanelHandler _ph;
 //       Options _o = new Options();
 //       optionsSaveLoad _optsave;
        private delegate void Applyopts();

        private string _projectPath = "";
        private bool _panelsCreated = false;
        readonly FileSaveLoad _fsl = new FileSaveLoad();
        //TODO: move browser to another form
        private WebBrowser PrintWebBrowser = new WebBrowser();
        
        /// <summary>
        /// Form initializing
        /// </summary>
        /// <param name="filename"></param>
        public MainForm(string filename)
        {
            InitializeComponent();

            if (File.Exists(filename))
            {
                _projectPath = filename;
            }
            PanelsTable.AutoGenerateColumns = false; DetailsTable.AutoGenerateColumns = false;
            PrintWebBrowser.Parent = this;
            PrintWebBrowser.CreateControl();
            PrintWebBrowser.DocumentCompleted += PrintWebBrowserDocumentCompleted;
        }

        /// <summary>
        /// Show print preview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PrintWebBrowserDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            PrintWebBrowser.ShowPrintPreviewDialog();
        }

        /// <summary>
        /// Load options, open project if needed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormLoad(object sender, EventArgs e)
        {
            //_optsave = new optionsSaveLoad(ref _o);
           // _o = _optsave.LoadOrCreateDefault(Program.SettingPath);
            _autosavetimer.Elapsed += autosavetimer_Elapsed;
            UpdateTimer(Settings.Default.Autosave, Settings.Default.AutoInterval);

            if (_projectPath == "")
            {
                NewProj();
            }
            else
            {
                LoadProj();
            }
        }

        /// <summary>
        /// On zoom changing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Zoom(object sender, EventArgs e)
        {
            CurrentZoom.Z = zoomComboBox.SelectedIndex + 2;
            if (_ph != null)
            {
                _ph.ApplyOptions();
            }
        }

        /// <summary>
        /// Locks gui components
        /// </summary>
        private void SealGui(bool unSeal)
        {
            swidthUpDown.Enabled = unSeal;
            PanelsTable.ReadOnly = !unSeal;
            DetailsTable.ReadOnly = !unSeal;
            createPlanesButton.Enabled = unSeal;
            destructPlanesButton.Enabled = !unSeal;
// ReSharper disable PossibleNullReferenceException
            PanelsTable.Columns["PID"].ReadOnly = true;
            DetailsTable.Columns["DID"].ReadOnly = true;
// ReSharper restore PossibleNullReferenceException
            toolStrip2.Enabled = !unSeal;
        }

        /// <summary>
        /// Clears panels, tables, etc
        /// </summary>
        private void ClearGui()
        {
            PanelsTable.DataSource = _cPanelsList;
            DetailsTable.DataSource = _detailsList;
            PanelsTable.Rows.Clear();// _cPanelsList.Clear();
            DetailsTable.Rows.Clear(); //    _detailsList.Clear();
            zoomComboBox.SelectedIndex = 4;
            FlowPanel.Controls.Clear();
            splitContainer1.Panel2.Controls.Clear();
        }

        private void NewProj()
        {
            ClearGui();
            _panelsCreated = false;
            SealGui(true);
            /////////////////////                    
            Text = Program.Productname + Resources.__new_project_;
            _projectPath = "";
        }

        private void LoadProj()
        {
            ClearGui();
            try
            {
                List<CutPanel> panels;
                List<Detail> details;
                int sawSize;
                bool panelIsCreated;
// ReSharper disable InconsistentNaming
                DT[] _details;
                PL[] _panels;
// ReSharper restore InconsistentNaming
                _fsl.loadFile(_projectPath, out panels, out details, out sawSize, out panelIsCreated, out _details, out _panels);
                swidthUpDown.Value = sawSize;
                if (panelIsCreated)
                {
                    PLDTToTables(_panels, _details);
                    CreatePanels(panels, details, sawSize);
                    _panelsCreated = true;
                }
                else
                {
                    PLDTToTables(_panels, _details);
                }
                SealGui(!panelIsCreated);
                // DetailsTable.Invalidate();
                // PanelsTable.Invalidate();                
            }
            catch (Exception e)
            {
                MessageBox.Show(Resources.FloadErr + e.Message, Resources.Error);
                NewProj();
            }
            Text = Program.Productname + " " + Path.GetFileName(_projectPath);
        }

        private void newProjButton_Click(object sender, EventArgs e)
        {
            var proc = new System.Diagnostics.Process();
            proc.EnableRaisingEvents = false;
            proc.StartInfo.FileName = Application.ExecutablePath;
            proc.Start();
        }

        private void OpenProjButtonClick(object sender, EventArgs e)
        {
            OpenProjDialog.ShowDialog();
        }

        private void DestructPanels()
        {
            MainTabControl.SelectedIndex = 0;
            if (_ph != null)
            {
                _ph.Dispose();
                _ph = null;
            }
            _panelsCreated = false;
            SealGui(true);
            GC.Collect();
        }

        void DestructPlanesButtonClick(object sender, EventArgs e)
        {
            if (MessageBox.Show(Resources.Delete_design_msg, Resources.Are_you_sure_, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DestructPanels();
            }
        }

        private void OpenFileDialog1FileOk(object sender, CancelEventArgs e)
        {
            if (File.Exists(OpenProjDialog.FileName))
            {
                if (OpenProjDialog.FileName == _projectPath || (!_panelsCreated && PanelsTable.Rows.Count == 1 && DetailsTable.Rows.Count == 1 && _projectPath == ""))
                {
                    DestructPanels();
                    _projectPath = OpenProjDialog.FileName;
                    LoadProj();
                }
                else
                {
                    var proc = new System.Diagnostics.Process();
                    proc.EnableRaisingEvents = false;
                    proc.StartInfo.FileName = Application.ExecutablePath;
                    proc.StartInfo.Arguments = "\"" + OpenProjDialog.FileName + "\"";
                    proc.Start();
                }
            }
        }

        private void SumCalc()
        {
            if (CheckPL() && CheckDt())
            {
                int detailsArea = 0;
                int panelsArea = 0;
                int panelsGoodArea = 0;

                foreach (var p in _cPanelsList)
                {
                    panelsArea += p.Height * p.Width * p.Quantity;
                    panelsGoodArea += (p.Height - p.Cutup - p.Cutdown) * (p.Width - p.Cutright - p.Cutleft) * p.Quantity;
                }
                foreach (var d in _detailsList)
                {
                    detailsArea += d._height * d._width * d._quantity;
                }
                var a = new AreaCalc(detailsArea, panelsArea, panelsGoodArea);
                a.Show();
            }
        }

        private void ApplyOpts()
        {
            UpdateTimer(Settings.Default.Autosave, Settings.Default.AutoInterval);
            if (_ph != null)
            {
                _ph.ApplyOptions();
            }
        }


        private void ToolStripButton1Click(object sender, EventArgs e)
        {
            foreach (Form openform in Application.OpenForms)
            {
                if (openform.GetType() == typeof(OptionsForm)) { openform.BringToFront(); return; } //Check if optform is not opened
            }
            var ao = new Applyopts(ApplyOpts);
            var optform = new OptionsForm(ao);
            optform.Show();
        }


        private void OnExit(object sender, FormClosingEventArgs e)
        {
            if (PanelsTable.Rows.Count > 1 || DetailsTable.Rows.Count > 1 || _projectPath != "")
            {
                DialogResult result = MessageBox.Show(Resources.Do_you_want_to_save_your_project_, Resources.Exit, MessageBoxButtons.YesNoCancel);
                switch (result)
                {
                    case (DialogResult.Yes):
                        {
                            e.Cancel = !save(false);
                            break;
                        }
                    case (DialogResult.No):
                        {
                            break;
                        }
                    case (DialogResult.Cancel):
                        {
                            e.Cancel = true;
                            break;
                        }
                }
            }
        }

        private void UndoButtonClick(object sender, EventArgs e)
        {
            if (_ph != null)
            {
                if (!_ph.Undo())
                {
                    statusLabel.Text = Resources.Cannot_undo_last_move;
                }
            }
        }

        private void SortButton_Click(object sender, EventArgs e)
        {

        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var t = new Printing(DetailsTabletoDt(), PanelsTableToPL(), _ph);
            var f = t.CreateReport();
            System.Diagnostics.Process.Start(f);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var t = new Printing(DetailsTabletoDt(), PanelsTableToPL(), _ph);
            var f = t.CreateReport();
            PrintWebBrowser.Url = new Uri(f);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void DeleteAfaButtonClick(object sender, EventArgs e)
        {
            if (_ph != null)
                _ph.DeleteAllFreeArea();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var a = new AboutBox();
            a.Show();
        }
        // ///////////////////////////////////////////
        private void RecentDDOpening(object sender, EventArgs e)
        {

        }

        void LoadFileFromLast(object sender, EventArgs e)
        {
            OpenProjDialog.FileName = (string)((ToolStripDropDownItem)sender).Tag;
            if (File.Exists(OpenProjDialog.FileName))
                OpenFileDialog1FileOk(null, null);
            else
                MessageBox.Show(Resources.File_not_found);
        }

        private void RedoButtonClick(object sender, EventArgs e)
        {
            if (_ph != null)
            {
                if (!_ph.Redo())
                {
                    statusLabel.Text = Resources.Cannot_redo_last_move;
                }
            }
        }

        private void widthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_ph != null)
                _ph.SortWorkShop(SortType.ByWidth);
        }

        private void heightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_ph != null)
                _ph.SortWorkShop(SortType.ByHeight);
        }

        private void iDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_ph != null)
                _ph.SortWorkShop(SortType.ById);
        }

        private void autoplacebutton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Resources.Auto_design_msg, Resources.Are_you_sure_, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (_ph != null)
                    _ph.ShowAutoPlaceWindow();
            }
        }


/*
        void MainForm_Click(object sender, EventArgs e)
        {
            Settings.Default.LastOpened.Clear();
            Settings.Default.Save();
        }
*/


    }
}
