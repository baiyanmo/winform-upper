namespace UpperComputer
{
    partial class MainForm
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
            this.groupBoxSerialSettings = new System.Windows.Forms.GroupBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.comboStopBits = new System.Windows.Forms.ComboBox();
            this.comboParity = new System.Windows.Forms.ComboBox();
            this.comboDataBits = new System.Windows.Forms.ComboBox();
            this.comboBaudRate = new System.Windows.Forms.ComboBox();
            this.comboSerialPort = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxReceive = new System.Windows.Forms.GroupBox();
            this.btnClearReceive = new System.Windows.Forms.Button();
            this.txtReceive = new System.Windows.Forms.TextBox();
            this.groupBoxSend = new System.Windows.Forms.GroupBox();
            this.chkAutoNewLine = new System.Windows.Forms.CheckBox();
            this.chkHexSend = new System.Windows.Forms.CheckBox();
            this.btnClearSend = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtSend = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.groupBoxSerialSettings.SuspendLayout();
            this.groupBoxReceive.SuspendLayout();
            this.groupBoxSend.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxSerialSettings
            // 
            this.groupBoxSerialSettings.Controls.Add(this.btnRefresh);
            this.groupBoxSerialSettings.Controls.Add(this.btnConnect);
            this.groupBoxSerialSettings.Controls.Add(this.comboStopBits);
            this.groupBoxSerialSettings.Controls.Add(this.comboParity);
            this.groupBoxSerialSettings.Controls.Add(this.comboDataBits);
            this.groupBoxSerialSettings.Controls.Add(this.comboBaudRate);
            this.groupBoxSerialSettings.Controls.Add(this.comboSerialPort);
            this.groupBoxSerialSettings.Controls.Add(this.label5);
            this.groupBoxSerialSettings.Controls.Add(this.label4);
            this.groupBoxSerialSettings.Controls.Add(this.label3);
            this.groupBoxSerialSettings.Controls.Add(this.label2);
            this.groupBoxSerialSettings.Controls.Add(this.label1);
            this.groupBoxSerialSettings.Location = new System.Drawing.Point(12, 12);
            this.groupBoxSerialSettings.Name = "groupBoxSerialSettings";
            this.groupBoxSerialSettings.Size = new System.Drawing.Size(200, 280);
            this.groupBoxSerialSettings.TabIndex = 0;
            this.groupBoxSerialSettings.TabStop = false;
            this.groupBoxSerialSettings.Text = "串口设置";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(110, 210);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 30);
            this.btnRefresh.TabIndex = 11;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(15, 210);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 30);
            this.btnConnect.TabIndex = 10;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // comboStopBits
            // 
            this.comboStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboStopBits.FormattingEnabled = true;
            this.comboStopBits.Items.AddRange(new object[] {
            "None",
            "One",
            "Two",
            "OnePointFive"});
            this.comboStopBits.Location = new System.Drawing.Point(80, 170);
            this.comboStopBits.Name = "comboStopBits";
            this.comboStopBits.Size = new System.Drawing.Size(105, 25);
            this.comboStopBits.TabIndex = 9;
            // 
            // comboParity
            // 
            this.comboParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboParity.FormattingEnabled = true;
            this.comboParity.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even",
            "Mark",
            "Space"});
            this.comboParity.Location = new System.Drawing.Point(80, 135);
            this.comboParity.Name = "comboParity";
            this.comboParity.Size = new System.Drawing.Size(105, 25);
            this.comboParity.TabIndex = 8;
            // 
            // comboDataBits
            // 
            this.comboDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboDataBits.FormattingEnabled = true;
            this.comboDataBits.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8"});
            this.comboDataBits.Location = new System.Drawing.Point(80, 100);
            this.comboDataBits.Name = "comboDataBits";
            this.comboDataBits.Size = new System.Drawing.Size(105, 25);
            this.comboDataBits.TabIndex = 7;
            // 
            // comboBaudRate
            // 
            this.comboBaudRate.FormattingEnabled = true;
            this.comboBaudRate.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.comboBaudRate.Location = new System.Drawing.Point(80, 65);
            this.comboBaudRate.Name = "comboBaudRate";
            this.comboBaudRate.Size = new System.Drawing.Size(105, 25);
            this.comboBaudRate.TabIndex = 6;
            // 
            // comboSerialPort
            // 
            this.comboSerialPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSerialPort.FormattingEnabled = true;
            this.comboSerialPort.Location = new System.Drawing.Point(80, 30);
            this.comboSerialPort.Name = "comboSerialPort";
            this.comboSerialPort.Size = new System.Drawing.Size(105, 25);
            this.comboSerialPort.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 173);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "停止位:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "校验位:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "数据位:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "波特率:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "串口:";
            // 
            // groupBoxReceive
            // 
            this.groupBoxReceive.Controls.Add(this.btnClearReceive);
            this.groupBoxReceive.Controls.Add(this.txtReceive);
            this.groupBoxReceive.Location = new System.Drawing.Point(220, 12);
            this.groupBoxReceive.Name = "groupBoxReceive";
            this.groupBoxReceive.Size = new System.Drawing.Size(600, 280);
            this.groupBoxReceive.TabIndex = 1;
            this.groupBoxReceive.TabStop = false;
            this.groupBoxReceive.Text = "接收区";
            // 
            // btnClearReceive
            // 
            this.btnClearReceive.Location = new System.Drawing.Point(510, 240);
            this.btnClearReceive.Name = "btnClearReceive";
            this.btnClearReceive.Size = new System.Drawing.Size(75, 30);
            this.btnClearReceive.TabIndex = 1;
            this.btnClearReceive.Text = "清空";
            this.btnClearReceive.UseVisualStyleBackColor = true;
            this.btnClearReceive.Click += new System.EventHandler(this.btnClearReceive_Click);
            // 
            // txtReceive
            // 
            this.txtReceive.Location = new System.Drawing.Point(15, 25);
            this.txtReceive.Multiline = true;
            this.txtReceive.Name = "txtReceive";
            this.txtReceive.ReadOnly = true;
            this.txtReceive.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtReceive.Size = new System.Drawing.Size(570, 200);
            this.txtReceive.TabIndex = 0;
            // 
            // groupBoxSend
            // 
            this.groupBoxSend.Controls.Add(this.chkAutoNewLine);
            this.groupBoxSend.Controls.Add(this.chkHexSend);
            this.groupBoxSend.Controls.Add(this.btnClearSend);
            this.groupBoxSend.Controls.Add(this.btnSend);
            this.groupBoxSend.Controls.Add(this.txtSend);
            this.groupBoxSend.Location = new System.Drawing.Point(220, 300);
            this.groupBoxSend.Name = "groupBoxSend";
            this.groupBoxSend.Size = new System.Drawing.Size(600, 180);
            this.groupBoxSend.TabIndex = 2;
            this.groupBoxSend.TabStop = false;
            this.groupBoxSend.Text = "发送区";
            // 
            // chkAutoNewLine
            // 
            this.chkAutoNewLine.AutoSize = true;
            this.chkAutoNewLine.Location = new System.Drawing.Point(130, 145);
            this.chkAutoNewLine.Name = "chkAutoNewLine";
            this.chkAutoNewLine.Size = new System.Drawing.Size(87, 21);
            this.chkAutoNewLine.TabIndex = 4;
            this.chkAutoNewLine.Text = "自动换行";
            this.chkAutoNewLine.UseVisualStyleBackColor = true;
            // 
            // chkHexSend
            // 
            this.chkHexSend.AutoSize = true;
            this.chkHexSend.Location = new System.Drawing.Point(15, 145);
            this.chkHexSend.Name = "chkHexSend";
            this.chkHexSend.Size = new System.Drawing.Size(99, 21);
            this.chkHexSend.TabIndex = 3;
            this.chkHexSend.Text = "十六进制发送";
            this.chkHexSend.UseVisualStyleBackColor = true;
            // 
            // btnClearSend
            // 
            this.btnClearSend.Location = new System.Drawing.Point(510, 140);
            this.btnClearSend.Name = "btnClearSend";
            this.btnClearSend.Size = new System.Drawing.Size(75, 30);
            this.btnClearSend.TabIndex = 2;
            this.btnClearSend.Text = "清空";
            this.btnClearSend.UseVisualStyleBackColor = true;
            this.btnClearSend.Click += new System.EventHandler(this.btnClearSend_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(410, 140);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 30);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtSend
            // 
            this.txtSend.Location = new System.Drawing.Point(15, 25);
            this.txtSend.Multiline = true;
            this.txtSend.Name = "txtSend";
            this.txtSend.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSend.Size = new System.Drawing.Size(570, 100);
            this.txtSend.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStatus.ForeColor = System.Drawing.Color.Red;
            this.lblStatus.Location = new System.Drawing.Point(12, 310);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(102, 19);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "状态: 未连接";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 491);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.groupBoxSend);
            this.Controls.Add(this.groupBoxReceive);
            this.Controls.Add(this.groupBoxSerialSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "串口上位机";
            this.groupBoxSerialSettings.ResumeLayout(false);
            this.groupBoxSerialSettings.PerformLayout();
            this.groupBoxReceive.ResumeLayout(false);
            this.groupBoxReceive.ResumeLayout(false);
            this.groupBoxReceive.PerformLayout();
            this.groupBoxSend.ResumeLayout(false);
            this.groupBoxSend.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GroupBox groupBoxSerialSettings;
        private ComboBox comboSerialPort;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private ComboBox comboStopBits;
        private ComboBox comboParity;
        private ComboBox comboDataBits;
        private ComboBox comboBaudRate;
        private Button btnConnect;
        private Button btnRefresh;
        private GroupBox groupBoxReceive;
        private TextBox txtReceive;
        private Button btnClearReceive;
        private GroupBox groupBoxSend;
        private TextBox txtSend;
        private Button btnSend;
        private Button btnClearSend;
        private CheckBox chkHexSend;
        private CheckBox chkAutoNewLine;
        private Label lblStatus;
    }
}
