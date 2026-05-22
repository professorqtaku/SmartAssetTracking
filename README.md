# Smart Asset Tracking System (Level 1 - 4 Comprehensive)

An enterprise-grade .NET Console inventory management solution built with C#, Entity Framework Core, SQL Server, and extensive Language Integrated Query (LINQ) pipelines. The system enables global enterprises to track, manage, search, and audit capital hardware networks deployed across multiple regional offices with independent economic profiles and currency requirements.

## 🚀 Architectural & System Features

### 📋 Level 1 & Level 2: Full CRUD & Asset Lifecycle
* **Polymorphic Model Traversal:** Utilizes object inheritance pattern via a core abstract `Asset` class separating domain requirements cleanly down to `ComputerAsset` and `MobileAsset` specs.
* **Status Color Lifecycle Monitoring:** Computes hardware deprecation thresholds dynamically. Row text formats alter color variables natively on execution based on remaining deployment windows:
    * 🔴 **RED Warning:** Less than 6 months remaining before the 3-year standard end-of-life cycle.
    * 🟡 **YELLOW Warning:** Less than 3 months remaining before end-of-life cycle.
    * ⚪ **Standard Format:** Operates securely within nominal parameters.
* **LINQ Engine Pipelines:** Sorting workflows parse assets by category type first, followed by historical acquisition dates.

### 🌍 Level 3: International Office Domain & Currency Exchange
* **Relational Entity Mapping:** Explicit 1-to-Many relational database structure handles multi-office environments (`Sweden Office`, `USA Office`, `Germany Office`, `Turkey Office`).
* **Localized Pricing Engines:** Assets are cost-tracked in base USD parameters and converted at runtime to target localized currencies using clean formatting markers (`USD`, `EUR`, `SEK`, `TRY`).
* **Dynamic Intelligence Dashboard:** Generates aggregate metrics on demand using LINQ mathematical transformations:
    * Total capitalization value assigned per regional office.
    * Distinct counts of deployed hardware.
    * Premium investment indexing (Extracting top 3 highest investment pieces).

### 🔍 Level 4: Business Lookup Engine & Data Portability
* **SQL `LIKE` Query Simulation:** Advanced search tools apply case-insensitive `.Contains()` operations. Users can input fractional text (e.g., searching `"swed"` or `"germ"`) to pull related matches cleanly from database layers.
* **Structured Document Exporters:** Includes data persistence pipelines writing reports straight to disk into an isolated, auto-managed `AssetDownloads` directory residing within the project workspace. 
    * **TXT Reporting:** Text format summary ledger featuring administrative headers.
    * **CSV Table Matrix:** Flat file structured dataset featuring standardized headers (`Id,Type,Brand,Model,Office,Price,ExportTime`), ready to open in Excel.
    * **JSON Schema:** Structured data wrapper decoupling system processing metadata fields from actual records. All files append a secure `yyyyMMdd_HHmmss` timestamp signature directly to the filename to prevent collision over-writes.

---

## 🛠️ Technology Stack
* **Language Runtime:** C# | .NET Core 8.0/9.0 Console Pipeline
* **ORM Framework:** Entity Framework Core (EF Core)
* **Database Engine:** Microsoft SQL Server (LocalDB / Express)
* **Data Layout Engine:** Language Integrated Query (LINQ)
* **Serialization Tools:** System.Text.Json

---

## 📁 Directory Structure
```text
SmartAssetTracking/
│
├── Data/
│   └── AssetDbContext.cs       # Entity Framework SQL Server configurations
│
├── Entities/
│   ├── Asset.cs                # Base polymorphic domain model 
│   ├── ComputerAsset.cs        # Extended model for Desktops and Laptops
│   ├── MobileAsset.cs          # Extended model for Phones and Tablets
│   └── Office.cs               # Regional office entity with exchange variables
│
├── Services/
│   └── AssetService.cs         # Core LINQ query execution engine
│
└── Application/
    ├── Helpers/
    │   ├── AssetParser.cs      # User safe console parse constraints
    │   └── PrintHelper.cs      # Success/Error UI styling modifiers
    │
    └── Menus/
        ├── AdvancedSearchMenu.cs # LINQ partial-string lookups & lifecycle filters
        ├── DataExportMenu.cs   # File export logic (TXT, CSV, JSON)
        ├── GlobalSummaryMenu.cs # Analytical dashboard report metrics
        ├── UpdateAssetMenu.cs  # Asset updating with existing office declarations
        └── MainMenu.cs         # Application entry router



```

⚡ Quick Start Configuration
Option 1: Open in Visual Studio

1. Clone the project.
2. Open the SmartAssetTracking.slnx with Visual Studio.
3. Enjoy!

Option 2: Start project in the Terminal
1. Prerequisites

Ensure you have the following installed locally:
```
    .NET SDK (8.0 or later)

    SQL Server Express LocalDB
```

2. Database Synchronization

Run migrations through the .NET Core CLI to establish the underlying database architecture:
Bash
```
  dotnet ef migrations add InitialAssetArchitecture
  dotnet ef database update
```

3. Execution

Compile and launch the application profile:
Bash
```
dotnet run
```
On initial execution, the background engine detects an empty database state and automatically builds your seed nodes (creates the 4 international corporate offices and seeds sample equipment matrix).
📊 Export Output Formats

Every file output generated by the Export Engine is stored inside the AssetDownloads folder located in your base project directory, complete with precise system timestamps:
```
    TXT: AssetDownloads/AssetReport-20260522_150249.txt

    CSV: AssetDownloads/AssetReport-20260522_150249.csv

    JSON: AssetDownloads/AssetReport-20260522_150249.json
```
