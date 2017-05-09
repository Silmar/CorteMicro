using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace CutPro
{

    [Serializable]
    public class SaveLoad
    {
        bool PanelIsCreated = false;
        PL[] plGridList;
        DT[] dtGridList;        

        // ////////////////////////////////////////////////////////

        public void save(string filename, List<CutPanel> panels, CutPanel shoppanel, NumericUpDown sawwidth, DataGridView DetailsGrid, DataGridView PanelsGrid)
        {
            ///HUITA!!!!!
            XmlSerializer xs = new XmlSerializer(typeof(SaveLoad));
            StreamWriter writer = new StreamWriter(filename, false, Encoding.UTF8);
            ////Get all panels into array
            List<PL> plist = new List<PL>();
            int i = 1;
            foreach (CutPanel panel in panels)
            {
                plist.Add(new PL(i, panel.pboardWidth, panel.pboardHeight, panel.pCutLeft, panel.pCutRight, panel.pCutUp, panel.pCutDown));
                i++;                
            }
            this.plGridList = plist.ToArray();
            ////Grab All Details
            List<DT> dlist = new List<DT>();
            foreach(Detail detail in shoppanel.PlacedDetails)
            {
               dlist.Add(new DT(detail.detailID, detail.etc, detail.Width, detail.Height,  detail.X, detail.Y, -1, detail._CanBeRotated));
            }
        }

        public void load(string filename, FlowLayoutPanel panelscontainer, List<CutPanel> panels, CutPanel shoppanel, NumericUpDown sawwidth, DataGridView DetailsGrid, DataGridView PanelsGrid)
        {

        }
    }
}
