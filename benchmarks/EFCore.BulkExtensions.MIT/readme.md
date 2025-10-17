# 📊 Benchmark Results – EFCore.BulkExtensions.MIT vs EF Core

* **Comparison:** EFCore.BulkExtensions.MIT vs EF Core
* **Provider:** SQL Server
* **EF Version:** EF Core 9.x

## Benchmark Setup

We had to seed data using `SaveChanges()` instead of `BulkInsert`.
During our tests, we noticed some issues (possibly memory-related) when running with thousands of entities, causing **BenchmarkDotNet** to stop working.

This behavior made it difficult to run consistent tests for high-entity scenarios.
As a result, the seeding process uses standard EF Core for the **“BulkInsertOrUpdateBenchmark”** and **“BulkUpdateBenchmark”** to ensure stable benchmark results.

## Performance Notes

In most scenarios, the library is **much faster** than EF Core’s `SaveChanges()`.
However, we identified two specific cases where performance dropped:

* In the **“Bulk Insert (with Graph)”** scenario, the bulk operation became **10x slower** than `SaveChanges()` when processing a **very high number of entities** (around 10,000 or more).
  This slowdown seems related to internal memory usage when handling complex graphs.
* When working with a **small number of entities**, the library can also be **slightly slower** due to the fixed initialization overhead required by bulk operations.

Overall, the library remains **very fast** and provides **significantly better performance** than EF Core for SQL Server in most cases.
However, be careful — as shown in the result below, it can sometimes perform worse depending on the operation and data structure.

## Bulk Insert

![Benchmark EFCore vs EFCore.BulkExtensions.MIT – SQL Server - Bulk Insert](https://raw.githubusercontent.com/zzzprojects/learnentityframeworkcore/main/benchmarks/EFCore.BulkExtensions.MIT/benchmark-result/bulk-insert.png)

## Bulk Insert (with Graph)

![Benchmark EFCore vs EFCore.BulkExtensions.MIT – SQL Server - Bulk Insert with Graph](https://raw.githubusercontent.com/zzzprojects/learnentityframeworkcore/main/benchmarks/EFCore.BulkExtensions.MIT/benchmark-result/bulk-insert-with-graph.png)

## Bulk Update

![Benchmark EFCore vs EFCore.BulkExtensions.MIT – SQL Server - Bulk Update](https://raw.githubusercontent.com/zzzprojects/learnentityframeworkcore/main/benchmarks/EFCore.BulkExtensions.MIT/benchmark-result/bulk-update.png)

## Bulk InsertOrUpdate

![Benchmark EFCore vs EFCore.BulkExtensions.MIT – SQL Server - Bulk InsertOrUpdate](https://raw.githubusercontent.com/zzzprojects/learnentityframeworkcore/main/benchmarks/EFCore.BulkExtensions.MIT/benchmark-result/bulk-insert-or-update.png)
