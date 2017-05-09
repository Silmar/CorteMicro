/*
This file is part of Cut Micro.

Cut Micro is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.

Cut Micro is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with Cut Micro. If not, see http://www.gnu.org/licenses/.
*/
using System;
using System.ComponentModel;
using System.Windows.Forms;
using CPanel;
using CutMicro.Properties;


namespace CutMicro
{
    public partial class MainForm
    {
        private void dataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            ((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = Resources.Invalid_value;
            ((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
            ((DataGridView)sender).UpdateCellErrorText(e.ColumnIndex, e.RowIndex);
            e.Cancel = false;
        }

        private void cellValid(object sender, DataGridViewCellEventArgs e)
        {
            ((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = null;
            ((DataGridView)sender).UpdateCellErrorText(e.ColumnIndex, e.RowIndex);
        }

        private void Checkcontext(object sender, CancelEventArgs e)
        {
            if (_panelsCreated)
            {
                e.Cancel = true; //If we start designing - do not show context
                MiniContext.Show(MousePosition); //Show this instead
            }
        }

        private void Autonum(object sender, DataGridViewRowsAddedEventArgs e)
        {
            string columnname;
            if (((DataGridView)sender).Columns.Contains("PID"))
            { columnname = "PID"; }
            else
            { columnname = "DID"; }
            int i = 1;
            foreach (DataGridViewRow c in ((DataGridView)sender).Rows)
            {
                if (!c.IsNewRow)
                {
                    c.Cells[columnname].Value = i.ToString(); i++;
                }
            }
        }

        private void AutonumR(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            Autonum(sender, null);
        }

        private void DtTableNormalizeSelection()
        {
            foreach (DataGridViewCell c in DetailsTable.SelectedCells)
            {
                DetailsTable.Rows[c.RowIndex].Selected = true;
            }
        }

        private static void SelectNextEditableCell(DataGridView dataGrid)
        {
            int columnIndex = dataGrid.CurrentCell.ColumnIndex;
            int rowIndex = dataGrid.CurrentCell.RowIndex;
            while (dataGrid.Rows[rowIndex].Cells[columnIndex].ReadOnly)
            {
                columnIndex++;
                if (columnIndex >= dataGrid.Columns.Count)
                {
                    rowIndex++;
                    columnIndex = 0;
                }
                if (rowIndex >= dataGrid.Rows.Count)
                    return;
            }
            dataGrid.CurrentCell = dataGrid.Rows[rowIndex].Cells[columnIndex];
        }

        private void TableKeyUp(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 9 && ((DataGridView)sender).CurrentCell.ReadOnly)
            {
                SelectNextEditableCell((DataGridView)sender);
            }
        }

        //////CONTEXT MENU ACTIONS///////

        //T: WORKS WELL
        private void DeleteToolStripMenuItemClick(object sender, EventArgs e) //DETAILS
        {
            DtTableNormalizeSelection();
            foreach (DataGridViewRow r in DetailsTable.SelectedRows)
                if (!r.IsNewRow) 
                    _detailsList.Remove((DT) r.DataBoundItem);
            DetailsTable.Invalidate();
        }

        //T: WORKS WELL
        private void RotateSelectedToolStripMenuItemClick(object sender, EventArgs e) //DETAILS
        {
            DtTableNormalizeSelection();
            foreach (DataGridViewRow r in DetailsTable.SelectedRows)
                if (!r.IsNewRow) 
                    ((DT) r.DataBoundItem)._canberotated = true;
            DetailsTable.Invalidate();
        }

        //T: WORKS WELL
        private void DoNotRotateToolStripMenuItemClick(object sender, EventArgs e) //DETAILS
        {
            DtTableNormalizeSelection();
            foreach (DataGridViewRow r in DetailsTable.SelectedRows)
                if (!r.IsNewRow)
                    ((DT)r.DataBoundItem)._canberotated = false;
            DetailsTable.Invalidate();
        }

 //       private void DtTableOnKeyPress(object sender, KeyPressEventArgs e)
 //       {
 //           if ((ModifierKeys & Keys.Control) > 0 && e.KeyChar == 22 &&
 //               DetailsTable.SelectedCells.Count == 1 && !DetailsTable.IsCurrentCellInEditMode)
 //           {
 //               DetailsTable.SelectedCells[0].Value = Clipboard.GetText();
 //          }
 //       }

        //T: WORKS WELL
        private void CalcAreaToolStripMenuItemClick(object sender, EventArgs e) //BOTH
        {
            SumCalc();
        }

        //T: WORKS WELL
        private void ForceCheckToolStripMenuItemClick(object sender, EventArgs e) //BOTH
        {
            CheckPL(); CheckDt();
        }

        private void Delete2ToolStripMenuItem1Click(object sender, EventArgs e) //PANELS
        {
            foreach (DataGridViewCell c in PanelsTable.SelectedCells)
            {
                PanelsTable.Rows[c.RowIndex].Selected = true;
            }
            foreach (DataGridViewRow r in PanelsTable.SelectedRows)
                if (!r.IsNewRow) 
                    _cPanelsList.Remove((PL) r.DataBoundItem);

            PanelsTable.Invalidate();
        }

    }
}
