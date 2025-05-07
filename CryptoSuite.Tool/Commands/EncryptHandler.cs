using CryptoSuite.Extensions;
using CryptoSuite.KeyManagement.Enums;
using CryptoSuite.KeyManagement.Factories;
using CryptoSuite.KeyManagement.Models;
using CryptoSuite.Services;

namespace CryptoSuite.Tool.Commands
{
    /// <summary>
    /// 負責執行 EncryptOptions 對應的加密動作
    /// </summary>
    public static class EncryptHandler
    {
        public static void Handle(EncryptOptions options)
        {
            var service = new CryptoService();

            if (!File.Exists(options.InputFile))
            {
                Console.WriteLine($"❗ 輸入檔案不存在: {options.InputFile}");
                return;
            }

            if (!File.Exists(options.KeyPath))
            {
                Console.WriteLine($"❗ 金鑰檔案不存在: {options.KeyPath}");
                return;
            }

            var inputBytes = File.ReadAllBytes(options.InputFile);
            byte[] encrypted;

            switch (options.Algorithm)
            {
                case CryptoAlgorithmType.AES:
                    var aesKey = File.ReadAllText(options.KeyPath).FromJson<SymmetricKeyModel>();
                    encrypted = service.Encrypt(inputBytes, CryptoAlgorithmType.AES, aesKey);
                    break;

                case CryptoAlgorithmType.RSA:
                    var rsaKey = File.ReadAllText(options.KeyPath).FromJson<RsaKeyModel>();
                    encrypted = service.Encrypt(inputBytes, CryptoAlgorithmType.RSA, rsaKey);
                    break;

                default:
                    Console.WriteLine("目前僅支援 AES 與 RSA 演算法加密");
                    return;
            }

            File.WriteAllBytes(options.OutputFile, encrypted);
            Console.WriteLine($"加密完成 -> {options.OutputFile}");
        }
    }
}