namespace JigNetApi;

using System.Security.Cryptography;

public static class Id24
{
    // สร้าง 24 ตัวอักษรเป็น hex (0-9a-f)
    public static string NewHex24()
    {
        Span<byte> bytes = stackalloc byte[12]; // 12 bytes -> 24 hex chars
        RandomNumberGenerator.Fill(bytes);
        return Convert.ToHexString(bytes).ToLowerInvariant(); // 24 chars
    }
}

