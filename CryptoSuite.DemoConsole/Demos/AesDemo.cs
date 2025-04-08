using CryptoSuite.Config;
using CryptoSuite.KeyManagement.Enums;
using CryptoSuite.KeyManagement.Models;
using CryptoSuite.Services.Interfaces;
using System.Text;

namespace CryptoSuite.DemoConsole.Demos
{
    public class AesDemo
    {
        private readonly ICryptoService _cryptoService;
        private readonly ICryptoKeyService _keyService;

        public AesDemo(ICryptoService cryptoService, ICryptoKeyService keyService)
        {
            _cryptoService = cryptoService;
            _keyService = keyService;

            // 初始化測試用 CryptoConfig
            CryptoConfig.Override(new CryptoConfigModel
            {
                KeyDirectory = Path.GetTempPath(),
                AES = new AesConfig { KeySize = 256 }
            });
        }

        public void Run()
        {
            Console.WriteLine("[AES] 產生金鑰中...");

            var key = _keyService.GenerateKeyOnly<SymmetricKeyModel>(CryptoAlgorithmType.AES);

            Console.WriteLine("[AES] 金鑰內容：");
            Console.WriteLine($"Key: {Convert.ToBase64String(key.Key)}");
            Console.WriteLine($"IV:  {Convert.ToBase64String(key.IV)}");

            var plaintext = "Hello from CryptoSuite!";

            Console.WriteLine($"\n原文：{plaintext}");

            var encrypted = _cryptoService.Encrypt(
                Encoding.UTF8.GetBytes(plaintext),
                CryptoAlgorithmType.AES,
                key);

            Console.WriteLine($"加密（Base64）：{Convert.ToBase64String(encrypted)}");

            var decrypted = _cryptoService.Decrypt(
                encrypted,
                CryptoAlgorithmType.AES,
                key);

            Console.WriteLine($"解密還原：{Encoding.UTF8.GetString(decrypted)}");
        }
    }
}
