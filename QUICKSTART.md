# 🚀 快速入门指南

## 第一次使用 - 5分钟上手

### 1️⃣ 启动程序

```powershell
# 方式1: 直接运行
dotnet run --project UpperComputer.csproj

# 方式2: 先编译后运行
dotnet build UpperComputer.csproj
.\bin\Debug\net8.0-windows\UpperComputer.exe
```

### 2️⃣ 串口通信测试

1. **连接串口**
   - 选择串口: `COM3` (根据实际情况选择)
   - 波特率: `9600`
   - 数据位: `8`
   - 校验位: `None`
   - 停止位: `One`
   - 点击【连接】

2. **发送测试**
   - 在发送区输入: `Hello`
   - 点击【发送】
   - 在接收区查看回显(如果设备支持)

3. **十六进制发送**
   - 勾选"十六进制发送"
   - 输入: `AA BB CC`
   - 点击【发送】

✅ 到这里,基本的串口通信功能已经可以使用了!

---

## 配置MySQL数据库 (可选)

### 准备工作

#### 选项A: 使用XAMPP (推荐新手)
1. 下载 XAMPP: https://www.apachefriends.org/
2. 安装并启动 XAMPP Control Panel
3. 点击 MySQL 的【Start】按钮
4. 默认配置:
   - 服务器: `localhost`
   - 端口: `3306`
   - 用户名: `root`
   - 密码: (空)

#### 选项B: 独立安装MySQL
1. 下载 MySQL: https://dev.mysql.com/downloads/mysql/
2. 安装过程中设置root密码
3. 启动MySQL服务

### 配置步骤

#### 方法1: 使用Navicat (推荐)

1. **打开Navicat**
2. **新建连接**
   - 连接名: `本地MySQL`
   - 主机: `localhost`
   - 端口: `3306`
   - 用户名: `root`
   - 密码: (你的密码或留空)

3. **执行SQL脚本**
   - 打开 `database_init.sql` 文件
   - 点击【运行】按钮
   - 看到 "查询成功完成" 提示

#### 方法2: 在程序中初始化

1. **打开上位机程序**
2. **点击【数据库管理】**
3. **填写连接信息**:
   ```
   服务器IP: localhost
   端口: 3306
   数据库名: uppercomputer
   用户名: root
   密码: (你的密码)
   ```
4. **点击【测试连接】** - 应该显示"连接成功"
5. **点击【初始化数据库】** - 自动创建表结构

### 使用数据库功能

1. **启用自动保存**
   - 返回主界面
   - 勾选 "自动保存到数据库"
   - 现在所有接收的数据都会自动保存!

2. **查询历史数据**
   - 打开【数据库管理】
   - 点击【查询数据】
   - 查看历史记录

3. **导出数据**
   - 查询数据后
   - 点击【导出数据】
   - 选择保存位置

---

## 💡 实用技巧

### 技巧1: 监控实时数据
```
1. 连接串口
2. 勾选"自动保存到数据库"
3. 打开Navicat,刷新查看实时数据
```

### 技巧2: 批量发送
```
在发送区输入多行数据
勾选"自动换行"
多次点击发送按钮
```

### 技巧3: 数据分析
在Navicat中执行SQL:
```sql
-- 查询今天收到的数据量
SELECT COUNT(*) FROM serial_data 
WHERE DATE(receive_time) = CURDATE();

-- 按小时统计
SELECT HOUR(receive_time) as hour, COUNT(*) as count
FROM serial_data
WHERE DATE(receive_time) = CURDATE()
GROUP BY HOUR(receive_time);
```

---

## 🔧 故障排除

### 问题: 找不到串口
**解决方案:**
- 检查设备是否已连接
- 在设备管理器中查看COM口号
- 点击【刷新】按钮
- 安装相应的驱动程序(如CH340、FTDI等)

### 问题: 数据库连接失败
**解决方案:**
```powershell
# 检查MySQL服务是否运行
Get-Service -Name MySQL*

# 或启动MySQL服务
net start MySQL
```

### 问题: 乱码
**解决方案:**
- 检查串口波特率设置
- 确认数据位、校验位、停止位配置
- 如果是中文乱码,确保数据库使用utf8mb4编码

---

## 📱 联系支持

遇到问题?
1. 查看 [README.md](README.md) 完整文档
2. 查看 [DATABASE_CONFIG.md](DATABASE_CONFIG.md) 数据库详细配置
3. 检查错误提示信息

---

## ✅ 检查清单

开始使用前确认:
- [ ] 已安装 .NET 8.0 SDK
- [ ] 串口设备已连接
- [ ] (可选) MySQL已安装并启动
- [ ] (可选) Navicat已配置

**祝使用愉快! 🎉**
