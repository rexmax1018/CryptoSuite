using Autofac;
using CryptoSuite.DemoConsole.Demos;
using CryptoSuite.KeyManagement.Factories;
using CryptoSuite.KeyManagement.Interfaces;
using CryptoSuite.Services;
using CryptoSuite.Services.Interfaces;

namespace CryptoSuite.DemoConsole
{
    public class CryptoSuiteModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CryptoKeyService>()
                   .As<ICryptoKeyService>()
                   .SingleInstance();

            builder.RegisterType<CryptoService>()
                   .As<ICryptoService>()
                   .SingleInstance();

            builder.RegisterType<KeyGeneratorFactory>()
                   .As<IKeyGeneratorFactory>()
                   .SingleInstance();

            builder.RegisterType<KeyLoaderFactory>()
                   .As<IKeyLoaderFactory>()
                   .SingleInstance();

            // Demo 類別註冊
            builder.RegisterType<AesDemo>().AsSelf().SingleInstance();
            builder.RegisterType<RsaDemo>().AsSelf().SingleInstance();
            builder.RegisterType<EccDemo>().AsSelf().SingleInstance();
        }
    }
}