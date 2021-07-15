namespace SSIM_Stabilization_GUI
{
    partial class SelectFeatures
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
            this.panelFeatures = new System.Windows.Forms.Panel();
            this.lblSelectFeatures = new System.Windows.Forms.Label();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.imgInitial = new System.Windows.Forms.Panel();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnUnselectAll = new System.Windows.Forms.Button();
            this.btnRMSD = new System.Windows.Forms.Button();
            this.chkRMSDInitial = new System.Windows.Forms.RadioButton();
            this.chkRMSDAverage = new System.Windows.Forms.RadioButton();
            this.chkRMSDMedian = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // panelFeatures
            // 
            this.panelFeatures.AutoScroll = true;
            this.panelFeatures.Location = new System.Drawing.Point(691, 146);
            this.panelFeatures.Name = "panelFeatures";
            this.panelFeatures.Size = new System.Drawing.Size(279, 253);
            this.panelFeatures.TabIndex = 1;
            // 
            // lblSelectFeatures
            // 
            this.lblSelectFeatures.AutoSize = true;
            this.lblSelectFeatures.Location = new System.Drawing.Point(688, 12);
            this.lblSelectFeatures.Name = "lblSelectFeatures";
            this.lblSelectFeatures.Size = new System.Drawing.Size(226, 30);
            this.lblSelectFeatures.TabIndex = 0;
            this.lblSelectFeatures.Text = "Check which features will be used for the \r\ntransformation\r\n";
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
            this.btnApply.Location = new System.Drawing.Point(876, 413);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(94, 27);
            this.btnApply.TabIndex = 7;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = false;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
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
            this.btnClose.Location = new System.Drawing.Point(691, 413);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(94, 27);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // imgInitial
            // 
            this.imgInitial.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.imgInitial.Location = new System.Drawing.Point(12, 12);
            this.imgInitial.Name = "imgInitial";
            this.imgInitial.Size = new System.Drawing.Size(670, 428);
            this.imgInitial.TabIndex = 9;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnSelectAll.FlatAppearance.BorderColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnSelectAll.FlatAppearance.BorderSize = 2;
            this.btnSelectAll.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectAll.Location = new System.Drawing.Point(691, 113);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(89, 27);
            this.btnSelectAll.TabIndex = 3;
            this.btnSelectAll.Text = "Select all";
            this.btnSelectAll.UseVisualStyleBackColor = false;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnUnselectAll
            // 
            this.btnUnselectAll.BackColor = System.Drawing.Color.LightSalmon;
            this.btnUnselectAll.FlatAppearance.BorderColor = System.Drawing.Color.LightSalmon;
            this.btnUnselectAll.FlatAppearance.BorderSize = 2;
            this.btnUnselectAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PeachPuff;
            this.btnUnselectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUnselectAll.Location = new System.Drawing.Point(786, 113);
            this.btnUnselectAll.Name = "btnUnselectAll";
            this.btnUnselectAll.Size = new System.Drawing.Size(87, 27);
            this.btnUnselectAll.TabIndex = 10;
            this.btnUnselectAll.Text = "Remove all";
            this.btnUnselectAll.UseVisualStyleBackColor = false;
            this.btnUnselectAll.Click += new System.EventHandler(this.btnUnselectAll_Click);
            // 
            // btnRMSD
            // 
            this.btnRMSD.BackColor = System.Drawing.Color.SandyBrown;
            this.btnRMSD.FlatAppearance.BorderColor = System.Drawing.Color.SandyBrown;
            this.btnRMSD.FlatAppearance.BorderSize = 2;
            this.btnRMSD.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PeachPuff;
            this.btnRMSD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRMSD.Location = new System.Drawing.Point(691, 45);
            this.btnRMSD.Name = "btnRMSD";
            this.btnRMSD.Size = new System.Drawing.Size(138, 62);
            this.btnRMSD.TabIndex = 11;
            this.btnRMSD.Text = "Analyse RMSD";
            this.btnRMSD.UseVisualStyleBackColor = false;
            this.btnRMSD.Click += new System.EventHandler(this.btnRMSD_Click);
            // 
            // chkRMSDInitial
            // 
            this.chkRMSDInitial.AutoSize = true;
            this.chkRMSDInitial.Checked = true;
            this.chkRMSDInitial.Location = new System.Drawing.Point(835, 45);
            this.chkRMSDInitial.Name = "chkRMSDInitial";
            this.chkRMSDInitial.Size = new System.Drawing.Size(112, 19);
            this.chkRMSDInitial.TabIndex = 12;
            this.chkRMSDInitial.Text = "Relative to initial";
            this.chkRMSDInitial.UseVisualStyleBackColor = true;
            // 
            // chkRMSDAverage
            // 
            this.chkRMSDAverage.AutoSize = true;
            this.chkRMSDAverage.Location = new System.Drawing.Point(835, 66);
            this.chkRMSDAverage.Name = "chkRMSDAverage";
            this.chkRMSDAverage.Size = new System.Drawing.Size(124, 19);
            this.chkRMSDAverage.TabIndex = 13;
            this.chkRMSDAverage.Text = "Relative to average";
            this.chkRMSDAverage.UseVisualStyleBackColor = true;
            // 
            // chkRMSDMedian
            // 
            this.chkRMSDMedian.AutoSize = true;
            this.chkRMSDMedian.Location = new System.Drawing.Point(835, 88);
            this.chkRMSDMedian.Name = "chkRMSDMedian";
            this.chkRMSDMedian.Size = new System.Drawing.Size(123, 19);
            this.chkRMSDMedian.TabIndex = 14;
            this.chkRMSDMedian.Text = "Relative to median";
            this.chkRMSDMedian.UseVisualStyleBackColor = true;
            // 
            // SelectFeatures
            // 
            this.AcceptButton = this.btnApply;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(982, 452);
            this.Controls.Add(this.chkRMSDMedian);
            this.Controls.Add(this.chkRMSDAverage);
            this.Controls.Add(this.chkRMSDInitial);
            this.Controls.Add(this.btnRMSD);
            this.Controls.Add(this.lblSelectFeatures);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.btnUnselectAll);
            this.Controls.Add(this.imgInitial);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.panelFeatures);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectFeatures";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select or discard features for transformation";
            this.Load += new System.EventHandler(this.SelectFeatures_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panelFeatures;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblSelectFeatures;
        private System.Windows.Forms.Panel imgInitial;
        private System.Windows.Forms.Button btnUnselectAll;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnRMSD;
        private System.Windows.Forms.RadioButton chkRMSDInitial;
        private System.Windows.Forms.RadioButton chkRMSDAverage;
        private System.Windows.Forms.RadioButton chkRMSDMedian;
    }
}