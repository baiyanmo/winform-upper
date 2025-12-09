# MySQL数据库配置指南

## 📋 前提条件

1. **安装 MySQL 服务器**
   - 下载 MySQL Community Server: https://dev.mysql.com/downloads/mysql/
   - 或使用 XAMPP、WAMP 等集成环境

2. **安装 Navicat (可选但推荐)**
   - 下载 Navicat for MySQL
   - 用于图形化管理数据库

## 🔧 配置步骤

### 步骤 1: 启动 MySQL 服务

**Windows 命令行:**
```powershell
# 启动MySQL服务
net start MySQL

# 或使用服务管理器启动
services.msc
```

### 步骤 2: 使用 Navicat 创建数据库

1. **打开 Navicat**
2. **创建新连接**
   - 点击 "连接" → "MySQL"
   - 连接名: `本地MySQL`
   - 主机: `localhost` 或 `127.0.0.1`
   - 端口: `3306`
   - 用户名: `root`
   - 密码: (你的MySQL密码)
   - 点击 "测试连接"

3. **执行SQL脚本**
   - 连接成功后,点击 "查询" → "新建查询"
   - 打开项目中的 `database_init.sql` 文件
   - 或复制以下SQL:
   ```sql
   CREATE DATABASE IF NOT EXISTS uppercomputer 
   DEFAULT CHARACTER SET utf8mb4;
   
   USE uppercomputer;
   
   CREATE TABLE IF NOT EXISTS serial_data (
     id INT NOT NULL AUTO_INCREMENT,
     receive_time DATETIME NOT NULL,
     data_content TEXT NOT NULL,
     data_length INT NOT NULL,
     port_name VARCHAR(50),
     PRIMARY KEY (id),
     INDEX idx_time (receive_time)
   ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
   ```
   - 点击 "运行" 执行

### 步骤 3: 在上位机软件中配置

1. **运行上位机程序**
   ```powershell
   dotnet run --project UpperComputer.csproj
   ```

2. **打开数据库管理**
   - 点击主界面的 "数据库管理" 按钮

3. **填写数据库连接信息**
   - 服务器IP: `localhost`
   - 端口: `3306`
   - 数据库名: `uppercomputer`
   - 用户名: `root`
   - 密码: (你的MySQL密码)

4. **测试连接**
   - 点击 "测试连接" 按钮
   - 看到 "连接成功" 提示

5. **初始化数据库** (可选)
   - 如果没有执行SQL脚本,点击 "初始化数据库"
   - 自动创建数据表

## 📊 功能使用

### 自动保存数据
1. 在主界面勾选 "自动保存到数据库"
2. 所有接收到的串口数据将自动保存

### 查询历史数据
1. 打开 "数据库管理"
2. 设置查询条件:
   - 勾选开始/结束时间(可选)
   - 设置记录数量
3. 点击 "查询数据"
4. 在数据表格中查看结果

### 数据统计
- 点击 "数据统计" 查看:
  - 总记录数
  - 首条记录时间
  - 末条记录时间

### 导出数据
1. 查询数据后
2. 点击 "导出数据"
3. 选择保存位置和格式(CSV/TXT)

### 清空数据
- 点击 "清空数据"
- 确认后删除所有历史记录

## 🔍 在 Navicat 中查看数据

1. 连接到 `uppercomputer` 数据库
2. 展开 "表" → `serial_data`
3. 右键 → "打开表" 查看数据
4. 使用查询工具执行自定义SQL

## 常用 SQL 查询

### 查询今天的数据
```sql
SELECT * FROM serial_data 
WHERE DATE(receive_time) = CURDATE()
ORDER BY receive_time DESC;
```

### 按串口统计
```sql
SELECT port_name, COUNT(*) as count, 
       MIN(receive_time) as first_time,
       MAX(receive_time) as last_time
FROM serial_data 
GROUP BY port_name;
```

### 查询包含特定内容的数据
```sql
SELECT * FROM serial_data 
WHERE data_content LIKE '%关键字%'
ORDER BY receive_time DESC;
```

## ❗ 常见问题

### 连接失败
**问题**: "无法连接到MySQL服务器"
**解决**:
1. 确认MySQL服务已启动
2. 检查防火墙设置
3. 验证用户名密码
4. 确认端口号正确

### 密码错误
**问题**: "Access denied for user 'root'"
**解决**:
1. 重置MySQL root密码
2. 使用正确的用户凭据

### 字符集问题
**问题**: 中文显示乱码
**解决**:
- 确保数据库字符集为 `utf8mb4`
- 修改数据库:
```sql
ALTER DATABASE uppercomputer 
CHARACTER SET utf8mb4 
COLLATE utf8mb4_general_ci;
```

## 📝 数据库表结构

| 字段名 | 类型 | 说明 |
|--------|------|------|
| id | INT | 主键,自增 |
| receive_time | DATETIME | 数据接收时间 |
| data_content | TEXT | 串口数据内容 |
| data_length | INT | 数据长度 |
| port_name | VARCHAR(50) | 串口名称(COM3等) |

## 🛠️ 维护建议

1. **定期备份**
   - 在Navicat中右键数据库 → "转储SQL文件"
   
2. **性能优化**
   - 定期清理旧数据
   - 对常用查询字段建立索引

3. **安全建议**
   - 不要使用root用户,创建专用用户
   - 设置强密码
   - 限制远程访问

## 📞 技术支持

如遇问题,请检查:
- MySQL服务状态
- 网络连接
- 防火墙设置
- 日志文件
