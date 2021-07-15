using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SSIM_Stabilization_GUI
{
    public partial class Orthorectify : Form
    {
        public Orthorectify()
        {
            InitializeComponent();

            bxPxRatio.Leave += (sender, e) => Main.IsFieldNumeric((sender as TextBox));
            bxXPaddingLeft.Leave += (sender, e) => Main.IsFieldNumeric((sender as TextBox));
            bxXPaddingRight.Leave += (sender, e) => Main.IsFieldNumeric((sender as TextBox));
            bxYPaddingUp.Leave += (sender, e) => Main.IsFieldNumeric((sender as TextBox));
            bxYPaddingDown.Leave += (sender, e) => Main.IsFieldNumeric((sender as TextBox));

            try
            {
                bxPxRatio.Text = Main.PxRatio.ToString();
                string[] xPadd = Main.PaddX.Split('-');
                string[] yPadd = Main.PaddY.Split('-');

                bxXPaddingLeft.Text = xPadd[0];
                bxXPaddingRight.Text = xPadd[1];
                bxYPaddingUp.Text = yPadd[0];
                bxYPaddingDown.Text = yPadd[1];
            }
            catch { }

            try
            {
                if (File.Exists($"{Main.OutputFolder}\\gcps_real.txt"))
                {
                    string[] lines = File.ReadAllLines($"{Main.OutputFolder}\\gcps_real.txt");

                    int i = 0;
                    foreach (string s in lines)
                    {
                        if (s != "")
                        {
                            string[] xy = s.Split(' ');
                            tableGCPs.Rows.Add();
                            tableGCPs[0, i].Value = xy[0];
                            tableGCPs[1, i].Value = xy[1];

                            i++;
                        }
                    }
                }
            }
            catch { }
        }

        private void btnApplyOrthorectification_Click(object sender, EventArgs e)
        {
            if (tableGCPs.Rows.Count < 3)
            {
                MessageBox.Show("At least 3 GCPs are needed for the orthorectification!\n" +
                                "Please check the table and try again.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    Main.NumOrthoGCPs = tableGCPs.Rows.Count - 1;
                    Main.PxRatio = Convert.ToSingle(bxPxRatio.Text);

                    Main.PaddX = $"{bxXPaddingLeft.Text}-{bxXPaddingRight.Text}";
                    Main.PaddY = $"{bxYPaddingUp.Text}-{bxYPaddingDown.Text}";

                    var sb = new StringBuilder();

                    foreach (DataGridViewRow row in tableGCPs.Rows)
                    {
                        var cells = row.Cells.Cast<DataGridViewCell>();
                        sb.AppendLine(string.Join(" ", cells.Select(cell => cell.Value).ToArray()));
                    }

                    using (StreamWriter file = new StreamWriter($"{Main.OutputFolder}\\gcps_real.txt"))
                    {
                        file.WriteLine(sb.ToString(), Encoding.UTF8);
                    }

                    Close();
                    DialogResult = DialogResult.OK;
                }
                catch
                {
                    MessageBox.Show("There was a problem with writting data to the Output Folder!\n" +
                                    "Please check the Configuration and try again.",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Main.NumOrthoGCPs = 0;
            Main.PxRatio = 100.0f;
            Main.PaddX = "0-0";
            Main.PaddY = "0-0";
        }

        private void CheckFieldNumericAndPositive(object sender, EventArgs e)
        {
            string bxText = (sender as TextBox).Text;
            float bxValue;

            if (!Single.TryParse(bxText, out bxValue))
            {
                MessageBox.Show("This field must be numeric\n" +
                                "Please enter a numeric value to continue.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                (sender as TextBox).Focus();
            }
            else if (bxValue < 0)
            {
                MessageBox.Show("This field value must be positive\n" +
                                "Please enter a positive numeric value to continue.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                (sender as TextBox).Focus();
            }
        }
    }
}
