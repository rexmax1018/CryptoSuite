using CryptoSuite.KeyManagement.Enums;
using CryptoSuite.KeyManagement.Interfaces;
using CryptoSuite.KeyManagement.Models;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using ConfigRoot = CryptoSuite.Config.CryptoConfig;

namespace CryptoSuite.KeyManagement.KeyGenerators
{
    /// <summary>
    /// RSA 金鑰產生器，產生 PEM 格式的公私鑰並儲存至檔案。
    /// </summary>
    public class RsaKeyGenerator : IKeyGenerator<RsaKeyModel>
    {
        /// <summary>
        /// 僅產生 RSA 金鑰模型（包含 PEM 公私鑰）
        /// </summary>
        public RsaKeyModel GenerateKeyOnly()
        {
            using var rsa = RSA.Create(ConfigRoot.Current.RSA.KeySize);

            var privateKey = ExportPrivateKeyPem(rsa);
            var publicKey = ExportPublicKeyPem(rsa);

            return new RsaKeyModel
            {
                PrivateKey = privateKey,
                PublicKey = publicKey,
                KeySize = ConfigRoot.Current.RSA.KeySize,
                CreatedAt = DateTime.UtcNow
            };
        }

        public KeyGenerationResult GenerateAndSaveKey(string? filePath = null)
        {
            var model = GenerateKeyOnly();
            var fileName = filePath ?? ConfigRoot.GenerateKeyFileName(".pem");

            var fullPath = ConfigRoot.GetKeyPath("RSA", fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);

            var json = JsonConvert.SerializeObject(model, Formatting.Indented);
            File.WriteAllText(fullPath, json);

            return new KeyGenerationResult
            {
                KeyFilePath = fullPath,
                Algorithm = CryptoAlgorithmType.RSA
            };
        }

        public async Task<KeyGenerationResult> GenerateAndSaveKeyAsync(string? filePath = null)
        {
            var model = GenerateKeyOnly();
            var fileName = filePath ?? ConfigRoot.GenerateKeyFileName(".pem");

            var fullPath = ConfigRoot.GetKeyPath("RSA", fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);

            var json = JsonConvert.SerializeObject(model, Formatting.Indented);
            await File.WriteAllTextAsync(fullPath, json);

            return new KeyGenerationResult
            {
                KeyFilePath = fullPath,
                Algorithm = CryptoAlgorithmType.RSA
            };
        }

        /// <summary>
        /// 匯出私鑰為 PEM 格式
        /// </summary>
        private static string ExportPrivateKeyPem(RSA rsa)
        {
            var keyBytes = rsa.ExportPkcs8PrivateKey();
            return PemEncode("PRIVATE KEY", keyBytes);
        }

        /// <summary>
        /// 匯出公鑰為 PEM 格式
        /// </summary>
        private static string ExportPublicKeyPem(RSA rsa)
        {
            var keyBytes = rsa.ExportSubjectPublicKeyInfo();
            return PemEncode("PUBLIC KEY", keyBytes);
        }

        /// <summary>
        /// 將位元組資料以 PEM 格式包裝
        /// </summary>
        private static string PemEncode(string label, byte[] data)
        {
            var base64 = Convert.ToBase64String(data);
            var sb = new StringBuilder();
            sb.AppendLine($"-----BEGIN {label}-----");

            for (int i = 0; i < base64.Length; i += 64)
            {
                sb.AppendLine(base64.Substring(i, Math.Min(64, base64.Length - i)));
            }

            sb.AppendLine($"-----END {label}-----");
            return sb.ToString();
        }
    }
}