using CommandLine;
using CryptoSuite.Tool.Commands;

Parser.Default.ParseArguments<KeygenOptions, EncryptOptions, DecryptOptions, SignOptions, VerifyOptions>(args)
              .WithParsed<KeygenOptions>(opts => KeygenHandler.Handle(opts))
              .WithParsed<EncryptOptions>(opts => EncryptHandler.Handle(opts))
              .WithParsed<DecryptOptions>(opts => DecryptHandler.Handle(opts))
              .WithParsed<SignOptions>(opts => SignHandler.Handle(opts))
              .WithParsed<VerifyOptions>(opts => VerifyHandler.Handle(opts))
              .WithNotParsed(errors =>
              {
                  Console.WriteLine("CryptoSuite CLI 工具");
                  Console.WriteLine("請使用 --help 查看可用指令與參數");
              });