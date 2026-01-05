# Japan Birth Statistics Web App (日本出生人數統計 Web 應用程式)

這是一個基於 ASP.NET Core MVC 建構的 Web 應用程式，旨在將原本的 WinForms 統計程式現代化。程式會讀取 CSV 數據並透過網頁呈現互動式圖表與詳細數據分析。

## ✨ 主要功能

1.  **趨勢圖表**:
    *   整合 **Chart.js** 繪製歷年出生人數折線圖。
    *   視覺化呈現「男性」與「女性」出生人數的變化趨勢。
    *   修正了時間軸顯示，確保年份與數據精確對應。

2.  **數據列表**:
    *   使用 Bootstrap 表格展示完整的 CSV 原始數據。
    *   包含年份、男性出生數、女性出生數及總計。

3.  **進階查詢與統計**:
    *   **年份搜尋**: 可輸入特定年份快速定位資料。
    *   **統計分析**: 自動計算選定年份的**前後三年**區間之出生人數標準差 (Standard Deviation)，協助分析數據波動。

## 🚀 如何執行 (How to Run)

### 前置需求
*   .NET SDK 9.0 或更高版本

### 執行步驟

1.  開啟終端機 (Terminal) 並切換到專案目錄：

2.  執行應用程式：
    ```bash
    dotnet run
    ```

3.  開啟瀏覽器：
    根據終端機顯示的 URL (通常為 `http://localhost:5231`) 進行訪問。

## 📂 專案結構

*   **Controllers/BirthController.cs**: 核心邏輯，負責讀取 CSV、解析數據與計算標準差。
*   **Views/Birth/Index.cshtml**: 前端介面，包含搜尋表單、Chart.js 設定與數據表格。
*   **Models/BirthData.cs**: 定義出生資料的資料結構。
*   **ViewModels/BirthIndexViewModel.cs**: 用於視圖的複合模型，包含搜尋結果與統計數據。
*   **wwwroot/data/japan_birth.csv**: 原始數據來源檔案。

## 📝 注意事項

*   若修改了 `wwwroot/data/japan_birth.csv` 中的數據，請重新整理網頁即可查看最新結果。

