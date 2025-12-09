-- ================================================
-- 串口上位机 MySQL 数据库初始化脚本
-- ================================================
-- 使用说明:
-- 1. 在 Navicat 中连接到 MySQL 服务器
-- 2. 打开此文件或复制以下SQL语句
-- 3. 执行脚本创建数据库和表
-- ================================================

-- 创建数据库
CREATE DATABASE IF NOT EXISTS `uppercomputer` 
DEFAULT CHARACTER SET utf8mb4 
DEFAULT COLLATE utf8mb4_general_ci;

-- 使用数据库
USE `uppercomputer`;

-- 创建串口数据表
CREATE TABLE IF NOT EXISTS `serial_data` (
  `id` INT NOT NULL AUTO_INCREMENT COMMENT '主键ID',
  `receive_time` DATETIME NOT NULL COMMENT '接收时间',
  `data_content` TEXT NOT NULL COMMENT '数据内容',
  `data_length` INT NOT NULL COMMENT '数据长度',
  `port_name` VARCHAR(50) DEFAULT NULL COMMENT '串口名称',
  PRIMARY KEY (`id`),
  INDEX `idx_time` (`receive_time`),
  INDEX `idx_port` (`port_name`)
) ENGINE=InnoDB 
DEFAULT CHARSET=utf8mb4 
COMMENT='串口接收数据表';

-- 插入测试数据(可选)
-- INSERT INTO serial_data (receive_time, data_content, data_length, port_name) 
-- VALUES (NOW(), 'Test Data', 9, 'COM3');

-- 查询验证
SELECT * FROM serial_data ORDER BY receive_time DESC LIMIT 10;

-- ================================================
-- 常用查询语句
-- ================================================

-- 查询今天的数据
-- SELECT * FROM serial_data WHERE DATE(receive_time) = CURDATE();

-- 查询最近100条数据
-- SELECT * FROM serial_data ORDER BY receive_time DESC LIMIT 100;

-- 按串口统计数据量
-- SELECT port_name, COUNT(*) as count, MAX(receive_time) as last_time 
-- FROM serial_data 
-- GROUP BY port_name;

-- 查询指定时间范围的数据
-- SELECT * FROM serial_data 
-- WHERE receive_time BETWEEN '2025-12-01 00:00:00' AND '2025-12-31 23:59:59'
-- ORDER BY receive_time DESC;

-- 清空所有数据(谨慎使用!)
-- TRUNCATE TABLE serial_data;
