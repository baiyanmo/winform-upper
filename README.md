# 串口上位机 WinForms 应用程序

一个基于 .NET 8.0 的 Windows Forms 串口通信上位机应用程序,支持MySQL数据库存储。

## 功能特性

### 🔌 串口通信
- 自动检测可用串口
- 支持多种波特率 (1200-115200)
- 可配置数据位、校验位、停止位
- 实时连接状态显示

### 📡 数据收发
- 文本模式发送/接收
- 十六进制模式发送
- 自动换行功能
- 接收数据自动滚动显示
- 清空接收区/发送区

### 💾 数据库功能 (NEW!)
- MySQL数据库集成
- 自动保存串口接收数据
- 历史数据查询(支持时间范围筛选)
- 数据统计分析
- CSV/TXT格式数据导出
- 数据库可视化管理界面
- 兼容Navicat管理工具

### 🎨 界面特点
- 直观的分组布局
- 实时状态指示(绿色=已连接,红色=已断开)
- 独立的数据库管理窗口
- 固定窗口大小,防止误操作

## 运行要求

- Windows 操作系统
- .NET 8.0 Runtime 或更高版本
- 至少一个可用的串口设备(COM口)
- MySQL 5.7+ (如需使用数据库功能)

## 快速开始

### 编译运行

```powershell
# 进入项目目录
cd b:\winform

# 还原依赖包
dotnet restore

# 编译项目
dotnet build

# 运行程序
dotnet run
```

### 使用说明

1. **连接串口**
   - 从下拉菜单选择串口号(如 COM3)
   - 设置波特率(默认 9600)
   - 设置数据位(默认 8)
   - 设置校验位(默认 None)
   - 设置停止位(默认 One)
   - 点击"连接"按钮

2. **发送数据**
   - 在发送区输入要发送的数据
   - 勾选"十六进制发送"可以发送十六进制数据(格式: `AA BB CC`)
   - 勾选"自动换行"会在发送数据后自动添加 `\r\n`
   - 点击"发送"按钮

3. **接收数据**
   - 连接成功后,接收到的数据会自动显示在接收区
   - 点击"清空"可清除接收区内容

4. **数据库功能** (可选)
   - 点击"数据库管理"打开数据库配置窗口
   - 配置MySQL连接信息并测试连接
   - 勾选"自动保存到数据库"启用自动记录
   - 查看详细配置: [数据库配置指南](DATABASE_CONFIG.md)

## 项目结构

```
b:\winform\
├── UpperComputer.csproj      # 项目文件
├── Program.cs                # 程序入口
├── MainForm.cs               # 主窗体逻辑
├── MainForm.Designer.cs      # 主窗体设计器代码
├── MainForm.resx             # 主窗体资源文件
├── DatabaseForm.cs           # 数据库管理窗体
├── DatabaseForm.Designer.cs  # 数据库窗体设计器
├── DatabaseConfig.cs         # 数据库配置类
├── DatabaseManager.cs        # 数据库操作类
├── database_init.sql         # 数据库初始化脚本
├── DATABASE_CONFIG.md        # 数据库配置指南
└── README.md                 # 项目说明
```

## 技术栈

- **.NET 8.0** - 应用框架
- **Windows Forms** - UI 框架
- **System.IO.Ports** - 串口通信库
- **MySql.Data** - MySQL数据库驱动

## 常见问题

**Q: 找不到串口怎么办?**  
A: 点击"刷新"按钮重新扫描串口,确保串口设备已正确连接并安装驱动。

**Q: 连接失败?**  
A: 检查串口是否被其他程序占用,确认波特率等参数设置正确。

**Q: 十六进制发送格式?**  
A: 使用空格分隔,例如: `AA BB CC 01 02 03`

**Q: 数据库连接失败?**  
A: 请参考 [数据库配置指南](DATABASE_CONFIG.md),确保MySQL服务已启动并配置正确。

**Q: 如何在Navicat中查看数据?**  
A: 连接到`uppercomputer`数据库,打开`serial_data`表即可查看所有记录。

## 📚 相关文档

- [数据库配置指南](DATABASE_CONFIG.md) - MySQL配置和Navicat使用说明
- [database_init.sql](database_init.sql) - 数据库初始化脚本

## 许可证

本项目仅供学习参考使用。

## 已将项目从 .NET 6.0 升级到 .NET 8.0,现在可以正常运行了
dotnet run --project UpperComputer.csproj

如果您需要重启服务,可以使用:
net stop MySQL80
net start MySQL80
或者使用 PowerShell 命令:
Restart-Service MySQL80

查看数据库状态：
 Get-Service | Where-Object { $_.DisplayName -like '*MySQL*' -or $_.Name -like '*MySQL*' } | Select-Object Name, DisplayName, Status | Format-Table -AutoSize

生成文件
dotnet new winforms -n student -f net8.0
dotnet publish -c Release -r win-x64 --self-contained false -o ./publish