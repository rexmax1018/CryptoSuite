using CommandLine;
using CryptoSuite.Config.Enums;
using CryptoSuite.KeyManagement.Enums;

namespace CryptoSuite.Tool.Commands
{
    /// <summary>
    /// 定義 keygen 指令的參數
    /// </summary>
    [Verb("keygen", HelpText = "產生金鑰 (AES, RSA, ECC)")]
    public class KeygenOptions
    {
        [Option("alg", Required = true, HelpText = "加密演算法 (AES, RSA, ECC)")]
        public CryptoAlgorithmType Algorithm { get; set; }

        [Option("out", Required = true, HelpText = "輸出檔案路徑")]
        public string Output { get; set; }

        [Option("keysize", Required = false, HelpText = "金鑰大小 (AES/RSA 適用)")]
        public int? KeySize { get; set; }

        [Option("curve", Required = false, HelpText = "ECC 曲線 (ECC 適用)")]
        public EccCurveType? Curve { get; set; }
    }
}