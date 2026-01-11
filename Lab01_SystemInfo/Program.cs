using System;
using System.IO;

Console.WriteLine("=== SYSTEM INFORMATION PROGRAM ===");

// 1. Lấy thông tin phiên bản Hệ điều hành
var osVersion = Environment.OSVersion;
Console.WriteLine($"[1] OS Version: {osVersion}");

// 2. Lấy đường dẫn thư mục hiện tại mà tiến trình đang chạy
var currentDirectory = Environment.CurrentDirectory;
Console.WriteLine($"[2] Current Directory: {currentDirectory}");

// 3. Lấy thời gian hiện tại của hệ thống
var systemTime = DateTime.Now;
Console.WriteLine($"[3] System Time: {systemTime}");

Console.WriteLine("==================================");
Console.WriteLine("Press any key to exit...");
Console.ReadKey();

/* Output:
=== SYSTEM INFORMATION PROGRAM ===
[1] OS Version: Microsoft Windows NT 10.0.26100.0
[2] Current Directory: H:\chuong_trinh_hoc_UEH\mon_hoc_ki_6\systems_programming\systems_programming_labs\SystemsProgramming_Labs\Lab01_SystemInfo\bin\Debug\net8.0
[3] System Time: 1/11/2026 15:28:06
==================================
*/