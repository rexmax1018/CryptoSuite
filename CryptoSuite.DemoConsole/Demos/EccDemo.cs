using CryptoSuite.Config;
using CryptoSuite.Config.Enums;
using CryptoSuite.KeyManagement.Enums;
using CryptoSuite.KeyManagement.Models;
using CryptoSuite.Services.Interfaces;
using System.Text;

namespace CryptoSuite.DemoConsole.Demos
{
    public class EccDemo
    {
        private readonly ICryptoService _cryptoService;
        private readonly ICryptoKeyService _keyService;

        public EccDemo(ICryptoService cryptoService, ICryptoKeyService keyService)
        {
            _cryptoService = cryptoService;
            _keyService = keyService;

            // 初始化 ECC 設定
            CryptoConfig.Override(new CryptoConfigModel
            {
                KeyDirectory = Path.GetTempPath(),
                ECC = new EccConfig
                {
                    Curve = EccCurveType.NistP256
                }
            });
        }

        public void Run()
        {
            Console.WriteLine("[ECC] 產生金鑰中...");

            var key = _keyService.GenerateKeyOnly<EccKeyModel>(CryptoAlgorithmType.ECC);

            Console.WriteLine("[ECC] 公鑰（PEM）：\n" + key.PublicKey);
            Console.WriteLine("[ECC] 私鑰（PEM）：\n" + key.PrivateKey);

            var plaintext = "Hello ECC!";

            Console.WriteLine($"\n原文：{plaintext}");

            var signature = _cryptoService.Sign(
                Encoding.UTF8.GetBytes(plaintext),
                CryptoAlgorithmType.ECC,
                key);

            Console.WriteLine($"簽章（Base64）：{Convert.ToBase64String(signature)}");

            var verified = _cryptoService.Verify(
                Encoding.UTF8.GetBytes(plaintext),
                signature,
                CryptoAlgorithmType.ECC,
                key);

            Console.WriteLine($"驗章結果：{(verified ? "✔ 通過" : "✘ 失敗")}");
        }
    }
}