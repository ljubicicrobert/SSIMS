using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SSIM_Stabilization_GUI
{
    public partial class TooltipForm : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public TooltipForm(string heading, string[] lines)
        {
            InitializeComponent();
            lblHintHeading.Text = heading;
            hintBox.Lines = lines;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TooltipForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void hintBox_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            if (Height < 800)
            {
                ((RichTextBox)sender).Height = e.NewRectangle.Height + 3;
                Height = ((RichTextBox)sender).Height + 95;
            }
            else
            {
                Height = 800;
                ((RichTextBox)sender).Height = Height - 95;
            }
            btnClose.Top = Height - 40;
        }
    }
}
