using CryptoSuite.KeyManagement.Enums;
using CryptoSuite.KeyManagement.Factories;
using CryptoSuite.KeyManagement.Interfaces;
using CryptoSuite.KeyManagement.Models;

namespace CryptoSuite.Tests.Factories
{
    public class KeyGeneratorFactoryTests
    {
        private readonly KeyGeneratorFactory _factory = new();

        [Fact(DisplayName = "應傳回 AES 的 AesKeyGenerator 實例")]
        public void Create_AES_ShouldReturnAesKeyGenerator()
        {
            var generator = _factory.Create<SymmetricKeyModel>(CryptoAlgorithmType.AES);
            Assert.IsAssignableFrom<IKeyGenerator<SymmetricKeyModel>>(generator);
        }

        [Fact(DisplayName = "應傳回 RSA 的 RsaKeyGenerator 實例")]
        public void Create_RSA_ShouldReturnRsaKeyGenerator()
        {
            var generator = _factory.Create<RsaKeyModel>(CryptoAlgorithmType.RSA);
            Assert.IsAssignableFrom<IKeyGenerator<RsaKeyModel>>(generator);
        }

        [Fact(DisplayName = "應傳回 ECC 的 EccKeyGenerator 實例")]
        public void Create_ECC_ShouldReturnEccKeyGenerator()
        {
            var generator = _factory.Create<EccKeyModel>(CryptoAlgorithmType.ECC);
            Assert.IsAssignableFrom<IKeyGenerator<EccKeyModel>>(generator);
        }

        [Fact(DisplayName = "不相符的模型型別應拋出 NotSupportedException")]
        public void Create_InvalidModelType_ShouldThrow()
        {
            Assert.Throws<NotSupportedException>(() =>
            {
                _factory.Create<string>(CryptoAlgorithmType.RSA);
            });
        }
    }
}