using System;
using System.Diagnostics;

// Định nghĩa Struct (Dữ liệu gói gọn)
struct PointStruct
{
    public int X, Y;
}

// Định nghĩa Class (Dữ liệu + Overhead)
class PointClass
{
    public int X, Y;
}

class Program
{
    const int COUNT = 1_000_000; // 1 triệu phần tử

    static void Main(string[] args)
    {
        Console.WriteLine($"=== BENCHMARKING: {COUNT:N0} ELEMENTS ===");

        // --- TEST 1: ARRAY OF STRUCTS ---
        ForceGC(); // Dọn dẹp bộ nhớ trước khi đo
        long startMemStruct = GC.GetTotalMemory(true);
        Stopwatch swStruct = Stopwatch.StartNew();

        // Khởi tạo mảng Struct
        // Lưu ý: Chỉ cần 'new' mảng là bộ nhớ cho 1 triệu struct được cấp phát ngay lập tức
        PointStruct[] structArray = new PointStruct[COUNT];

        // Gán dữ liệu để mô phỏng làm việc thực tế
        for (int i = 0; i < COUNT; i++)
        {
            structArray[i].X = i;
            structArray[i].Y = i;
        }

        swStruct.Stop();
        long endMemStruct = GC.GetTotalMemory(true);
        long sizeStruct = endMemStruct - startMemStruct;

        Console.WriteLine("\n[STRUCT ARRAY]");
        Console.WriteLine($"Memory Used : {sizeStruct / 1024 / 1024} MB ({sizeStruct:N0} bytes)");
        Console.WriteLine($"Time Taken  : {swStruct.ElapsedMilliseconds} ms");

        // --- TEST 2: ARRAY OF CLASSES ---
        structArray = null; // Hủy tham chiếu để GC dọn dẹp
        ForceGC();

        long startMemClass = GC.GetTotalMemory(true);
        Stopwatch swClass = Stopwatch.StartNew();

        // Khởi tạo mảng Class
        // Lưu ý: Lúc này chỉ mới có mảng chứa 1 triệu tham chiếu (null), chưa có object
        PointClass[] classArray = new PointClass[COUNT];

        // Phải 'new' từng phần tử -> Cấp phát 1 triệu lần trên Heap
        for (int i = 0; i < COUNT; i++)
        {
            classArray[i] = new PointClass();
            classArray[i].X = i;
            classArray[i].Y = i;
        }

        swClass.Stop();
        long endMemClass = GC.GetTotalMemory(true);
        long sizeClass = endMemClass - startMemClass;

        Console.WriteLine("\n[CLASS ARRAY]");
        Console.WriteLine($"Memory Used : {sizeClass / 1024 / 1024} MB ({sizeClass:N0} bytes)");
        Console.WriteLine($"Time Taken  : {swClass.ElapsedMilliseconds} ms");

        // So sánh
        Console.WriteLine("\n=== SUMMARY ===");
        Console.WriteLine($"Memory Difference: Class uses {sizeClass / (double)sizeStruct:F2}x more memory");
        Console.WriteLine($"Speed Difference : Struct is {swClass.ElapsedMilliseconds / (double)swStruct.ElapsedMilliseconds:F2}x faster");

        Console.ReadKey();
    }

    // Hàm ép Garbage Collector chạy để đo chính xác
    static void ForceGC()
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
    }
}