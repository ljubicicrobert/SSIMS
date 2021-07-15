namespace SSIM_Stabilization_GUI
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.btnFrames = new System.Windows.Forms.Button();
            this.btnOutput_folder = new System.Windows.Forms.Button();
            this.btnTrackFeatures = new System.Windows.Forms.Button();
            this.btnStabilize = new System.Windows.Forms.Button();
            this.bxOutputFolder = new System.Windows.Forms.TextBox();
            this.selectFramesFolder = new System.Windows.Forms.OpenFileDialog();
            this.selectOutputFolder = new System.Windows.Forms.OpenFileDialog();
            this.btnUnpackVideo = new System.Windows.Forms.Button();
            this.bxFramesFolder = new System.Windows.Forms.TextBox();
            this.grpBasicParameters = new System.Windows.Forms.GroupBox();
            this.bxInputExtension = new System.Windows.Forms.ComboBox();
            this.lblInputExtension = new System.Windows.Forms.Label();
            this.lblSubpixelSizeUnits = new System.Windows.Forms.Label();
            this.imgHintBasicTracking = new System.Windows.Forms.PictureBox();
            this.lblSASizeUnits = new System.Windows.Forms.Label();
            this.lblIASizeUnits = new System.Windows.Forms.Label();
            this.lblSubpixelSize = new System.Windows.Forms.Label();
            this.bxSubpixelSize = new System.Windows.Forms.NumericUpDown();
            this.cbxSubpixelEstimator = new System.Windows.Forms.CheckBox();
            this.lblSASize = new System.Windows.Forms.Label();
            this.lblIASize = new System.Windows.Forms.Label();
            this.bxSASize = new System.Windows.Forms.NumericUpDown();
            this.bxIASize = new System.Windows.Forms.NumericUpDown();
            this.grpAdvancedParameters = new System.Windows.Forms.GroupBox();
            this.imgHintAdvancedTracking = new System.Windows.Forms.PictureBox();
            this.cbxUpdateKernels = new System.Windows.Forms.CheckBox();
            this.bxExpandSAThr = new System.Windows.Forms.NumericUpDown();
            this.lblExpandSAThr = new System.Windows.Forms.Label();
            this.lblExpandSACoef = new System.Windows.Forms.Label();
            this.bxExpandSACoef = new System.Windows.Forms.NumericUpDown();
            this.cbxExpandSA = new System.Windows.Forms.CheckBox();
            this.grpTransformation = new System.Windows.Forms.GroupBox();
            this.chkCreateVideo = new System.Windows.Forms.CheckBox();
            this.btnSelectGCPs = new System.Windows.Forms.Button();
            this.cbxOrthorectify = new System.Windows.Forms.CheckBox();
            this.imgHintTransformation = new System.Windows.Forms.PictureBox();
            this.bxOutputQuality = new System.Windows.Forms.NumericUpDown();
            this.lblOutputQuality = new System.Windows.Forms.Label();
            this.bxOutputExtension = new System.Windows.Forms.ComboBox();
            this.lblOutputExtension = new System.Windows.Forms.Label();
            this.bxVideoFramerate = new System.Windows.Forms.TextBox();
            this.lblVideoFramerateUnits = new System.Windows.Forms.Label();
            this.lblVideoFramerate = new System.Windows.Forms.Label();
            this.lblRANSACThrUnits = new System.Windows.Forms.Label();
            this.lblRANSACThr = new System.Windows.Forms.Label();
            this.bxRANSACThr = new System.Windows.Forms.NumericUpDown();
            this.cbxUseRANSAC = new System.Windows.Forms.CheckBox();
            this.lblTransformMethod = new System.Windows.Forms.Label();
            this.bxTransformMethod = new System.Windows.Forms.ComboBox();
            this.lblNumFramesStabilization = new System.Windows.Forms.Label();
            this.btnFramesToVideo = new System.Windows.Forms.Button();
            this.lblNumFramesTracking = new System.Windows.Forms.Label();
            this.lblOrthoConfiguration = new System.Windows.Forms.Label();
            this.lblPython = new System.Windows.Forms.Label();
            this.lblBuild = new System.Windows.Forms.Label();
            this.grpBasicParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgHintBasicTracking)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bxSubpixelSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bxSASize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bxIASize)).BeginInit();
            this.grpAdvancedParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgHintAdvancedTracking)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bxExpandSAThr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bxExpandSACoef)).BeginInit();
            this.grpTransformation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgHintTransformation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bxOutputQuality)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bxRANSACThr)).BeginInit();
            this.SuspendLayout();
            // 
            // btnFrames
            // 
            this.btnFrames.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnFrames.FlatAppearance.BorderColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnFrames.FlatAppearance.BorderSize = 2;
            this.btnFrames.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnFrames.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFrames.Location = new System.Drawing.Point(12, 63);
            this.btnFrames.Name = "btnFrames";
            this.btnFrames.Size = new System.Drawing.Size(135, 27);
            this.btnFrames.TabIndex = 2;
            this.btnFrames.Text = "Select frames folder";
            this.btnFrames.UseVisualStyleBackColor = false;
            this.btnFrames.Click += new System.EventHandler(this.btnFramesFolder_Click);
            // 
            // btnOutput_folder
            // 
            this.btnOutput_folder.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnOutput_folder.FlatAppearance.BorderColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnOutput_folder.FlatAppearance.BorderSize = 2;
            this.btnOutput_folder.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnOutput_folder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOutput_folder.Location = new System.Drawing.Point(12, 96);
            this.btnOutput_folder.Name = "btnOutput_folder";
            this.btnOutput_folder.Size = new System.Drawing.Size(135, 27);
            this.btnOutput_folder.TabIndex = 4;
            this.btnOutput_folder.Text = "Select output folder";
            this.btnOutput_folder.UseVisualStyleBackColor = false;
            this.btnOutput_folder.Click += new System.EventHandler(this.btnOutput_folder_Click);
            // 
            // btnTrackFeatures
            // 
            this.btnTrackFeatures.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTrackFeatures.BackColor = System.Drawing.Color.DarkGray;
            this.btnTrackFeatures.Enabled = false;
            this.btnTrackFeatures.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnTrackFeatures.FlatAppearance.BorderSize = 2;
            this.btnTrackFeatures.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PaleGreen;
            this.btnTrackFeatures.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrackFeatures.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrackFeatures.Location = new System.Drawing.Point(12, 506);
            this.btnTrackFeatures.Name = "btnTrackFeatures";
            this.btnTrackFeatures.Size = new System.Drawing.Size(235, 27);
            this.btnTrackFeatures.TabIndex = 12;
            this.btnTrackFeatures.Text = "Track features";
            this.btnTrackFeatures.UseVisualStyleBackColor = false;
            this.btnTrackFeatures.Click += new System.EventHandler(this.btnTrackFeatures_Click);
            // 
            // btnStabilize
            // 
            this.btnStabilize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStabilize.BackColor = System.Drawing.Color.DarkGray;
            this.btnStabilize.Enabled = false;
            this.btnStabilize.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnStabilize.FlatAppearance.BorderSize = 2;
            this.btnStabilize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PaleGreen;
            this.btnStabilize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStabilize.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStabilize.Location = new System.Drawing.Point(253, 506);
            this.btnStabilize.Name = "btnStabilize";
            this.btnStabilize.Size = new System.Drawing.Size(235, 27);
            this.btnStabilize.TabIndex = 13;
            this.btnStabilize.Text = "Stabilize";
            this.btnStabilize.UseVisualStyleBackColor = false;
            this.btnStabilize.Click += new System.EventHandler(this.btnStabilize_Click);
            // 
            // bxOutputFolder
            // 
            this.bxOutputFolder.Location = new System.Drawing.Point(156, 99);
            this.bxOutputFolder.Name = "bxOutputFolder";
            this.bxOutputFolder.Size = new System.Drawing.Size(332, 23);
            this.bxOutputFolder.TabIndex = 5;
            this.bxOutputFolder.Text = "Output folder";
            this.bxOutputFolder.Leave += new System.EventHandler(this.bxOutputFolder_Leave);
            // 
            // selectFramesFolder
            // 
            this.selectFramesFolder.CheckFileExists = false;
            this.selectFramesFolder.FileName = "Select folder";
            this.selectFramesFolder.ValidateNames = false;
            // 
            // selectOutputFolder
            // 
            this.selectOutputFolder.CheckFileExists = false;
            this.selectOutputFolder.FileName = "Select folder";
            this.selectOutputFolder.ValidateNames = false;
            // 
            // btnUnpackVideo
            // 
            this.btnUnpackVideo.BackColor = System.Drawing.Color.LightSalmon;
            this.btnUnpackVideo.FlatAppearance.BorderColor = System.Drawing.Color.LightSalmon;
            this.btnUnpackVideo.FlatAppearance.BorderSize = 2;
            this.btnUnpackVideo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PeachPuff;
            this.btnUnpackVideo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUnpackVideo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUnpackVideo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnUnpackVideo.Location = new System.Drawing.Point(12, 12);
            this.btnUnpackVideo.Name = "btnUnpackVideo";
            this.btnUnpackVideo.Size = new System.Drawing.Size(199, 27);
            this.btnUnpackVideo.TabIndex = 0;
            this.btnUnpackVideo.Text = "Unpack video into frames";
            this.btnUnpackVideo.UseVisualStyleBackColor = false;
            this.btnUnpackVideo.Click += new System.EventHandler(this.btnUnpackVideo_Click);
            // 
            // bxFramesFolder
            // 
            this.bxFramesFolder.Location = new System.Drawing.Point(156, 66);
            this.bxFramesFolder.Name = "bxFramesFolder";
            this.bxFramesFolder.Size = new System.Drawing.Size(332, 23);
            this.bxFramesFolder.TabIndex = 3;
            this.bxFramesFolder.Text = "Frames folder";
            this.bxFramesFolder.Leave += new System.EventHandler(this.bxFramesFolder_Leave);
            // 
            // grpBasicParameters
            // 
            this.grpBasicParameters.Controls.Add(this.bxInputExtension);
            this.grpBasicParameters.Controls.Add(this.lblInputExtension);
            this.grpBasicParameters.Controls.Add(this.lblSubpixelSizeUnits);
            this.grpBasicParameters.Controls.Add(this.imgHintBasicTracking);
            this.grpBasicParameters.Controls.Add(this.lblSASizeUnits);
            this.grpBasicParameters.Controls.Add(this.lblIASizeUnits);
            this.grpBasicParameters.Controls.Add(this.lblSubpixelSize);
            this.grpBasicParameters.Controls.Add(this.bxSubpixelSize);
            this.grpBasicParameters.Controls.Add(this.cbxSubpixelEstimator);
            this.grpBasicParameters.Controls.Add(this.lblSASize);
            this.grpBasicParameters.Controls.Add(this.lblIASize);
            this.grpBasicParameters.Controls.Add(this.bxSASize);
            this.grpBasicParameters.Controls.Add(this.bxIASize);
            this.grpBasicParameters.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpBasicParameters.Location = new System.Drawing.Point(12, 134);
            this.grpBasicParameters.Name = "grpBasicParameters";
            this.grpBasicParameters.Size = new System.Drawing.Size(235, 171);
            this.grpBasicParameters.TabIndex = 6;
            this.grpBasicParameters.TabStop = false;
            this.grpBasicParameters.Text = "       Basic tracking parameters";
            // 
            // bxInputExtension
            // 
            this.bxInputExtension.FormattingEnabled = true;
            this.bxInputExtension.Location = new System.Drawing.Point(148, 24);
            this.bxInputExtension.Name = "bxInputExtension";
            this.bxInputExtension.Size = new System.Drawing.Size(78, 23);
            this.bxInputExtension.TabIndex = 1;
            this.bxInputExtension.SelectedIndexChanged += new System.EventHandler(this.bxInputExtension_SelectedIndexChanged);
            this.bxInputExtension.TextChanged += new System.EventHandler(this.bxInputExtension_TextChanged);
            // 
            // lblInputExtension
            // 
            this.lblInputExtension.AutoSize = true;
            this.lblInputExtension.Location = new System.Drawing.Point(6, 27);
            this.lblInputExtension.Name = "lblInputExtension";
            this.lblInputExtension.Size = new System.Drawing.Size(92, 15);
            this.lblInputExtension.TabIndex = 0;
            this.lblInputExtension.Text = "Input extension:";
            // 
            // lblSubpixelSizeUnits
            // 
            this.lblSubpixelSizeUnits.AutoSize = true;
            this.lblSubpixelSizeUnits.Location = new System.Drawing.Point(206, 139);
            this.lblSubpixelSizeUnits.Name = "lblSubpixelSizeUnits";
            this.lblSubpixelSizeUnits.Size = new System.Drawing.Size(20, 15);
            this.lblSubpixelSizeUnits.TabIndex = 11;
            this.lblSubpixelSizeUnits.Text = "px";
            // 
            // imgHintBasicTracking
            // 
            this.imgHintBasicTracking.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.imgHintBasicTracking.Image = ((System.Drawing.Image)(resources.GetObject("imgHintBasicTracking.Image")));
            this.imgHintBasicTracking.Location = new System.Drawing.Point(9, 0);
            this.imgHintBasicTracking.Name = "imgHintBasicTracking";
            this.imgHintBasicTracking.Size = new System.Drawing.Size(16, 16);
            this.imgHintBasicTracking.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgHintBasicTracking.TabIndex = 15;
            this.imgHintBasicTracking.TabStop = false;
            // 
            // lblSASizeUnits
            // 
            this.lblSASizeUnits.AutoSize = true;
            this.lblSASizeUnits.Location = new System.Drawing.Point(206, 85);
            this.lblSASizeUnits.Name = "lblSASizeUnits";
            this.lblSASizeUnits.Size = new System.Drawing.Size(20, 15);
            this.lblSASizeUnits.TabIndex = 7;
            this.lblSASizeUnits.Text = "px";
            // 
            // lblIASizeUnits
            // 
            this.lblIASizeUnits.AutoSize = true;
            this.lblIASizeUnits.Location = new System.Drawing.Point(206, 56);
            this.lblIASizeUnits.Name = "lblIASizeUnits";
            this.lblIASizeUnits.Size = new System.Drawing.Size(20, 15);
            this.lblIASizeUnits.TabIndex = 4;
            this.lblIASizeUnits.Text = "px";
            // 
            // lblSubpixelSize
            // 
            this.lblSubpixelSize.AutoSize = true;
            this.lblSubpixelSize.Location = new System.Drawing.Point(6, 139);
            this.lblSubpixelSize.Name = "lblSubpixelSize";
            this.lblSubpixelSize.Size = new System.Drawing.Size(130, 15);
            this.lblSubpixelSize.TabIndex = 9;
            this.lblSubpixelSize.Text = "Subpixel estimator size:";
            // 
            // bxSubpixelSize
            // 
            this.bxSubpixelSize.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.bxSubpixelSize.Location = new System.Drawing.Point(148, 137);
            this.bxSubpixelSize.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.bxSubpixelSize.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.bxSubpixelSize.Name = "bxSubpixelSize";
            this.bxSubpixelSize.Size = new System.Drawing.Size(53, 23);
            this.bxSubpixelSize.TabIndex = 10;
            this.bxSubpixelSize.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.bxSubpixelSize.Leave += new System.EventHandler(this.FieldToOddInt);
            // 
            // cbxSubpixelEstimator
            // 
            this.cbxSubpixelEstimator.AutoSize = true;
            this.cbxSubpixelEstimator.Checked = true;
            this.cbxSubpixelEstimator.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxSubpixelEstimator.Location = new System.Drawing.Point(9, 112);
            this.cbxSubpixelEstimator.Name = "cbxSubpixelEstimator";
            this.cbxSubpixelEstimator.Size = new System.Drawing.Size(145, 19);
            this.cbxSubpixelEstimator.TabIndex = 8;
            this.cbxSubpixelEstimator.Text = "Use subpixel estimator";
            this.cbxSubpixelEstimator.UseVisualStyleBackColor = true;
            // 
            // lblSASize
            // 
            this.lblSASize.AutoSize = true;
            this.lblSASize.Location = new System.Drawing.Point(6, 85);
            this.lblSASize.Name = "lblSASize";
            this.lblSASize.Size = new System.Drawing.Size(92, 15);
            this.lblSASize.TabIndex = 5;
            this.lblSASize.Text = "Search area size:";
            // 
            // lblIASize
            // 
            this.lblIASize.AutoSize = true;
            this.lblIASize.Location = new System.Drawing.Point(6, 56);
            this.lblIASize.Name = "lblIASize";
            this.lblIASize.Size = new System.Drawing.Size(126, 15);
            this.lblIASize.TabIndex = 2;
            this.lblIASize.Text = "Interrogation area size:";
            // 
            // bxSASize
            // 
            this.bxSASize.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.bxSASize.Location = new System.Drawing.Point(148, 83);
            this.bxSASize.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.bxSASize.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.bxSASize.Name = "bxSASize";
            this.bxSASize.Size = new System.Drawing.Size(53, 23);
            this.bxSASize.TabIndex = 6;
            this.bxSASize.Value = new decimal(new int[] {
            21,
            0,
            0,
            0});
            this.bxSASize.Leave += new System.EventHandler(this.FieldToOddInt);
            // 
            // bxIASize
            // 
            this.bxIASize.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.bxIASize.Location = new System.Drawing.Point(148, 54);
            this.bxIASize.Maximum = new decimal(new int[] {
            997,
            0,
            0,
            0});
            this.bxIASize.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.bxIASize.Name = "bxIASize";
            this.bxIASize.Size = new System.Drawing.Size(53, 23);
            this.bxIASize.TabIndex = 3;
            this.bxIASize.Value = new decimal(new int[] {
            11,
            0,
            0,
            0});
            this.bxIASize.Leave += new System.EventHandler(this.FieldToOddInt);
            // 
            // grpAdvancedParameters
            // 
            this.grpAdvancedParameters.Controls.Add(this.imgHintAdvancedTracking);
            this.grpAdvancedParameters.Controls.Add(this.cbxUpdateKernels);
            this.grpAdvancedParameters.Controls.Add(this.bxExpandSAThr);
            this.grpAdvancedParameters.Controls.Add(this.lblExpandSAThr);
            this.grpAdvancedParameters.Controls.Add(this.lblExpandSACoef);
            this.grpAdvancedParameters.Controls.Add(this.bxExpandSACoef);
            this.grpAdvancedParameters.Controls.Add(this.cbxExpandSA);
            this.grpAdvancedParameters.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpAdvancedParameters.Location = new System.Drawing.Point(12, 311);
            this.grpAdvancedParameters.Name = "grpAdvancedParameters";
            this.grpAdvancedParameters.Size = new System.Drawing.Size(235, 145);
            this.grpAdvancedParameters.TabIndex = 7;
            this.grpAdvancedParameters.TabStop = false;
            this.grpAdvancedParameters.Text = "       Advanced tracking parameters";
            // 
            // imgHintAdvancedTracking
            // 
            this.imgHintAdvancedTracking.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.imgHintAdvancedTracking.Image = ((System.Drawing.Image)(resources.GetObject("imgHintAdvancedTracking.Image")));
            this.imgHintAdvancedTracking.Location = new System.Drawing.Point(9, 0);
            this.imgHintAdvancedTracking.Name = "imgHintAdvancedTracking";
            this.imgHintAdvancedTracking.Size = new System.Drawing.Size(16, 16);
            this.imgHintAdvancedTracking.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgHintAdvancedTracking.TabIndex = 16;
            this.imgHintAdvancedTracking.TabStop = false;
            // 
            // cbxUpdateKernels
            // 
            this.cbxUpdateKernels.AutoSize = true;
            this.cbxUpdateKernels.Location = new System.Drawing.Point(9, 113);
            this.cbxUpdateKernels.Name = "cbxUpdateKernels";
            this.cbxUpdateKernels.Size = new System.Drawing.Size(198, 19);
            this.cbxUpdateKernels.TabIndex = 5;
            this.cbxUpdateKernels.Text = "Update features after each frame";
            this.cbxUpdateKernels.UseVisualStyleBackColor = true;
            // 
            // bxExpandSAThr
            // 
            this.bxExpandSAThr.DecimalPlaces = 2;
            this.bxExpandSAThr.Enabled = false;
            this.bxExpandSAThr.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.bxExpandSAThr.Location = new System.Drawing.Point(148, 82);
            this.bxExpandSAThr.Maximum = new decimal(new int[] {
            95,
            0,
            0,
            131072});
            this.bxExpandSAThr.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.bxExpandSAThr.Name = "bxExpandSAThr";
            this.bxExpandSAThr.Size = new System.Drawing.Size(51, 23);
            this.bxExpandSAThr.TabIndex = 4;
            this.bxExpandSAThr.Value = new decimal(new int[] {
            50,
            0,
            0,
            131072});
            // 
            // lblExpandSAThr
            // 
            this.lblExpandSAThr.AutoSize = true;
            this.lblExpandSAThr.Enabled = false;
            this.lblExpandSAThr.Location = new System.Drawing.Point(6, 84);
            this.lblExpandSAThr.Name = "lblExpandSAThr";
            this.lblExpandSAThr.Size = new System.Drawing.Size(102, 15);
            this.lblExpandSAThr.TabIndex = 3;
            this.lblExpandSAThr.Text = "Expand threshold:";
            // 
            // lblExpandSACoef
            // 
            this.lblExpandSACoef.AutoSize = true;
            this.lblExpandSACoef.Enabled = false;
            this.lblExpandSACoef.Location = new System.Drawing.Point(6, 56);
            this.lblExpandSACoef.Name = "lblExpandSACoef";
            this.lblExpandSACoef.Size = new System.Drawing.Size(108, 15);
            this.lblExpandSACoef.TabIndex = 1;
            this.lblExpandSACoef.Text = "Expand coefficient:";
            // 
            // bxExpandSACoef
            // 
            this.bxExpandSACoef.DecimalPlaces = 1;
            this.bxExpandSACoef.Enabled = false;
            this.bxExpandSACoef.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.bxExpandSACoef.Location = new System.Drawing.Point(148, 54);
            this.bxExpandSACoef.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.bxExpandSACoef.Minimum = new decimal(new int[] {
            11,
            0,
            0,
            65536});
            this.bxExpandSACoef.Name = "bxExpandSACoef";
            this.bxExpandSACoef.Size = new System.Drawing.Size(51, 23);
            this.bxExpandSACoef.TabIndex = 2;
            this.bxExpandSACoef.Value = new decimal(new int[] {
            20,
            0,
            0,
            65536});
            // 
            // cbxExpandSA
            // 
            this.cbxExpandSA.AutoSize = true;
            this.cbxExpandSA.Location = new System.Drawing.Point(9, 26);
            this.cbxExpandSA.Name = "cbxExpandSA";
            this.cbxExpandSA.Size = new System.Drawing.Size(127, 19);
            this.cbxExpandSA.TabIndex = 0;
            this.cbxExpandSA.Text = "Expand search area";
            this.cbxExpandSA.UseVisualStyleBackColor = true;
            this.cbxExpandSA.CheckedChanged += new System.EventHandler(this.cbxExpandSA_CheckedChanged);
            // 
            // grpTransformation
            // 
            this.grpTransformation.Controls.Add(this.chkCreateVideo);
            this.grpTransformation.Controls.Add(this.btnSelectGCPs);
            this.grpTransformation.Controls.Add(this.cbxOrthorectify);
            this.grpTransformation.Controls.Add(this.imgHintTransformation);
            this.grpTransformation.Controls.Add(this.bxOutputQuality);
            this.grpTransformation.Controls.Add(this.lblOutputQuality);
            this.grpTransformation.Controls.Add(this.bxOutputExtension);
            this.grpTransformation.Controls.Add(this.lblOutputExtension);
            this.grpTransformation.Controls.Add(this.bxVideoFramerate);
            this.grpTransformation.Controls.Add(this.lblVideoFramerateUnits);
            this.grpTransformation.Controls.Add(this.lblVideoFramerate);
            this.grpTransformation.Controls.Add(this.lblRANSACThrUnits);
            this.grpTransformation.Controls.Add(this.lblRANSACThr);
            this.grpTransformation.Controls.Add(this.bxRANSACThr);
            this.grpTransformation.Controls.Add(this.cbxUseRANSAC);
            this.grpTransformation.Controls.Add(this.lblTransformMethod);
            this.grpTransformation.Controls.Add(this.bxTransformMethod);
            this.grpTransformation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpTransformation.Location = new System.Drawing.Point(253, 134);
            this.grpTransformation.Name = "grpTransformation";
            this.grpTransformation.Size = new System.Drawing.Size(235, 322);
            this.grpTransformation.TabIndex = 9;
            this.grpTransformation.TabStop = false;
            this.grpTransformation.Text = "       Image transformation";
            // 
            // chkCreateVideo
            // 
            this.chkCreateVideo.AutoSize = true;
            this.chkCreateVideo.Checked = true;
            this.chkCreateVideo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCreateVideo.Location = new System.Drawing.Point(9, 112);
            this.chkCreateVideo.Name = "chkCreateVideo";
            this.chkCreateVideo.Size = new System.Drawing.Size(162, 19);
            this.chkCreateVideo.TabIndex = 18;
            this.chkCreateVideo.Text = "Create video from images";
            this.chkCreateVideo.UseVisualStyleBackColor = true;
            this.chkCreateVideo.CheckedChanged += new System.EventHandler(this.chkCreateVideo_CheckedChanged);
            // 
            // btnSelectGCPs
            // 
            this.btnSelectGCPs.BackColor = System.Drawing.Color.DarkGray;
            this.btnSelectGCPs.Enabled = false;
            this.btnSelectGCPs.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnSelectGCPs.FlatAppearance.BorderSize = 2;
            this.btnSelectGCPs.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnSelectGCPs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectGCPs.Location = new System.Drawing.Point(9, 21);
            this.btnSelectGCPs.Name = "btnSelectGCPs";
            this.btnSelectGCPs.Size = new System.Drawing.Size(217, 27);
            this.btnSelectGCPs.TabIndex = 17;
            this.btnSelectGCPs.Text = "Select features for transformation";
            this.btnSelectGCPs.UseVisualStyleBackColor = false;
            this.btnSelectGCPs.Click += new System.EventHandler(this.btnSelectGCPs_Click);
            // 
            // cbxOrthorectify
            // 
            this.cbxOrthorectify.AutoSize = true;
            this.cbxOrthorectify.Enabled = false;
            this.cbxOrthorectify.Location = new System.Drawing.Point(9, 285);
            this.cbxOrthorectify.Name = "cbxOrthorectify";
            this.cbxOrthorectify.Size = new System.Drawing.Size(90, 19);
            this.cbxOrthorectify.TabIndex = 15;
            this.cbxOrthorectify.Text = "Orthorectify";
            this.cbxOrthorectify.UseVisualStyleBackColor = true;
            this.cbxOrthorectify.CheckedChanged += new System.EventHandler(this.cbxOrthorectify_CheckedChanged);
            // 
            // imgHintTransformation
            // 
            this.imgHintTransformation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.imgHintTransformation.Image = ((System.Drawing.Image)(resources.GetObject("imgHintTransformation.Image")));
            this.imgHintTransformation.Location = new System.Drawing.Point(9, 0);
            this.imgHintTransformation.Name = "imgHintTransformation";
            this.imgHintTransformation.Size = new System.Drawing.Size(16, 16);
            this.imgHintTransformation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgHintTransformation.TabIndex = 16;
            this.imgHintTransformation.TabStop = false;
            // 
            // bxOutputQuality
            // 
            this.bxOutputQuality.Location = new System.Drawing.Point(148, 82);
            this.bxOutputQuality.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.bxOutputQuality.Name = "bxOutputQuality";
            this.bxOutputQuality.Size = new System.Drawing.Size(53, 23);
            this.bxOutputQuality.TabIndex = 5;
            this.bxOutputQuality.Value = new decimal(new int[] {
            95,
            0,
            0,
            0});
            // 
            // lblOutputQuality
            // 
            this.lblOutputQuality.AutoSize = true;
            this.lblOutputQuality.Location = new System.Drawing.Point(6, 84);
            this.lblOutputQuality.Name = "lblOutputQuality";
            this.lblOutputQuality.Size = new System.Drawing.Size(122, 15);
            this.lblOutputQuality.TabIndex = 4;
            this.lblOutputQuality.Text = "Image quality (1-100):";
            // 
            // bxOutputExtension
            // 
            this.bxOutputExtension.FormattingEnabled = true;
            this.bxOutputExtension.Location = new System.Drawing.Point(148, 53);
            this.bxOutputExtension.Name = "bxOutputExtension";
            this.bxOutputExtension.Size = new System.Drawing.Size(78, 23);
            this.bxOutputExtension.TabIndex = 3;
            // 
            // lblOutputExtension
            // 
            this.lblOutputExtension.AutoSize = true;
            this.lblOutputExtension.Location = new System.Drawing.Point(6, 56);
            this.lblOutputExtension.Name = "lblOutputExtension";
            this.lblOutputExtension.Size = new System.Drawing.Size(102, 15);
            this.lblOutputExtension.TabIndex = 2;
            this.lblOutputExtension.Text = "Output extension:";
            // 
            // bxVideoFramerate
            // 
            this.bxVideoFramerate.Location = new System.Drawing.Point(148, 136);
            this.bxVideoFramerate.Name = "bxVideoFramerate";
            this.bxVideoFramerate.Size = new System.Drawing.Size(53, 23);
            this.bxVideoFramerate.TabIndex = 7;
            this.bxVideoFramerate.Text = "30.00";
            this.bxVideoFramerate.Leave += new System.EventHandler(this.bxVideoFramerate_Leave);
            // 
            // lblVideoFramerateUnits
            // 
            this.lblVideoFramerateUnits.AutoSize = true;
            this.lblVideoFramerateUnits.Location = new System.Drawing.Point(203, 139);
            this.lblVideoFramerateUnits.Name = "lblVideoFramerateUnits";
            this.lblVideoFramerateUnits.Size = new System.Drawing.Size(26, 15);
            this.lblVideoFramerateUnits.TabIndex = 8;
            this.lblVideoFramerateUnits.Text = "FPS";
            // 
            // lblVideoFramerate
            // 
            this.lblVideoFramerate.AutoSize = true;
            this.lblVideoFramerate.Location = new System.Drawing.Point(6, 139);
            this.lblVideoFramerate.Name = "lblVideoFramerate";
            this.lblVideoFramerate.Size = new System.Drawing.Size(94, 15);
            this.lblVideoFramerate.TabIndex = 6;
            this.lblVideoFramerate.Text = "Video framerate:";
            // 
            // lblRANSACThrUnits
            // 
            this.lblRANSACThrUnits.AutoSize = true;
            this.lblRANSACThrUnits.Location = new System.Drawing.Point(203, 250);
            this.lblRANSACThrUnits.Name = "lblRANSACThrUnits";
            this.lblRANSACThrUnits.Size = new System.Drawing.Size(20, 15);
            this.lblRANSACThrUnits.TabIndex = 14;
            this.lblRANSACThrUnits.Text = "px";
            // 
            // lblRANSACThr
            // 
            this.lblRANSACThr.AutoSize = true;
            this.lblRANSACThr.Enabled = false;
            this.lblRANSACThr.Location = new System.Drawing.Point(6, 250);
            this.lblRANSACThr.Name = "lblRANSACThr";
            this.lblRANSACThr.Size = new System.Drawing.Size(109, 15);
            this.lblRANSACThr.TabIndex = 12;
            this.lblRANSACThr.Text = "RANSAC threshold:";
            // 
            // bxRANSACThr
            // 
            this.bxRANSACThr.DecimalPlaces = 1;
            this.bxRANSACThr.Enabled = false;
            this.bxRANSACThr.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.bxRANSACThr.Location = new System.Drawing.Point(148, 248);
            this.bxRANSACThr.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.bxRANSACThr.Name = "bxRANSACThr";
            this.bxRANSACThr.Size = new System.Drawing.Size(53, 23);
            this.bxRANSACThr.TabIndex = 13;
            this.bxRANSACThr.Value = new decimal(new int[] {
            20,
            0,
            0,
            65536});
            // 
            // cbxUseRANSAC
            // 
            this.cbxUseRANSAC.AutoSize = true;
            this.cbxUseRANSAC.Location = new System.Drawing.Point(9, 223);
            this.cbxUseRANSAC.Name = "cbxUseRANSAC";
            this.cbxUseRANSAC.Size = new System.Drawing.Size(138, 19);
            this.cbxUseRANSAC.TabIndex = 11;
            this.cbxUseRANSAC.Text = "Use RANSAC filtering";
            this.cbxUseRANSAC.UseVisualStyleBackColor = true;
            this.cbxUseRANSAC.CheckedChanged += new System.EventHandler(this.cbxUseRANSAC_CheckedChanged);
            // 
            // lblTransformMethod
            // 
            this.lblTransformMethod.AutoSize = true;
            this.lblTransformMethod.Location = new System.Drawing.Point(6, 168);
            this.lblTransformMethod.Name = "lblTransformMethod";
            this.lblTransformMethod.Size = new System.Drawing.Size(135, 15);
            this.lblTransformMethod.TabIndex = 9;
            this.lblTransformMethod.Text = "Transformation method:";
            // 
            // bxTransformMethod
            // 
            this.bxTransformMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bxTransformMethod.FormattingEnabled = true;
            this.bxTransformMethod.Items.AddRange(new object[] {
            "Similarity (2 points, optimal)",
            "Affine 2D (strict)",
            "Affine 2D (optimal)",
            "Projective (strict)",
            "Projective (optimal)"});
            this.bxTransformMethod.Location = new System.Drawing.Point(9, 189);
            this.bxTransformMethod.Name = "bxTransformMethod";
            this.bxTransformMethod.Size = new System.Drawing.Size(217, 23);
            this.bxTransformMethod.TabIndex = 10;
            this.bxTransformMethod.SelectedIndexChanged += new System.EventHandler(this.bxTransformMethod_SelectedIndexChanged);
            // 
            // lblNumFramesStabilization
            // 
            this.lblNumFramesStabilization.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNumFramesStabilization.AutoSize = true;
            this.lblNumFramesStabilization.ForeColor = System.Drawing.Color.Red;
            this.lblNumFramesStabilization.Location = new System.Drawing.Point(250, 463);
            this.lblNumFramesStabilization.Name = "lblNumFramesStabilization";
            this.lblNumFramesStabilization.Size = new System.Drawing.Size(214, 15);
            this.lblNumFramesStabilization.TabIndex = 10;
            this.lblNumFramesStabilization.Text = "No available frames for transformation.";
            // 
            // btnFramesToVideo
            // 
            this.btnFramesToVideo.BackColor = System.Drawing.Color.LightSalmon;
            this.btnFramesToVideo.FlatAppearance.BorderColor = System.Drawing.Color.LightSalmon;
            this.btnFramesToVideo.FlatAppearance.BorderSize = 2;
            this.btnFramesToVideo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PeachPuff;
            this.btnFramesToVideo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFramesToVideo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFramesToVideo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnFramesToVideo.Location = new System.Drawing.Point(289, 12);
            this.btnFramesToVideo.Name = "btnFramesToVideo";
            this.btnFramesToVideo.Size = new System.Drawing.Size(199, 27);
            this.btnFramesToVideo.TabIndex = 1;
            this.btnFramesToVideo.Text = "Create a video from frames";
            this.btnFramesToVideo.UseVisualStyleBackColor = false;
            this.btnFramesToVideo.Click += new System.EventHandler(this.BtnFramesToVideo_Click);
            // 
            // lblNumFramesTracking
            // 
            this.lblNumFramesTracking.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNumFramesTracking.AutoSize = true;
            this.lblNumFramesTracking.ForeColor = System.Drawing.Color.Red;
            this.lblNumFramesTracking.Location = new System.Drawing.Point(9, 463);
            this.lblNumFramesTracking.Name = "lblNumFramesTracking";
            this.lblNumFramesTracking.Size = new System.Drawing.Size(178, 15);
            this.lblNumFramesTracking.TabIndex = 8;
            this.lblNumFramesTracking.Text = "No available frames for tracking.";
            // 
            // lblOrthoConfiguration
            // 
            this.lblOrthoConfiguration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblOrthoConfiguration.AutoSize = true;
            this.lblOrthoConfiguration.ForeColor = System.Drawing.Color.Red;
            this.lblOrthoConfiguration.Location = new System.Drawing.Point(250, 483);
            this.lblOrthoConfiguration.Name = "lblOrthoConfiguration";
            this.lblOrthoConfiguration.Size = new System.Drawing.Size(186, 15);
            this.lblOrthoConfiguration.TabIndex = 11;
            this.lblOrthoConfiguration.Text = "Orthorectification not configured.";
            // 
            // lblPython
            // 
            this.lblPython.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPython.AutoSize = true;
            this.lblPython.Enabled = false;
            this.lblPython.Location = new System.Drawing.Point(9, 541);
            this.lblPython.Name = "lblPython";
            this.lblPython.Size = new System.Drawing.Size(121, 15);
            this.lblPython.TabIndex = 14;
            this.lblPython.Text = "Python version: NULL";
            // 
            // lblBuild
            // 
            this.lblBuild.Enabled = false;
            this.lblBuild.Location = new System.Drawing.Point(253, 541);
            this.lblBuild.Name = "lblBuild";
            this.lblBuild.Size = new System.Drawing.Size(235, 15);
            this.lblBuild.TabIndex = 19;
            this.lblBuild.Text = "Build version:";
            this.lblBuild.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(498, 564);
            this.Controls.Add(this.lblBuild);
            this.Controls.Add(this.lblPython);
            this.Controls.Add(this.lblOrthoConfiguration);
            this.Controls.Add(this.lblNumFramesTracking);
            this.Controls.Add(this.btnFramesToVideo);
            this.Controls.Add(this.lblNumFramesStabilization);
            this.Controls.Add(this.grpTransformation);
            this.Controls.Add(this.grpAdvancedParameters);
            this.Controls.Add(this.grpBasicParameters);
            this.Controls.Add(this.bxFramesFolder);
            this.Controls.Add(this.btnUnpackVideo);
            this.Controls.Add(this.bxOutputFolder);
            this.Controls.Add(this.btnStabilize);
            this.Controls.Add(this.btnTrackFeatures);
            this.Controls.Add(this.btnOutput_folder);
            this.Controls.Add(this.btnFrames);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SSIMS: SSIM Stabilization Tools";
            this.Load += new System.EventHandler(this.Main_Load);
            this.grpBasicParameters.ResumeLayout(false);
            this.grpBasicParameters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgHintBasicTracking)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bxSubpixelSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bxSASize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bxIASize)).EndInit();
            this.grpAdvancedParameters.ResumeLayout(false);
            this.grpAdvancedParameters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgHintAdvancedTracking)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bxExpandSAThr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bxExpandSACoef)).EndInit();
            this.grpTransformation.ResumeLayout(false);
            this.grpTransformation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgHintTransformation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bxOutputQuality)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bxRANSACThr)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFrames;
        private System.Windows.Forms.Button btnOutput_folder;
        private System.Windows.Forms.Button btnTrackFeatures;
        private System.Windows.Forms.Button btnStabilize;
        private System.Windows.Forms.TextBox bxOutputFolder;
        private System.Windows.Forms.OpenFileDialog selectFramesFolder;
        private System.Windows.Forms.OpenFileDialog selectOutputFolder;
        private System.Windows.Forms.Button btnUnpackVideo;
        private System.Windows.Forms.TextBox bxFramesFolder;
        private System.Windows.Forms.GroupBox grpBasicParameters;
        private System.Windows.Forms.Label lblSASize;
        private System.Windows.Forms.Label lblIASize;
        private System.Windows.Forms.NumericUpDown bxSASize;
        private System.Windows.Forms.NumericUpDown bxIASize;
        private System.Windows.Forms.GroupBox grpAdvancedParameters;
        private System.Windows.Forms.CheckBox cbxUpdateKernels;
        private System.Windows.Forms.NumericUpDown bxExpandSAThr;
        private System.Windows.Forms.Label lblExpandSAThr;
        private System.Windows.Forms.Label lblExpandSACoef;
        private System.Windows.Forms.NumericUpDown bxExpandSACoef;
        private System.Windows.Forms.CheckBox cbxExpandSA;
        private System.Windows.Forms.GroupBox grpTransformation;
        private System.Windows.Forms.Label lblRANSACThr;
        private System.Windows.Forms.NumericUpDown bxRANSACThr;
        private System.Windows.Forms.CheckBox cbxUseRANSAC;
        private System.Windows.Forms.Label lblTransformMethod;
        private System.Windows.Forms.ComboBox bxTransformMethod;
        private System.Windows.Forms.Label lblVideoFramerateUnits;
        private System.Windows.Forms.Label lblVideoFramerate;
        private System.Windows.Forms.Label lblRANSACThrUnits;
        private System.Windows.Forms.TextBox bxVideoFramerate;
        private System.Windows.Forms.Label lblNumFramesStabilization;
        private System.Windows.Forms.Label lblSubpixelSize;
        private System.Windows.Forms.NumericUpDown bxSubpixelSize;
        private System.Windows.Forms.CheckBox cbxSubpixelEstimator;
        private System.Windows.Forms.Label lblOutputExtension;
        private System.Windows.Forms.Button btnFramesToVideo;
        private System.Windows.Forms.ComboBox bxOutputExtension;
        private System.Windows.Forms.Label lblSubpixelSizeUnits;
        private System.Windows.Forms.Label lblSASizeUnits;
        private System.Windows.Forms.Label lblIASizeUnits;
        private System.Windows.Forms.NumericUpDown bxOutputQuality;
        private System.Windows.Forms.Label lblOutputQuality;
        private System.Windows.Forms.Label lblNumFramesTracking;
        private System.Windows.Forms.ComboBox bxInputExtension;
        private System.Windows.Forms.Label lblInputExtension;
        private System.Windows.Forms.PictureBox imgHintBasicTracking;
        private System.Windows.Forms.PictureBox imgHintAdvancedTracking;
        private System.Windows.Forms.PictureBox imgHintTransformation;
        private System.Windows.Forms.CheckBox cbxOrthorectify;
        private System.Windows.Forms.Label lblOrthoConfiguration;
        private System.Windows.Forms.Button btnSelectGCPs;
        private System.Windows.Forms.Label lblPython;
        private System.Windows.Forms.CheckBox chkCreateVideo;
        private System.Windows.Forms.Label lblBuild;
    }
}

