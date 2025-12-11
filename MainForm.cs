using System.IO.Ports;
using System.Text;

namespace UpperComputer
{
    /// <summary>
    /// 串口上位机主窗体类
    /// 实现串口通信、数据收发、数据库存储等功能
    /// </summary>
    public partial class MainForm : Form
    {
        #region 字段定义
        
        /// <summary>
        /// 串口对象,用于串口通信
        /// </summary>
        private SerialPort? serialPort;
        
        /// <summary>
        /// 接收数据缓冲区(预留,当前未使用)
        /// </summary>
        private StringBuilder receiveBuffer = new StringBuilder();
        
        /// <summary>
        /// 数据库管理器,负责数据的存储和查询
        /// </summary>
        private DatabaseManager? dbManager;
        
        /// <summary>
        /// 是否自动保存接收到的数据到数据库
        /// </summary>
        private bool autoSaveToDatabase = false;
        
        /// <summary>
        /// 窗体淡入动画定时器
        /// </summary>
        private System.Windows.Forms.Timer? fadeTimer;
        
        /// <summary>
        /// 淡入动画步骤计数器
        /// </summary>
        private int fadeStep = 0;
        
        #endregion

        #region 构造函数和初始化

        /// <summary>
        /// 构造函数:初始化主窗体
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            InitializeSerialPort();  // 初始化串口配置
            InitializeDatabase();    // 初始化数据库
            this.Opacity = 0;        // 设置初始透明度为0,用于淡入动画
        }

        /// <summary>
        /// 窗体加载事件:启动淡入动画
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            fadeTimer = new System.Windows.Forms.Timer();
            fadeTimer.Interval = 20;           // 每20毫秒触发一次
            fadeTimer.Tick += FadeIn_Tick;     // 绑定定时器事件
            fadeTimer.Start();                 // 启动定时器
        }

        /// <summary>
        /// 淡入动画定时器事件:逐步增加窗体透明度
        /// </summary>
        private void FadeIn_Tick(object? sender, EventArgs e)
        {
            fadeStep++;
            this.Opacity = fadeStep * 0.05;  // 每次增加5%透明度
            
            // 完全显示时停止定时器
            if (this.Opacity >= 1.0)
            {
                fadeTimer?.Stop();
                fadeTimer?.Dispose();
            }
        }

        #endregion

        #region 按钮悬停效果

        /// <summary>
        /// 鼠标进入按钮时:放大字体并加亮颜色
        /// </summary>
        private void Button_MouseEnter(object? sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                // 放大字体
                btn.Font = new Font(btn.Font.FontFamily, btn.Font.Size + 0.5f, btn.Font.Style);
                
                // 加亮按钮颜色
                Color currentColor = btn.BackColor;
                int r = Math.Min(255, currentColor.R + 20);
                int g = Math.Min(255, currentColor.G + 20);
                int b = Math.Min(255, currentColor.B + 20);
                btn.BackColor = Color.FromArgb(r, g, b);
            }
        }

        /// <summary>
        /// 鼠标离开按钮时:还原字体和颜色
        /// </summary>
        private void Button_MouseLeave(object? sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                // 还原字体大小
                btn.Font = new Font(btn.Font.FontFamily, btn.Font.Size - 0.5f, btn.Font.Style);
                
                // 还原原始颜色
                if (btn == btnConnect)
                {
                    // 根据连接状态设置颜色
                    if (serialPort?.IsOpen == true)
                        btn.BackColor = Color.FromArgb(244, 67, 54); // 已连接显示红色(断开按钮)
                    else
                        btn.BackColor = Color.FromArgb(67, 160, 71); // 未连接显示绿色(连接按钮)
                }
                else if (btn == btnRefresh)
                    btn.BackColor = Color.FromArgb(66, 133, 244);    // 蓝色
                else if (btn == btnDatabase)
                    btn.BackColor = Color.FromArgb(255, 143, 0);     // 橙色
                else if (btn == btnSend)
                    btn.BackColor = Color.FromArgb(33, 150, 243);    // 浅蓝色
                else if (btn == btnClearReceive || btn == btnClearSend)
                    btn.BackColor = Color.FromArgb(244, 67, 54);     // 红色
            }
        }

        #endregion

        #region 串口和数据库初始化

        /// <summary>
        /// 初始化串口配置:刷新串口列表并设置默认参数
        /// </summary>
        private void InitializeSerialPort()
        {
            // 刷新可用串口列表
            RefreshSerialPorts();
            
            // 设置默认参数
            comboBaudRate.SelectedIndex = 3; // 默认波特率: 9600
            comboDataBits.SelectedIndex = 3; // 默认数据位: 8
            comboParity.SelectedIndex = 0;   // 默认校验位: None
            comboStopBits.SelectedIndex = 1; // 默认停止位: One
        }

        /// <summary>
        /// 初始化数据库:使用默认配置创建数据库管理器
        /// </summary>
        private void InitializeDatabase()
        {
            var config = DatabaseConfig.Default;
            dbManager = new DatabaseManager(config);
        }

        /// <summary>
        /// 刷新串口列表:扫描系统可用串口并填充到下拉框
        /// </summary>
        private void RefreshSerialPorts()
        {
            comboSerialPort.Items.Clear();
            string[] ports = SerialPort.GetPortNames();  // 获取所有可用串口
            comboSerialPort.Items.AddRange(ports);
            
            // 如果有可用串口,默认选择第一个
            if (ports.Length > 0)
                comboSerialPort.SelectedIndex = 0;
        }

        #endregion

        #region 按钮点击事件

        #endregion

        #region 按钮点击事件

        /// <summary>
        /// 刷新按钮点击事件:重新扫描系统可用串口
        /// </summary>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshSerialPorts();
        }

        /// <summary>
        /// 连接/断开按钮点击事件:建立或断开串口连接
        /// </summary>
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (serialPort?.IsOpen == true)
            {
                // ===== 断开连接 =====
                serialPort.Close();
                btnConnect.Text = "连接";
                btnConnect.BackColor = Color.FromArgb(67, 160, 71); // 绿色
                lblStatus.Text = "状态: 已断开";
                lblStatus.ForeColor = Color.Red;
                
                // 启用串口设置控件,允许重新配置
                comboSerialPort.Enabled = true;
                comboBaudRate.Enabled = true;
                comboDataBits.Enabled = true;
                comboParity.Enabled = true;
                comboStopBits.Enabled = true;
                btnRefresh.Enabled = true;
            }
            else
            {
                // ===== 建立连接 =====
                try
                {
                    // 创建串口对象并配置参数
                    serialPort = new SerialPort
                    {
                        PortName = comboSerialPort.Text,                          // 串口号
                        BaudRate = int.Parse(comboBaudRate.Text),                 // 波特率
                        DataBits = int.Parse(comboDataBits.Text),                 // 数据位
                        Parity = (Parity)comboParity.SelectedIndex,               // 校验位
                        StopBits = comboStopBits.SelectedIndex == 0 ? StopBits.None :
                                   comboStopBits.SelectedIndex == 1 ? StopBits.One :
                                   comboStopBits.SelectedIndex == 2 ? StopBits.Two : StopBits.OnePointFive  // 停止位
                    };

                    // 注册数据接收事件
                    serialPort.DataReceived += SerialPort_DataReceived;
                    
                    // 打开串口
                    serialPort.Open();

                    // 更新UI状态
                    btnConnect.Text = "断开";
                    btnConnect.BackColor = Color.FromArgb(244, 67, 54); // 红色表示断开
                    lblStatus.Text = "状态: 已连接";
                    lblStatus.ForeColor = Color.Green;
                    
                    // 禁用串口设置控件,防止连接中修改参数
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

        /// <summary>
        /// 发送按钮点击事件:通过串口发送数据
        /// 支持文本模式和十六进制模式
        /// </summary>
        private void btnSend_Click(object sender, EventArgs e)
        {
            // 检查串口是否已打开
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
                    // ===== 十六进制发送模式 =====
                    // 格式: "AA BB CC" → 字节数组 [0xAA, 0xBB, 0xCC]
                    string[] hexValues = sendData.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    byte[] bytes = new byte[hexValues.Length];
                    
                    for (int i = 0; i < hexValues.Length; i++)
                    {
                        bytes[i] = Convert.ToByte(hexValues[i], 16);  // 将十六进制字符串转为字节
                    }
                    
                    serialPort.Write(bytes, 0, bytes.Length);
                }
                else
                {
                    // ===== 文本发送模式 =====
                    if (chkAutoNewLine.Checked)
                        sendData += "\r\n";  // 自动添加换行符
                    
                    serialPort.Write(sendData);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发送失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 清空接收区按钮点击事件:清除接收文本框内容
        /// </summary>
        private void btnClearReceive_Click(object sender, EventArgs e)
        {
            txtReceive.Clear();
        }

        /// <summary>
        /// 清空发送区按钮点击事件:清除发送文本框内容
        /// </summary>
        private void btnClearSend_Click(object sender, EventArgs e)
        {
            txtSend.Clear();
        }

        /// <summary>
        /// 数据库管理按钮点击事件:打开数据库配置窗口
        /// </summary>
        private void btnDatabase_Click(object sender, EventArgs e)
        {
            using var dbForm = new DatabaseForm();
            dbForm.ShowDialog();  // 模态对话框
            
            // 获取更新后的数据库管理器
            var newDbManager = dbForm.GetDatabaseManager();
            if (newDbManager != null)
            {
                dbManager = newDbManager;
            }
        }

        #endregion

        #region 串口数据接收

        /// <summary>
        /// 串口数据接收事件:当串口收到数据时触发
        /// 将数据显示到接收区,并可选择保存到数据库
        /// </summary>
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (serialPort?.IsOpen == true)
                {
                    // 读取串口接收到的所有数据
                    string data = serialPort.ReadExisting();
                    
                    // 使用Invoke确保在UI线程中更新界面
                    this.Invoke(new Action(() =>
                    {
                        // 显示接收的数据
                        txtReceive.AppendText(data);
                        
                        // 自动滚动到底部,显示最新数据
                        txtReceive.SelectionStart = txtReceive.Text.Length;
                        txtReceive.ScrollToCaret();

                        // 如果启用了自动保存,将数据存储到数据库
                        if (autoSaveToDatabase && dbManager != null && !string.IsNullOrWhiteSpace(data))
                        {
                            dbManager.InsertSerialData(data, serialPort.PortName, out _);
                        }
                    }));
                }
            }
            catch (Exception ex)
            {
                // 在UI线程中显示错误消息
                this.Invoke(new Action(() =>
                {
                    MessageBox.Show($"接收数据错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }));
            }
        }

        #endregion

        #region 复选框事件

        /// <summary>
        /// 自动保存复选框状态改变事件:启用或禁用自动保存到数据库功能
        /// </summary>
        private void chkAutoSave_CheckedChanged(object sender, EventArgs e)
        {
            autoSaveToDatabase = chkAutoSave.Checked;
            
            // 如果启用自动保存但数据库未配置,提示用户
            if (autoSaveToDatabase && dbManager == null)
            {
                MessageBox.Show("请先配置并测试数据库连接!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                chkAutoSave.Checked = false;
                autoSaveToDatabase = false;
            }
        }

        #endregion

        #region 窗体关闭事件

        /// <summary>
        /// 窗体关闭事件:关闭串口连接,释放资源
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // 如果串口处于打开状态,先关闭
            if (serialPort?.IsOpen == true)
            {
                serialPort.Close();
            }
            
            base.OnFormClosing(e);
        }

        #endregion
    }
}


