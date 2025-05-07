using CryptoSuite.Extensions;
using CryptoSuite.KeyManagement.Enums;
using CryptoSuite.KeyManagement.Models;
using CryptoSuite.Services;

namespace CryptoSuite.Tool.Commands
{
    public static class SignHandler
    {
        public static void Handle(SignOptions options)
        {
            var service = new CryptoService();

            if (!File.Exists(options.InputFile))
            {
                Console.WriteLine($"❗ 輸入檔案不存在: {options.InputFile}");
                return;
            }

            if (!File.Exists(options.KeyPath))
            {
                Console.WriteLine($"❗ 私鑰檔案不存在: {options.KeyPath}");
                return;
            }

            var data = File.ReadAllBytes(options.InputFile);
            byte[] signature;

            switch (options.Algorithm)
            {
                case CryptoAlgorithmType.RSA:
                    var rsaKey = File.ReadAllText(options.KeyPath).FromJson<RsaKeyModel>();
                    signature = service.Sign(data, CryptoAlgorithmType.RSA, rsaKey);
                    break;

                case CryptoAlgorithmType.ECC:
                    var eccKey = File.ReadAllText(options.KeyPath).FromJson<EccKeyModel>();
                    signature = service.Sign(data, CryptoAlgorithmType.ECC, eccKey);
                    break;

                default:
                    Console.WriteLine("目前僅支援 RSA 與 ECC 簽章");
                    return;
            }

            File.WriteAllBytes(options.OutputFile, signature);
            Console.WriteLine($"簽章完成 -> {options.OutputFile}");
        }
    }
}