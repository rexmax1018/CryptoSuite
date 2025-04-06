using CryptoSuite.KeyManagement.Enums;
using CryptoSuite.KeyManagement.Interfaces;
using CryptoSuite.KeyManagement.Models;
using Newtonsoft.Json;
using System.Security.Cryptography;
using ConfigRoot = CryptoSuite.Config.CryptoConfig;

namespace CryptoSuite.KeyManagement.KeyGenerators
{
    /// <summary>
    /// AES 金鑰產生器，產生 Key 與 IV 並儲存為 JSON 檔案。
    /// </summary>
    public class AesKeyGenerator : IKeyGenerator<SymmetricKeyModel>
    {
        /// <summary>
        /// 僅產生 AES 金鑰模型（Key + IV）
        /// </summary>
        /// <returns>產生的對稱式金鑰模型</returns>
        public SymmetricKeyModel GenerateKeyOnly()
        {
            var keySizeInBits = ConfigRoot.Current.AES.KeySize;
            var keySizeInBytes = keySizeInBits / 8;

            using var aes = Aes.Create();
            aes.KeySize = keySizeInBits;
            aes.GenerateKey();
            aes.GenerateIV();

            return new SymmetricKeyModel
            {
                Key = aes.Key,
                IV = aes.IV
            };
        }

        /// <summary>
        /// 產生 AES 金鑰並儲存為 JSON 檔案（同步）
        /// </summary>
        /// <param name="filePath">自訂儲存路徑，預設為自動產生</param>
        /// <returns>金鑰產生結果，包含儲存路徑與演算法</returns>
        public KeyGenerationResult GenerateAndSaveKey(string? filePath = null)
        {
            var model = GenerateKeyOnly();
            var fileName = filePath ?? ConfigRoot.GenerateKeyFileName(".json");
            var fullPath = ConfigRoot.GetKeyPath("AES", fileName);

            Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);
            var json = JsonConvert.SerializeObject(model, Formatting.Indented);
            File.WriteAllText(fullPath, json);

            return new KeyGenerationResult
            {
                KeyFilePath = fullPath,
                Algorithm = CryptoAlgorithmType.AES
            };
        }

        /// <summary>
        /// 非同步產生 AES 金鑰並儲存為 JSON 檔案
        /// </summary>
        /// <param name="filePath">自訂儲存路徑，預設為自動產生</param>
        /// <returns>金鑰產生結果，包含儲存路徑與演算法</returns>
        public async Task<KeyGenerationResult> GenerateAndSaveKeyAsync(string? filePath = null)
        {
            var model = GenerateKeyOnly();
            var fileName = filePath ?? ConfigRoot.GenerateKeyFileName(".json");
            var fullPath = ConfigRoot.GetKeyPath("AES", fileName);

            Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);
            var json = JsonConvert.SerializeObject(model, Formatting.Indented);
            await File.WriteAllTextAsync(fullPath, json);

            return new KeyGenerationResult
            {
                KeyFilePath = fullPath,
                Algorithm = CryptoAlgorithmType.AES
            };
        }
    }
}