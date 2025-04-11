using Autofac;
using Autofac.Extensions.DependencyInjection;
using CryptoSuite.DemoConsole;
using CryptoSuite.DemoConsole.Demos;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();
var builder = new ContainerBuilder();

builder.Populate(serviceCollection);
builder.RegisterModule<CryptoSuiteModule>();

var container = builder.Build();
using var scope = container.BeginLifetimeScope();

Console.WriteLine("=== CryptoSuite Demo Console ===\n");

var aesDemo = scope.Resolve<AesDemo>();
aesDemo.Run();

Console.WriteLine();

var rsaDemo = scope.Resolve<RsaDemo>();
rsaDemo.Run();

Console.WriteLine();

var eccDemo = scope.Resolve<EccDemo>();
eccDemo.Run();

Console.WriteLine("\n=== Demo 結束 ===");