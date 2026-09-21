# 🎨 繪圖委託管理系統 - 後端 API

本專案為「繪圖委託管理系統」的後端 RESTful API 服務，提供訪客建立委託、創作者管理訂單、排程、圖片上傳及權限驗證等功能。專案已完成正式雲端部署，並實作多重安全性與防爆破機制。

🔗 **前端儲存庫**：[CommissionVue (Vue 3)](https://github.com/tomyahoo39/CommissionVue)

---

## 🛠 技術棧 (Tech Stack)

- **Framework**: C# / ASP.NET Core Web API (.NET 10)
- **Database**: MSSQL / Entity Framework Core (Database First)
- **Authentication**: JWT (JSON Web Token)
- **Storage**: Cloudinary SDK
- **Architecture**: Layered Architecture / Dependency Injection / DTO Pattern
- **Deployment**: Render (Web API) + MonsterASP.net (MSSQL)

---

## 🌟 核心功能 (Features)

- [x] **委託項目管理**：CRUD 創作者的委託方案（價目表、委託規範）
- [x] **訂單狀態追蹤**：管理委託狀態（稿件狀態、付款狀態等）
- [x] **身份驗證與授權**：JWT Auth（區分「創作者」與「訪客」權限）
- [x] **作品與草稿圖片上傳**：整合 Cloudinary 雲端儲存 API
- [x] **雲端自動化部署**：完成 Render 與 MonsterASP.net 多雲架構部署

---

## 🛡️ 安全性與防禦機制 (Security Hardening)

為確保系統生產環境運作安全，實作了以下防禦機制：

* **防爆破與 DoS 防禦 (Flood Guard)**：
  - **Login Rate Limiting**：精準判斷「帳號存在但密碼連續錯誤 5 次」實施 10 分鐘帳號冷卻鎖定，隔離 IP/User 層級以防止惡意 DoS 鎖定。
  - **API 限流**：同 IP 一分鐘內限制僅能建立一次委託單，防範惡意灌單攻擊。
* **嚴格 JWT 機制 (Strict Token Validation)**：
  - 啟動時強制校驗金鑰強度（Key 長度 ≥ 32 bytes）。
  - 設定 `RequireExpirationTime = true` 並將 `ClockSkew` 歸零，消除過期寬限期。
* **圖片與 XSS 攻擊防護**：
  - 實作網域白名單與 Protocol (`https:`) 校驗，阻擋惡意 JavaScript 腳本注入與無效 CDN 圖片網域。
* **機密資訊隔離**：
  - 本地開發採用 `secrets.json` 隔離 Connection String 與 JwtSettings；生產環境統一使用雲端平台環境變數（Environment Variables）託管。
  - 全域錯誤處理（Global Error Handling）回傳模糊化失敗訊息，防止敏感伺服器 Stack Trace 洩漏。

---

## 📐 資料庫架構 (Database Schema)

採用 Entity Framework Core (Database First) 進行 ORM 對映，主要資料表如下：

- `commission_period`：委託期間設定
- `commission_order`：委託訂單紀錄
- `social_platform`：社群平台設定
- `commission_type`：委託項目與價目設定
- `qa_question`：訪客 Q&A 提問與諮詢紀錄
- `qa_setting`：管理者 Q&A 與「關於我」資訊
- `images`：圖片上傳與展示紀錄管理
- `config`：首頁委前須知說明
- `Users`：JWT 管理員的資料紀錄

---

## 🔐 權限與角色設計 (Role-Based Control)

本系統採用 **單一創作者管理 + 多訪客/委託者互動** 模式：

- **👑 創作者 (Admin)**：
  - 獨占管理權限，透過 JWT 權限驗證登入後台。
  - 管理委託方案（價目表、開放名額）。
  - 掌控訂單生命週期（稿件完成狀態、付款狀態、中選狀態與排序日期）。
  - 上傳成品圖檔並展示。

- **👤 訪客 / 委託者 (Guest / Client)**：
  - 免登入瀏覽創作者作品集、價目規範與目前排程（進度看板）。
  - 線上填寫單頁委託申請單。
  - 透過提問框聯絡創作者。

---

## 🚀 本地開發與啟動 (Getting Started)

### 前置需求
- .NET 10.0 SDK
- MSSQL Server (LocalDB 或 SQL Server Express)

### 安裝與執行步驟

1. **複製專案 (Clone Repository)**
   ```bash
   git clone [https://github.com/tomyahoo39/CommissionManagement.git](https://github.com/tomyahoo39/CommissionManagement.git)

### 設定 User Secrets (機密資訊)
於專案目錄下執行以下指令設定本地開發變數，避免將真實密碼 commit 到 Git：
```bash
# 設定資料庫連線與 JWT 密鑰
dotnet user-secrets set "ConnectionStrings:CommissionContext" "Server=YOUR_SERVER;Database=YOUR_DB;User Id=YOUR_USER;Password=YOUR_PASSWORD;Encrypt=True;TrustServerCertificate=True;"
dotnet user-secrets set "JwtSettings:Key" "YOUR_SUPER_SECRET_KEY_AT_LEAST_32_BYTES"
dotnet user-secrets set "JwtSettings:Issuer" "CommissionApi"
dotnet user-secrets set "JwtSettings:Audience" "CommissionClient"
dotnet user-secrets set "JwtSettings:ExpiresInMinutes" "120"

# 設定 Cloudinary 圖片上傳服務金鑰
dotnet user-secrets set "Cloudinary:CloudName" "YOUR_CLOUD_NAME"
dotnet user-secrets set "Cloudinary:ApiKey" "YOUR_API_KEY"
dotnet user-secrets set "Cloudinary:ApiSecret" "YOUR_API_SECRET"
```
### 啟動專案與檢視 API 文件

透過 Visual Studio 按下 F5 或執行 dotnet run 啟動專案。

專案啟動後，瀏覽器前往 https://localhost:xxxx/swagger 即可存取 Swagger API 文件進行測試。

---

## 📄 授權條款 (License)

本專案採用 [MIT License](LICENSE) 授權。
