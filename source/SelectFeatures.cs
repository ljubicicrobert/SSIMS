using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SSIM_Stabilization_GUI
{
    public partial class SelectFeatures : Form
    {
        private List<double> GCP_X = new List<double>();
        private List<double> GCP_Y = new List<double>();
        private List<CheckBox> ChkBoxes = new List<CheckBox>();
        private List<Button> Markers = new List<Button>();
        private List<RadioButton> RMSDRelative = new List<RadioButton>();
        private int ChkLeftPos = 10;
        private int ChkTopPosStart = 10;
        private int ChkTopPosStep = 25;
        private int ImgWidth;
        private int ImgHeight;
        private int BoxWidth;
        private int BoxHeight;
        private float WidthRatio;
        private float HeightRatio;
        private int SubSizeDimension;
        private float DominantRatio;
        private int SpareBoxX;
        private int SpareBoxY;
        private int MarkerSize = 7;

        public SelectFeatures()
        {
            InitializeComponent();

            RMSDRelative.Add(chkRMSDInitial);
            RMSDRelative.Add(chkRMSDAverage);
            RMSDRelative.Add(chkRMSDMedian);
        }

        private void SelectFeatures_Load(object sender, EventArgs e)
        {
            try
            {
                string ImgPath = Path.GetFileName(Directory.GetFiles(Main.FramesFolder, $"*.{Main.Extension}")
                                            .FirstOrDefault(f => !String.Equals(
                                                Path.GetFileName(f),
                                                "Thumbs.db",
                                                StringComparison.InvariantCultureIgnoreCase)));

                Image img = Image.FromFile(Main.FramesFolder + "\\" + ImgPath);
                imgInitial.BackgroundImage = img;

                ImgWidth = img.Width;
                ImgHeight = img.Height;

                BoxWidth = imgInitial.Width;
                BoxHeight = imgInitial.Height;

                WidthRatio = (float)BoxWidth / (float)ImgWidth;
                HeightRatio = (float)BoxHeight / (float)ImgHeight;

                SubSizeDimension = WidthRatio < HeightRatio ? 0 : 1;
                SpareBoxX = SubSizeDimension == 0 ? 0 : Convert.ToInt32((BoxWidth - HeightRatio * ImgWidth) / 2);
                SpareBoxY = SubSizeDimension == 0 ? Convert.ToInt32((BoxHeight - WidthRatio * ImgHeight) / 2) : 0;
                DominantRatio = SubSizeDimension == 0 ? WidthRatio : HeightRatio;
            }
            catch
            {
                MessageBox.Show($"Images not found in the folder {Main.FramesFolder}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }

            try
            {
                string gcp_path = Path.GetFileName(Directory.GetFiles(Main.OutputFolder + "\\gcps_csv", $"*.txt")
                                                .FirstOrDefault(f => !String.Equals(
                                                    Path.GetFileName(f),
                                                    "Thumbs.db",
                                                    StringComparison.InvariantCultureIgnoreCase)));

                using (var reader = new StreamReader(Main.OutputFolder + "\\gcps_csv\\" + gcp_path))
                {
                    int i = 1;
                    int top = ChkTopPosStart;

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(' ');
                        if (values.Length == 0) break;

                        double X = Math.Round(Convert.ToDouble(values[0]), 0);
                        double Y = Math.Round(Convert.ToDouble(values[1]), 0);

                        GCP_X.Add(X);
                        GCP_Y.Add(Y);

                        CheckBox chk = new CheckBox();
                        chk.Parent = panelFeatures;
                        chk.AutoSize = true;
                        chk.Left = ChkLeftPos;
                        chk.Top = top;
                        chk.Text = $"# {i}: X = {X}, Y = {Y}";
                        chk.Checked = true;
                        chk.CheckStateChanged += new EventHandler((s, eh) => RepaintGCPs());
                        ChkBoxes.Add(chk);

                        i += 1;
                        top += ChkTopPosStep;
                    }
                }
            }
            catch
            {
                MessageBox.Show($"No features coordinates found in {Main.OutputFolder}\\gcps_csv", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }

            PaintGCPs();
        }

        private void PaintGCPs()
        {
            for (int i=0; i < GCP_X.Count(); i++)
            {
                Button btn = new Button();
                Markers.Add(btn);
                btn.Parent = imgInitial;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.Enabled = false;
                btn.Size = new Size(MarkerSize, MarkerSize);

                int PosX = Convert.ToInt32(DominantRatio * GCP_X[i] + SpareBoxX - (MarkerSize/2 + 0.5));
                int PosY = Convert.ToInt32(DominantRatio * GCP_Y[i] + SpareBoxY - (MarkerSize/2 + 1.5));

                btn.Left = PosX;
                btn.Top = PosY;
                btn.BringToFront();

                Label lbl = new Label();
                lbl.Parent = imgInitial;
                lbl.Text = (i+1).ToString();
                lbl.TextAlign = ContentAlignment.BottomLeft;
                lbl.Left = PosX + 7;
                lbl.Top = PosY;
                lbl.AutoSize = true;
                lbl.Font = new Font(Label.DefaultFont, FontStyle.Bold);
                lbl.BackColor = Color.Transparent;
                lbl.BringToFront();

                btn.BackColor = ChkBoxes[i].Checked ? Color.Green : Color.Maroon;
            }

            if (Main.GCPsMask != "1")
            {
                int i = 0;

                foreach (char c in Main.GCPsMask)
                {
                    ChkBoxes[i].Checked = c == '1' ? true : false;
                    i += 1;
                }
            }
        }

        private void RepaintGCPs()
        {
            for (int i = 0; i < GCP_X.Count(); i++)
            {
                Button btn = Markers[i];
                if (ChkBoxes[i].Checked) btn.BackColor = Color.Green;
                else btn.BackColor = Color.Maroon;
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < GCP_X.Count(); i++)
            {
                ChkBoxes[i].Checked = true;
            }
        }

        private void btnUnselectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < GCP_X.Count(); i++)
            {
                ChkBoxes[i].Checked = false;
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            string s = "";

            for (int i = 0; i < GCP_X.Count(); i++)
            {
                if (ChkBoxes[i].Checked) s += "1";
                else s += "0";
            }

            Main.GCPsMask = s;
            DialogResult = DialogResult.Yes;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Main.GCPsMask = "1";
            DialogResult = DialogResult.Cancel;
        }

        private void btnRMSD_Click(object sender, EventArgs e)
        {
            int RMSDRef = 0;

            foreach (RadioButton rbtn in RMSDRelative)
            {
                if (rbtn.Checked) { RMSDRef = RMSDRelative.IndexOf(rbtn); }
            }

            string ScriptPath = Path.GetFullPath("..\\scripts\\feature_goodness.py");

            if (!File.Exists(ScriptPath))
            {
                MessageBox.Show($"Python script not found:\n\n{ScriptPath}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string args = string.Format($"-u \"{ScriptPath}\" --fold \"{Main.OutputFolder}\" --ref {RMSDRef}");

            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = Main.PythonPath,
                        Arguments = args,
                        UseShellExecute = true,
                        RedirectStandardOutput = false,
                    },
                };

                process.Start();
                process.WaitForExit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem with executing the Python script:\n\n" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
