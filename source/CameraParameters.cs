using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using IniParser;
using IniParser.Model;
using System.Diagnostics;

namespace SSIM_Stabilization_GUI
{
    public partial class CameraParameters : Form
    {
        private static string CameraProfileFolder = @"..\camera_profiles";
        private static List<string> CameraProfiles = new List<string>();

        private static float fx;
        private static float fy;
        private static float s;
        private static float cx;
        private static float cy;

        private static float k1;
        private static float k2;
        private static float k3 = 0;
        private static float k4 = 0;
        private static float k5 = 0;
        private static float k6 = 0;

        private static float p1;
        private static float p2;

        private static List<float> RadialCoefs = new List<float>();
        private static List<float> TangentialCoefs = new List<float>();

        private static List<TextBox> ListIntrinsics;
        private static List<TextBox> ListRadial;
        private static List<TextBox> ListTangential;

        public CameraParameters()
        {
            InitializeComponent();

            ListIntrinsics = new List<TextBox>() { bxFx, bxFy, bxS, bxCx, bxCy };
            ListRadial = new List<TextBox>() { bxK1, bxK2, bxK3, bxK4, bxK5, bxK6 };
            ListTangential = new List<TextBox>() { bxP1, bxP2 };

            cbxCameraProfile.Items.Clear();
            cbxCameraProfile.Items.Add("Custom");

            string[] files = Directory.GetFiles(CameraProfileFolder);
            foreach (string f in files)
            {
                if (f.EndsWith(".cpf"))
                {
                    string fwe = Path.GetFileNameWithoutExtension(f);
                    CameraProfiles.Add(fwe);
                    cbxCameraProfile.Items.Add(fwe);
                }
            }

            if (UnpackVideo.CameraProfile != null && UnpackVideo.CameraProfile != "Custom")
            {
                int i = 0;
                foreach (string item in cbxCameraProfile.Items)
                {
                    if (item == UnpackVideo.CameraProfile)
                    {
                        cbxCameraProfile.SelectedIndex = i;
                    }
                    i++;
                }
            }
            else if (UnpackVideo.CameraProfile == "Custom")
            {
                cbxCameraProfile.SelectedIndex = 0;

                for (int i = 0; i < 5; i++)
                {
                    ListIntrinsics[i].Text = UnpackVideo.CameraIntrinsics[i].ToString();
                }

                for (int i = 0; i < 6; i++)
                {
                    ListRadial[i].Text = UnpackVideo.RadialCoefs[i].ToString();
                }

                for (int i = 0; i < 2; i++)
                {
                    ListTangential[i].Text = UnpackVideo.TangentialCoefs[i].ToString();
                }
            }

            imgHintCameraParameters.MouseEnter += new EventHandler(Main.EnterTooltip);
            imgHintCameraParameters.MouseLeave += new EventHandler(Main.ExitTooltip);

            imgHintCameraParameters1.MouseEnter += new EventHandler(Main.EnterTooltip);
            imgHintCameraParameters1.MouseLeave += new EventHandler(Main.ExitTooltip);

            imgHintCameraParameters2.MouseEnter += new EventHandler(Main.EnterTooltip);
            imgHintCameraParameters2.MouseLeave += new EventHandler(Main.ExitTooltip);
        }

        private void cbxCameraProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxCameraProfile.SelectedIndex > 0)
            {
                string path = $"{CameraProfileFolder}\\{cbxCameraProfile.Text}.cpf";
                try
                {
                    var parser = new FileIniDataParser();

                    IniData data = parser.ReadFile(path);

                    string Section = "Intrinsics";

                    fx = Convert.ToSingle(data[Section]["fx"]);
                    fy = Convert.ToSingle(data[Section]["fy"]);
                    s = Convert.ToSingle(data[Section]["s"]);
                    cx = Convert.ToSingle(data[Section]["cx"]);
                    cy = Convert.ToSingle(data[Section]["cy"]);

                    bxFx.Text = fx.ToString();
                    bxFy.Text = fy.ToString();
                    bxS.Text = s.ToString();
                    bxCx.Text = cx.ToString();
                    bxCy.Text = cy.ToString();

                    Section = "Radial";

                    k1 = Convert.ToSingle(data[Section]["k1"]);
                    k2 = Convert.ToSingle(data[Section]["k2"]);
                    Single.TryParse(data[Section]["k3"], out k3);
                    Single.TryParse(data[Section]["k4"], out k4);
                    Single.TryParse(data[Section]["k5"], out k5);
                    Single.TryParse(data[Section]["k6"], out k6);

                    RadialCoefs.Clear();
                    RadialCoefs.Add(k1);
                    RadialCoefs.Add(k2);
                    RadialCoefs.Add(k3);
                    RadialCoefs.Add(k4);
                    RadialCoefs.Add(k5);
                    RadialCoefs.Add(k6);

                    int i = 0;
                    foreach(TextBox tb in ListRadial)
                    {
                        tb.Text = RadialCoefs[i].ToString();
                        i++;
                    }

                    Section = "Tangential";

                    p1 = Convert.ToSingle(data[Section]["p1"]);
                    p2 = Convert.ToSingle(data[Section]["p2"]);

                    TangentialCoefs.Clear();
                    TangentialCoefs.Add(p1);
                    TangentialCoefs.Add(p2);

                    i = 0;
                    foreach (TextBox tb in ListTangential)
                    {
                        tb.Text = TangentialCoefs[i].ToString();
                        i++;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"There was a problem with reading the camera parameters from:\n" +
                        $"{path}\n\n" +
                        $"Error message:\n" +
                        ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            UnpackVideo.CameraProfile = cbxCameraProfile.Text;

            UnpackVideo.CameraIntrinsics = new float[] { fx, fy, s, cx, cy };
            UnpackVideo.RadialCoefs = new float[] { k1, k2, k3, k4, k5, k6 };
            UnpackVideo.TangentialCoefs = new float[] { p1, p2 };

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void imgHintCameraParameters_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.mathworks.com/help/vision/ref/cameraparameters.html");
        }

        private void bxNumeric_Leave(object sender, EventArgs e)
        {
            TextBox bx = (sender as TextBox);
            if (!Main.IsFieldNumeric(bx))
            {
                bx.Focus();
            }
        }
    }
}
