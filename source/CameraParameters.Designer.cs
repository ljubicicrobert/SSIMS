namespace SSIM_Stabilization_GUI
{
    partial class CameraParameters
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CameraParameters));
            this.btnClose = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.lblCameraModel = new System.Windows.Forms.Label();
            this.cbxCameraProfile = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bxFx = new System.Windows.Forms.TextBox();
            this.bxS = new System.Windows.Forms.TextBox();
            this.bxCx = new System.Windows.Forms.TextBox();
            this.bxCy = new System.Windows.Forms.TextBox();
            this.bxFy = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.openMatFile = new System.Windows.Forms.OpenFileDialog();
            this.bxK1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.bxK3 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.bxK2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.bxK4 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.bxK5 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.bxK6 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.bxP1 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.bxP2 = new System.Windows.Forms.TextBox();
            this.imgHintCameraParameters = new System.Windows.Forms.PictureBox();
            this.imgHintCameraParameters1 = new System.Windows.Forms.PictureBox();
            this.imgHintCameraParameters2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.imgHintCameraParameters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgHintCameraParameters1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgHintCameraParameters2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClose.BackColor = System.Drawing.Color.LightSalmon;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.LightSalmon;
            this.btnClose.FlatAppearance.BorderSize = 2;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PeachPuff;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(12, 367);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(94, 27);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "Cancel";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnApply.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnApply.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnApply.FlatAppearance.BorderColor = System.Drawing.Color.MediumSeaGreen;
            this.btnApply.FlatAppearance.BorderSize = 2;
            this.btnApply.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PaleGreen;
            this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApply.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApply.Location = new System.Drawing.Point(188, 367);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(94, 27);
            this.btnApply.TabIndex = 10;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = false;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // lblCameraModel
            // 
            this.lblCameraModel.AutoSize = true;
            this.lblCameraModel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCameraModel.Location = new System.Drawing.Point(12, 9);
            this.lblCameraModel.Name = "lblCameraModel";
            this.lblCameraModel.Size = new System.Drawing.Size(188, 15);
            this.lblCameraModel.TabIndex = 11;
            this.lblCameraModel.Text = "Choose camera/platform profile:";
            // 
            // cbxCameraProfile
            // 
            this.cbxCameraProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCameraProfile.FormattingEnabled = true;
            this.cbxCameraProfile.Items.AddRange(new object[] {
            "Custom",
            "DJI Phantom 4 Pro II",
            "DJI Mavic 2 Pro",
            "DJI Phantom 4",
            "DJI Phantom 3",
            "Sony RX10 M2"});
            this.cbxCameraProfile.Location = new System.Drawing.Point(15, 27);
            this.cbxCameraProfile.Name = "cbxCameraProfile";
            this.cbxCameraProfile.Size = new System.Drawing.Size(267, 23);
            this.cbxCameraProfile.TabIndex = 12;
            this.cbxCameraProfile.SelectedIndexChanged += new System.EventHandler(this.cbxCameraProfile_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 15);
            this.label1.TabIndex = 13;
            this.label1.Text = "Camera matrix:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 181);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "Radial distortion:";
            // 
            // bxFx
            // 
            this.bxFx.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.bxFx.Location = new System.Drawing.Point(15, 85);
            this.bxFx.Name = "bxFx";
            this.bxFx.Size = new System.Drawing.Size(85, 23);
            this.bxFx.TabIndex = 15;
            this.bxFx.Text = "fx";
            this.bxFx.Leave += new System.EventHandler(this.bxNumeric_Leave);
            // 
            // bxS
            // 
            this.bxS.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.bxS.Location = new System.Drawing.Point(15, 114);
            this.bxS.Name = "bxS";
            this.bxS.Size = new System.Drawing.Size(85, 23);
            this.bxS.TabIndex = 16;
            this.bxS.Text = "s";
            this.bxS.Leave += new System.EventHandler(this.bxNumeric_Leave);
            // 
            // bxCx
            // 
            this.bxCx.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.bxCx.Location = new System.Drawing.Point(15, 143);
            this.bxCx.Name = "bxCx";
            this.bxCx.Size = new System.Drawing.Size(85, 23);
            this.bxCx.TabIndex = 17;
            this.bxCx.Text = "cx";
            this.bxCx.Leave += new System.EventHandler(this.bxNumeric_Leave);
            // 
            // bxCy
            // 
            this.bxCy.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.bxCy.Location = new System.Drawing.Point(106, 143);
            this.bxCy.Name = "bxCy";
            this.bxCy.Size = new System.Drawing.Size(85, 23);
            this.bxCy.TabIndex = 20;
            this.bxCy.Text = "cy";
            this.bxCy.Leave += new System.EventHandler(this.bxNumeric_Leave);
            // 
            // bxFy
            // 
            this.bxFy.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.bxFy.Location = new System.Drawing.Point(106, 114);
            this.bxFy.Name = "bxFy";
            this.bxFy.Size = new System.Drawing.Size(85, 23);
            this.bxFy.TabIndex = 19;
            this.bxFy.Text = "fy";
            this.bxFy.Leave += new System.EventHandler(this.bxNumeric_Leave);
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.SystemColors.Window;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox7.Enabled = false;
            this.textBox7.Location = new System.Drawing.Point(197, 146);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(85, 16);
            this.textBox7.TabIndex = 23;
            this.textBox7.Text = "1";
            this.textBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox6
            // 
            this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox6.Enabled = false;
            this.textBox6.Location = new System.Drawing.Point(197, 117);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(85, 16);
            this.textBox6.TabIndex = 26;
            this.textBox6.Text = "0";
            this.textBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox9
            // 
            this.textBox9.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox9.Enabled = false;
            this.textBox9.Location = new System.Drawing.Point(197, 88);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(85, 16);
            this.textBox9.TabIndex = 21;
            this.textBox9.Text = "0";
            this.textBox9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox8
            // 
            this.textBox8.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox8.Enabled = false;
            this.textBox8.Location = new System.Drawing.Point(106, 88);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(85, 16);
            this.textBox8.TabIndex = 22;
            this.textBox8.Text = "0";
            this.textBox8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 300);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 15);
            this.label3.TabIndex = 27;
            this.label3.Text = "Tangential distortion:";
            // 
            // openMatFile
            // 
            this.openMatFile.DefaultExt = "mat";
            this.openMatFile.Filter = "Matlab file|*.mat";
            // 
            // bxK1
            // 
            this.bxK1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.bxK1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.bxK1.Location = new System.Drawing.Point(45, 202);
            this.bxK1.Name = "bxK1";
            this.bxK1.Size = new System.Drawing.Size(96, 23);
            this.bxK1.TabIndex = 29;
            this.bxK1.Text = "k1";
            this.bxK1.Leave += new System.EventHandler(this.bxNumeric_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 205);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 15);
            this.label4.TabIndex = 30;
            this.label4.Text = "k1 =";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 234);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 15);
            this.label5.TabIndex = 32;
            this.label5.Text = "k3 =";
            // 
            // bxK3
            // 
            this.bxK3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.bxK3.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.bxK3.Location = new System.Drawing.Point(45, 231);
            this.bxK3.Name = "bxK3";
            this.bxK3.Size = new System.Drawing.Size(96, 23);
            this.bxK3.TabIndex = 31;
            this.bxK3.Text = "k3";
            this.bxK3.Leave += new System.EventHandler(this.bxNumeric_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(153, 205);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 15);
            this.label6.TabIndex = 34;
            this.label6.Text = "k2 =";
            // 
            // bxK2
            // 
            this.bxK2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.bxK2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.bxK2.Location = new System.Drawing.Point(186, 202);
            this.bxK2.Name = "bxK2";
            this.bxK2.Size = new System.Drawing.Size(96, 23);
            this.bxK2.TabIndex = 33;
            this.bxK2.Text = "k2";
            this.bxK2.Leave += new System.EventHandler(this.bxNumeric_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(153, 234);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 15);
            this.label7.TabIndex = 36;
            this.label7.Text = "k4 =";
            // 
            // bxK4
            // 
            this.bxK4.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.bxK4.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.bxK4.Location = new System.Drawing.Point(186, 231);
            this.bxK4.Name = "bxK4";
            this.bxK4.Size = new System.Drawing.Size(96, 23);
            this.bxK4.TabIndex = 35;
            this.bxK4.Text = "k4";
            this.bxK4.Leave += new System.EventHandler(this.bxNumeric_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 263);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 15);
            this.label8.TabIndex = 38;
            this.label8.Text = "k5 =";
            // 
            // bxK5
            // 
            this.bxK5.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.bxK5.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.bxK5.Location = new System.Drawing.Point(45, 260);
            this.bxK5.Name = "bxK5";
            this.bxK5.Size = new System.Drawing.Size(96, 23);
            this.bxK5.TabIndex = 37;
            this.bxK5.Text = "k5";
            this.bxK5.Leave += new System.EventHandler(this.bxNumeric_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(153, 263);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(30, 15);
            this.label9.TabIndex = 40;
            this.label9.Text = "k6 =";
            // 
            // bxK6
            // 
            this.bxK6.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.bxK6.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.bxK6.Location = new System.Drawing.Point(186, 260);
            this.bxK6.Name = "bxK6";
            this.bxK6.Size = new System.Drawing.Size(96, 23);
            this.bxK6.TabIndex = 39;
            this.bxK6.Text = "k6";
            this.bxK6.Leave += new System.EventHandler(this.bxNumeric_Leave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 323);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 15);
            this.label10.TabIndex = 42;
            this.label10.Text = "p1 =";
            // 
            // bxP1
            // 
            this.bxP1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.bxP1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.bxP1.Location = new System.Drawing.Point(45, 320);
            this.bxP1.Name = "bxP1";
            this.bxP1.Size = new System.Drawing.Size(96, 23);
            this.bxP1.TabIndex = 41;
            this.bxP1.Text = "p1";
            this.bxP1.Leave += new System.EventHandler(this.bxNumeric_Leave);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(153, 323);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(31, 15);
            this.label11.TabIndex = 44;
            this.label11.Text = "p2 =";
            // 
            // bxP2
            // 
            this.bxP2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.bxP2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.bxP2.Location = new System.Drawing.Point(186, 320);
            this.bxP2.Name = "bxP2";
            this.bxP2.Size = new System.Drawing.Size(96, 23);
            this.bxP2.TabIndex = 43;
            this.bxP2.Text = "p2";
            this.bxP2.Leave += new System.EventHandler(this.bxNumeric_Leave);
            // 
            // imgHintCameraParameters
            // 
            this.imgHintCameraParameters.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.imgHintCameraParameters.Image = ((System.Drawing.Image)(resources.GetObject("imgHintCameraParameters.Image")));
            this.imgHintCameraParameters.Location = new System.Drawing.Point(102, 65);
            this.imgHintCameraParameters.Name = "imgHintCameraParameters";
            this.imgHintCameraParameters.Size = new System.Drawing.Size(16, 16);
            this.imgHintCameraParameters.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgHintCameraParameters.TabIndex = 45;
            this.imgHintCameraParameters.TabStop = false;
            this.imgHintCameraParameters.Click += new System.EventHandler(this.imgHintCameraParameters_Click);
            // 
            // imgHintCameraParameters1
            // 
            this.imgHintCameraParameters1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.imgHintCameraParameters1.Image = ((System.Drawing.Image)(resources.GetObject("imgHintCameraParameters1.Image")));
            this.imgHintCameraParameters1.Location = new System.Drawing.Point(112, 181);
            this.imgHintCameraParameters1.Name = "imgHintCameraParameters1";
            this.imgHintCameraParameters1.Size = new System.Drawing.Size(16, 16);
            this.imgHintCameraParameters1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgHintCameraParameters1.TabIndex = 46;
            this.imgHintCameraParameters1.TabStop = false;
            this.imgHintCameraParameters1.Click += new System.EventHandler(this.imgHintCameraParameters_Click);
            // 
            // imgHintCameraParameters2
            // 
            this.imgHintCameraParameters2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.imgHintCameraParameters2.Image = ((System.Drawing.Image)(resources.GetObject("imgHintCameraParameters2.Image")));
            this.imgHintCameraParameters2.Location = new System.Drawing.Point(132, 300);
            this.imgHintCameraParameters2.Name = "imgHintCameraParameters2";
            this.imgHintCameraParameters2.Size = new System.Drawing.Size(16, 16);
            this.imgHintCameraParameters2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgHintCameraParameters2.TabIndex = 47;
            this.imgHintCameraParameters2.TabStop = false;
            this.imgHintCameraParameters2.Click += new System.EventHandler(this.imgHintCameraParameters_Click);
            // 
            // CameraParameters
            // 
            this.AcceptButton = this.btnApply;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(295, 406);
            this.Controls.Add(this.imgHintCameraParameters2);
            this.Controls.Add(this.imgHintCameraParameters1);
            this.Controls.Add(this.imgHintCameraParameters);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.bxP2);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.bxP1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.bxK6);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.bxK5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.bxK4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.bxK2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.bxK3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.bxK1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.textBox9);
            this.Controls.Add(this.bxCy);
            this.Controls.Add(this.bxFy);
            this.Controls.Add(this.bxCx);
            this.Controls.Add(this.bxS);
            this.Controls.Add(this.bxFx);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxCameraProfile);
            this.Controls.Add(this.lblCameraModel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnClose);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CameraParameters";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Camera parameters";
            ((System.ComponentModel.ISupportInitialize)(this.imgHintCameraParameters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgHintCameraParameters1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgHintCameraParameters2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Label lblCameraModel;
        private System.Windows.Forms.ComboBox cbxCameraProfile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox bxFx;
        private System.Windows.Forms.TextBox bxS;
        private System.Windows.Forms.TextBox bxCx;
        private System.Windows.Forms.TextBox bxCy;
        private System.Windows.Forms.TextBox bxFy;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.OpenFileDialog openMatFile;
        private System.Windows.Forms.TextBox bxK1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox bxK3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox bxK2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox bxK4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox bxK5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox bxK6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox bxP1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox bxP2;
        private System.Windows.Forms.PictureBox imgHintCameraParameters;
        private System.Windows.Forms.PictureBox imgHintCameraParameters1;
        private System.Windows.Forms.PictureBox imgHintCameraParameters2;
    }
}