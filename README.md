<img src="./assets/logo.png" alt="Logo" width="200"/>

CryptoSuite 是一個用於 .NET 的模組化加解密套件，支援 AES、RSA、ECC 等多種演算法，並具備金鑰管理、設定檔讀取、簽署與驗簽等功能，適用於各類安全通訊與數位簽署應用。  
本套件以 **擴充性與可讀性為核心設計理念**，並整合多項現代加密技術，適合用於企業後端、API 安全、CLI 工具與桌面應用等場景。

---

## 📦 專案架構

```
CryptoSuite/
├── CryptoSuite.Core            # 核心介面與資料組織（演算法、金鑰模型等）
├── CryptoSuite.Helpers         # 共用工具類（Base64、檔案處理、路徑等）
├── CryptoSuite.KeyManagement   # 金鑰產生與儲存（AES / RSA / ECC）
├── CryptoSuite.Services        # 整合加解密與簽署驗簽功能
├── CryptoSuite.Extensions      # 擴充方法（EncryptWith、ToBase64 等）
├── CryptoSuite.DemoConsole     # CLI 示範用戶端
├── CryptoSuite.Cli             # 可封裝成 dotnet global tool 的 CLI 工具
├── CryptoSuite.Tests           # 單元測試（xUnit）
├── crypto_config.json          # 設定檔範本
└── LICENSE / LICENSE.zh-TW     # 授權條款與繁中摘要
```

---

## ⚙️ 功能特色

- ✅ 支援 AES / RSA / ECC 加解密與簽署
- ✅ 金鑰資料夾自動管理，含隨機命名機制（8 碼英數）
- ✅ 讀取自訂設定檔 `crypto_config.json`，集中管理
- ✅ 使用 Newtonsoft.Json 處理所有 JSON
- ✅ 支援 `ICryptoService` 與 `ICryptoKeyService` 接口
- ✅ 擴充方法方便使用：`data.EncryptWith(...)`、`"abc".ToBytes()` 等
- ✅ 可搭配 Autofac 依賴加入（Demo 已示範）
- ✅ 多層模組化結構，方便獨立使用或封裝為 NuGet 套件

---

## 🚀 快速上手

### 🔐 加解密範例（使用 Service）
```csharp
var data = "Hello World!";
var keyModel = ...; // 從 key loader 載入的金鑰模型
var encrypted = cryptoService.Encrypt(data, CryptoAlgorithm.AES, keyModel);
var decrypted = cryptoService.Decrypt(encrypted, CryptoAlgorithm.AES, keyModel);
```

### 💡 加解密範例（使用 Extension）
```csharp
var encrypted = "Hello World!".EncryptWith(CryptoAlgorithm.AES, keyModel);
var plainText = encrypted.DecryptWith(CryptoAlgorithm.AES, keyModel);
```

---

## 🧰 單元測試

- 使用 [xUnit](https://xunit.net/)
- 含 AES / RSA / ECC 金鑰產生與驗證測試
- 擴充方法皆有對應測試（CryptoExtensions / StringExtensions / ByteExtensions）

執行方式：

```bash
dotnet test
```

---

## 📂 設定檔格式 `crypto_config.json`

```json
{
  "KeyRootPath": "keys",
  "AES": { "KeySize": 256 },
  "RSA": { "KeySize": 2048 },
  "ECC": { "CurveName": "nistP256" }
}
```

---

## 📄 License

CryptoSuite is licensed under the [Apache License 2.0](./LICENSE).

本專案亦提供 [繁體中文授權條款摘要](./LICENSE.zh-TW)，供中文使用者參考（非正式法律文件，以原始英文授權為準）。

---

## 🙌 貢獻方式（未來開放）

目前專案由作者個人維護中，未來將一步步開放外部貢獻，敬請關注。

如有建議或回報 bug，歡迎透過 [Issues](https://github.com/rexmax1018/CryptoSuite/issues) 與我聯絡。

---

## 🔗 延伸規劃（Roadmap）

- [ ] JWT 套件整合（作為安全驗證模組）
- [ ] 支援 NuGet 發展（CryptoSuite.Core / Extensions / Services）
- [ ] 跨平台 CLI 工具封裝（dotnet tool install）
- [ ] 支援 OpenAPI 的加密參數中介層（可套用於 Swagger）

