using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using IniParser;
using IniParser.Model;
using SSIM_Stabilization_GUI.Properties;

namespace SSIM_Stabilization_GUI
{
    public partial class Main : Form
    {
        public static string PythonPath;
        public static string PythonVersion;
        private bool FramesSelected = false;
        public static bool Unpacked = false;
        public static String FramesFolder = "";
        public static String OutputFolder = "";
        public static String Extension = "jpg";
        public static String Framerate = "30.00";
        private bool RANSACAvailable = false;
        private bool TrackingPossible = false;
        private bool TransformPossible = false;
        private int AvailableFramesTracking = 0;
        private int AvailableFramesStabilization = 0;
        public static int NumOrthoGCPs = 0;
        public static float PxRatio = 1.0f;
        public static string PaddX = "0-0";
        public static string PaddY = "0-0";
        public static string GCPsMask = "1";
        public static int NumGCPs;

        private string[] hintBasicTrackingLines;
        private string[] hintAdvancedTrackingLines;
        private string[] hintTransformationLines;
        private static Image imgTooltipEnter;
        private static Image imgTooltipExit;

        private string[] StabilizationMethods = new string[]
                                        { "similarity",
                                          "affine_2D_strict",
                                          "affine_2D_optimal",
                                          "projective_strict",
                                          "projective_optimal"
        };

        private int[] StabilizationMethodsMinFeatures = new int[]
                                        { 2,
                                          3,
                                          3,
                                          4,
                                          4,
        };

        private int[] StabilizationMethodsMaxFeatures = new int[]
                                        { -1,
                                          3,
                                          -1,
                                          4,
                                          -1,
        };

        public static string[] extensions = { "jpg", "jpeg", "png", "webp", "tif", "tiff", "bmp" };

        public Main()
        {
            InitializeComponent();
            bxTransformMethod.SelectedIndex = 4;

            try
            {
                imgTooltipEnter = Resources.hint_hover;
                imgTooltipExit = Resources.hint;
            }
            catch { }

            try
            {
                hintBasicTrackingLines = File.ReadAllLines("..\\help\\HelpBasicTracking.help");
                imgHintBasicTracking.MouseEnter += new EventHandler(EnterTooltip);
                imgHintBasicTracking.MouseLeave += new EventHandler(ExitTooltip);
                imgHintBasicTracking.Click += (sender, e) => HintShow(sender, e, "Basic tracking parameters", hintBasicTrackingLines);
            }
            catch { }

            try
            {
                hintAdvancedTrackingLines = File.ReadAllLines("..\\help\\HelpAdvancedTracking.help");
                imgHintAdvancedTracking.MouseEnter += new EventHandler(EnterTooltip);
                imgHintAdvancedTracking.MouseLeave += new EventHandler(ExitTooltip);
                imgHintAdvancedTracking.Click += (sender, e) => HintShow(sender, e, "Advanced tracking parameters", hintAdvancedTrackingLines);
            }
            catch { }

            try
            {
                hintTransformationLines = File.ReadAllLines("..\\help\\HelpTransformation.help");
                imgHintTransformation.MouseEnter += new EventHandler(EnterTooltip);
                imgHintTransformation.MouseLeave += new EventHandler(ExitTooltip);
                imgHintTransformation.Click += (sender, e) => HintShow(sender, e, "Image transformation parameters", hintTransformationLines);
            }
            catch { }

            bxInputExtension.Items.AddRange(extensions);
            bxInputExtension.SelectedIndex = 0;

            bxOutputExtension.Items.AddRange(extensions);
            bxOutputExtension.SelectedIndex = 0;
        }

        public static void EnterTooltip(object sender, EventArgs e)
        {
            (sender as PictureBox).Cursor = Cursors.Hand;
            (sender as PictureBox).Image = imgTooltipEnter;
        }

        public static void ExitTooltip(object sender, EventArgs e)
        {
            (sender as PictureBox).Cursor = Cursors.Default;
            (sender as PictureBox).Image = imgTooltipExit;
        }

        private void HintShow(object sender, EventArgs e, string heading, string[] lines)
        {
            TooltipForm TTF = new TooltipForm(heading, lines);
            TTF.ShowDialog();
        }

        private void btnOutput_folder_Click(object sender, EventArgs e)
        {
            if (bxOutputFolder.Text != "Output folder" && FramesSelected)
            {
                selectOutputFolder.InitialDirectory = Path.GetDirectoryName(bxFramesFolder.Text);
            }

            var selection = selectOutputFolder.ShowDialog();

            if (selection == DialogResult.OK)
            {
                OutputFolder = Path.GetDirectoryName(selectOutputFolder.FileName);

                if (Directory.EnumerateFileSystemEntries(OutputFolder).Any())
                {
                    var dialog = MessageBox.Show("The selected folder is not empty. Are you sure you want to use this folder?\n\n" +
                                                 "Old files will be overwritten if they have the same name as the new ones.",
                                                 "Folder not empty!",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Warning);

                    if (dialog == DialogResult.Yes)
                    {
                        bxOutputFolder.Text = OutputFolder;
                    }
                }
                else
                {
                    bxOutputFolder.Text = OutputFolder;
                }
            }

            CheckAvailableFramesStabilization();
        }

        private void btnFramesFolder_Click(object sender, EventArgs e)
        {
            var selection = selectFramesFolder.ShowDialog();
            if (selection == DialogResult.OK)
            {
                FramesSelected = true;
                FramesFolder = Path.GetDirectoryName(selectFramesFolder.FileName);
                bxFramesFolder.Text = Path.GetDirectoryName(selectFramesFolder.FileName);

                if (bxOutputFolder.Text == "Output folder")
                {
                    bxOutputFolder.Text = Path.GetDirectoryName(bxFramesFolder.Text) + "\\stabilization";
                    OutputFolder = bxOutputFolder.Text;
                }
            }

            CheckAvailableFramesTracking();
            CheckAvailableFramesStabilization();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CheckAvailableFramesTracking()
        {
            DirectoryInfo di = new DirectoryInfo(bxFramesFolder.Text);

            try
            {
                var TXTFiles = di.GetFiles($"*.{bxInputExtension.Text}", SearchOption.TopDirectoryOnly);
                AvailableFramesTracking = Directory.Exists(bxFramesFolder.Text) ? TXTFiles.Length : 0;

                if (AvailableFramesTracking > 0)
                {
                    TrackingPossible = true;
                    lblNumFramesTracking.Text = "Tracking: # of frames = " + AvailableFramesTracking.ToString();
                    lblNumFramesTracking.ForeColor = Color.Green;
                }
                else
                {
                    TrackingPossible = false;
                    lblNumFramesTracking.Text = "No available frames for tracking.";
                    lblNumFramesTracking.ForeColor = Color.Red;
                }

                btnTrackFeatures.Enabled = TrackingPossible ? true : false;
                btnTrackFeatures.BackColor = TrackingPossible ? Color.MediumSeaGreen : Color.DarkGray;
                btnTrackFeatures.FlatAppearance.BorderColor = TrackingPossible ? Color.MediumSeaGreen : Color.Gray;
            }
            catch
            {
                TrackingPossible = false;

                lblNumFramesTracking.Text = "No available frames for tracking.";
                lblNumFramesTracking.ForeColor = Color.Red;

                btnTrackFeatures.Enabled = TrackingPossible ? true : false;
                btnTrackFeatures.BackColor = TrackingPossible ? Color.MediumSeaGreen : Color.DarkGray;
                btnTrackFeatures.FlatAppearance.BorderColor = TrackingPossible ? Color.MediumSeaGreen : Color.Gray;
            }
        }

        private void CheckAvailableFramesStabilization()
        {
            OutputFolder = bxOutputFolder.Text;
            string CSVFolder = OutputFolder + "\\gcps_csv";
            DirectoryInfo di = new DirectoryInfo(CSVFolder);

            try
            {
                var TXTFiles = di.GetFiles("*.txt", SearchOption.TopDirectoryOnly);
                AvailableFramesStabilization = Directory.Exists(CSVFolder) ? TXTFiles.Length : 0;

                if (AvailableFramesStabilization > 0)
                {
                    var FirstFile = TXTFiles[0].FullName;

                    TransformPossible = true;
                    lblNumFramesStabilization.Text = "Transformation: # of frames = " + AvailableFramesStabilization.ToString();
                    lblNumFramesStabilization.ForeColor = Color.Green;
                }
                else
                {
                    TransformPossible = false;
                    lblNumFramesStabilization.Text = "No available frames for transformation.";
                    lblNumFramesStabilization.ForeColor = Color.Red;
                }

                btnStabilize.Enabled = TransformPossible ? true : false;
                btnStabilize.BackColor = TransformPossible ? Color.MediumSeaGreen : Color.DarkGray;
                btnStabilize.FlatAppearance.BorderColor = TransformPossible ? Color.MediumSeaGreen : Color.Gray;

                btnSelectGCPs.Enabled = TransformPossible ? true : false;
                btnSelectGCPs.BackColor = TransformPossible ? SystemColors.GradientActiveCaption : Color.DarkGray;
                btnSelectGCPs.FlatAppearance.BorderColor = TransformPossible ? SystemColors.GradientActiveCaption : Color.Gray;
            }
            catch
            {
                TransformPossible = false;

                lblNumFramesStabilization.Text = "No available frames for transformation.";
                lblNumFramesStabilization.ForeColor = Color.Red;

                btnStabilize.Enabled = false;
                btnStabilize.BackColor = Color.DarkGray;
                btnStabilize.FlatAppearance.BorderColor = Color.Gray;

                btnSelectGCPs.Enabled = false;
                btnSelectGCPs.BackColor = Color.DarkGray;
                btnSelectGCPs.FlatAppearance.BorderColor = Color.Gray;
            }

            cbxOrthorectify.Enabled = btnStabilize.Enabled;
            CountGCPs(GCPsMask != "1");
        }

        private void cbxExpandSA_CheckedChanged(object sender, EventArgs e)
        {
            bool switcher = (sender as CheckBox).Checked ? true : false;

            lblExpandSACoef.Enabled = switcher;
            lblExpandSAThr.Enabled = switcher;
            bxExpandSACoef.Enabled = switcher;
            bxExpandSAThr.Enabled = switcher;
        }

        private void cbxSubpixelEstimator_CheckedChanged(object sender, EventArgs e)
        {
            bool switcher = (sender as CheckBox).Checked ? true : false;

            lblSubpixelSize.Enabled = switcher;
            bxSubpixelSize.Enabled = switcher;
        }

        private void btnUnpackVideo_Click(object sender, EventArgs e)
        {
            Unpacked = false;
            Form UV = new UnpackVideo();
            UV.ShowDialog();

            if (Unpacked)
            {
                bxFramesFolder.Text = FramesFolder;
                bxOutputFolder.Text = Path.GetDirectoryName(FramesFolder) + "\\stabilization";
                OutputFolder = bxOutputFolder.Text;
                bxInputExtension.Text = Extension;
                bxOutputExtension.Text = Extension;
                bxVideoFramerate.Text = Framerate;

                CheckAvailableFramesTracking();
                CheckAvailableFramesStabilization();
            }
        }

        private void cbxUseRANSAC_CheckedChanged(object sender, EventArgs e)
        {
            bool switcher = ((sender as CheckBox).Checked) ? true : false;

            lblRANSACThr.Enabled = switcher;
            bxRANSACThr.Enabled = switcher;
            RANSACAvailable = switcher;
        }

        private void bxTransformMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<int> OptimalMethods = new List<int>() { 0, 2, 4 };
            if (OptimalMethods.Contains(bxTransformMethod.SelectedIndex))
            {
                cbxUseRANSAC.Enabled = true;
                lblRANSACThr.Enabled = RANSACAvailable;
                bxRANSACThr.Enabled = RANSACAvailable;
            }
            else
            {
                cbxUseRANSAC.Enabled = false;
                lblRANSACThr.Enabled = false;
                bxRANSACThr.Enabled = false;
            }
        }

        public static bool IsFieldNumeric(TextBox sender)
        {
            bool isNumeric = float.TryParse(sender.Text, out _);
            if (!isNumeric)
            {
                MessageBox.Show("This field can only contain numeric input!\n\n" +
                                "Please provide a valid input.",
                                "Field format error!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                sender.Focus();

                return false;
            }
            else
            {
                return true;
            }
        }

        private void bxVideoFramerate_Leave(object sender, EventArgs e)
        {
            if (!IsFieldNumeric((sender as TextBox)))
            {
                Framerate = bxVideoFramerate.Text;
            }
        }

        public static string[] GetPython()
        {
            var path = Environment.GetEnvironmentVariable("PATH");
            string python;

            try
            {
                foreach (var p in path.Split(new char[] { ';' }))
                {
                    var fullPath = Path.Combine(p, "python.exe");

                    if (File.Exists(fullPath))
                    {
                        string command = "python --version";
                        string output = null;
                        string version = "";

                        using (Process pr = new Process())
                        {
                            pr.StartInfo.UseShellExecute = false;
                            pr.StartInfo.RedirectStandardOutput = true;
                            pr.StartInfo.FileName = "cmd.exe";
                            pr.StartInfo.Arguments = String.Format(@"/c {0} ", command);
                            pr.StartInfo.CreateNoWindow = true;
                            if (pr.Start())
                                output = pr.StandardOutput.ReadToEnd();
                        }

                        foreach (string s in output.Split(' '))
                        {
                            if (s.StartsWith("3."))
                            {
                                python = fullPath;
                                version = s;
                                return new string[] { python, version };
                            }
                        }
                    }
                }
            }
            catch { }
          
            MessageBox.Show("Couldn't find python (version 3+) in %PATH%!\n" +
                            "Please check %PATH% in the Environmental variables.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

            return new string[] { "", "" };
        }

        private void btnTrackFeatures_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(bxOutputFolder.Text))
            {
                Directory.CreateDirectory(bxOutputFolder.Text);
            }

            string savePath = $"{bxOutputFolder.Text}\\config_tracking.txt";
            var parser = new FileIniDataParser();

            IniData data = new IniData();

            string Section = "Basic";
            data[Section]["InputFolder"] = bxFramesFolder.Text;
            data[Section]["OutputFolder"] = bxOutputFolder.Text;
            data[Section]["ImageExtension"] = bxOutputExtension.Text;
            data[Section]["SearchAreaSize"] = bxSASize.Value.ToString();
            data[Section]["InterrogationAreaSize"] = bxIASize.Value.ToString();
            data[Section]["Subpixel"] = (cbxSubpixelEstimator.Checked) ? bxSubpixelSize.Value.ToString() : "0"; ;

            Section = "Advanced";
            data[Section]["ExpandSA"] = (cbxExpandSA.Checked) ? "1" : "0";
            data[Section]["ExpandSACoef"] = bxExpandSACoef.Value.ToString();
            data[Section]["ExpandSAThreshold"] = bxExpandSAThr.Value.ToString();
            data[Section]["UpdateKernels"] = (cbxUpdateKernels.Checked) ? "1" : "0";

            parser.WriteFile(savePath, data);

            string ScriptPath = Path.GetFullPath("..\\scripts\\feature_tracking.py");

            if (!File.Exists(ScriptPath))
            {
                MessageBox.Show($"Python script not found:\n\n{ScriptPath}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string args = string.Format($"-u \"{ScriptPath}\" --cfg \"{savePath}\"");

            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = PythonPath,
                        Arguments = args,
                        UseShellExecute = true,
                        RedirectStandardOutput = false,
                    },
                };

                process.Start();
                process.WaitForExit();

                try
                {
                    var temp_parser = new FileIniDataParser();
                    var temp_INI_data = temp_parser.ReadFile(savePath);
                    string new_SA_size = temp_INI_data["Basic"]["SearchAreaSize"];
                    string new_IA_size = temp_INI_data["Basic"]["InterrogationAreaSize"];
                    bxSASize.Value = Int32.Parse(new_SA_size);
                    bxIASize.Value = Int32.Parse(new_IA_size);
                }
                catch { }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem with executing the Python script:\n\n" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Main.DumpLog(new List<string> { PythonPath + " " + args });
            CheckAvailableFramesStabilization();
        }

        private void bxOutputFolder_Leave(object sender, EventArgs e)
        {
            CheckAvailableFramesStabilization();
        }

        private void btnStabilize_Click(object sender, EventArgs e)
        {
            int MaxFeatures = StabilizationMethodsMaxFeatures[bxTransformMethod.SelectedIndex];
            int MinFeatures = StabilizationMethodsMinFeatures[bxTransformMethod.SelectedIndex];
            bool strict = MaxFeatures != -1;
            string exact = strict ? "exactly " : "at least ";

            if ((strict && NumGCPs != MinFeatures) || (!strict && NumGCPs < MinFeatures))
            {
                MessageBox.Show($"Invalid number of features selected for the selected transformation method!\n\n" +
                                $"Number of features for the method [{StabilizationMethods[bxTransformMethod.SelectedIndex]}] " +
                                $"must be {exact}{MinFeatures}.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            string savePath = $"{OutputFolder}\\config_stabilization.txt";
            var parser = new FileIniDataParser();

            IniData data = new IniData();

            string Section = "Stabilization";
            data[Section]["InputFolder"] = FramesFolder;
            data[Section]["OutputFolder"] = OutputFolder;
            data[Section]["ImageExtension"] = bxOutputExtension.Text;
            data[Section]["ImageQuality"] = bxOutputQuality.Value.ToString();
            data[Section]["NumSchemeLen"] = ((int)(Math.Log10((double)AvailableFramesStabilization) + 1)).ToString();
            data[Section]["fps"] = bxVideoFramerate.Text;
            data[Section]["Method"] = bxTransformMethod.SelectedIndex.ToString();
            data[Section]["UseRANSAC"] = cbxUseRANSAC.Checked ? "1" : "0";
            data[Section]["RANSACThreshold"] = bxRANSACThr.Value.ToString();
            data[Section]["Orthorectify"] = cbxOrthorectify.Checked ? "1" : "0";
            data[Section]["PXRatio"] = PxRatio.ToString();
            data[Section]["FeatureMask"] = GCPsMask;
            data[Section]["PaddX"] = PaddX;
            data[Section]["PaddY"] = PaddY;
            data[Section]["CreateVideo"] = chkCreateVideo.Checked ? "1" : "0";

            parser.WriteFile(savePath, data);

            string OutputPath;
            string EndFile = $"{OutputFolder}\\end";

            if (cbxOrthorectify.Checked)
            {
                OutputPath = $"{OutputFolder}\\images_orthorectified\\{StabilizationMethods[bxTransformMethod.SelectedIndex]}";
            }
            else
            {
                OutputPath = $"{OutputFolder}\\images_stabilized\\{StabilizationMethods[bxTransformMethod.SelectedIndex]}";
            }

            bool EndFileNew = false;
            DateTime pwt, nwt;

            if (File.Exists(EndFile))
            {
                FileInfo fi = new FileInfo(EndFile);
                pwt = fi.LastWriteTime;
            }
            else
            {
                pwt = DateTime.Now;
            }

            string ScriptPath = Path.GetFullPath("..\\scripts\\stabilize_frames.py");

            if (!File.Exists(ScriptPath))
            {
                MessageBox.Show($"Python script not found:\n\n{ScriptPath}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string args = string.Format($"-u \"{ScriptPath}\" --cfg \"{savePath}\"");

            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = PythonPath,
                        Arguments = args,
                        UseShellExecute = true,
                        RedirectStandardOutput = false,
                    },
                };

                process.Start();
                process.WaitForExit();

                try
                {
                    FileInfo fi = new FileInfo(EndFile);
                    nwt = fi.LastWriteTime;

                    if ((nwt - pwt).TotalSeconds > 3)
                    {
                        EndFileNew = true;
                    }
                }
                catch { }

                if (Directory.Exists(OutputPath) && EndFileNew)
                { 
                    var dialog = MessageBox.Show("Would you like to review the stabilized images?", "Open results folder?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialog == DialogResult.Yes)
                    {
                        Process.Start("explorer.exe", $"/open, {OutputPath}");
                    }
                }
                else
                {
                    MessageBox.Show("There might be a problem with the stabilization!\n" +
                                    "Please check the configuration and try again.",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem with executing the Python script:\n\n" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Main.DumpLog(new List<string> { PythonPath + " " + args });
        }

        private void BtnFramesToVideo_Click(object sender, EventArgs e)
        {
            Extension = bxOutputExtension.Text;
            Framerate = bxVideoFramerate.Text;
            OutputFolder = bxOutputFolder.Text;

            Form CV = new CreateVideo();
            CV.ShowDialog();
        }

        private int ToOdd(int x)
        {
            if (x % 2 == 0)
            {
                return x + 1;
            }
            else
            {
                return x;
            }
        }

        private void FieldToOddInt(object sender, EventArgs e)
        {
            NumericUpDown TB = (sender as NumericUpDown);
            TB.Value = ToOdd((int)TB.Value);

            if (TB.Name == "bxIASize")
            {
                if (TB.Value >= bxSASize.Value) bxSASize.Value = TB.Value + 2;
            }
            else if (TB.Name == "bxSASize")
            {
                if (TB.Value <= bxIASize.Value) bxIASize.Value = TB.Value - 2;
            }
        }

        private void bxFramesFolder_Leave(object sender, EventArgs e)
        {
            CheckAvailableFramesTracking();
        }

        private void bxInputExtension_TextChanged(object sender, EventArgs e)
        {
            CheckAvailableFramesTracking();
        }

        private void bxInputExtension_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckAvailableFramesTracking();
        }

        private void cbxOrthorectify_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxOrthorectify.Checked)
            {
                Orthorectify OF = new Orthorectify();

                if (OF.ShowDialog() == DialogResult.Cancel)
                {
                    cbxOrthorectify.Checked = false;
                    btnStabilize.Text = "Stabilize";

                    lblOrthoConfiguration.Text = "Orthorectification not configured.";
                    lblOrthoConfiguration.ForeColor = Color.Red;
                }
                else
                {
                    btnStabilize.Text = "Stabilize + orthorectify";

                    lblOrthoConfiguration.Text = $"Orthorectification: {NumOrthoGCPs} GCPs";
                    lblOrthoConfiguration.ForeColor = Color.Green;
                }
            }
            else
            {
                btnStabilize.Text = "Stabilize";

                lblOrthoConfiguration.Text = "Orthorectification not configured.";
                lblOrthoConfiguration.ForeColor = Color.Red;
            }
        }

        public static void DumpLog(List<string> list)
        {
            TextWriter tw = new StreamWriter("console_log.txt");

            foreach (string s in list)
                tw.WriteLine(s);

            tw.Close();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            string[] Python = GetPython();
            PythonPath = Python[0];
            PythonVersion = Python[1];

            if (PythonVersion != "") lblPython.Text = $"Python version: {PythonVersion}";
            else
            { 
                lblPython.Text = $"Python version: {PythonVersion}";
                Enabled = false;
            }

            try
            {
                string[] lines = File.ReadAllLines("..\\scripts\\__init__.py");

                foreach(string s in lines)
                {
                    if (s.Contains("__version__"))
                    {
                        string[] spl = s.Split('\'');
                        lblBuild.Text = $"Build version: {spl[1]}";
                        break;
                    }
                }
            }
            catch 
            {
                lblBuild.Text = "";
            }
        }

        private void btnSelectGCPs_Click(object sender, EventArgs e)
        {
            SelectFeatures SF = new SelectFeatures();

            if (SF.ShowDialog() == DialogResult.Cancel)
            {
                CountGCPs(false);
            }
            else
            {
                CountGCPs(true);
            }
        }

        private void CountGCPs(bool masked)
        {
            try
            {
                if (!masked)
                {
                    string gcp_path = Path.GetFileName(Directory.GetFiles(OutputFolder + "\\gcps_csv", $"*.txt")
                                                    .FirstOrDefault(f => !String.Equals(
                                                        Path.GetFileName(f),
                                                        "Thumbs.db",
                                                        StringComparison.InvariantCultureIgnoreCase)));

                    using (var reader = new StreamReader(OutputFolder + "\\gcps_csv\\" + gcp_path))
                    {
                        NumGCPs = 0;
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            NumGCPs += 1;
                        }
                    }
                }
                else
                {
                    NumGCPs = GCPsMask.Count(f => f == '1');
                }
            }
            catch
            {
                NumGCPs = 0;
            }

            btnSelectGCPs.Text = TransformPossible ? $"# features for transformation = {NumGCPs}" : "Select features for transformation";
        }

        private void chkCreateVideo_CheckedChanged(object sender, EventArgs e)
        {
            bool switcher = ((sender as CheckBox).Checked) ? true : false;

            lblVideoFramerate.Enabled = switcher;
            lblVideoFramerateUnits.Enabled = switcher;
            bxVideoFramerate.Enabled = switcher;
        }
    }
}
