using CryptoSuite.Config;
using CryptoSuite.Config.Enums;
using CryptoSuite.Extensions;
using CryptoSuite.KeyManagement.Enums;
using CryptoSuite.KeyManagement.Models;
using CryptoSuite.Services;

namespace CryptoSuite.Tool.Commands
{
    /// <summary>
    /// 負責執行 KeygenOptions 對應的金鑰產生動作
    /// </summary>
    public static class KeygenHandler
    {
        public static void Handle(KeygenOptions options)
        {
            var generatorFactory = new KeyManagement.Factories.KeyGeneratorFactory();
            var loaderFactory = new KeyManagement.Factories.KeyLoaderFactory();

            var service = new CryptoKeyService(generatorFactory, loaderFactory);
            object keyModel;

            switch (options.Algorithm)
            {
                case CryptoAlgorithmType.AES:
                    CryptoConfig.Override(new CryptoConfigModel
                    {
                        KeyDirectory = Path.GetDirectoryName(options.Output)!,
                        AES = new AesConfig { KeySize = options.KeySize ?? 256 }
                    });
                    keyModel = service.GenerateKeyOnly<SymmetricKeyModel>(options.Algorithm);
                    File.WriteAllText(options.Output, keyModel.ToJson(true));
                    break;

                case CryptoAlgorithmType.RSA:
                    CryptoConfig.Override(new CryptoConfigModel
                    {
                        KeyDirectory = Path.GetDirectoryName(options.Output)!,
                        RSA = new RsaConfig { KeySize = options.KeySize ?? 2048, Encoding = TextEncodingType.UTF8 }
                    });
                    keyModel = service.GenerateKeyOnly<RsaKeyModel>(options.Algorithm);
                    File.WriteAllText(options.Output, keyModel.ToJson(true));
                    break;

                case CryptoAlgorithmType.ECC:
                    CryptoConfig.Override(new CryptoConfigModel
                    {
                        KeyDirectory = Path.GetDirectoryName(options.Output)!,
                        ECC = new EccConfig { Curve = options.Curve ?? EccCurveType.NistP256 }
                    });
                    keyModel = service.GenerateKeyOnly<EccKeyModel>(options.Algorithm);
                    File.WriteAllText(options.Output, keyModel.ToJson(true));
                    break;

                default:
                    Console.WriteLine("不支援的演算法");
                    return;
            }

            Console.WriteLine($"{options.Algorithm} 金鑰已產生 -> {options.Output}");
        }
    }
}