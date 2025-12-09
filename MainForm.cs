using System.IO.Ports;
using System.Text;

namespace UpperComputer
{
    public partial class MainForm : Form
    {
        private SerialPort? serialPort;
        private StringBuilder receiveBuffer = new StringBuilder();
        private DatabaseManager? dbManager;
        private bool autoSaveToDatabase = false;
        private System.Windows.Forms.Timer? fadeTimer;
        private int fadeStep = 0;

        public MainForm()
        {
            InitializeComponent();
            InitializeSerialPort();
            InitializeDatabase();
            this.Opacity = 0; // 启动时透明度为0，用于淡入动画
        }

        // 窗体加载事件 - 淡入动画
        private void MainForm_Load(object sender, EventArgs e)
        {
            fadeTimer = new System.Windows.Forms.Timer();
            fadeTimer.Interval = 20;
            fadeTimer.Tick += FadeIn_Tick;
            fadeTimer.Start();
        }

        // 淡入动画效果
        private void FadeIn_Tick(object? sender, EventArgs e)
        {
            fadeStep++;
            this.Opacity = fadeStep * 0.05;
            if (this.Opacity >= 1.0)
            {
                fadeTimer?.Stop();
                fadeTimer?.Dispose();
            }
        }

        // 按钮悬停效果 - 放大
        private void Button_MouseEnter(object? sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                btn.Font = new Font(btn.Font.FontFamily, btn.Font.Size + 0.5f, btn.Font.Style);
                
                // 根据按钮当前颜色加亮
                Color currentColor = btn.BackColor;
                int r = Math.Min(255, currentColor.R + 20);
                int g = Math.Min(255, currentColor.G + 20);
                int b = Math.Min(255, currentColor.B + 20);
                btn.BackColor = Color.FromArgb(r, g, b);
            }
        }

        // 按钮离开效果 - 还原
        private void Button_MouseLeave(object? sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                btn.Font = new Font(btn.Font.FontFamily, btn.Font.Size - 0.5f, btn.Font.Style);
                
                // 还原原始颜色
                if (btn == btnConnect)
                {
                    // 根据连接状态设置颜色
                    if (serialPort?.IsOpen == true)
                        btn.BackColor = Color.FromArgb(244, 67, 54); // 已连接显示红色（断开按钮）
                    else
                        btn.BackColor = Color.FromArgb(67, 160, 71); // 未连接显示绿色（连接按钮）
                }
                else if (btn == btnRefresh)
                    btn.BackColor = Color.FromArgb(66, 133, 244);
                else if (btn == btnDatabase)
                    btn.BackColor = Color.FromArgb(255, 143, 0);
                else if (btn == btnSend)
                    btn.BackColor = Color.FromArgb(33, 150, 243);
                else if (btn == btnClearReceive || btn == btnClearSend)
                    btn.BackColor = Color.FromArgb(244, 67, 54);
            }
        }

        private void InitializeSerialPort()
        {
            // 初始化串口列表
            RefreshSerialPorts();
            
            // 设置默认值
            comboBaudRate.SelectedIndex = 3; // 9600
            comboDataBits.SelectedIndex = 3; // 8
            comboParity.SelectedIndex = 0;   // None
            comboStopBits.SelectedIndex = 1; // One
        }

        private void InitializeDatabase()
        {
            // 默认数据库配置
            var config = DatabaseConfig.Default;
            dbManager = new DatabaseManager(config);
        }

        private void RefreshSerialPorts()
        {
            comboSerialPort.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            comboSerialPort.Items.AddRange(ports);
            if (ports.Length > 0)
                comboSerialPort.SelectedIndex = 0;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshSerialPorts();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (serialPort?.IsOpen == true)
            {
                // 断开连接
                serialPort.Close();
                btnConnect.Text = "连接";
                btnConnect.BackColor = Color.FromArgb(67, 160, 71); // 绿色
                lblStatus.Text = "状态: 已断开";
                lblStatus.ForeColor = Color.Red;
                
                // 启用串口设置控件
                comboSerialPort.Enabled = true;
                comboBaudRate.Enabled = true;
                comboDataBits.Enabled = true;
                comboParity.Enabled = true;
                comboStopBits.Enabled = true;
                btnRefresh.Enabled = true;
            }
            else
            {
                // 建立连接
                try
                {
                    serialPort = new SerialPort
                    {
                        PortName = comboSerialPort.Text,
                        BaudRate = int.Parse(comboBaudRate.Text),
                        DataBits = int.Parse(comboDataBits.Text),
                        Parity = (Parity)comboParity.SelectedIndex,
                        StopBits = comboStopBits.SelectedIndex == 0 ? StopBits.None :
                                   comboStopBits.SelectedIndex == 1 ? StopBits.One :
                                   comboStopBits.SelectedIndex == 2 ? StopBits.Two : StopBits.OnePointFive
                    };

                    serialPort.DataReceived += SerialPort_DataReceived;
                    serialPort.Open();

                    btnConnect.Text = "断开";
                    btnConnect.BackColor = Color.FromArgb(244, 67, 54); // 红色表示断开
                    lblStatus.Text = "状态: 已连接";
                    lblStatus.ForeColor = Color.Green;
                    
                    // 禁用串口设置控件，但保持连接按钮可用
                    comboSerialPort.Enabled = false;
                    comboBaudRate.Enabled = false;
                    comboDataBits.Enabled = false;
                    comboParity.Enabled = false;
                    comboStopBits.Enabled = false;
                    btnRefresh.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"连接失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (serialPort?.IsOpen == true)
                {
                    string data = serialPort.ReadExisting();
                    this.Invoke(new Action(() =>
                    {
                        txtReceive.AppendText(data);
                        // 自动滚动到底部
                        txtReceive.SelectionStart = txtReceive.Text.Length;
                        txtReceive.ScrollToCaret();

                        // 保存到数据库
                        if (autoSaveToDatabase && dbManager != null && !string.IsNullOrWhiteSpace(data))
                        {
                            dbManager.InsertSerialData(data, serialPort.PortName, out _);
                        }
                    }));
                }
            }
            catch (Exception ex)
            {
                this.Invoke(new Action(() =>
                {
                    MessageBox.Show($"接收数据错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }));
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (serialPort?.IsOpen != true)
            {
                MessageBox.Show("请先连接串口!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string sendData = txtSend.Text;
                if (chkHexSend.Checked)
                {
                    // 十六进制发送
                    string[] hexValues = sendData.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    byte[] bytes = new byte[hexValues.Length];
                    for (int i = 0; i < hexValues.Length; i++)
                    {
                        bytes[i] = Convert.ToByte(hexValues[i], 16);
                    }
                    serialPort.Write(bytes, 0, bytes.Length);
                }
                else
                {
                    // 文本发送
                    if (chkAutoNewLine.Checked)
                        sendData += "\r\n";
                    serialPort.Write(sendData);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发送失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearReceive_Click(object sender, EventArgs e)
        {
            txtReceive.Clear();
        }

        private void btnClearSend_Click(object sender, EventArgs e)
        {
            txtSend.Clear();
        }

        private void btnDatabase_Click(object sender, EventArgs e)
        {
            using var dbForm = new DatabaseForm();
            dbForm.ShowDialog();
            
            // 获取更新后的数据库管理器
            var newDbManager = dbForm.GetDatabaseManager();
            if (newDbManager != null)
            {
                dbManager = newDbManager;
            }
        }

        private void chkAutoSave_CheckedChanged(object sender, EventArgs e)
        {
            autoSaveToDatabase = chkAutoSave.Checked;
            
            if (autoSaveToDatabase && dbManager == null)
            {
                MessageBox.Show("请先配置并测试数据库连接!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                chkAutoSave.Checked = false;
                autoSaveToDatabase = false;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (serialPort?.IsOpen == true)
            {
                serialPort.Close();
            }
            base.OnFormClosing(e);
        }
    }
}
