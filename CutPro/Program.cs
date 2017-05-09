/*
This file is part of Cut Micro.

Cut Micro is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.

Cut Micro is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with Cut Micro. If not, see http://www.gnu.org/licenses/.
*/
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using CPanel;
using CutMicro.Properties;

namespace CutMicro
{
    static class Program
    {
        public static string Productname = Resources.Program_Productname;
        public static string UserPath = Path.Combine(Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)), Productname);
        public static string SettingPath = Path.Combine(UserPath, "options");
        public static string AutosavePath = Path.Combine(UserPath, "autosave");
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (!Directory.Exists(UserPath))
            {
                Directory.CreateDirectory(UserPath);
                if (!Directory.Exists(AutosavePath))
                {
                    Directory.CreateDirectory(AutosavePath);
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length > 0)
            {
                Application.Run(new MainForm(args[0]));
            }
            else
            {
                Application.Run(new MainForm(""));
            }
        }
    }
}
