# CryptoSuite
## 貢獻

歡迎提交問題回報與功能建議！請參考 [CONTRIBUTING.md](CONTRIBUTING.md) 以了解如何參與貢獻。

## 授權

此專案採用 [MIT 授權](LICENSE)。
## 範例程式

以下是使用 RSA 加密的簡單範例：
### 建置專案

1. 使用 Visual Studio 2022 開啟解決方案檔案 `CryptoSuite.sln`。
2. 選擇 `Release` 或 `Debug` 模式，並建置專案。

### 執行範例

1. 設定啟動專案為 `CryptoSuite.DemoConsole`。
2. 執行專案以查看範例程式的運行結果。

### 測試

執行以下指令以執行所有單元測試：
CryptoSuite 是一個基於 .NET 8 的加密工具套件，提供多種加密、解密、金鑰管理與數據處理功能，旨在幫助開發者在 .NET 平台上輕鬆實現加密相關的應用程式開發。此專案使用 Visual Studio 2022 開發，並依賴 `Newtonsoft.Json` 進行 JSON 處理。

## 功能特性

- **加密與解密**：
  - 支援對稱加密（如 AES）。
  - 支援非對稱加密（如 RSA）。
- **金鑰管理**：
  - 提供金鑰生成、存儲與載入功能，確保金鑰的安全性與可用性。
- **配置管理**：
  - 通過 `CryptoSuite.Config` 管理加密相關的配置，並支持以 `ConfigRoot` 為基礎的靈活配置。
- **數據處理**：
  - 提供多種數據處理工具，如數據簽名與驗證，適用於多種場景。

## 專案結構

- `CryptoSuite.Config`：負責加密與配置的管理，基於 `ConfigRoot`。
- `CryptoSuite.Core`：核心功能模組，包含主要的加密與解密邏輯。
- `CryptoSuite.Encryption`：實現具體的加密算法（如 AES、RSA）。
- `CryptoSuite.KeyManagement`：提供金鑰生成、存儲與載入功能。
- `CryptoSuite.Helpers`：包含輔助工具，如數據格式轉換與錯誤處理。
- `CryptoSuite.Services`：提供加密相關的服務，支持業務邏輯集成。
- `CryptoSuite.DemoConsole`：範例應用程式，展示如何使用 CryptoSuite 的功能。
- `CryptoSuite.Tests`：單元測試模組，確保功能的正確性與穩定性。

## 安裝與使用

### 下載專案

使用以下指令下載專案：
