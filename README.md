# CryptoSuite

CryptoSuite 是一個基於 .NET 8 的加密套件，提供多種加密、解密、金鑰管理和輔助功能，旨在簡化開發人員在 .NET 平台上的加密操作。該專案使用 Visual Studio 2022 作為主要開發環境，並採用了 `Newtonsoft.Json` 作為 JSON 處理庫。

## 功能特性

- **加密與解密**：提供對稱（如 AES）與非對稱（如 RSA）加密演算法的實作，方便進行資料的加解密操作。
- **金鑰管理**：包含金鑰的生成、儲存與載入功能，確保金鑰的安全性與可用性。
- **配置管理**：透過 `CryptoSuite.Config` 命名空間（使用別名 `ConfigRoot`）進行加密相關配置的管理。
- **輔助工具**：提供各種輔助方法，簡化加密相關的操作，如編碼轉換、隨機數生成等。

## 專案結構

- `CryptoSuite.Config`：負責加密套件的配置管理，使用別名 `ConfigRoot`。
- `CryptoSuite.Core`：核心功能模組，包含主要的加密與解密邏輯。
- `CryptoSuite.Encryption`：實作具體的加密演算法，如 AES、RSA 等。
- `CryptoSuite.KeyManagement`：提供金鑰的生成、儲存與載入功能。
- `CryptoSuite.Helpers`：輔助工具類別，提供編碼轉換、隨機數生成等功能。
- `CryptoSuite.Services`：服務層，封裝高階的加密服務供外部呼叫。
- `CryptoSuite.DemoConsole`：主控台示例應用程式，展示如何使用 CryptoSuite 的各項功能。
- `CryptoSuite.Tests`：單元測試專案，確保各模組的功能正確性。

## 安裝與使用

1. **克隆儲存庫**：

   ```bash
   git clone https://github.com/rexmax1018/CryptoSuite.git
