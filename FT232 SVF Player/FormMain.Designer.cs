namespace FT232_SVF_Player
{
    partial class FormMain
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox_log = new System.Windows.Forms.TextBox();
            this.label_Interface = new System.Windows.Forms.Label();
            this.comboBox_Interfaces = new System.Windows.Forms.ComboBox();
            this.button_rescan = new System.Windows.Forms.Button();
            this.groupBox_boundaryScan = new System.Windows.Forms.GroupBox();
            this.label_Revision = new System.Windows.Forms.Label();
            this.label_Part = new System.Windows.Forms.Label();
            this.label_Manufactor = new System.Windows.Forms.Label();
            this.label_revision_DESCR = new System.Windows.Forms.Label();
            this.label_manufactor_DESCR = new System.Windows.Forms.Label();
            this.label_Part_DESCR = new System.Windows.Forms.Label();
            this.comboBox_devices = new System.Windows.Forms.ComboBox();
            this.label_Device = new System.Windows.Forms.Label();
            this.groupBox_assignment = new System.Windows.Forms.GroupBox();
            this.comboBox_TDO = new System.Windows.Forms.ComboBox();
            this.comboBox_TCK = new System.Windows.Forms.ComboBox();
            this.comboBox_TDI = new System.Windows.Forms.ComboBox();
            this.comboBox_TMS = new System.Windows.Forms.ComboBox();
            this.label_TCK = new System.Windows.Forms.Label();
            this.label_TDO = new System.Windows.Forms.Label();
            this.label_TDI = new System.Windows.Forms.Label();
            this.label_TMS = new System.Windows.Forms.Label();
            this.button_Start = new System.Windows.Forms.Button();
            this.button_Stop = new System.Windows.Forms.Button();
            this.textBox_fileName = new System.Windows.Forms.TextBox();
            this.label_File = new System.Windows.Forms.Label();
            this.button_loadFile = new System.Windows.Forms.Button();
            this.label_About = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label_VerboseLevel = new System.Windows.Forms.Label();
            this.comboBox_verboseLevel = new System.Windows.Forms.ComboBox();
            this.progressBar = new FT232_SVF_Player.CustomProgressBar();
            this.groupBox_boundaryScan.SuspendLayout();
            this.groupBox_assignment.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_log
            // 
            this.textBox_log.Font = new System.Drawing.Font("Courier New", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_log.Location = new System.Drawing.Point(12, 260);
            this.textBox_log.Multiline = true;
            this.textBox_log.Name = "textBox_log";
            this.textBox_log.ReadOnly = true;
            this.textBox_log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_log.Size = new System.Drawing.Size(661, 271);
            this.textBox_log.TabIndex = 0;
            this.textBox_log.VisibleChanged += new System.EventHandler(this.textBox_log_VisibleChanged);
            // 
            // label_Interface
            // 
            this.label_Interface.AutoSize = true;
            this.label_Interface.Location = new System.Drawing.Point(9, 7);
            this.label_Interface.Name = "label_Interface";
            this.label_Interface.Size = new System.Drawing.Size(63, 17);
            this.label_Interface.TabIndex = 2;
            this.label_Interface.Text = "Interface";
            // 
            // comboBox_Interfaces
            // 
            this.comboBox_Interfaces.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Interfaces.FormattingEnabled = true;
            this.comboBox_Interfaces.Location = new System.Drawing.Point(12, 29);
            this.comboBox_Interfaces.Name = "comboBox_Interfaces";
            this.comboBox_Interfaces.Size = new System.Drawing.Size(169, 24);
            this.comboBox_Interfaces.TabIndex = 3;
            this.comboBox_Interfaces.SelectedIndexChanged += new System.EventHandler(this.comboBox_Interfaces_SelectedIndexChanged);
            // 
            // button_rescan
            // 
            this.button_rescan.Location = new System.Drawing.Point(187, 29);
            this.button_rescan.Name = "button_rescan";
            this.button_rescan.Size = new System.Drawing.Size(147, 24);
            this.button_rescan.TabIndex = 4;
            this.button_rescan.Text = "Rescan Interfaces";
            this.button_rescan.UseVisualStyleBackColor = true;
            this.button_rescan.Click += new System.EventHandler(this.button_rescan_Click);
            // 
            // groupBox_boundaryScan
            // 
            this.groupBox_boundaryScan.Controls.Add(this.label_Revision);
            this.groupBox_boundaryScan.Controls.Add(this.label_Part);
            this.groupBox_boundaryScan.Controls.Add(this.label_Manufactor);
            this.groupBox_boundaryScan.Controls.Add(this.label_revision_DESCR);
            this.groupBox_boundaryScan.Controls.Add(this.label_manufactor_DESCR);
            this.groupBox_boundaryScan.Controls.Add(this.label_Part_DESCR);
            this.groupBox_boundaryScan.Controls.Add(this.comboBox_devices);
            this.groupBox_boundaryScan.Controls.Add(this.label_Device);
            this.groupBox_boundaryScan.Location = new System.Drawing.Point(366, 107);
            this.groupBox_boundaryScan.Name = "groupBox_boundaryScan";
            this.groupBox_boundaryScan.Size = new System.Drawing.Size(307, 97);
            this.groupBox_boundaryScan.TabIndex = 5;
            this.groupBox_boundaryScan.TabStop = false;
            this.groupBox_boundaryScan.Text = "Boundary Scan";
            // 
            // label_Revision
            // 
            this.label_Revision.AutoSize = true;
            this.label_Revision.Location = new System.Drawing.Point(241, 44);
            this.label_Revision.Name = "label_Revision";
            this.label_Revision.Size = new System.Drawing.Size(12, 17);
            this.label_Revision.TabIndex = 7;
            this.label_Revision.Text = " ";
            // 
            // label_Part
            // 
            this.label_Part.AutoSize = true;
            this.label_Part.Location = new System.Drawing.Point(241, 19);
            this.label_Part.Name = "label_Part";
            this.label_Part.Size = new System.Drawing.Size(12, 17);
            this.label_Part.TabIndex = 6;
            this.label_Part.Text = " ";
            // 
            // label_Manufactor
            // 
            this.label_Manufactor.AutoSize = true;
            this.label_Manufactor.Location = new System.Drawing.Point(241, 69);
            this.label_Manufactor.Name = "label_Manufactor";
            this.label_Manufactor.Size = new System.Drawing.Size(12, 17);
            this.label_Manufactor.TabIndex = 5;
            this.label_Manufactor.Text = " ";
            // 
            // label_revision_DESCR
            // 
            this.label_revision_DESCR.AutoSize = true;
            this.label_revision_DESCR.Location = new System.Drawing.Point(152, 44);
            this.label_revision_DESCR.Name = "label_revision_DESCR";
            this.label_revision_DESCR.Size = new System.Drawing.Size(66, 17);
            this.label_revision_DESCR.TabIndex = 4;
            this.label_revision_DESCR.Text = "Revision:";
            // 
            // label_manufactor_DESCR
            // 
            this.label_manufactor_DESCR.AutoSize = true;
            this.label_manufactor_DESCR.Location = new System.Drawing.Point(152, 69);
            this.label_manufactor_DESCR.Name = "label_manufactor_DESCR";
            this.label_manufactor_DESCR.Size = new System.Drawing.Size(83, 17);
            this.label_manufactor_DESCR.TabIndex = 3;
            this.label_manufactor_DESCR.Text = "Manufactor:";
            // 
            // label_Part_DESCR
            // 
            this.label_Part_DESCR.AutoSize = true;
            this.label_Part_DESCR.Location = new System.Drawing.Point(152, 19);
            this.label_Part_DESCR.Name = "label_Part_DESCR";
            this.label_Part_DESCR.Size = new System.Drawing.Size(38, 17);
            this.label_Part_DESCR.TabIndex = 2;
            this.label_Part_DESCR.Text = "Part:";
            // 
            // comboBox_devices
            // 
            this.comboBox_devices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_devices.FormattingEnabled = true;
            this.comboBox_devices.Location = new System.Drawing.Point(6, 44);
            this.comboBox_devices.Name = "comboBox_devices";
            this.comboBox_devices.Size = new System.Drawing.Size(127, 24);
            this.comboBox_devices.TabIndex = 1;
            this.comboBox_devices.SelectedIndexChanged += new System.EventHandler(this.comboBox_devices_SelectedIndexChanged);
            // 
            // label_Device
            // 
            this.label_Device.AutoSize = true;
            this.label_Device.Location = new System.Drawing.Point(6, 24);
            this.label_Device.Name = "label_Device";
            this.label_Device.Size = new System.Drawing.Size(51, 17);
            this.label_Device.TabIndex = 0;
            this.label_Device.Text = "Device";
            // 
            // groupBox_assignment
            // 
            this.groupBox_assignment.Controls.Add(this.comboBox_TDO);
            this.groupBox_assignment.Controls.Add(this.comboBox_TCK);
            this.groupBox_assignment.Controls.Add(this.comboBox_TDI);
            this.groupBox_assignment.Controls.Add(this.comboBox_TMS);
            this.groupBox_assignment.Controls.Add(this.label_TCK);
            this.groupBox_assignment.Controls.Add(this.label_TDO);
            this.groupBox_assignment.Controls.Add(this.label_TDI);
            this.groupBox_assignment.Controls.Add(this.label_TMS);
            this.groupBox_assignment.Location = new System.Drawing.Point(15, 104);
            this.groupBox_assignment.Name = "groupBox_assignment";
            this.groupBox_assignment.Size = new System.Drawing.Size(342, 100);
            this.groupBox_assignment.TabIndex = 6;
            this.groupBox_assignment.TabStop = false;
            this.groupBox_assignment.Text = "Pin Assignment";
            // 
            // comboBox_TDO
            // 
            this.comboBox_TDO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_TDO.FormattingEnabled = true;
            this.comboBox_TDO.Location = new System.Drawing.Point(221, 24);
            this.comboBox_TDO.Name = "comboBox_TDO";
            this.comboBox_TDO.Size = new System.Drawing.Size(84, 24);
            this.comboBox_TDO.TabIndex = 7;
            this.comboBox_TDO.SelectedIndexChanged += new System.EventHandler(this.comboBox_TDO_SelectedIndexChanged);
            // 
            // comboBox_TCK
            // 
            this.comboBox_TCK.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_TCK.FormattingEnabled = true;
            this.comboBox_TCK.Location = new System.Drawing.Point(221, 57);
            this.comboBox_TCK.Name = "comboBox_TCK";
            this.comboBox_TCK.Size = new System.Drawing.Size(84, 24);
            this.comboBox_TCK.TabIndex = 6;
            this.comboBox_TCK.SelectedIndexChanged += new System.EventHandler(this.comboBox_TCK_SelectedIndexChanged);
            // 
            // comboBox_TDI
            // 
            this.comboBox_TDI.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_TDI.FormattingEnabled = true;
            this.comboBox_TDI.Location = new System.Drawing.Point(58, 61);
            this.comboBox_TDI.Name = "comboBox_TDI";
            this.comboBox_TDI.Size = new System.Drawing.Size(84, 24);
            this.comboBox_TDI.TabIndex = 5;
            this.comboBox_TDI.SelectedIndexChanged += new System.EventHandler(this.comboBox_TDI_SelectedIndexChanged);
            // 
            // comboBox_TMS
            // 
            this.comboBox_TMS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_TMS.FormattingEnabled = true;
            this.comboBox_TMS.Location = new System.Drawing.Point(58, 26);
            this.comboBox_TMS.Name = "comboBox_TMS";
            this.comboBox_TMS.Size = new System.Drawing.Size(84, 24);
            this.comboBox_TMS.TabIndex = 4;
            this.comboBox_TMS.SelectedIndexChanged += new System.EventHandler(this.comboBox_TMS_SelectedIndexChanged);
            // 
            // label_TCK
            // 
            this.label_TCK.AutoSize = true;
            this.label_TCK.Location = new System.Drawing.Point(180, 64);
            this.label_TCK.Name = "label_TCK";
            this.label_TCK.Size = new System.Drawing.Size(35, 17);
            this.label_TCK.TabIndex = 3;
            this.label_TCK.Text = "TCK";
            // 
            // label_TDO
            // 
            this.label_TDO.AutoSize = true;
            this.label_TDO.Location = new System.Drawing.Point(177, 29);
            this.label_TDO.Name = "label_TDO";
            this.label_TDO.Size = new System.Drawing.Size(38, 17);
            this.label_TDO.TabIndex = 2;
            this.label_TDO.Text = "TDO";
            // 
            // label_TDI
            // 
            this.label_TDI.AutoSize = true;
            this.label_TDI.Location = new System.Drawing.Point(17, 64);
            this.label_TDI.Name = "label_TDI";
            this.label_TDI.Size = new System.Drawing.Size(30, 17);
            this.label_TDI.TabIndex = 1;
            this.label_TDI.Text = "TDI";
            // 
            // label_TMS
            // 
            this.label_TMS.AutoSize = true;
            this.label_TMS.Location = new System.Drawing.Point(15, 29);
            this.label_TMS.Name = "label_TMS";
            this.label_TMS.Size = new System.Drawing.Size(37, 17);
            this.label_TMS.TabIndex = 0;
            this.label_TMS.Text = "TMS";
            // 
            // button_Start
            // 
            this.button_Start.Location = new System.Drawing.Point(255, 211);
            this.button_Start.Name = "button_Start";
            this.button_Start.Size = new System.Drawing.Size(101, 43);
            this.button_Start.TabIndex = 7;
            this.button_Start.Text = "&Start";
            this.button_Start.UseVisualStyleBackColor = true;
            this.button_Start.Click += new System.EventHandler(this.button_Start_Click);
            // 
            // button_Stop
            // 
            this.button_Stop.Location = new System.Drawing.Point(366, 211);
            this.button_Stop.Name = "button_Stop";
            this.button_Stop.Size = new System.Drawing.Size(101, 43);
            this.button_Stop.TabIndex = 8;
            this.button_Stop.Text = "S&top";
            this.button_Stop.UseVisualStyleBackColor = true;
            this.button_Stop.Click += new System.EventHandler(this.button_Stop_Click);
            // 
            // textBox_fileName
            // 
            this.textBox_fileName.Location = new System.Drawing.Point(15, 76);
            this.textBox_fileName.Name = "textBox_fileName";
            this.textBox_fileName.Size = new System.Drawing.Size(609, 22);
            this.textBox_fileName.TabIndex = 9;
            // 
            // label_File
            // 
            this.label_File.AutoSize = true;
            this.label_File.Location = new System.Drawing.Point(12, 56);
            this.label_File.Name = "label_File";
            this.label_File.Size = new System.Drawing.Size(79, 17);
            this.label_File.TabIndex = 10;
            this.label_File.Text = "(X)SVF File";
            // 
            // button_loadFile
            // 
            this.button_loadFile.Location = new System.Drawing.Point(630, 75);
            this.button_loadFile.Name = "button_loadFile";
            this.button_loadFile.Size = new System.Drawing.Size(43, 25);
            this.button_loadFile.TabIndex = 11;
            this.button_loadFile.Text = "...";
            this.button_loadFile.UseVisualStyleBackColor = true;
            this.button_loadFile.Click += new System.EventHandler(this.button_loadFile_Click);
            // 
            // label_About
            // 
            this.label_About.AutoSize = true;
            this.label_About.Location = new System.Drawing.Point(421, 32);
            this.label_About.Name = "label_About";
            this.label_About.Size = new System.Drawing.Size(245, 17);
            this.label_About.TabIndex = 12;
            this.label_About.Text = "FT232 (X)SVF Player © 2017 by Nelix";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 571);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(598, 17);
            this.label2.TabIndex = 14;
            this.label2.Text = "Based on Lib(X)SVF: A library for implementing SVF and XSVF JTAG players by Cliff" +
    "ord Wolf. ";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(9, 588);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(179, 17);
            this.linkLabel1.TabIndex = 15;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "http://www.clifford.at/libxsvf/";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label_VerboseLevel
            // 
            this.label_VerboseLevel.AutoSize = true;
            this.label_VerboseLevel.Location = new System.Drawing.Point(12, 224);
            this.label_VerboseLevel.Name = "label_VerboseLevel";
            this.label_VerboseLevel.Size = new System.Drawing.Size(103, 17);
            this.label_VerboseLevel.TabIndex = 16;
            this.label_VerboseLevel.Text = "Verbose Level:";
            // 
            // comboBox_verboseLevel
            // 
            this.comboBox_verboseLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_verboseLevel.FormattingEnabled = true;
            this.comboBox_verboseLevel.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.comboBox_verboseLevel.Location = new System.Drawing.Point(121, 221);
            this.comboBox_verboseLevel.Name = "comboBox_verboseLevel";
            this.comboBox_verboseLevel.Size = new System.Drawing.Size(50, 24);
            this.comboBox_verboseLevel.TabIndex = 17;
            this.comboBox_verboseLevel.SelectedIndexChanged += new System.EventHandler(this.comboBox_verboseLevel_SelectedIndexChanged);
            // 
            // progressBar
            // 
            this.progressBar.CustomText = " ";
            this.progressBar.DisplayStyle = FT232_SVF_Player.ProgressBarDisplayText.Percentage;
            this.progressBar.Location = new System.Drawing.Point(12, 537);
            this.progressBar.Maximum = 1000;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(666, 31);
            this.progressBar.TabIndex = 1;
            this.progressBar.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 614);
            this.Controls.Add(this.comboBox_verboseLevel);
            this.Controls.Add(this.label_VerboseLevel);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label_About);
            this.Controls.Add(this.button_loadFile);
            this.Controls.Add(this.label_File);
            this.Controls.Add(this.textBox_fileName);
            this.Controls.Add(this.button_Stop);
            this.Controls.Add(this.button_Start);
            this.Controls.Add(this.groupBox_assignment);
            this.Controls.Add(this.groupBox_boundaryScan);
            this.Controls.Add(this.button_rescan);
            this.Controls.Add(this.comboBox_Interfaces);
            this.Controls.Add(this.label_Interface);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.textBox_log);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "FT232 (X)SVF Player";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.groupBox_boundaryScan.ResumeLayout(false);
            this.groupBox_boundaryScan.PerformLayout();
            this.groupBox_assignment.ResumeLayout(false);
            this.groupBox_assignment.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_log;
        private CustomProgressBar progressBar;
        private System.Windows.Forms.Label label_Interface;
        private System.Windows.Forms.ComboBox comboBox_Interfaces;
        private System.Windows.Forms.Button button_rescan;
        private System.Windows.Forms.GroupBox groupBox_boundaryScan;
        private System.Windows.Forms.Label label_revision_DESCR;
        private System.Windows.Forms.Label label_manufactor_DESCR;
        private System.Windows.Forms.Label label_Part_DESCR;
        private System.Windows.Forms.ComboBox comboBox_devices;
        private System.Windows.Forms.Label label_Device;
        private System.Windows.Forms.Label label_Revision;
        private System.Windows.Forms.Label label_Part;
        private System.Windows.Forms.Label label_Manufactor;
        private System.Windows.Forms.GroupBox groupBox_assignment;
        private System.Windows.Forms.ComboBox comboBox_TMS;
        private System.Windows.Forms.Label label_TCK;
        private System.Windows.Forms.Label label_TDO;
        private System.Windows.Forms.Label label_TDI;
        private System.Windows.Forms.Label label_TMS;
        private System.Windows.Forms.ComboBox comboBox_TDO;
        private System.Windows.Forms.ComboBox comboBox_TCK;
        private System.Windows.Forms.ComboBox comboBox_TDI;
        private System.Windows.Forms.Button button_Start;
        private System.Windows.Forms.Button button_Stop;
        private System.Windows.Forms.TextBox textBox_fileName;
        private System.Windows.Forms.Label label_File;
        private System.Windows.Forms.Button button_loadFile;
        private System.Windows.Forms.Label label_About;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label_VerboseLevel;
        private System.Windows.Forms.ComboBox comboBox_verboseLevel;
    }
}

