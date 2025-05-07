using CryptoSuite.Extensions;
using CryptoSuite.KeyManagement.Enums;
using CryptoSuite.KeyManagement.Models;
using CryptoSuite.Services;

namespace CryptoSuite.Tool.Commands
{
    /// <summary>
    /// 負責執行 DecryptOptions 對應的解密動作
    /// </summary>
    public static class DecryptHandler
    {
        public static void Handle(DecryptOptions options)
        {
            var service = new CryptoService();

            if (!File.Exists(options.InputFile))
            {
                Console.WriteLine($"❗ 加密檔案不存在: {options.InputFile}");
                return;
            }

            if (!File.Exists(options.KeyPath))
            {
                Console.WriteLine($"❗ 金鑰檔案不存在: {options.KeyPath}");
                return;
            }

            var encrypted = File.ReadAllBytes(options.InputFile);
            byte[] decrypted;

            switch (options.Algorithm)
            {
                case CryptoAlgorithmType.AES:
                    var aesKey = File.ReadAllText(options.KeyPath).FromJson<SymmetricKeyModel>();
                    decrypted = service.Decrypt(encrypted, CryptoAlgorithmType.AES, aesKey);
                    break;

                case CryptoAlgorithmType.RSA:
                    var rsaKey = File.ReadAllText(options.KeyPath).FromJson<RsaKeyModel>();
                    decrypted = service.Decrypt(encrypted, CryptoAlgorithmType.RSA, rsaKey);
                    break;

                default:
                    Console.WriteLine("❗ 目前僅支援 AES 與 RSA 演算法解密");
                    return;
            }

            File.WriteAllBytes(options.OutputFile, decrypted);
            Console.WriteLine($"解密完成 -> {options.OutputFile}");
        }
    }
}