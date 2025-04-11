using CryptoSuite.KeyManagement.Enums;
using CryptoSuite.KeyManagement.Interfaces;
using CryptoSuite.KeyManagement.KeyLoaders;
using CryptoSuite.KeyManagement.Models;

namespace CryptoSuite.KeyManagement.Factories;

/// <summary>
/// 根據指定加密演算法產生對應的金鑰載入器實例。
/// </summary>
public class KeyLoaderFactory : IKeyLoaderFactory
{
    /// <summary>
    /// 建立對應演算法的金鑰載入器實例。
    /// </summary>
    /// <typeparam name="TModel">金鑰模型類型</typeparam>
    /// <param name="algorithm">加密演算法類型</param>
    /// <returns>對應的 IKeyLoader 實例</returns>
    /// <exception cref="NotSupportedException">若不支援的演算法則拋出</exception>
    public IKeyLoader<TModel> Create<TModel>(CryptoAlgorithmType algorithm) where TModel : class
    {
        return algorithm switch
        {
            CryptoAlgorithmType.AES when typeof(TModel) == typeof(SymmetricKeyModel) =>
                (IKeyLoader<TModel>)new AesKeyLoader(),

            CryptoAlgorithmType.RSA when typeof(TModel) == typeof(RsaKeyModel) =>
                (IKeyLoader<TModel>)new RsaKeyLoader(),

            CryptoAlgorithmType.ECC when typeof(TModel) == typeof(EccKeyModel) =>
                (IKeyLoader<TModel>)new EccKeyLoader(),

            _ => throw new NotSupportedException($"不支援的演算法或模型類型：{algorithm} → {typeof(TModel).Name}")
        };
    }
}