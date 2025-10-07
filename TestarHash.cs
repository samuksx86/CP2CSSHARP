using System;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main()
    {
        string senha = "admin123";
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(senha);
        var hash = sha256.ComputeHash(bytes);
        string hashBase64 = Convert.ToBase64String(hash);

        Console.WriteLine($"Senha: {senha}");
        Console.WriteLine($"Hash: {hashBase64}");
    }
}
