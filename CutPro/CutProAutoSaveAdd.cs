/*
This file is part of Cut Micro.

Cut Micro is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.

Cut Micro is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with Cut Micro. If not, see http://www.gnu.org/licenses/.
*/
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using CPanel;
using CutMicro.Properties;

namespace CutMicro
{
    public partial class MainForm : Form
    {
        private readonly System.Timers.Timer _autosavetimer = new System.Timers.Timer();
        private readonly BindingList<DT> _detailsList = new BindingList<DT>();


        private void AutoSaveDDOpening(object sender, EventArgs e)
        {
            try
            {
                autosaveload.DropDownItems.Clear();
                foreach (string filename in Directory.GetFiles(Program.AutosavePath))
                {
                    if (filename.ToLower().EndsWith("cpxml"))
                        autosaveload.DropDownItems.Add(Path.GetFileName(filename));
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(Resources.Error__ + E.Message);
            }
            foreach (ToolStripItem T in autosaveload.DropDownItems)
            {
                T.Click += new EventHandler(LoadFileFromAutoSave);
            }
        }


        private void AutosaveloadDropDownOpened(object sender, EventArgs e)
        {
            try
            {
                ((ToolStripMenuItem)sender).DropDownItems.Clear();
                foreach (string filename in Directory.GetFiles(Program.AutosavePath))
                {
                    if (filename.ToLower().EndsWith("cpxml"))
                        ((ToolStripMenuItem)sender).DropDownItems.Add(Path.GetFileName(filename));
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(Resources.Error__ + E.Message);
            }
            foreach (ToolStripItem T in ((ToolStripMenuItem)sender).DropDownItems)
            {
                T.Click += new EventHandler(LoadFileFromAutoSave);
            }
        }

        void LoadFileFromAutoSave(object sender, EventArgs e)
        {
            string filename = Path.Combine(Program.AutosavePath, ((ToolStripItem) sender).Text);
            if (File.Exists(filename))
            {
                OpenProjDialog.FileName = filename;
                OpenFileDialog1FileOk(null, null);
            }
        }

        private bool save(bool saveAs)
        {
            try
            {
                if (saveAs || _projectPath == "")
                {
                    SaveProjDialog.ShowDialog();
                }
                else
                {
                    if (!_panelsCreated)
                        _fsl.saveProj(_projectPath, PanelsTableToPL(), DetailsTabletoDt(), null, null, (int)swidthUpDown.Value);
                    else
                        _fsl.saveProj(_projectPath, PanelsTableToPL(), DetailsTabletoDt(), PanelsToPanelsList(), PanelstoDetailList(), (int)swidthUpDown.Value);
                }
                this.Text = Program.Productname + " " + Path.GetFileName(_projectPath);
            }
            catch (Exception E)
            {
                MessageBox.Show(Resources.FSerr + E.Message, Resources.Error); return false;
            }
            return true;
        }

        private bool save(string fileName)
        {
            try
            {
                if (!_panelsCreated)
                    _fsl.saveProj(fileName, PanelsTableToPL(), DetailsTabletoDt(), null, null, (int)swidthUpDown.Value);
                else
                    _fsl.saveProj(fileName, PanelsTableToPL(), DetailsTabletoDt(), PanelsToPanelsList(), PanelstoDetailList(), (int)swidthUpDown.Value);

            }
            catch (Exception E)
            {
                MessageBox.Show(Resources.FSerr + E.Message, Resources.Error); return false;
            }
            return true;
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            _projectPath = SaveProjDialog.FileName;
            save(false);
        }

        private void saveProjButton_Click(object sender, EventArgs e)
        {
            save(false);
        }

        private void saveAsButton_Click(object sender, EventArgs e)
        {
            save(true);
        }

        void autosavetimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            string f1 = Path.GetFileNameWithoutExtension(_projectPath) + "_";
            if (_projectPath == "")
            {
                f1 = "unsaved_";
            }
            string f2 = DateTime.Now.ToShortDateString() + "_" + DateTime.Now.ToLongTimeString().Replace(':','-');
            string filename = Path.Combine(Program.AutosavePath, f1 + f2 + ".cpxml");
            save(filename);
        }

        private void UpdateTimer(bool start, int minutes)
        {
            _autosavetimer.Interval = minutes * 60000;
            _autosavetimer.Enabled = start;
            _autosavetimer.Start();
        }

    }
}
