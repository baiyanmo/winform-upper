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

        public MainForm()
        {
            InitializeComponent();
            InitializeSerialPort();
            InitializeDatabase();
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
                lblStatus.Text = "状态: 已断开";
                lblStatus.ForeColor = Color.Red;
                groupBoxSerialSettings.Enabled = true;
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
                    lblStatus.Text = "状态: 已连接";
                    lblStatus.ForeColor = Color.Green;
                    groupBoxSerialSettings.Enabled = false;
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
