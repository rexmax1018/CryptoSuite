using CryptoSuite.KeyManagement.Enums;
using CryptoSuite.KeyManagement.Models;
using CryptoSuite.Services;
using System.Security.Cryptography;
using System.Text;

namespace CryptoSuite.Tests.Services
{
    public class CryptoServiceTests
    {
        private readonly CryptoService _service = new();

        [Fact(DisplayName = "AES 加解密應正確還原原文")]
        public void Aes_Encrypt_And_Decrypt_ShouldReturnOriginal()
        {
            var key = new SymmetricKeyModel
            {
                Key = new byte[32], // AES-256
                IV = new byte[16]
            };

            var data = Encoding.UTF8.GetBytes("Hello CryptoSuite");
            var encrypted = _service.Encrypt(data, CryptoAlgorithmType.AES, key);
            var decrypted = _service.Decrypt(encrypted, CryptoAlgorithmType.AES, key);

            Assert.Equal(data, decrypted);
        }

        [Fact(DisplayName = "RSA 簽章與驗章應正確對應")]
        public void Rsa_Sign_And_Verify_ShouldPass()
        {
            using var rsa = RSA.Create(2048);
            var privatePem = ExportPrivateKey(rsa);
            var publicPem = ExportPublicKey(rsa);

            var key = new RsaKeyModel
            {
                PrivateKeyPem = privatePem,
                PublicKeyPem = publicPem
            };

            var data = Encoding.UTF8.GetBytes("CryptoSuite RSA Test");
            var signature = _service.Sign(data, CryptoAlgorithmType.RSA, key);
            var isValid = _service.Verify(data, signature, CryptoAlgorithmType.RSA, key);

            Assert.True(isValid);
        }

        [Fact(DisplayName = "ECC 簽章與驗章應正確對應")]
        public void Ecc_Sign_And_Verify_ShouldPass()
        {
            using var ecc = ECDsa.Create(ECCurve.NamedCurves.nistP256);
            var privatePem = ExportPrivateKey(ecc);
            var publicPem = ExportPublicKey(ecc);

            var key = new EccKeyModel
            {
                PrivateKeyPem = privatePem,
                PublicKeyPem = publicPem
            };

            var data = Encoding.UTF8.GetBytes("CryptoSuite ECC Test");
            var signature = _service.Sign(data, CryptoAlgorithmType.ECC, key);
            var isValid = _service.Verify(data, signature, CryptoAlgorithmType.ECC, key);

            Assert.True(isValid);
        }

        private static string ExportPrivateKey(RSA rsa)
        {
            var builder = new StringBuilder();
            builder.AppendLine("-----BEGIN PRIVATE KEY-----");
            builder.AppendLine(Convert.ToBase64String(rsa.ExportPkcs8PrivateKey(), Base64FormattingOptions.InsertLineBreaks));
            builder.AppendLine("-----END PRIVATE KEY-----");
            return builder.ToString();
        }

        private static string ExportPublicKey(RSA rsa)
        {
            var builder = new StringBuilder();
            builder.AppendLine("-----BEGIN PUBLIC KEY-----");
            builder.AppendLine(Convert.ToBase64String(rsa.ExportSubjectPublicKeyInfo(), Base64FormattingOptions.InsertLineBreaks));
            builder.AppendLine("-----END PUBLIC KEY-----");
            return builder.ToString();
        }

        private static string ExportPrivateKey(ECDsa ecc)
        {
            var builder = new StringBuilder();
            builder.AppendLine("-----BEGIN PRIVATE KEY-----");
            builder.AppendLine(Convert.ToBase64String(ecc.ExportPkcs8PrivateKey(), Base64FormattingOptions.InsertLineBreaks));
            builder.AppendLine("-----END PRIVATE KEY-----");
            return builder.ToString();
        }

        private static string ExportPublicKey(ECDsa ecc)
        {
            var builder = new StringBuilder();
            builder.AppendLine("-----BEGIN PUBLIC KEY-----");
            builder.AppendLine(Convert.ToBase64String(ecc.ExportSubjectPublicKeyInfo(), Base64FormattingOptions.InsertLineBreaks));
            builder.AppendLine("-----END PUBLIC KEY-----");
            return builder.ToString();
        }
    }
}
