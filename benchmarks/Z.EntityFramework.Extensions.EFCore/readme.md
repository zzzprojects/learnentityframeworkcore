# 📊 Benchmark Results – Entity Framework Extensions vs EF Core

* **Comparison:** Entity Framework Extensions vs EF Core
* **Provider:** SQL Server
* **EF Version:** EF Core 9.x

## Benchmark Setup

Before running the benchmark, make sure you’ve updated the **Z.EntityFramework.Extensions.EFCore** NuGet package to the latest version.
This ensures that the **trial version** works properly and that you’re testing the most optimized build.

## Performance Notes

In nearly all scenarios, **Entity Framework Extensions** delivers **dramatically faster performance** than EF Core’s native `SaveChanges()` — both **with** and **without graphs**.

The library offers **an exceptional balance between speed and flexibility**, allowing you to process thousands or even millions of entities efficiently while maintaining full control over your operations.

Even for smaller data sets, performance is sometimes **just as fast or even slightly better** than `SaveChanges()`, meaning there’s virtually **no downside** to using the library in everyday operations.

* 👉 **Download the library here:** [https://entityframework-extensions.net/download](https://entityframework-extensions.net/download)
* 📈 **More benchmark results:** [https://github.com/zzzprojects/EntityFramework-Extensions](https://github.com/zzzprojects/EntityFramework-Extensions)

## Bulk Insert

![Benchmark EFCore vs Entity Framework Extensions – SQL Server - Bulk Insert](https://raw.githubusercontent.com/zzzprojects/learnentityframeworkcore/main/benchmarks/Z.EntityFramework.Extensions.EFCore/benchmark-result/bulk-insert.png)

## Bulk Update

![Benchmark EFCore vs Entity Framework Extensions – SQL Server - Bulk Update](https://raw.githubusercontent.com/zzzprojects/learnentityframeworkcore/main/benchmarks/Z.EntityFramework.Extensions.EFCore/benchmark-result/bulk-update.png)

## Bulk Merge (Upsert / InsertOrUpdate)

![Benchmark EFCore vs Entity Framework Extensions – SQL Server - Bulk Merge](https://raw.githubusercontent.com/zzzprojects/learnentityframeworkcore/main/benchmarks/Z.EntityFramework.Extensions.EFCore/benchmark-result/bulk-merge.png)
