using CryptoSuite.Config.Enums;
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
    /// ECC 金鑰產生器，產生 PEM 格式的 ECDsa 公私鑰。
    /// </summary>
    public class EccKeyGenerator : IKeyGenerator<EccKeyModel>
    {
        /// <summary>
        /// 僅產生 ECC 金鑰模型（PEM 公私鑰 + 曲線）
        /// </summary>
        public EccKeyModel GenerateKeyOnly()
        {
            var curve = ConfigRoot.Current.ECC.Curve;
            using var ecdsa = ECDsa.Create(GetCurve(curve));

            var privateKey = ExportPrivateKeyPem(ecdsa);
            var publicKey = ExportPublicKeyPem(ecdsa);

            return new EccKeyModel
            {
                PrivateKey = privateKey,
                PublicKey = publicKey,
                Curve = curve,
                CreatedAt = DateTime.UtcNow
            };
        }

        /// <summary>
        /// 同步產生並儲存 ECC 金鑰 JSON 檔案
        /// </summary>
        public KeyGenerationResult GenerateAndSaveKey(string? filePath = null)
        {
            var model = GenerateKeyOnly();
            var fileName = filePath ?? ConfigRoot.GenerateKeyFileName(".json");
            var fullPath = ConfigRoot.GetKeyPath("ECC", fileName);

            Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);
            var json = JsonConvert.SerializeObject(model, Formatting.Indented);
            File.WriteAllText(fullPath, json);

            return new KeyGenerationResult
            {
                KeyFilePath = fullPath,
                Algorithm = CryptoAlgorithmType.ECC
            };
        }

        /// <summary>
        /// 非同步產生並儲存 ECC 金鑰 JSON 檔案
        /// </summary>
        public async Task<KeyGenerationResult> GenerateAndSaveKeyAsync(string? filePath = null)
        {
            var model = GenerateKeyOnly();
            var fileName = filePath ?? ConfigRoot.GenerateKeyFileName(".json");
            var fullPath = ConfigRoot.GetKeyPath("ECC", fileName);

            Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);
            var json = JsonConvert.SerializeObject(model, Formatting.Indented);
            await File.WriteAllTextAsync(fullPath, json);

            return new KeyGenerationResult
            {
                KeyFilePath = fullPath,
                Algorithm = CryptoAlgorithmType.ECC
            };
        }

        /// <summary>
        /// 匯出私鑰為 PEM 格式（PKCS#8）
        /// </summary>
        private static string ExportPrivateKeyPem(ECDsa ecdsa)
        {
            var keyBytes = ecdsa.ExportPkcs8PrivateKey();
            return PemEncode("PRIVATE KEY", keyBytes);
        }

        /// <summary>
        /// 匯出公鑰為 PEM 格式（SubjectPublicKeyInfo）
        /// </summary>
        private static string ExportPublicKeyPem(ECDsa ecdsa)
        {
            var keyBytes = ecdsa.ExportSubjectPublicKeyInfo();
            return PemEncode("PUBLIC KEY", keyBytes);
        }

        /// <summary>
        /// 以 PEM 格式包裝位元組資料
        /// </summary>
        private static string PemEncode(string label, byte[] data)
        {
            var base64 = Convert.ToBase64String(data);
            var sb = new StringBuilder();
            sb.AppendLine($"-----BEGIN {label}-----");
            for (int i = 0; i < base64.Length; i += 64)
                sb.AppendLine(base64.Substring(i, Math.Min(64, base64.Length - i)));
            sb.AppendLine($"-----END {label}-----");
            return sb.ToString();
        }

        /// <summary>
        /// 將 enum 轉為 ECC 對應曲線實體
        /// </summary>
        private static ECCurve GetCurve(EccCurveType type) => type switch
        {
            EccCurveType.NistP256 => ECCurve.NamedCurves.nistP256,
            EccCurveType.NistP384 => ECCurve.NamedCurves.nistP384,
            EccCurveType.NistP521 => ECCurve.NamedCurves.nistP521,
            EccCurveType.Secp256k1 => ECCurve.CreateFromFriendlyName("secP256k1"),
            _ => throw new NotSupportedException($"不支援的橢圓曲線：{type}")
        };
    }
}