using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SSIM_Stabilization_GUI
{
    public partial class CreateVideo : Form
    {
        private int NumValidFrames = 0;

        public CreateVideo()
        {
            InitializeComponent();
            bxVideoCodec.SelectedIndex = 0;
            bxExtension.Text = Main.Extension;
            bxInterpolationMethod.SelectedIndex = 0;

            selectFramesFolder.InitialDirectory = Main.OutputFolder;

            try
            {
                bxExtension.Text = Main.Extension;
            }
            catch { }

            try
            {
                bxVideoFramerate.Text = Main.Framerate;
            }
            catch { }

            CountFrames();

            bxExtension.Items.AddRange(Main.extensions);
            bxExtension.SelectedIndex = 0;
        }

        private void CountFrames()
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(bxFramesFolderPath.Text);

                NumValidFrames = di.GetFiles("*." + bxExtension.Text,
                                             SearchOption.TopDirectoryOnly).Length;

                if (NumValidFrames > 0)
                {
                    lblNumberOfFrames.Text = "Number of frames = " + NumValidFrames.ToString();
                    lblNumberOfFrames.ForeColor = Color.Green;
                }
                else
                {
                    lblNumberOfFrames.Text = "No valid frames: check path and file extension!";
                    lblNumberOfFrames.ForeColor = Color.Red;
                }
            }
            catch
            {
                NumValidFrames = 0;

                lblNumberOfFrames.Text = "No valid frames: check path and file extension!";
                lblNumberOfFrames.ForeColor = Color.Red;
            }

            btnCreateVideo.Enabled = NumValidFrames > 0 ? true : false;
            btnCreateVideo.BackColor = NumValidFrames > 0 ? Color.MediumSeaGreen : Color.DarkGray;
            btnCreateVideo.FlatAppearance.BorderColor = NumValidFrames > 0 ? Color.MediumSeaGreen : Color.Gray;
        }

        private void BxVideoFramerate_Leave(object sender, EventArgs e)
        {
            bool isNumeric = float.TryParse(bxVideoFramerate.Text, out _);
            if (!isNumeric)
            {
                var dialog = MessageBox.Show("This field can only contain numeric input!\n\n" +
                                             "Please provide a valid input.",
                                             "Field format error!",
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Error);
                bxVideoFramerate.Focus();
            }
        }

        private void BtnSelectFramesFolder_Click(object sender, EventArgs e)
        {
            var selection = selectFramesFolder.ShowDialog();
            if (selection == DialogResult.OK)
            {
                bxFramesFolderPath.Text = Path.GetDirectoryName(selectFramesFolder.FileName);
                CountFrames();
            }
        }

        private void BxFramesExtension_Leave(object sender, EventArgs e)
        {
            CountFrames();
        }

        private void BxFramesExtension_SelectedIndexChanged(object sender, EventArgs e)
        {
            CountFrames();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        bool IsValidFilename(string testName)
        {
            Regex containsABadCharacter = new Regex("["
                  + Regex.Escape(new string(System.IO.Path.GetInvalidFileNameChars())) + "]");
            if (containsABadCharacter.IsMatch(testName)) { return false; };

            return true;
        }

        private void BxVideoName_Leave(object sender, EventArgs e)
        {
            if (!IsValidFilename(bxVideoName.Text))
            {
                MessageBox.Show("This field cmust contain a valid filename!\n\n" +
                                "Please provide a valid input.",
                                "Field format error!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                bxVideoName.Focus();
            }
        }

        private void BtnCreateVideo_Click(object sender, EventArgs e)
        {
            string savePath = $"{bxFramesFolderPath.Text}\\config_create_video.txt";
            var parser = new FileIniDataParser();

            IniData data = new IniData();

            string Section = "Create video";
            data[Section]["VideoName"] = bxVideoName.Text;
            data[Section]["FramesFolder"] = bxFramesFolderPath.Text;
            data[Section]["ImageExtension"] = bxExtension.Text;
            data[Section]["Codec"] = bxVideoCodec.Text;
            data[Section]["FPS"] = bxVideoFramerate.Text;
            data[Section]["Scale"] = bxScale.Value.ToString();
            data[Section]["Interpolation"] = bxInterpolationMethod.SelectedIndex.ToString();

            parser.WriteFile(savePath, data);

            string OutPath = $"{bxFramesFolderPath.Text}\\{bxVideoName.Text}.avi";
            bool OutputVideoNew = false;
            DateTime pwt, nwt;

            if (File.Exists(OutPath))
            {
                FileInfo fi = new FileInfo(OutPath);
                pwt = fi.LastWriteTime;
            }
            else
            {
                pwt = DateTime.Now;
            }

            string ScriptPath = Path.GetFullPath("..\\scripts\\frames_to_video.py");

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
                        FileName = Main.PythonPath,
                        Arguments = args,
                        UseShellExecute = true,
                        RedirectStandardOutput = false,
                    },
                };

                process.Start();
                process.WaitForExit();

                try
                {
                    FileInfo fi = new FileInfo(OutPath);
                    nwt = fi.LastWriteTime;

                    if ((nwt - pwt).TotalSeconds > 3)
                    {
                        OutputVideoNew = true;
                    }
                }
                catch { }

                if (File.Exists(OutPath) && OutputVideoNew)
                {
                    var dialog = MessageBox.Show("Would you like to open the created video?", "Open video?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialog == DialogResult.Yes)
                    {
                        Process.Start("explorer.exe", $"/open, {OutPath}");
                    }
                }
                else
                {
                    MessageBox.Show("There might be a problem with the video creation!\n" +
                                    "Please check the configuration.",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem with executing the Python script:\n\n" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Main.DumpLog(new List<string> { Main.PythonPath + " " + args });
        }

        private void CheckInterpAvailable()
        {
            bool switcher = ((float)bxScale.Value != 1.0) ? true : false;

            lblInterpolationMethod.Enabled = switcher;
            bxInterpolationMethod.Enabled = switcher;
        }

        private void BxScale_ValueChanged(object sender, EventArgs e)
        {
            CheckInterpAvailable();
        }

        private void BxScale_Leave(object sender, EventArgs e)
        {
            CheckInterpAvailable();
        }
    }
}
