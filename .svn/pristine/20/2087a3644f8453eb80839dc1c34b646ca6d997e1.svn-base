/*
This file is part of Cut Micro.

Cut Micro is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.

Cut Micro is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with Cut Micro. If not, see http://www.gnu.org/licenses/.
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CutMicro.Properties;

namespace CutMicro
{
    public partial class AreaCalc : Form
    {
        public AreaCalc(double detarea, double panelsarea, double panelsgarea)
        {
            InitializeComponent();
            textBox1.Text = (panelsarea/1000000).ToString();
            textBox2.Text = (panelsgarea/1000000).ToString();
            textBox3.Text = (detarea / 1000000).ToString();
            if (panelsgarea <= detarea)
            { label4.Text = Resources.Status_Fail; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
