namespace SSIM_Stabilization_GUI
{
    partial class CreateVideo
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
            this.btnSelectFramesFolder = new System.Windows.Forms.Button();
            this.bxFramesFolderPath = new System.Windows.Forms.TextBox();
            this.lblVideoCodec = new System.Windows.Forms.Label();
            this.bxVideoCodec = new System.Windows.Forms.ComboBox();
            this.lblVideoFramerate = new System.Windows.Forms.Label();
            this.bxVideoFramerate = new System.Windows.Forms.TextBox();
            this.lblVideoFramerateUnits = new System.Windows.Forms.Label();
            this.lblVideoName = new System.Windows.Forms.Label();
            this.bxVideoName = new System.Windows.Forms.TextBox();
            this.lblVideoExt = new System.Windows.Forms.Label();
            this.selectFramesFolder = new System.Windows.Forms.OpenFileDialog();
            this.lblExtension = new System.Windows.Forms.Label();
            this.bxExtension = new System.Windows.Forms.ComboBox();
            this.bxScale = new System.Windows.Forms.NumericUpDown();
            this.lblScale = new System.Windows.Forms.Label();
            this.lblNumberOfFrames = new System.Windows.Forms.Label();
            this.btnCreateVideo = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.bxInterpolationMethod = new System.Windows.Forms.ComboBox();
            this.lblInterpolationMethod = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bxScale)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSelectFramesFolder
            // 
            this.btnSelectFramesFolder.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnSelectFramesFolder.FlatAppearance.BorderColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnSelectFramesFolder.FlatAppearance.BorderSize = 2;
            this.btnSelectFramesFolder.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnSelectFramesFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectFramesFolder.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectFramesFolder.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSelectFramesFolder.Location = new System.Drawing.Point(12, 12);
            this.btnSelectFramesFolder.Name = "btnSelectFramesFolder";
            this.btnSelectFramesFolder.Size = new System.Drawing.Size(255, 27);
            this.btnSelectFramesFolder.TabIndex = 0;
            this.btnSelectFramesFolder.Text = "Select frames folder";
            this.btnSelectFramesFolder.UseVisualStyleBackColor = false;
            this.btnSelectFramesFolder.Click += new System.EventHandler(this.BtnSelectFramesFolder_Click);
            // 
            // bxFramesFolderPath
            // 
            this.bxFramesFolderPath.Location = new System.Drawing.Point(12, 45);
            this.bxFramesFolderPath.Name = "bxFramesFolderPath";
            this.bxFramesFolderPath.ReadOnly = true;
            this.bxFramesFolderPath.Size = new System.Drawing.Size(255, 23);
            this.bxFramesFolderPath.TabIndex = 1;
            this.bxFramesFolderPath.Text = "Path to frames folder";
            // 
            // lblVideoCodec
            // 
            this.lblVideoCodec.AutoSize = true;
            this.lblVideoCodec.Location = new System.Drawing.Point(9, 162);
            this.lblVideoCodec.Name = "lblVideoCodec";
            this.lblVideoCodec.Size = new System.Drawing.Size(75, 15);
            this.lblVideoCodec.TabIndex = 8;
            this.lblVideoCodec.Text = "Video codec:";
            // 
            // bxVideoCodec
            // 
            this.bxVideoCodec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bxVideoCodec.FormattingEnabled = true;
            this.bxVideoCodec.Items.AddRange(new object[] {
            "MJPG",
            "DIVX",
            "XVID",
            "WMV1",
            "WMV2"});
            this.bxVideoCodec.Location = new System.Drawing.Point(144, 159);
            this.bxVideoCodec.Name = "bxVideoCodec";
            this.bxVideoCodec.Size = new System.Drawing.Size(83, 23);
            this.bxVideoCodec.TabIndex = 9;
            // 
            // lblVideoFramerate
            // 
            this.lblVideoFramerate.AutoSize = true;
            this.lblVideoFramerate.Location = new System.Drawing.Point(9, 192);
            this.lblVideoFramerate.Name = "lblVideoFramerate";
            this.lblVideoFramerate.Size = new System.Drawing.Size(63, 15);
            this.lblVideoFramerate.TabIndex = 10;
            this.lblVideoFramerate.Text = "Framerate:";
            // 
            // bxVideoFramerate
            // 
            this.bxVideoFramerate.Location = new System.Drawing.Point(144, 189);
            this.bxVideoFramerate.Name = "bxVideoFramerate";
            this.bxVideoFramerate.Size = new System.Drawing.Size(57, 23);
            this.bxVideoFramerate.TabIndex = 11;
            this.bxVideoFramerate.Text = "30.00";
            this.bxVideoFramerate.Leave += new System.EventHandler(this.BxVideoFramerate_Leave);
            // 
            // lblVideoFramerateUnits
            // 
            this.lblVideoFramerateUnits.AutoSize = true;
            this.lblVideoFramerateUnits.Location = new System.Drawing.Point(205, 192);
            this.lblVideoFramerateUnits.Name = "lblVideoFramerateUnits";
            this.lblVideoFramerateUnits.Size = new System.Drawing.Size(26, 15);
            this.lblVideoFramerateUnits.TabIndex = 12;
            this.lblVideoFramerateUnits.Text = "FPS";
            // 
            // lblVideoName
            // 
            this.lblVideoName.AutoSize = true;
            this.lblVideoName.Location = new System.Drawing.Point(9, 132);
            this.lblVideoName.Name = "lblVideoName";
            this.lblVideoName.Size = new System.Drawing.Size(113, 15);
            this.lblVideoName.TabIndex = 5;
            this.lblVideoName.Text = "Output video name:";
            // 
            // bxVideoName
            // 
            this.bxVideoName.Location = new System.Drawing.Point(144, 129);
            this.bxVideoName.Name = "bxVideoName";
            this.bxVideoName.Size = new System.Drawing.Size(83, 23);
            this.bxVideoName.TabIndex = 6;
            this.bxVideoName.Text = "output";
            this.bxVideoName.Leave += new System.EventHandler(this.BxVideoName_Leave);
            // 
            // lblVideoExt
            // 
            this.lblVideoExt.AutoSize = true;
            this.lblVideoExt.Location = new System.Drawing.Point(230, 132);
            this.lblVideoExt.Name = "lblVideoExt";
            this.lblVideoExt.Size = new System.Drawing.Size(25, 15);
            this.lblVideoExt.TabIndex = 7;
            this.lblVideoExt.Text = ".avi";
            // 
            // selectFramesFolder
            // 
            this.selectFramesFolder.CheckFileExists = false;
            this.selectFramesFolder.FileName = "Select frames folder";
            this.selectFramesFolder.Title = "Select frames folder";
            this.selectFramesFolder.ValidateNames = false;
            // 
            // lblExtension
            // 
            this.lblExtension.AutoSize = true;
            this.lblExtension.Location = new System.Drawing.Point(9, 102);
            this.lblExtension.Name = "lblExtension";
            this.lblExtension.Size = new System.Drawing.Size(124, 15);
            this.lblExtension.TabIndex = 3;
            this.lblExtension.Text = "Frame files\' extension:";
            // 
            // bxExtension
            // 
            this.bxExtension.FormattingEnabled = true;
            this.bxExtension.Location = new System.Drawing.Point(144, 99);
            this.bxExtension.Name = "bxExtension";
            this.bxExtension.Size = new System.Drawing.Size(83, 23);
            this.bxExtension.TabIndex = 4;
            this.bxExtension.SelectedIndexChanged += new System.EventHandler(this.BxFramesExtension_SelectedIndexChanged);
            this.bxExtension.Leave += new System.EventHandler(this.BxFramesExtension_Leave);
            // 
            // bxScale
            // 
            this.bxScale.DecimalPlaces = 1;
            this.bxScale.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.bxScale.Location = new System.Drawing.Point(144, 220);
            this.bxScale.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            65536});
            this.bxScale.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.bxScale.Name = "bxScale";
            this.bxScale.Size = new System.Drawing.Size(57, 23);
            this.bxScale.TabIndex = 14;
            this.bxScale.Value = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.bxScale.ValueChanged += new System.EventHandler(this.BxScale_ValueChanged);
            this.bxScale.Leave += new System.EventHandler(this.BxScale_Leave);
            // 
            // lblScale
            // 
            this.lblScale.AutoSize = true;
            this.lblScale.Location = new System.Drawing.Point(9, 222);
            this.lblScale.Name = "lblScale";
            this.lblScale.Size = new System.Drawing.Size(76, 15);
            this.lblScale.TabIndex = 13;
            this.lblScale.Text = "Scale frames:";
            // 
            // lblNumberOfFrames
            // 
            this.lblNumberOfFrames.AutoSize = true;
            this.lblNumberOfFrames.ForeColor = System.Drawing.Color.Red;
            this.lblNumberOfFrames.Location = new System.Drawing.Point(9, 71);
            this.lblNumberOfFrames.Name = "lblNumberOfFrames";
            this.lblNumberOfFrames.Size = new System.Drawing.Size(253, 15);
            this.lblNumberOfFrames.TabIndex = 2;
            this.lblNumberOfFrames.Text = "No valid frames: check path and file extension!";
            // 
            // btnCreateVideo
            // 
            this.btnCreateVideo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCreateVideo.BackColor = System.Drawing.Color.DarkGray;
            this.btnCreateVideo.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCreateVideo.Enabled = false;
            this.btnCreateVideo.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnCreateVideo.FlatAppearance.BorderSize = 2;
            this.btnCreateVideo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PaleGreen;
            this.btnCreateVideo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateVideo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateVideo.Location = new System.Drawing.Point(175, 288);
            this.btnCreateVideo.Name = "btnCreateVideo";
            this.btnCreateVideo.Size = new System.Drawing.Size(94, 27);
            this.btnCreateVideo.TabIndex = 17;
            this.btnCreateVideo.Text = "Create";
            this.btnCreateVideo.UseVisualStyleBackColor = false;
            this.btnCreateVideo.Click += new System.EventHandler(this.BtnCreateVideo_Click);
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
            this.btnClose.Location = new System.Drawing.Point(12, 288);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(94, 27);
            this.btnClose.TabIndex = 18;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // bxInterpolationMethod
            // 
            this.bxInterpolationMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bxInterpolationMethod.FormattingEnabled = true;
            this.bxInterpolationMethod.Items.AddRange(new object[] {
            "Bilinear",
            "Bicubic",
            "Lanczos"});
            this.bxInterpolationMethod.Location = new System.Drawing.Point(144, 249);
            this.bxInterpolationMethod.Name = "bxInterpolationMethod";
            this.bxInterpolationMethod.Size = new System.Drawing.Size(83, 23);
            this.bxInterpolationMethod.TabIndex = 16;
            // 
            // lblInterpolationMethod
            // 
            this.lblInterpolationMethod.AutoSize = true;
            this.lblInterpolationMethod.Location = new System.Drawing.Point(9, 252);
            this.lblInterpolationMethod.Name = "lblInterpolationMethod";
            this.lblInterpolationMethod.Size = new System.Drawing.Size(123, 15);
            this.lblInterpolationMethod.TabIndex = 15;
            this.lblInterpolationMethod.Text = "Interpolation method:";
            // 
            // CreateVideo
            // 
            this.AcceptButton = this.btnCreateVideo;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(281, 327);
            this.Controls.Add(this.bxInterpolationMethod);
            this.Controls.Add(this.lblInterpolationMethod);
            this.Controls.Add(this.btnCreateVideo);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblNumberOfFrames);
            this.Controls.Add(this.bxScale);
            this.Controls.Add(this.lblScale);
            this.Controls.Add(this.bxExtension);
            this.Controls.Add(this.lblExtension);
            this.Controls.Add(this.lblVideoExt);
            this.Controls.Add(this.bxVideoName);
            this.Controls.Add(this.lblVideoName);
            this.Controls.Add(this.bxVideoFramerate);
            this.Controls.Add(this.lblVideoFramerateUnits);
            this.Controls.Add(this.lblVideoFramerate);
            this.Controls.Add(this.bxVideoCodec);
            this.Controls.Add(this.lblVideoCodec);
            this.Controls.Add(this.bxFramesFolderPath);
            this.Controls.Add(this.btnSelectFramesFolder);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateVideo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create video";
            ((System.ComponentModel.ISupportInitialize)(this.bxScale)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelectFramesFolder;
        private System.Windows.Forms.TextBox bxFramesFolderPath;
        private System.Windows.Forms.Label lblVideoCodec;
        private System.Windows.Forms.ComboBox bxVideoCodec;
        private System.Windows.Forms.Label lblVideoFramerate;
        private System.Windows.Forms.TextBox bxVideoFramerate;
        private System.Windows.Forms.Label lblVideoFramerateUnits;
        private System.Windows.Forms.Label lblVideoName;
        private System.Windows.Forms.TextBox bxVideoName;
        private System.Windows.Forms.Label lblVideoExt;
        private System.Windows.Forms.OpenFileDialog selectFramesFolder;
        private System.Windows.Forms.Label lblExtension;
        private System.Windows.Forms.ComboBox bxExtension;
        private System.Windows.Forms.NumericUpDown bxScale;
        private System.Windows.Forms.Label lblScale;
        private System.Windows.Forms.Label lblNumberOfFrames;
        private System.Windows.Forms.Button btnCreateVideo;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox bxInterpolationMethod;
        private System.Windows.Forms.Label lblInterpolationMethod;
    }
}