namespace UpperComputer
{
    /// <summary>
    /// 数据库配置类
    /// </summary>
    public class DatabaseConfig
    {
        public string Server { get; set; } = "localhost";
        public string Port { get; set; } = "3306";
        public string Database { get; set; } = "uppercomputer";
        public string Username { get; set; } = "root";
        public string Password { get; set; } = "";

        /// <summary>
        /// 获取MySQL连接字符串
        /// </summary>
        public string GetConnectionString()
        {
            return $"Server={Server};Port={Port};Database={Database};Uid={Username};Pwd={Password};CharSet=utf8mb4;";
        }

        /// <summary>
        /// 默认配置
        /// </summary>
        public static DatabaseConfig Default => new DatabaseConfig();
    }
}
