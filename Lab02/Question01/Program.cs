using System;

// Create a struct PointStruct
struct PointStruct
{
    public int X;
    public int Y;
}

// Create a class PointClass
// Class là Reference Type (Kiểu tham chiếu)
class PointClass
{
    public int X;
    public int Y;
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== TESTING STRUCT (VALUE TYPE) ===");
        PointStruct ps1 = new PointStruct();
        ps1.X = 10;
        ps1.Y = 20;

        PointStruct ps2 = ps1;

        ps2.X = 999;

        Console.WriteLine($"Original (ps1.X): {ps1.X}"); // Vẫn là 10
        Console.WriteLine($"Copy     (ps2.X): {ps2.X}"); // Là 999
        Console.WriteLine("-> Nhan xet: Thay doi ps2 KHONG anh huong ps1");

        Console.WriteLine("\n=== TESTING CLASS (REFERENCE TYPE) ===");
        PointClass pc1 = new PointClass();
        pc1.X = 10;
        pc1.Y = 20;

        PointClass pc2 = pc1;

        pc2.X = 999;

        Console.WriteLine($"Original (pc1.X): {pc1.X}"); // Biến thành 999
        Console.WriteLine($"Copy     (pc2.X): {pc2.X}"); // Là 999
        Console.WriteLine("-> Nhan xet: Thay doi pc2 LAM THAY DOI pc1");

        Console.ReadKey();
    }
}