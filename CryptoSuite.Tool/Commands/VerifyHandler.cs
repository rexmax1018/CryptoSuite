using CryptoSuite.Extensions;
using CryptoSuite.KeyManagement.Enums;
using CryptoSuite.KeyManagement.Models;
using CryptoSuite.Services;

namespace CryptoSuite.Tool.Commands
{
    public static class VerifyHandler
    {
        public static void Handle(VerifyOptions options)
        {
            var service = new CryptoService();

            if (!File.Exists(options.InputFile))
            {
                Console.WriteLine($"❗ 輸入檔案不存在: {options.InputFile}");
                return;
            }

            if (!File.Exists(options.KeyPath))
            {
                Console.WriteLine($"❗ 公鑰檔案不存在: {options.KeyPath}");
                return;
            }

            if (!File.Exists(options.SignatureFile))
            {
                Console.WriteLine($"❗ 簽章檔案不存在: {options.SignatureFile}");
                return;
            }

            var data = File.ReadAllBytes(options.InputFile);
            var signature = File.ReadAllBytes(options.SignatureFile);
            bool verified;

            switch (options.Algorithm)
            {
                case CryptoAlgorithmType.RSA:
                    var rsaKey = File.ReadAllText(options.KeyPath).FromJson<RsaKeyModel>();
                    verified = service.Verify(data, signature, CryptoAlgorithmType.RSA, rsaKey);
                    break;

                case CryptoAlgorithmType.ECC:
                    var eccKey = File.ReadAllText(options.KeyPath).FromJson<EccKeyModel>();
                    verified = service.Verify(data, signature, CryptoAlgorithmType.ECC, eccKey);
                    break;

                default:
                    Console.WriteLine("目前僅支援 RSA 與 ECC 驗章");
                    return;
            }

            Console.WriteLine(verified ? "驗章成功" : "驗章失敗");
        }
    }
}