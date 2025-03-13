namespace DeviceTogglerUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            cboDevices = new ComboBox();
            btnFindDevices = new Button();
            btnToggle = new Button();
            SuspendLayout();
            // 
            // cboDevices
            // 
            cboDevices.FormattingEnabled = true;
            cboDevices.Location = new Point(12, 12);
            cboDevices.Name = "cboDevices";
            cboDevices.Size = new Size(356, 28);
            cboDevices.TabIndex = 0;
            cboDevices.SelectedIndexChanged += cboDevices_SelectedIndexChanged;
            cboDevices.KeyDown += cboDevices_KeyDown;
            // 
            // btnFindDevices
            // 
            btnFindDevices.Location = new Point(374, 11);
            btnFindDevices.Name = "btnFindDevices";
            btnFindDevices.Size = new Size(94, 29);
            btnFindDevices.TabIndex = 1;
            btnFindDevices.Text = "Find Devices";
            btnFindDevices.UseVisualStyleBackColor = true;
            btnFindDevices.Click += btnFindDevices_Click;
            // 
            // btnToggle
            // 
            btnToggle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnToggle.BackColor = Color.FromArgb(255, 255, 128);
            btnToggle.Location = new Point(12, 46);
            btnToggle.Name = "btnToggle";
            btnToggle.Size = new Size(456, 79);
            btnToggle.TabIndex = 2;
            btnToggle.Text = "Toggle";
            btnToggle.UseVisualStyleBackColor = false;
            btnToggle.Click += btnToggle_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(480, 139);
            Controls.Add(btnToggle);
            Controls.Add(btnFindDevices);
            Controls.Add(cboDevices);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MZ DeviceToggler";
            ResumeLayout(false);
        }

        #endregion

        private ComboBox cboDevices;
        private Button btnFindDevices;
        private Button btnToggle;
    }
}
