using CryptoSuite.KeyManagement.Enums;
using CryptoSuite.KeyManagement.Interfaces;
using CryptoSuite.KeyManagement.KeyGenerators;
using CryptoSuite.KeyManagement.Models;

namespace CryptoSuite.KeyManagement.Factories;

/// <summary>
/// 根據指定加密演算法產生對應的金鑰產生器實例。
/// </summary>
public class KeyGeneratorFactory : IKeyGeneratorFactory
{
    /// <summary>
    /// 建立對應演算法的金鑰產生器實例。
    /// </summary>
    /// <typeparam name="TModel">金鑰模型類型</typeparam>
    /// <param name="algorithm">加密演算法類型</param>
    /// <returns>對應的 IKeyGenerator 實例</returns>
    /// <exception cref="NotSupportedException">若不支援的演算法則拋出</exception>
    public IKeyGenerator<TModel> Create<TModel>(CryptoAlgorithmType algorithm) where TModel : class
    {
        return algorithm switch
        {
            CryptoAlgorithmType.AES when typeof(TModel) == typeof(SymmetricKeyModel) =>
                (IKeyGenerator<TModel>)new AesKeyGenerator(),

            CryptoAlgorithmType.RSA when typeof(TModel) == typeof(RsaKeyModel) =>
                (IKeyGenerator<TModel>)new RsaKeyGenerator(),

            CryptoAlgorithmType.ECC when typeof(TModel) == typeof(EccKeyModel) =>
                (IKeyGenerator<TModel>)new EccKeyGenerator(),

            _ => throw new NotSupportedException($"不支援的演算法或模型類型：{algorithm} → {typeof(TModel).Name}")
        };
    }
}