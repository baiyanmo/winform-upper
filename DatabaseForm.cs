using System.Data;

namespace UpperComputer
{
    public partial class DatabaseForm : Form
    {
        private DatabaseManager? dbManager;
        private DatabaseConfig config;
        private System.Windows.Forms.Timer? fadeTimer;
        private int fadeStep = 0;

        public DatabaseForm()
        {
            InitializeComponent();
            config = DatabaseConfig.Default;
            LoadConfig();
            this.Opacity = 0; // 启动时透明度为0
        }

        // 窗体加载事件 - 淡入动画
        private void DatabaseForm_Load(object sender, EventArgs e)
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

        // 按钮悬停效果
        private void Button_MouseEnter(object? sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                btn.Font = new Font(btn.Font.FontFamily, btn.Font.Size + 0.5f, btn.Font.Style);
                
                Color currentColor = btn.BackColor;
                int r = Math.Min(255, currentColor.R + 20);
                int g = Math.Min(255, currentColor.G + 20);
                int b = Math.Min(255, currentColor.B + 20);
                btn.BackColor = Color.FromArgb(r, g, b);
            }
        }

        // 按钮离开效果
        private void Button_MouseLeave(object? sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                btn.Font = new Font(btn.Font.FontFamily, btn.Font.Size - 0.5f, btn.Font.Style);
                
                // 还原原始颜色
                if (btn == btnTestConnection)
                    btn.BackColor = Color.FromArgb(67, 160, 71);
                else if (btn == btnInitDatabase)
                    btn.BackColor = Color.FromArgb(33, 150, 243);
                else if (btn == btnQueryData)
                    btn.BackColor = Color.FromArgb(66, 133, 244);
                else if (btn == btnStatistics)
                    btn.BackColor = Color.FromArgb(156, 39, 176);
                else if (btn == btnClearData)
                    btn.BackColor = Color.FromArgb(244, 67, 54);
                else if (btn == btnExport)
                    btn.BackColor = Color.FromArgb(255, 143, 0);
            }
        }

        private void LoadConfig()
        {
            txtServer.Text = config.Server;
            txtPort.Text = config.Port;
            txtDatabase.Text = config.Database;
            txtUsername.Text = config.Username;
            txtPassword.Text = config.Password;
        }

        private void SaveConfig()
        {
            config.Server = txtServer.Text;
            config.Port = txtPort.Text;
            config.Database = txtDatabase.Text;
            config.Username = txtUsername.Text;
            config.Password = txtPassword.Text;
            
            dbManager = new DatabaseManager(config);
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            SaveConfig();
            
            if (dbManager!.TestConnection(out string message))
            {
                MessageBox.Show(message, "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblStatus.Text = "状态: 连接成功";
                lblStatus.ForeColor = Color.Green;
            }
            else
            {
                MessageBox.Show(message, "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "状态: 连接失败";
                lblStatus.ForeColor = Color.Red;
            }
        }

        private void btnInitDatabase_Click(object sender, EventArgs e)
        {
            if (dbManager == null)
            {
                MessageBox.Show("请先测试数据库连接!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dbManager.InitializeDatabase(out string message))
            {
                MessageBox.Show(message, "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(message, "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnQueryData_Click(object sender, EventArgs e)
        {
            if (dbManager == null)
            {
                MessageBox.Show("请先测试数据库连接!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime? startTime = chkStartTime.Checked ? dtpStartTime.Value : null;
            DateTime? endTime = chkEndTime.Checked ? dtpEndTime.Value : null;
            int limit = (int)numLimit.Value;

            DataTable dt = dbManager.QuerySerialData(startTime, endTime, limit);
            dgvData.DataSource = dt;

            // 设置列标题
            if (dt.Columns.Count > 0)
            {
                dgvData.Columns["id"].HeaderText = "ID";
                dgvData.Columns["receive_time"].HeaderText = "接收时间";
                dgvData.Columns["data_content"].HeaderText = "数据内容";
                dgvData.Columns["data_length"].HeaderText = "数据长度";
                dgvData.Columns["port_name"].HeaderText = "串口";
            }
        }

        private void btnStatistics_Click(object sender, EventArgs e)
        {
            if (dbManager == null)
            {
                MessageBox.Show("请先测试数据库连接!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var (total, first, last) = dbManager.GetStatistics();
            
            string stats = $"总记录数: {total}\n";
            stats += first.HasValue ? $"首条记录: {first.Value:yyyy-MM-dd HH:mm:ss}\n" : "首条记录: 无\n";
            stats += last.HasValue ? $"末条记录: {last.Value:yyyy-MM-dd HH:mm:ss}" : "末条记录: 无";

            MessageBox.Show(stats, "数据统计", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnClearData_Click(object sender, EventArgs e)
        {
            if (dbManager == null)
            {
                MessageBox.Show("请先测试数据库连接!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("确定要清空所有历史数据吗?此操作不可恢复!", "警告", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            
            if (result == DialogResult.Yes)
            {
                if (dbManager.ClearAllData(out string message))
                {
                    MessageBox.Show(message, "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnQueryData_Click(sender, e); // 刷新显示
                }
                else
                {
                    MessageBox.Show(message, "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dgvData.Rows.Count == 0)
            {
                MessageBox.Show("没有数据可导出!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV文件|*.csv|文本文件|*.txt";
            sfd.FileName = $"SerialData_{DateTime.Now:yyyyMMdd_HHmmss}";
            
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using StreamWriter sw = new StreamWriter(sfd.FileName, false, System.Text.Encoding.UTF8);
                    
                    // 写入列标题
                    for (int i = 0; i < dgvData.Columns.Count; i++)
                    {
                        sw.Write(dgvData.Columns[i].HeaderText);
                        if (i < dgvData.Columns.Count - 1)
                            sw.Write(",");
                    }
                    sw.WriteLine();

                    // 写入数据
                    foreach (DataGridViewRow row in dgvData.Rows)
                    {
                        if (row.IsNewRow) continue;
                        
                        for (int i = 0; i < dgvData.Columns.Count; i++)
                        {
                            string value = row.Cells[i].Value?.ToString() ?? "";
                            // 如果包含逗号,用引号包裹
                            if (value.Contains(","))
                                value = $"\"{value}\"";
                            sw.Write(value);
                            if (i < dgvData.Columns.Count - 1)
                                sw.Write(",");
                        }
                        sw.WriteLine();
                    }

                    MessageBox.Show("数据导出成功!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"导出失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public DatabaseManager? GetDatabaseManager()
        {
            return dbManager;
        }
    }
}
