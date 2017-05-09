/*
This file is part of Cut Micro.

Cut Micro is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.

Cut Micro is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with Cut Micro. If not, see http://www.gnu.org/licenses/.
*/
using System;
using System.IO;
using System.Windows.Forms;
using CutMicro.Properties;


namespace CutMicro
{
    public partial class OptionsForm : Form
    {
        Delegate apply;
        //Options opts;
        //optionsSaveLoad OSL;
        //string optsfile;
        public OptionsForm(Delegate applyOpts)
        {
            InitializeComponent();
            apply = applyOpts;
        }

        private void GetFileListSize()
        {
            long size = 0;
            FileInfo f;
            try
            {
                foreach (string fileName in Directory.GetFiles(Program.AutosavePath))
                {
                    if (fileName.ToLower().EndsWith("cpxml"))
                    {
                       f  = new FileInfo(fileName);
                        size += f.Length;
                    }
                }
                AutoSaveSize.Text = size.ToString();
            }
            catch
            {
                AutoSaveSize.Text = "Error";
            }
        }

        private void OptionsForm_Load(object sender, EventArgs e)
        {
            Settings.Default.Reload();
            BC.BackColor = Settings.Default.BackColor;
            LC.BackColor = Settings.Default.LineColor;
            HC.BackColor = Settings.Default.BackHatchColor;
            TC.BackColor = Settings.Default.FontColor;
            DC.BackColor = Settings.Default.DetailColor;
            FHC.BackColor = Settings.Default.FAHatchColor;
            FONT.Font = Settings.Default.TextFont;
            NormalAlt.Checked = !Settings.Default.AltX; ReverseAlt.Checked = Settings.Default.AltX;
            Supershift.Checked = Settings.Default.SuperShift; NormalShift.Checked = !Settings.Default.SuperShift;
            autoSaveCheckBox.Checked = Settings.Default.Autosave;
            autosaveinterval.Value = Settings.Default.AutoInterval;
            GetFileListSize();
        }

        private void BC_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = ((Button)sender).BackColor;
            colorDialog1.ShowDialog();
            ((Button)sender).BackColor = colorDialog1.Color;
        }

        private void FONT_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = FONT.Font;
            fontDialog1.ShowDialog();
            FONT.Font = fontDialog1.Font;
        }

        private void WriteOpts()
        {
            Settings.Default.BackColor = BC.BackColor;
            Settings.Default.LineColor = LC.BackColor;
            Settings.Default.BackHatchColor = HC.BackColor;
            Settings.Default.FontColor = TC.BackColor;
            Settings.Default.DetailColor = DC.BackColor;
            Settings.Default.FAHatchColor = FHC.BackColor;
            Settings.Default.TextFont = FONT.Font;
            Settings.Default.AltX = !NormalAlt.Checked;
            Settings.Default.SuperShift = Supershift.Checked;
            Settings.Default.Autosave = autoSaveCheckBox.Checked;
            Settings.Default.AutoInterval = (int)autosaveinterval.Value;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            WriteOpts();
            try
            {
                Invoke(apply);
            }
            catch { }
            Settings.Default.Save();
        }

        private void OKbutton_Click(object sender, EventArgs e)
        {
            WriteOpts();
            try
            {
                Invoke(apply);
            }
            catch { }
            Settings.Default.Save();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Settings.Default.Reset();
            try
            {
                Invoke(apply);
            }
            catch { }
            Settings.Default.Save(); ;
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClearAutoSave_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (string fileName in Directory.GetFiles(Program.AutosavePath))
                {
                    if (fileName.ToLower().EndsWith("cpxml"))
                    {
                        File.Delete(fileName);
                    }
                }
                GetFileListSize();
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileAssociation FA = new FileAssociation();
            FA.ContentType = "text/xml";
            FA.Extension = "cpxml";
            FA.FullName = Resources.Program_Productname + " File";
            FA.ProperName = Resources.Program_Productname + " File";
            FA.AddCommand("Open", Application.ExecutablePath + " \"%1\"");
            FA.IconPath = Application.ExecutablePath;
            FA.IconIndex = 0;
            FA.Create();
        }

    }
}
