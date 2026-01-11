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