using System;
using System.Drawing; // Cần cài NuGet: System.Drawing.Common
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices;

class MembershipCardPrinter
{
    // --- PHẦN 1: LOW-LEVEL API DECLARATION (P/Invoke) ---
    // Gọi hàm AddFontResource từ thư viện GDI32.DLL của Windows
    [DllImport("gdi32.dll", EntryPoint = "AddFontResourceW", SetLastError = true)]
    public static extern int AddFontResource([In][MarshalAs(UnmanagedType.LPWStr)] string lpFileName);

    static void Main(string[] args)
    {
        Console.WriteLine("=== CRM MEMBERSHIP CARD PRINTER SYSTEM ===");

        // Ta có file font 'MyCustomFont.ttf' (Barcode font) để in thẻ
        string fontPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MyCustomFont.ttf");

        // Kiểm tra và cài đặt Font nếu cần
        InstallFontIfMissing(fontPath);

        // Thông tin thành viên
        string name = "Nguyen Trong Huong";
        string id = "31231023691";
        string level = "VIP GOLD";

        // Thực hiện in thẻ
        PrintCard(name, id, level, fontPath);

        Console.WriteLine("\nCard generated successfully: 'MemberCard.png'");
        Console.ReadKey();
    }

    // --- PHẦN 2: LOGIC CÀI ĐẶT FONT ---
    static void InstallFontIfMissing(string fontPath)
    {
        if (!File.Exists(fontPath))
        {
            Console.WriteLine($"[WARNING] Font file not found at: {fontPath}");
            Console.WriteLine("Using default system font instead.");
            return;
        }

        // Gọi API hệ thống để đăng ký Font tạm thời cho phiên làm việc này
        int result = AddFontResource(fontPath);

        if (result > 0)
        {
            Console.WriteLine($"[SYSTEM] Font loaded successfully via Low-Level API.");
        }
        else
        {
            Console.WriteLine($"[ERROR] Failed to load font. Error code: {Marshal.GetLastWin32Error()}");
        }
    }

    // --- PHẦN 3: VẼ VÀ IN THẺ ---
    static void PrintCard(string name, string id, string level, string fontPath)
    {
        int width = 500;
        int height = 300;
        using (Bitmap bmp = new Bitmap(width, height))
        using (Graphics g = Graphics.FromImage(bmp))
        {
            // 1. Vẽ nền và viền
            g.Clear(Color.White);
            g.DrawRectangle(new Pen(Color.Black, 5), 0, 0, width - 1, height - 1);
            g.FillRectangle(Brushes.DarkBlue, 0, 0, width, 60);

            // 2. Cấu hình Font
            Font titleFont = new Font("Arial", 20, FontStyle.Bold);
            Font infoFont = new Font("Consolas", 14, FontStyle.Regular);

            PrivateFontCollection pfc = new PrivateFontCollection();
            Font customFont;
            try
            {
                pfc.AddFontFile(fontPath);
                // Số 36 ở đây là kích thước chữ (bạn có thể chỉnh lên 40, 50 nếu muốn to hơn nữa)
                customFont = new Font(pfc.Families[0], 36, FontStyle.Bold);
            }
            catch
            {
                customFont = new Font("Arial", 36, FontStyle.Bold); // Fallback
            }

            // 3. Vẽ thông tin
            // Tiêu đề
            g.DrawString("UEH MEMBERSHIP", titleFont, Brushes.White, new PointF(120, 15));

            // Thông tin cá nhân
            g.DrawString($"NAME : {name}", infoFont, Brushes.Black, new PointF(30, 80));
            g.DrawString($"ID   : {id}", infoFont, Brushes.Black, new PointF(30, 110));

            // Tạo định dạng căn giữa
            StringFormat centerFormat = new StringFormat();
            centerFormat.Alignment = StringAlignment.Center;      // Căn giữa theo chiều ngang
            centerFormat.LineAlignment = StringAlignment.Center;  // Căn giữa theo chiều dọc

            // Tạo một vùng hình chữ nhật ở nửa dưới của thẻ để chứa chữ
            RectangleF rect = new RectangleF(0, 140, width, 150);

            // Vẽ chữ vào trong vùng hình chữ nhật đó với định dạng centerFormat
            g.DrawString(level, customFont, Brushes.Gold, rect, centerFormat);

            // 4. Lưu file
            bmp.Save("MemberCard.png", System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}