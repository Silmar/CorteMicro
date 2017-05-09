/*
This file is part of Cut Micro.

Cut Micro is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.

Cut Micro is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with Cut Micro. If not, see http://www.gnu.org/licenses/.
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.IO;

namespace HtmlReport
{
    public class HtmlWrite
    {
        private readonly TextWriter _txtwrite;
        private readonly HtmlTextWriter _htw;
        public string Filename { get; protected set; }
        public string Imgdir { get; protected set; }

        public HtmlWrite()
        {
            string tempPath = Path.GetTempPath();
            string tempdir = "PrintReport" + DateTime.Now.ToBinary();
            Directory.CreateDirectory((Path.Combine(tempPath, tempdir)));
            Directory.CreateDirectory(Path.Combine(Path.Combine(tempPath, tempdir), "img"));
            _txtwrite = new StreamWriter(Path.Combine(Path.Combine(tempPath, tempdir), "report.html"), false, Encoding.UTF8);
            _htw = new HtmlTextWriter(_txtwrite);
            Filename = (Path.Combine(Path.Combine(tempPath, tempdir), "report.html"));
            Imgdir = Path.Combine(Path.Combine(tempPath, tempdir), "img");
        }

        public void WriteHead(string caption)
        {
            _htw.WriteLine("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\"> <html> <head> <title>");
            _htw.WriteLine(caption);
            _htw.WriteLine("</title></head><body>");
        }

        public void PageBreak()
        {
            _htw.WriteLine("<p style=\"page-break-before: always\"> </p>");
        }

        public void WriteTable(List<string[]> data, string caption)
        {
            _htw.WriteLine("<p style=\"font-size: small\">");
            _htw.WriteLine(caption);
            _htw.WriteLine("<table cellpadding=\"1\" cellspacing=\"0\" style=\"margin: 0px; padding: 0px; width: 100%; border: 1px single black\">");
            foreach(string[] row in data)
            {
                _htw.WriteLine("<tr>");
                foreach (string cell in row)
                {
                    _htw.Write("<td>");
                    _htw.Write(string.IsNullOrEmpty(cell) ? "-" : cell);
                    _htw.WriteLine("</td>");
                }
                _htw.WriteLine("</tr>");
            }
            _htw.WriteLine("</table>");
            _htw.WriteLine("</p>");
        }

        public void WriteText(string text)
        {
            _htw.WriteLine("<p>");
            _htw.WriteLine(text);
            _htw.WriteLine("</p>");
        }

        public void WriteHeadLine(string text, int size)
        {
            _htw.WriteLine("<h"+size+">");
            _htw.WriteLine(text);
            _htw.WriteLine("</h" + size + ">");
        }

        public void WriteInfoAndImg(string info, string caption, string img)
        {
            _htw.WriteLine("<p>");
            _htw.WriteLine(caption);
            _htw.WriteLine("<table border=\"1\" cellpadding=\"1\" cellspacing=\"0\" style=\"margin: 0px; padding: 0px; width: 100%; font-size: xx-small; page-break-inside: avoid;\">");
            _htw.WriteLine("<tr>");          
            _htw.WriteLine("<td width=\"80%\"><img src=\"" + img + "\"/></td>");
            _htw.WriteLine("<td>" + info + "</td>"); 
            _htw.WriteLine("</tr>");
            _htw.WriteLine("</table>");
            _htw.WriteLine("</p>");
        }

        public void WriteImg(string img)
        {
            _htw.WriteLine("<p>");
            _htw.WriteLine("<img src=\"" + img + "\"/>");
            _htw.WriteLine("</p>");
        }

        public void WriteTail()
        {
            _htw.WriteLine("</body></html>");
            _txtwrite.Close();
            
        }
    }
}