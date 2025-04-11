using CryptoSuite.Config;
using CryptoSuite.KeyManagement.KeyGenerators;
using CryptoSuite.KeyManagement.Models;
using Newtonsoft.Json;

namespace CryptoSuite.Tests.KeyGenerators;

public class AesKeyGeneratorTests
{
    private static void InitializeCryptoConfig()
    {
        CryptoConfig.Override(new CryptoConfigModel
        {
            KeyDirectory = Path.GetTempPath(),
            AES = new AesConfig
            {
                KeySize = 256
            }
        });
    }

    [Fact(DisplayName = "GenerateAndSaveKey 應建立合法的 JSON 檔")]
    public void GenerateAndSaveKey_ShouldCreateValidJsonFile()
    {
        InitializeCryptoConfig();

        var generator = new AesKeyGenerator();
        var result = generator.GenerateAndSaveKey();

        Assert.True(File.Exists(result.KeyFilePath));

        string json = File.ReadAllText(result.KeyFilePath);
        var model = JsonConvert.DeserializeObject<SymmetricKeyModel>(json);

        Assert.NotNull(model);
        Assert.Equal(32, model.Key.Length); // AES-256 key
        Assert.Equal(16, model.IV.Length);  // AES IV 長度

        File.Delete(result.KeyFilePath);
    }

    [Fact(DisplayName = "GenerateAndSaveKeyAsync 應建立合法的 JSON 檔（非同步）")]
    public async Task GenerateAndSaveKeyAsync_ShouldCreateValidJsonFileAsync()
    {
        InitializeCryptoConfig();

        var generator = new AesKeyGenerator();
        var result = await generator.GenerateAndSaveKeyAsync();

        Assert.True(File.Exists(result.KeyFilePath));

        string json = await File.ReadAllTextAsync(result.KeyFilePath);
        var model = JsonConvert.DeserializeObject<SymmetricKeyModel>(json);

        Assert.NotNull(model);
        Assert.Equal(32, model.Key.Length);
        Assert.Equal(16, model.IV.Length);

        File.Delete(result.KeyFilePath);
    }

    [Fact(DisplayName = "使用自訂路徑儲存應正確產生金鑰檔")]
    public void GenerateAndSaveKey_WithCustomPath_ShouldWriteCorrectFile()
    {
        InitializeCryptoConfig();

        string path = Path.Combine(Path.GetTempPath(), $"test_{Guid.NewGuid()}.json");

        var generator = new AesKeyGenerator();
        var result = generator.GenerateAndSaveKey(path);

        Assert.True(File.Exists(path));
        Assert.Equal(path, result.KeyFilePath);

        var json = File.ReadAllText(path);
        var model = JsonConvert.DeserializeObject<SymmetricKeyModel>(json);

        Assert.NotNull(model);
        Assert.Equal(32, model.Key.Length);
        Assert.Equal(16, model.IV.Length);

        File.Delete(path);
    }

    [Fact(DisplayName = "GenerateKeyOnly 應正確產生 SymmetricKeyModel")]
    public void GenerateKeyOnly_ShouldReturnValidSymmetricKeyModel()
    {
        InitializeCryptoConfig();

        var generator = new AesKeyGenerator();
        var model = generator.GenerateKeyOnly();

        Assert.NotNull(model);
        Assert.Equal(32, model.Key.Length);
        Assert.Equal(16, model.IV.Length);
    }
}