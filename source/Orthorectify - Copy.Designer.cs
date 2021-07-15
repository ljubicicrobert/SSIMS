namespace SSIM_Stabilization_GUI
{
    partial class Orthorectify
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
            this.btnClose = new System.Windows.Forms.Button();
            this.tableGCPs = new System.Windows.Forms.DataGridView();
            this.ColGCPx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColGCPy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bxPxRatio = new System.Windows.Forms.TextBox();
            this.lblPxRatio = new System.Windows.Forms.Label();
            this.btnApplyOrthorectification = new System.Windows.Forms.Button();
            this.lblGCPsHeading = new System.Windows.Forms.Label();
            this.grpPaddings = new System.Windows.Forms.GroupBox();
            this.lblYPaddingDown = new System.Windows.Forms.Label();
            this.lblXPaddingRight = new System.Windows.Forms.Label();
            this.bxYPaddingDown = new System.Windows.Forms.TextBox();
            this.bxYPaddingUp = new System.Windows.Forms.TextBox();
            this.lblYPaddingUp = new System.Windows.Forms.Label();
            this.bxXPaddingRight = new System.Windows.Forms.TextBox();
            this.bxXPaddingLeft = new System.Windows.Forms.TextBox();
            this.lblXPaddingLeft = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tableGCPs)).BeginInit();
            this.grpPaddings.SuspendLayout();
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
            this.btnClose.Location = new System.Drawing.Point(162, 422);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(94, 27);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tableGCPs
            // 
            this.tableGCPs.AllowUserToResizeRows = false;
            this.tableGCPs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableGCPs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColGCPx,
            this.ColGCPy});
            this.tableGCPs.Location = new System.Drawing.Point(12, 32);
            this.tableGCPs.Name = "tableGCPs";
            this.tableGCPs.Size = new System.Drawing.Size(244, 250);
            this.tableGCPs.TabIndex = 1;
            // 
            // ColGCPx
            // 
            this.ColGCPx.HeaderText = "GCP x [m]";
            this.ColGCPx.Name = "ColGCPx";
            // 
            // ColGCPy
            // 
            this.ColGCPy.HeaderText = "GCP y [m]";
            this.ColGCPy.Name = "ColGCPy";
            // 
            // bxPxRatio
            // 
            this.bxPxRatio.Location = new System.Drawing.Point(108, 294);
            this.bxPxRatio.Name = "bxPxRatio";
            this.bxPxRatio.Size = new System.Drawing.Size(53, 23);
            this.bxPxRatio.TabIndex = 3;
            this.bxPxRatio.Text = "100.0";
            // 
            // lblPxRatio
            // 
            this.lblPxRatio.AutoSize = true;
            this.lblPxRatio.Location = new System.Drawing.Point(12, 297);
            this.lblPxRatio.Name = "lblPxRatio";
            this.lblPxRatio.Size = new System.Drawing.Size(85, 15);
            this.lblPxRatio.TabIndex = 2;
            this.lblPxRatio.Text = "Set px/m ratio:";
            // 
            // btnApplyOrthorectification
            // 
            this.btnApplyOrthorectification.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnApplyOrthorectification.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnApplyOrthorectification.FlatAppearance.BorderColor = System.Drawing.Color.MediumSeaGreen;
            this.btnApplyOrthorectification.FlatAppearance.BorderSize = 2;
            this.btnApplyOrthorectification.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PaleGreen;
            this.btnApplyOrthorectification.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApplyOrthorectification.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApplyOrthorectification.Location = new System.Drawing.Point(12, 422);
            this.btnApplyOrthorectification.Name = "btnApplyOrthorectification";
            this.btnApplyOrthorectification.Size = new System.Drawing.Size(94, 27);
            this.btnApplyOrthorectification.TabIndex = 6;
            this.btnApplyOrthorectification.Text = "Apply";
            this.btnApplyOrthorectification.UseVisualStyleBackColor = false;
            this.btnApplyOrthorectification.Click += new System.EventHandler(this.btnApplyOrthorectification_Click);
            // 
            // lblGCPsHeading
            // 
            this.lblGCPsHeading.AutoSize = true;
            this.lblGCPsHeading.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGCPsHeading.Location = new System.Drawing.Point(9, 9);
            this.lblGCPsHeading.Name = "lblGCPsHeading";
            this.lblGCPsHeading.Size = new System.Drawing.Size(165, 15);
            this.lblGCPsHeading.TabIndex = 0;
            this.lblGCPsHeading.Text = "Real-world GCP coordinates:";
            // 
            // grpPaddings
            // 
            this.grpPaddings.Controls.Add(this.lblYPaddingDown);
            this.grpPaddings.Controls.Add(this.lblXPaddingRight);
            this.grpPaddings.Controls.Add(this.bxYPaddingDown);
            this.grpPaddings.Controls.Add(this.bxYPaddingUp);
            this.grpPaddings.Controls.Add(this.lblYPaddingUp);
            this.grpPaddings.Controls.Add(this.bxXPaddingRight);
            this.grpPaddings.Controls.Add(this.bxXPaddingLeft);
            this.grpPaddings.Controls.Add(this.lblXPaddingLeft);
            this.grpPaddings.Location = new System.Drawing.Point(12, 323);
            this.grpPaddings.Name = "grpPaddings";
            this.grpPaddings.Size = new System.Drawing.Size(243, 88);
            this.grpPaddings.TabIndex = 5;
            this.grpPaddings.TabStop = false;
            this.grpPaddings.Text = "Padding outside GCP area";
            // 
            // lblYPaddingDown
            // 
            this.lblYPaddingDown.AutoSize = true;
            this.lblYPaddingDown.Location = new System.Drawing.Point(118, 54);
            this.lblYPaddingDown.Name = "lblYPaddingDown";
            this.lblYPaddingDown.Size = new System.Drawing.Size(72, 15);
            this.lblYPaddingDown.TabIndex = 9;
            this.lblYPaddingDown.Text = "Y down [m]:";
            // 
            // lblXPaddingRight
            // 
            this.lblXPaddingRight.AutoSize = true;
            this.lblXPaddingRight.Location = new System.Drawing.Point(123, 25);
            this.lblXPaddingRight.Name = "lblXPaddingRight";
            this.lblXPaddingRight.Size = new System.Drawing.Size(67, 15);
            this.lblXPaddingRight.TabIndex = 8;
            this.lblXPaddingRight.Text = "X right [m]:";
            // 
            // bxYPaddingDown
            // 
            this.bxYPaddingDown.Location = new System.Drawing.Point(196, 51);
            this.bxYPaddingDown.Name = "bxYPaddingDown";
            this.bxYPaddingDown.Size = new System.Drawing.Size(41, 23);
            this.bxYPaddingDown.TabIndex = 7;
            this.bxYPaddingDown.Text = "1.0";
            this.bxYPaddingDown.Leave += new System.EventHandler(this.CheckFieldNumericAndPositive);
            // 
            // bxYPaddingUp
            // 
            this.bxYPaddingUp.Location = new System.Drawing.Point(71, 51);
            this.bxYPaddingUp.Name = "bxYPaddingUp";
            this.bxYPaddingUp.Size = new System.Drawing.Size(41, 23);
            this.bxYPaddingUp.TabIndex = 5;
            this.bxYPaddingUp.Text = "1.0";
            this.bxYPaddingUp.Leave += new System.EventHandler(this.CheckFieldNumericAndPositive);
            // 
            // lblYPaddingUp
            // 
            this.lblYPaddingUp.AutoSize = true;
            this.lblYPaddingUp.Location = new System.Drawing.Point(9, 54);
            this.lblYPaddingUp.Name = "lblYPaddingUp";
            this.lblYPaddingUp.Size = new System.Drawing.Size(56, 15);
            this.lblYPaddingUp.TabIndex = 4;
            this.lblYPaddingUp.Text = "Y up [m]:";
            // 
            // bxXPaddingRight
            // 
            this.bxXPaddingRight.Location = new System.Drawing.Point(196, 22);
            this.bxXPaddingRight.Name = "bxXPaddingRight";
            this.bxXPaddingRight.Size = new System.Drawing.Size(41, 23);
            this.bxXPaddingRight.TabIndex = 3;
            this.bxXPaddingRight.Text = "1.0";
            this.bxXPaddingRight.Leave += new System.EventHandler(this.CheckFieldNumericAndPositive);
            // 
            // bxXPaddingLeft
            // 
            this.bxXPaddingLeft.Location = new System.Drawing.Point(71, 22);
            this.bxXPaddingLeft.Name = "bxXPaddingLeft";
            this.bxXPaddingLeft.Size = new System.Drawing.Size(41, 23);
            this.bxXPaddingLeft.TabIndex = 1;
            this.bxXPaddingLeft.Text = "1.0";
            this.bxXPaddingLeft.Leave += new System.EventHandler(this.CheckFieldNumericAndPositive);
            // 
            // lblXPaddingLeft
            // 
            this.lblXPaddingLeft.AutoSize = true;
            this.lblXPaddingLeft.Location = new System.Drawing.Point(6, 25);
            this.lblXPaddingLeft.Name = "lblXPaddingLeft";
            this.lblXPaddingLeft.Size = new System.Drawing.Size(59, 15);
            this.lblXPaddingLeft.TabIndex = 0;
            this.lblXPaddingLeft.Text = "X left [m]:";
            // 
            // Orthorectify
            // 
            this.AcceptButton = this.btnApplyOrthorectification;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(267, 461);
            this.Controls.Add(this.grpPaddings);
            this.Controls.Add(this.lblGCPsHeading);
            this.Controls.Add(this.btnApplyOrthorectification);
            this.Controls.Add(this.bxPxRatio);
            this.Controls.Add(this.lblPxRatio);
            this.Controls.Add(this.tableGCPs);
            this.Controls.Add(this.btnClose);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.KeyPreview = true;
            this.Name = "Orthorectify";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Orthorectify";
            ((System.ComponentModel.ISupportInitialize)(this.tableGCPs)).EndInit();
            this.grpPaddings.ResumeLayout(false);
            this.grpPaddings.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView tableGCPs;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColGCPx;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColGCPy;
        private System.Windows.Forms.TextBox bxPxRatio;
        private System.Windows.Forms.Label lblPxRatio;
        private System.Windows.Forms.Button btnApplyOrthorectification;
        private System.Windows.Forms.Label lblGCPsHeading;
        private System.Windows.Forms.GroupBox grpPaddings;
        private System.Windows.Forms.TextBox bxYPaddingDown;
        private System.Windows.Forms.TextBox bxYPaddingUp;
        private System.Windows.Forms.Label lblYPaddingUp;
        private System.Windows.Forms.TextBox bxXPaddingRight;
        private System.Windows.Forms.TextBox bxXPaddingLeft;
        private System.Windows.Forms.Label lblXPaddingLeft;
        private System.Windows.Forms.Label lblYPaddingDown;
        private System.Windows.Forms.Label lblXPaddingRight;
    }
}