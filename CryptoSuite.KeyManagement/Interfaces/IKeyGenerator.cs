using CryptoSuite.KeyManagement.Models;

namespace CryptoSuite.KeyManagement.Interfaces
{
    /// <summary>
    /// 金鑰產生器介面，泛型版本，TModel 為金鑰模型型別。
    /// </summary>
    /// <typeparam name="TModel">金鑰模型型別</typeparam>
    public interface IKeyGenerator<TModel> where TModel : class
    {
        /// <summary>
        /// 僅產生金鑰並回傳模型
        /// </summary>
        /// <returns>金鑰模型</returns>
        TModel GenerateKeyOnly();

        /// <summary>
        /// 產生金鑰並儲存至檔案
        /// </summary>
        /// <param name="filePath">自訂儲存路徑（可選）</param>
        /// <returns>產生結果</returns>
        KeyGenerationResult GenerateAndSaveKey(string? filePath = null);

        /// <summary>
        /// 非同步產生金鑰並儲存至檔案
        /// </summary>
        /// <param name="filePath">自訂儲存路徑（可選）</param>
        /// <returns>產生結果</returns>
        Task<KeyGenerationResult> GenerateAndSaveKeyAsync(string? filePath = null);
    }
}
