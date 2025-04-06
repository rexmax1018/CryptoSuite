using CryptoSuite.Config.Enums;

namespace CryptoSuite.KeyManagement.Models
{
    /// <summary>
    /// ECC 金鑰模型，包含 PEM 格式的公私鑰與使用曲線等資訊。
    /// </summary>
    public class EccKeyModel
    {
        /// <summary>
        /// 公鑰（PEM 格式）
        /// </summary>
        public string PublicKeyPem { get; set; } = string.Empty;

        /// <summary>
        /// 私鑰（PEM 格式）
        /// </summary>
        public string PrivateKeyPem { get; set; } = string.Empty;

        /// <summary>
        /// 使用的橢圓曲線類型
        /// </summary>
        public EccCurveType Curve { get; set; }

        /// <summary>
        /// 金鑰建立時間
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
