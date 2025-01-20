using System.Security.Cryptography;
using System.Text;

namespace InventoryManagerMAUI.Commands;

public static class HashHelper
{
    public static async Task<string> ComputeMD5Hash(string input)
    {
        try
        {
            using var md5 = MD5.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
        }
        catch (Exception ex)
        {
            // Показать всплывающее окно с ошибкой
            if (Application.Current?.MainPage != null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Ошибка",
                    $"Не удалось вычислить MD5-хэш: {ex.Message}",
                    "OK"
                );
            }
            return string.Empty;
        }
    }
}