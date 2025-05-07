using CommandLine;
using CryptoSuite.KeyManagement.Enums;

namespace CryptoSuite.Tool.Commands
{
    /// <summary>
    /// 定義 encrypt 指令的參數
    /// </summary>
    [Verb("encrypt", HelpText = "加密檔案 (AES, RSA)")]
    public class EncryptOptions
    {
        [Option("alg", Required = true, HelpText = "加密演算法 (AES, RSA)")]
        public CryptoAlgorithmType Algorithm { get; set; }

        [Option("key", Required = true, HelpText = "金鑰檔案路徑")]
        public string KeyPath { get; set; }

        [Option("in", Required = true, HelpText = "輸入檔案路徑")]
        public string InputFile { get; set; }

        [Option("out", Required = true, HelpText = "輸出檔案路徑")]
        public string OutputFile { get; set; }
    }
}