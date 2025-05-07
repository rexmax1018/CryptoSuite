using CommandLine;
using CryptoSuite.KeyManagement.Enums;

namespace CryptoSuite.Tool.Commands
{
    [Verb("verify", HelpText = "驗章檔案 (RSA, ECC)")]
    public class VerifyOptions
    {
        [Option("alg", Required = true, HelpText = "驗章演算法 (RSA, ECC)")]
        public CryptoAlgorithmType Algorithm { get; set; }

        [Option("key", Required = true, HelpText = "公鑰檔案路徑")]
        public string KeyPath { get; set; }

        [Option("in", Required = true, HelpText = "輸入檔案路徑")]
        public string InputFile { get; set; }

        [Option("sig", Required = true, HelpText = "簽章檔案路徑")]
        public string SignatureFile { get; set; }
    }
}