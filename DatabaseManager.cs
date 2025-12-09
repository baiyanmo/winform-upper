using MySql.Data.MySqlClient;
using System.Data;

namespace UpperComputer
{
    /// <summary>
    /// 数据库管理类
    /// </summary>
    public class DatabaseManager
    {
        private readonly string connectionString;

        public DatabaseManager(DatabaseConfig config)
        {
            connectionString = config.GetConnectionString();
        }

        /// <summary>
        /// 测试数据库连接
        /// </summary>
        public bool TestConnection(out string message)
        {
            try
            {
                using var conn = new MySqlConnection(connectionString);
                conn.Open();
                message = "数据库连接成功!";
                return true;
            }
            catch (Exception ex)
            {
                message = $"数据库连接失败: {ex.Message}";
                return false;
            }
        }

        /// <summary>
        /// 初始化数据库表结构
        /// </summary>
        public bool InitializeDatabase(out string message)
        {
            try
            {
                using var conn = new MySqlConnection(connectionString);
                conn.Open();

                string createTableSql = @"
                    CREATE TABLE IF NOT EXISTS serial_data (
                        id INT AUTO_INCREMENT PRIMARY KEY,
                        receive_time DATETIME NOT NULL,
                        data_content TEXT NOT NULL,
                        data_length INT NOT NULL,
                        port_name VARCHAR(50),
                        INDEX idx_time (receive_time)
                    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
                ";

                using var cmd = new MySqlCommand(createTableSql, conn);
                cmd.ExecuteNonQuery();

                message = "数据库表初始化成功!";
                return true;
            }
            catch (Exception ex)
            {
                message = $"数据库初始化失败: {ex.Message}";
                return false;
            }
        }

        /// <summary>
        /// 插入串口接收数据
        /// </summary>
        public bool InsertSerialData(string data, string portName, out string message)
        {
            try
            {
                using var conn = new MySqlConnection(connectionString);
                conn.Open();

                string sql = @"
                    INSERT INTO serial_data (receive_time, data_content, data_length, port_name)
                    VALUES (@time, @data, @length, @port)
                ";

                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@time", DateTime.Now);
                cmd.Parameters.AddWithValue("@data", data);
                cmd.Parameters.AddWithValue("@length", data.Length);
                cmd.Parameters.AddWithValue("@port", portName);

                cmd.ExecuteNonQuery();
                message = "数据保存成功";
                return true;
            }
            catch (Exception ex)
            {
                message = $"数据保存失败: {ex.Message}";
                return false;
            }
        }

        /// <summary>
        /// 查询历史数据
        /// </summary>
        public DataTable QuerySerialData(DateTime? startTime = null, DateTime? endTime = null, int limit = 100)
        {
            DataTable dt = new DataTable();
            
            try
            {
                using var conn = new MySqlConnection(connectionString);
                conn.Open();

                string sql = "SELECT id, receive_time, data_content, data_length, port_name FROM serial_data WHERE 1=1";
                
                if (startTime.HasValue)
                    sql += " AND receive_time >= @startTime";
                if (endTime.HasValue)
                    sql += " AND receive_time <= @endTime";
                
                sql += " ORDER BY receive_time DESC LIMIT @limit";

                using var cmd = new MySqlCommand(sql, conn);
                if (startTime.HasValue)
                    cmd.Parameters.AddWithValue("@startTime", startTime.Value);
                if (endTime.HasValue)
                    cmd.Parameters.AddWithValue("@endTime", endTime.Value);
                cmd.Parameters.AddWithValue("@limit", limit);

                using var adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }

        /// <summary>
        /// 获取数据统计
        /// </summary>
        public (int totalRecords, DateTime? firstRecord, DateTime? lastRecord) GetStatistics()
        {
            try
            {
                using var conn = new MySqlConnection(connectionString);
                conn.Open();

                string sql = @"
                    SELECT 
                        COUNT(*) as total,
                        MIN(receive_time) as first_time,
                        MAX(receive_time) as last_time
                    FROM serial_data
                ";

                using var cmd = new MySqlCommand(sql, conn);
                using var reader = cmd.ExecuteReader();
                
                if (reader.Read())
                {
                    int total = reader.GetInt32("total");
                    DateTime? first = reader.IsDBNull(reader.GetOrdinal("first_time")) ? null : reader.GetDateTime("first_time");
                    DateTime? last = reader.IsDBNull(reader.GetOrdinal("last_time")) ? null : reader.GetDateTime("last_time");
                    return (total, first, last);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"统计查询失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return (0, null, null);
        }

        /// <summary>
        /// 清空历史数据
        /// </summary>
        public bool ClearAllData(out string message)
        {
            try
            {
                using var conn = new MySqlConnection(connectionString);
                conn.Open();

                string sql = "TRUNCATE TABLE serial_data";
                using var cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                message = "数据已清空";
                return true;
            }
            catch (Exception ex)
            {
                message = $"清空失败: {ex.Message}";
                return false;
            }
        }
    }
}
