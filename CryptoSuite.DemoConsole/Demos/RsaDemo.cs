using CryptoSuite.Config;
using CryptoSuite.Config.Enums;
using CryptoSuite.KeyManagement.Enums;
using CryptoSuite.KeyManagement.Models;
using CryptoSuite.Services.Interfaces;
using System.Text;

namespace CryptoSuite.DemoConsole.Demos
{
    public class RsaDemo
    {
        private readonly ICryptoService _cryptoService;
        private readonly ICryptoKeyService _keyService;

        public RsaDemo(ICryptoService cryptoService, ICryptoKeyService keyService)
        {
            _cryptoService = cryptoService;
            _keyService = keyService;

            // 初始化 RSA 設定（2048 bits）
            CryptoConfig.Override(new CryptoConfigModel
            {
                KeyDirectory = Path.GetTempPath(),
                RSA = new RsaConfig
                {
                    KeySize = 2048,
                    Encoding = TextEncodingType.UTF8
                }
            });
        }

        public void Run()
        {
            Console.WriteLine("[RSA] 產生金鑰中...");

            var key = _keyService.GenerateKeyOnly<RsaKeyModel>(CryptoAlgorithmType.RSA);

            Console.WriteLine("[RSA] 公鑰（PEM）：\n" + key.PublicKey);
            Console.WriteLine("[RSA] 私鑰（PEM）：\n" + key.PrivateKey);

            var plaintext = "Hello RSA!";

            Console.WriteLine($"\n原文：{plaintext}");

            var encrypted = _cryptoService.Encrypt(
                Encoding.UTF8.GetBytes(plaintext),
                CryptoAlgorithmType.RSA,
                key);

            Console.WriteLine($"加密（Base64）：{Convert.ToBase64String(encrypted)}");

            var decrypted = _cryptoService.Decrypt(
                encrypted,
                CryptoAlgorithmType.RSA,
                key);

            Console.WriteLine($"解密還原：{Encoding.UTF8.GetString(decrypted)}");

            var signature = _cryptoService.Sign(
                Encoding.UTF8.GetBytes(plaintext),
                CryptoAlgorithmType.RSA,
                key);

            Console.WriteLine($"簽章（Base64）：{Convert.ToBase64String(signature)}");

            var verified = _cryptoService.Verify(
                Encoding.UTF8.GetBytes(plaintext),
                signature,
                CryptoAlgorithmType.RSA,
                key);

            Console.WriteLine($"驗章結果：{(verified ? "✔ 通過" : "✘ 失敗")}");
        }
    }
}