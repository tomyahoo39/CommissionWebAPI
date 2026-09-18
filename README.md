# 🎨 繪圖委託管理系統 - 後端 API

本專案為「繪圖委託管理系統」的後端 RESTful API 服務，提供訪客建立委託、創作者管理訂單、排程、圖片上傳及權限驗證等功能。

🔗 **前端儲存庫**：[CommissionVue (Vue)](https://github.com/tomyahoo39/CommissionVue)

---

## 🛠 技術棧 (Tech Stack)

- **Framework**: C# / ASP.NET Core Web API (.NET 10)
- **Database**: MSSQL / Entity Framework Core (Database First)
- **Authentication**: JWT (JSON Web Token) 
- **Storage**: Cloudinary 
- **Architecture**: Layered Architecture / Dependency Injection / DTO Pattern

---

## 🌟 核心功能 (Features)

- [✔] **委託項目管理**：CRUD 創作者的委託方案（價目表、委託規範）
- [✔] **訂單狀態追蹤**：管理委託狀態（稿件狀態、付款狀態等）
- [✔] **身份驗證與授權**：JWT Auth（區分「創作者」與「訪客」權限）
- [✔] **作品與草稿圖片上傳**：整合雲端儲存 API
- [ ] **雲端部署**：部署至 Azure / Render

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
- `Users`：JWT管理員的資料紀錄

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
透過 Visual Studio 選取「複製存放庫」或使用 CLI 複製：
```bash
git clone https://github.com/tomyahoo39/CommissionManagement.git

```


2. **設定資料庫連接字串**
於 `appsettings.json` 或 `appsettings.Development.json` 中確認資料庫連接字串：
```json
{
  "ConnectionStrings": {
    "CommissionContext": "Server=(localdb)\\mssqllocaldb;Database=Commission;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}

```

3. **啟動專案與檢視 API 文件**
* 透過 Visual Studio 按下 `F5` 或執行 `dotnet run` 啟動專案。
* 專案啟動後，瀏覽器前往 `https://localhost:xxxx/swagger`（`xxxx` 為主控台輸出的 Port 號）即可存取 Swagger API 文件進行測試。




