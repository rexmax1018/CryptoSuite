using CommandLine;
using CryptoSuite.KeyManagement.Enums;

namespace CryptoSuite.Tool.Commands
{
    [Verb("sign", HelpText = "簽章檔案 (RSA, ECC)")]
    public class SignOptions
    {
        [Option("alg", Required = true, HelpText = "簽章演算法 (RSA, ECC)")]
        public CryptoAlgorithmType Algorithm { get; set; }

        [Option("key", Required = true, HelpText = "私鑰檔案路徑")]
        public string KeyPath { get; set; }

        [Option("in", Required = true, HelpText = "輸入檔案路徑")]
        public string InputFile { get; set; }

        [Option("out", Required = true, HelpText = "簽章輸出檔案路徑")]
        public string OutputFile { get; set; }
    }
}