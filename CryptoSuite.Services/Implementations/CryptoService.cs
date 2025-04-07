﻿using CryptoSuite.KeyManagement.Enums;
using CryptoSuite.KeyManagement.Models;
using CryptoSuite.Services.Interfaces;
using System.Security.Cryptography;

namespace CryptoSuite.Services.Implementations
{
    /// <summary>
    /// 提供 AES、RSA、ECC 的加解密、簽章與驗章等整合性功能。
    /// </summary>
    public class CryptoService : ICryptoService
    {
        /// <summary>
        /// 加密資料。
        /// </summary>
        /// <typeparam name="TKeyModel">金鑰模型型別</typeparam>
        /// <param name="data">要加密的資料</param>
        /// <param name="algorithm">加密演算法</param>
        /// <param name="keyModel">金鑰模型</param>
        /// <returns>加密後資料</returns>
        public byte[] Encrypt<TKeyModel>(byte[] data, CryptoAlgorithmType algorithm, TKeyModel keyModel) where TKeyModel : class
        {
            return algorithm switch
            {
                CryptoAlgorithmType.AES when keyModel is SymmetricKeyModel aes => AesEncrypt(data, aes),
                _ => throw new NotSupportedException($"Encrypt 不支援的演算法或金鑰類型: {algorithm}")
            };
        }

        /// <summary>
        /// 解密資料。
        /// </summary>
        /// <typeparam name="TKeyModel">金鑰模型型別</typeparam>
        /// <param name="encrypted">已加密的資料</param>
        /// <param name="algorithm">加密演算法</param>
        /// <param name="keyModel">金鑰模型</param>
        /// <returns>解密後資料</returns>
        public byte[] Decrypt<TKeyModel>(byte[] encrypted, CryptoAlgorithmType algorithm, TKeyModel keyModel) where TKeyModel : class
        {
            return algorithm switch
            {
                CryptoAlgorithmType.AES when keyModel is SymmetricKeyModel aes => AesDecrypt(encrypted, aes),
                _ => throw new NotSupportedException($"Decrypt 不支援的演算法或金鑰類型: {algorithm}")
            };
        }

        /// <summary>
        /// 使用私鑰對資料進行簽章。
        /// </summary>
        /// <typeparam name="TKeyModel">金鑰模型型別</typeparam>
        /// <param name="data">原始資料</param>
        /// <param name="algorithm">簽章演算法</param>
        /// <param name="privateKeyModel">私鑰模型</param>
        /// <returns>簽章結果</returns>
        public byte[] Sign<TKeyModel>(byte[] data, CryptoAlgorithmType algorithm, TKeyModel privateKeyModel) where TKeyModel : class
        {
            return algorithm switch
            {
                CryptoAlgorithmType.RSA when privateKeyModel is RsaKeyModel rsa => RsaSign(data, rsa),
                CryptoAlgorithmType.ECC when privateKeyModel is EccKeyModel ecc => EccSign(data, ecc),
                _ => throw new NotSupportedException($"Sign 不支援的演算法或金鑰類型: {algorithm}")
            };
        }

        /// <summary>
        /// 驗證簽章正確性。
        /// </summary>
        /// <typeparam name="TKeyModel">金鑰模型型別</typeparam>
        /// <param name="data">原始資料</param>
        /// <param name="signature">簽章資料</param>
        /// <param name="algorithm">驗章演算法</param>
        /// <param name="publicKeyModel">公鑰模型</param>
        /// <returns>是否驗證成功</returns>
        public bool Verify<TKeyModel>(byte[] data, byte[] signature, CryptoAlgorithmType algorithm, TKeyModel publicKeyModel) where TKeyModel : class
        {
            return algorithm switch
            {
                CryptoAlgorithmType.RSA when publicKeyModel is RsaKeyModel rsa => RsaVerify(data, signature, rsa),
                CryptoAlgorithmType.ECC when publicKeyModel is EccKeyModel ecc => EccVerify(data, signature, ecc),
                _ => throw new NotSupportedException($"Verify 不支援的演算法或金鑰類型: {algorithm}")
            };
        }

        #region AES

        /// <summary>
        /// 使用 AES 演算法加密資料。
        /// </summary>
        private byte[] AesEncrypt(byte[] data, SymmetricKeyModel keyModel)
        {
            using var aes = Aes.Create();
            aes.Key = keyModel.Key;
            aes.IV = keyModel.IV;

            using var encryptor = aes.CreateEncryptor();
            return encryptor.TransformFinalBlock(data, 0, data.Length);
        }

        /// <summary>
        /// 使用 AES 演算法解密資料。
        /// </summary>
        private byte[] AesDecrypt(byte[] encrypted, SymmetricKeyModel keyModel)
        {
            using var aes = Aes.Create();
            aes.Key = keyModel.Key;
            aes.IV = keyModel.IV;

            using var decryptor = aes.CreateDecryptor();
            return decryptor.TransformFinalBlock(encrypted, 0, encrypted.Length);
        }

        #endregion

        #region RSA

        /// <summary>
        /// 使用 RSA 私鑰進行簽章。
        /// </summary>
        private byte[] RsaSign(byte[] data, RsaKeyModel keyModel)
        {
            using var rsa = RSA.Create();
            rsa.ImportFromPem(keyModel.PrivateKeyPem.ToCharArray());
            return rsa.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }

        /// <summary>
        /// 使用 RSA 公鑰進行簽章驗證。
        /// </summary>
        private bool RsaVerify(byte[] data, byte[] signature, RsaKeyModel keyModel)
        {
            using var rsa = RSA.Create();
            rsa.ImportFromPem(keyModel.PublicKeyPem.ToCharArray());
            return rsa.VerifyData(data, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }

        #endregion

        #region ECC

        /// <summary>
        /// 使用 ECC 私鑰進行簽章。
        /// </summary>
        private byte[] EccSign(byte[] data, EccKeyModel keyModel)
        {
            using var ecdsa = ECDsa.Create();
            ecdsa.ImportFromPem(keyModel.PrivateKeyPem.ToCharArray());
            return ecdsa.SignData(data, HashAlgorithmName.SHA256);
        }

        /// <summary>
        /// 使用 ECC 公鑰驗證簽章。
        /// </summary>
        private bool EccVerify(byte[] data, byte[] signature, EccKeyModel keyModel)
        {
            using var ecdsa = ECDsa.Create();
            ecdsa.ImportFromPem(keyModel.PublicKeyPem.ToCharArray());
            return ecdsa.VerifyData(data, signature, HashAlgorithmName.SHA256);
        }

        #endregion
    }
}
