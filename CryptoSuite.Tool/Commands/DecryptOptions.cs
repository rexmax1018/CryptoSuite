using CommandLine;
using CryptoSuite.KeyManagement.Enums;

namespace CryptoSuite.Tool.Commands
{
    /// <summary>
    /// 定義 decrypt 指令的參數
    /// </summary>
    [Verb("decrypt", HelpText = "解密檔案 (AES, RSA)")]
    public class DecryptOptions
    {
        [Option("alg", Required = true, HelpText = "加密演算法 (AES, RSA)")]
        public CryptoAlgorithmType Algorithm { get; set; }

        [Option("key", Required = true, HelpText = "金鑰檔案路徑")]
        public string KeyPath { get; set; }

        [Option("in", Required = true, HelpText = "輸入加密檔案路徑")]
        public string InputFile { get; set; }

        [Option("out", Required = true, HelpText = "解密後輸出檔案路徑")]
        public string OutputFile { get; set; }
    }
}