﻿using CryptoSuite.KeyManagement.Interfaces;
using CryptoSuite.KeyManagement.Models;
using Newtonsoft.Json;
using System.Text;

namespace CryptoSuite.KeyManagement.KeyLoaders;

/// <summary>
/// RSA 金鑰載入器，從 JSON、Base64、Stream 等來源載入 PEM 格式金鑰。
/// </summary>
public class RsaKeyLoader : IKeyLoader<RsaKeyModel>
{
    /// <inheritdoc/>
    public RsaKeyModel LoadFromFile(string path)
    {
        var json = File.ReadAllText(path);
        return Deserialize(json);
    }

    /// <inheritdoc/>
    public async Task<RsaKeyModel> LoadFromFileAsync(string path)
    {
        var json = await File.ReadAllTextAsync(path);
        return Deserialize(json);
    }

    /// <inheritdoc/>
    public RsaKeyModel LoadFromString(string content)
    {
        return Deserialize(content);
    }

    /// <inheritdoc/>
    public RsaKeyModel LoadFromBase64(string base64)
    {
        try
        {
            var json = Encoding.UTF8.GetString(Convert.FromBase64String(base64));
            return Deserialize(json);
        }
        catch (FormatException ex)
        {
            throw new InvalidDataException("無效的 Base64 字串格式", ex);
        }
    }

    /// <inheritdoc/>
    public RsaKeyModel LoadFromStream(Stream stream)
    {
        using var reader = new StreamReader(stream, Encoding.UTF8);
        var json = reader.ReadToEnd();
        return Deserialize(json);
    }

    /// <summary>
    /// 將 JSON 反序列化為 RsaKeyModel。
    /// </summary>
    /// <param name="json">JSON 字串</param>
    /// <returns>還原的 RSA 金鑰模型</returns>
    private static RsaKeyModel Deserialize(string json)
    {
        return JsonConvert.DeserializeObject<RsaKeyModel>(json)
               ?? throw new InvalidDataException("無法解析 RSA 金鑰資料");
    }
}