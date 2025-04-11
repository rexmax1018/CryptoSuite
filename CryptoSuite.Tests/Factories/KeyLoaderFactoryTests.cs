using CryptoSuite.KeyManagement.Enums;
using CryptoSuite.KeyManagement.Factories;
using CryptoSuite.KeyManagement.Interfaces;
using CryptoSuite.KeyManagement.Models;

namespace CryptoSuite.Tests.Factories
{
    public class KeyLoaderFactoryTests
    {
        private readonly KeyLoaderFactory _factory = new();

        [Fact(DisplayName = "應傳回 AES 的 AesKeyLoader 實例")]
        public void Create_AES_ShouldReturnAesKeyLoader()
        {
            var loader = _factory.Create<SymmetricKeyModel>(CryptoAlgorithmType.AES);
            Assert.IsAssignableFrom<IKeyLoader<SymmetricKeyModel>>(loader);
        }

        [Fact(DisplayName = "應傳回 RSA 的 RsaKeyLoader 實例")]
        public void Create_RSA_ShouldReturnRsaKeyLoader()
        {
            var loader = _factory.Create<RsaKeyModel>(CryptoAlgorithmType.RSA);
            Assert.IsAssignableFrom<IKeyLoader<RsaKeyModel>>(loader);
        }

        [Fact(DisplayName = "應傳回 ECC 的 EccKeyLoader 實例")]
        public void Create_ECC_ShouldReturnEccKeyLoader()
        {
            var loader = _factory.Create<EccKeyModel>(CryptoAlgorithmType.ECC);
            Assert.IsAssignableFrom<IKeyLoader<EccKeyModel>>(loader);
        }

        [Fact(DisplayName = "不相符的模型型別應拋出 NotSupportedException")]
        public void Create_InvalidModelType_ShouldThrow()
        {
            Assert.Throws<NotSupportedException>(() =>
            {
                _factory.Create<string>(CryptoAlgorithmType.ECC);
            });
        }
    }
}