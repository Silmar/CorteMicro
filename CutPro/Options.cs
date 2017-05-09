using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Configuration;

namespace CutMicro
{
 /*   [Serializable]
    public class Options
    {
        private LocalFileSettingsProvider LSF = new LocalFileSettingsProvider();
        
        public bool AltX;
        public Font DetailFont; // = new Font(FontFamily.GenericMonospace, 6);
        public Color DetailFontColor; // = Color.Black;
        public Color DetailHatchColor; // = Color.Black;
        public Color DetailLineColor; // = Color.LightYellow;
        public Color FreeAreaHatchColor; // = Color.DarkBlue;
        public Color PanelBackColor; // = Color.DarkOrchid;
        public Color DetailColor;
        public bool SuperShift;
        public int Zoom;
        public bool Autosave;
        public int saveinterval;

        protected int autosave;
        public int _autoSave
        {
            get { return autosave; }
            set
            {
                if (value > 0 && value < 100)
                    autosave = value;
            }
        }

        public void SetDefault()
        {
            PanelBackColor = Color.FromArgb(128, 64, 64);
            DetailLineColor = Color.LightYellow;
            DetailFont = new Font(FontFamily.GenericSansSerif, 8);
            DetailHatchColor = Color.Gray;
            FreeAreaHatchColor = Color.Aqua;
            DetailFontColor = Color.White;
            DetailColor = Color.FromArgb(128, 64, 64);
            autosave = 10;
            Zoom = 4;
            AltX = true;
            SuperShift = false;
            Autosave = true;
            saveinterval = 10;
        }
    }

    public class optionsSaveLoad
    {
        private Options O;

        public optionsSaveLoad(ref Options options)
        {
            O = options;
        }

        public void Save(string filename)
        {
            var BF = new BinaryFormatter();
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            var sw = new FileStream(filename, FileMode.CreateNew);
            BF.Serialize(sw, O);
            sw.Flush();
            sw.Dispose();
            sw.Close();
        }

        public Options LoadOrCreateDefault(string filename)
        {
            var BF = new BinaryFormatter();
            try
            {
                var sw = new FileStream(filename, FileMode.Open);
                O = (Options) BF.Deserialize(sw); // Serialize(sw, this);
                sw.Flush();
                sw.Dispose();
                sw.Close();
            }
            catch
            {
                O.SetDefault();
                Save(filename);
            }
            return O;
        }
    } */
}