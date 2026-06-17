# Winform Upper Computer - Serial Port Monitor

.NET 8.0 Windows Forms serial communication tool with MySQL storage and image display.

[![Stars](https://img.shields.io/github/stars/baiyanmo/winform-upper?style=flat)](https://github.com/baiyanmo/winform-upper/stargazers)
[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4)](https://dotnet.microsoft.com)

## Features
### Serial Communication
- Auto-detect COM ports, 1200-115200 baud rates, configurable data/parity/stop bits
- Real-time connection status (green=connected, red=disconnected)

### Data Send/Receive
- Text mode and hex mode (format: AA BB CC)
- Auto newline, auto scroll

### Database
- MySQL integration, auto-save received data
- History query with time range filter, CSV/TXT export
- Visual DB management, Navicat compatible

### Image Display
- Receive and display images via serial port
- JPEG, PNG, BMP, raw RGB888, grayscale support

## Requirements
- Windows + .NET 8.0 Runtime
- Available COM port
- MySQL 5.7+ (optional for DB features)

## Quick Start


## Project Structure
- MainForm.cs: Main window logic
- DatabaseForm.cs: DB management window
- DatabaseManager.cs: DB operations
- Program.cs: Entry point

## Tech Stack
- .NET 8.0 / Windows Forms
- System.IO.Ports (serial)
- MySql.Data (MySQL driver)

## Docs
- [DATABASE_CONFIG.md](DATABASE_CONFIG.md)
- [database_init.sql](database_init.sql)

## License
MIT
