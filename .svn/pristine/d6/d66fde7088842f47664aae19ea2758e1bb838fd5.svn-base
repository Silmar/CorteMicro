/*
This file is part of Cut Micro.

Cut Micro is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.

Cut Micro is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with Cut Micro. If not, see http://www.gnu.org/licenses/.
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using CPanel;
using CutMicro.Properties;
using HtmlReport;
using System.IO;

namespace CutMicro
{
    class Printing
    {
        private const int MaxWPixels = 560;
        private const int MaxHPixels = 540;
        private readonly DT[] _detailsTable;
        private readonly PL[] _panelsTable;
        private readonly PanelHandler _ph;

        /// <summary>
        /// Printing
        /// </summary>
        /// <param name="detailsTable">DT struct</param>
        /// <param name="panelsTable">PL struct</param>
        /// <param name="ph">Panel handler.</param>
        public Printing(DT[] detailsTable, PL[] panelsTable, PanelHandler ph)
        {
            _detailsTable = detailsTable;
            _panelsTable = panelsTable;
            _ph = ph;
        }

        /// <summary>
        /// Returns number of detail
        /// </summary>
        /// <returns></returns>
        private string GetTotalDetails()
        {
            int detnum = 0;
            foreach (DT d in _detailsTable)
            {
                detnum += d._quantity;
            }
            return detnum.ToString();
        }

        /// <summary>
        /// Returns total detail area
        /// </summary>
        /// <returns></returns>
        private string GetTotalDetailsArea()
        {
            double detarea = 0;
            foreach (DT d in _detailsTable)
            {
                detarea += d._width*d._height*d._quantity;
            }
            return (detarea/1000000).ToString();
        }

        /// <summary>
        /// Returns number of unplaced detail
        /// </summary>
        /// <returns></returns>
        private static string GetTotalDetailsByPanel(CutPanel pan)
        {
            long detnum = 0;
            foreach (Detail d in pan.PlacedDetails)
            {
                if (!d.isFreeArea)
                    detnum++;
            }
            return detnum.ToString();
        }

        /// <summary>
        /// Details table to print
        /// </summary>
        /// <returns></returns>
        private List<string[]> GetDetailsTable()
        {
            var det = new List<string[]> {new[] {"#", Resources.Width, Resources.Height, Resources.Quantity, Resources.Text, Resources.Can_be_rotated}};
            foreach(DT d in _detailsTable)
            {
                det.Add(new[] { d._DetID.ToString(),
                d._width.ToString(), d._height.ToString(),
                d._quantity.ToString(), d._txt, d._canberotated.ToString()});
            }
            return det;
        }

        /// <summary>
        /// Panels table to print
        /// </summary>
        /// <returns></returns>
        private List<string[]> GetPanelsTable()
        {
            var pan = new List<string[]>
                          {
                              new[]
                                  {
                                      "#", Resources.Width, Resources.Height, Resources.Quantity, Resources.Text, Resources.Trim_left, Resources.Trim_right, Resources.Trim_up,
                                      Resources.Trim_Down
                                  }
                          };
            foreach (PL d in _panelsTable)
            {
                pan.Add(new[]
                            {
                                d.Id.ToString(), d.Width.ToString(), d.Height.ToString(), d.Quantity.ToString(),
                                d.Txt, d.Cutleft.ToString(), d.Cutright.ToString(), d.Cutup.ToString(),
                                d.Cutdown.ToString()
                            });  
            }
            return pan;
        }

        /// <summary>
        /// Generates bitmaps to fit in MaxPixels const. Places it in outdir with filenames like "1P.png", "2P.png"
        /// </summary>
        /// <param name="outdir"></param>
        private void GenerateCpanelImg(string outdir)
        {
            var renderPanel= new CutPanel
                                      {
                                          BackColor = Color.White,
                                          PTextColor = Color.Black,
                                          PTextFont = _ph.Plist[0].PTextFont,
                                          PRectBorderColor = Color.Black,
                                          PRectHatchColor = Color.LightGray,
                                      };
            //I will try to save memory here and do not create a lot of CutPanels
            foreach (CutPanel x in _ph.Plist)
            {
                if (x.PanelId != 0)
                {
                    renderPanel.PlacedDetails.Clear();
                    renderPanel.PboardHeight = x.PboardHeight;
                    renderPanel.PboardWidth = x.PboardWidth;
                    renderPanel.Txt = x.Txt;
                    renderPanel.PanelId = x.PanelId;
                    renderPanel.PCutDown = x.PCutDown;
                    renderPanel.PCutUp = x.PCutUp;
                    renderPanel.PCutLeft = x.PCutLeft;
                    renderPanel.PCutRight = x.PCutRight;

                    foreach (Detail d in x.PlacedDetails)
                        if (!d.isFreeArea)
                            renderPanel.PlacedDetails.Add(d);

                    double j = (double)x.PboardWidth / MaxWPixels;
                    double i = (double)x.PboardHeight / MaxHPixels;
                    renderPanel.SetUp(j > i ? j : i);
                    Bitmap b = renderPanel.GetBitmap();
                    try
                    {
                        b.Save(Path.Combine(outdir, x.PanelId + "P.png"),
                               System.Drawing.Imaging.ImageFormat.Png);
                    }
// ReSharper disable EmptyGeneralCatchClause
                    catch
// ReSharper restore EmptyGeneralCatchClause
                    {
                    }
                    b.Dispose();
                }
            }
            renderPanel.Dispose();
        }

        /// <summary>
        /// Returns: 0 - total area, 1 - used, 2 - usage percentage. All in M^2
        /// </summary>
        /// <param name="pan"></param>
        /// <returns></returns>
        private static string[] GetAreaByPanel(CutPanel pan)
        {
            var outstr = new string[3];
            double total = ((pan.PboardHeight - pan.PCutUp - pan.PCutDown)*(pan.PboardWidth-pan.PCutLeft - pan.PCutRight));
            double used = 0;
            foreach (Detail d in pan.PlacedDetails)
            {
                if (!d.isFreeArea)
                    used += d.RealHeight*d.RealWidth;
            }
            double percent = used*100/total;
            outstr[0] = (total/1000000).ToString();
            outstr[1] = (used/1000000).ToString();
            outstr[2] = percent.ToString("F2");
            return outstr;
        }

        private static string GetDetailsSideLenght(CutPanel pan)
        {
            long used = 0;
            foreach (Detail d in pan.PlacedDetails)
            {
                if (!d.isFreeArea)
                    used += d.RealHeight*2 + d.RealWidth*2;
            }
            return used.ToString();
        }

        private static string GetDetailsMiniTable(CutPanel pan)
        {
            string outstr = "";
            pan.PlacedDetails.Sort(new sortByID());
            int lastid = -99;
            foreach (Detail d in pan.PlacedDetails)
                if (!d.isFreeArea)
                {
                    if (d.detailID != lastid)
                    {
                        lastid = d.detailID;
                        outstr +="#"+lastid+" "+
                            d.RealWidth+"x"+d.RealHeight+" (" +
// ReSharper disable AccessToModifiedClosure
// ReSharper disable RedundantToStringCall
                            pan.PlacedDetails.FindAll(c => c.detailID == lastid).Count.ToString()+ Resources._pcs__br_;
// ReSharper restore RedundantToStringCall
// ReSharper restore AccessToModifiedClosure
                    }
                    else
                        continue;
                }
            return outstr;
        }

        
        public string CreateReport()
        {
            var hw = new HtmlWrite();
            hw.WriteHead(Program.Productname + Resources._printing_report);
            hw.WriteHeadLine(Resources.Report_created_at__ + DateTime.Now.ToShortDateString() + " " +
                             DateTime.Now.ToShortTimeString(),4);
            hw.WriteText(Resources.Saw_width__ + SawSize.S + Resources.mm);
            hw.WriteTable(GetPanelsTable(), Resources.Panels_);
            hw.WriteTable(GetDetailsTable(), Resources.Details_);
            hw.WriteText(Resources.You_have_total_ + GetTotalDetails() + Resources._details_with_area_ + GetTotalDetailsArea() + Resources._m_2);
            if (_ph != null)
            {
                hw.WriteText(Resources.Unplaced_details__ + GetTotalDetailsByPanel(_ph.Plist[0]) + Resources.__see_list_below__br_ + GetDetailsMiniTable(_ph.Plist[0]));
                GenerateCpanelImg(hw.Imgdir);
                foreach (CutPanel x in _ph.Plist)
                {
                    if (x.PanelId != 0)
                    {
                        string[] paninfo = GetAreaByPanel(x);
                        var r = new StringWriter();
                        r.WriteLine(Resources.Size__ + x.PboardWidth + "x" + x.PboardHeight + Resources._mm + "<br>");
                        r.WriteLine(Resources.Trim__ + x.PCutLeft + Resources.L_ + x.PCutRight + Resources.R_ + x.PCutUp + Resources.U_ + x.PCutDown +
                                    Resources.D + "<br>");
                        r.WriteLine(Resources.Details__ + GetTotalDetailsByPanel(x) + "<br>");
                        r.WriteLine(Resources.Area_usage__ +paninfo[1] +Resources._of_+paninfo[0]+Resources._m_2__ + paninfo[2]+"%)" + "<br>");
                        r.WriteLine(Resources.Details_edge_length__ + GetDetailsSideLenght(x) + " mm<br>");
                        r.WriteLine(Resources.Details_on_this_panel___br_ + GetDetailsMiniTable(x));
                        hw.PageBreak();
                        hw.WriteInfoAndImg(r.ToString(), Resources.Panel__ + x.PanelId, "img/" + x.PanelId + "P.png");
                       //hw.WriteText(r.ToString());
                       // hw.WriteImg("img/" + x.PanelID + "P.png");
                        r.Dispose();
                        r.Close();
                    }
                }
            }
            else
            {
                hw.WriteText(Resources.Detail_placing_not_started);
            }
            hw.WriteText(Resources.End_of_report);
            hw.WriteTail();

            return hw.Filename;
        }

    }
}
