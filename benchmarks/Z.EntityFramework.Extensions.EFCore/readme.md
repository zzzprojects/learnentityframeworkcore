# 📊 Benchmark Results – Entity Framework Extensions vs EF Core

* **Comparison:** Entity Framework Extensions vs EF Core
* **Provider:** SQL Server
* **EF Version:** EF Core 9.x

## Benchmark Setup

Before running the benchmark, make sure you’ve updated the **Z.EntityFramework.Extensions.EFCore** NuGet package to the latest version.
This ensures that the **trial version** works properly and that you’re testing the most optimized build.

## Performance Notes

In all scenarios, **Entity Framework Extensions** delivers **significantly faster performance** than EF Core’s native `SaveChanges()` — both **with** and **without graphs**.

The library offers **the best balance between performance and flexibility**, allowing you to handle large datasets efficiently without giving up control.

👉 **Download the library here:** [https://entityframework-extensions.net/download](https://entityframework-extensions.net/download)

## Bulk Insert

![Benchmark EFCore vs Entity Framework Extensions – SQL Server - Bulk Insert](https://raw.githubusercontent.com/zzzprojects/learnentityframeworkcore/main/benchmarks/Z.EntityFramework.Extensions.EFCore/benchmark-result/bulk-insert.png)

## Bulk Update

![Benchmark EFCore vs Entity Framework Extensions – SQL Server - Bulk Update](https://raw.githubusercontent.com/zzzprojects/learnentityframeworkcore/main/benchmarks/Z.EntityFramework.Extensions.EFCore/benchmark-result/bulk-update.png)

## Bulk Merge (Upsert / InsertOrUpdate)

![Benchmark EFCore vs Entity Framework Extensions – SQL Server - Bulk Merge](https://raw.githubusercontent.com/zzzprojects/learnentityframeworkcore/main/benchmarks/Z.EntityFramework.Extensions.EFCore/benchmark-result/bulk-merge.png)
