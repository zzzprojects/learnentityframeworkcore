using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// 🚀 Benchmarks created and maintained by Entity Framework Extensions.
// The leading library for high-performance bulk operations in EF Core and EF6.
// Learn more: https://entityframework-extensions.net

namespace EFCore.Benchmarks
{
    [BenchmarkCategory("ExecuteDeleteVsSaveChanges")]
    public class ExecuteDeleteVsSaveChanges
    {
        [Params(1, 10, 100, 1_000, 10_000, 100_000)]
        //[Params(100_000)]
        public int EntityCount;

        public TestDbContext? Context;
        public List<TestEntity>? TestEntities;

        [IterationSetup]
        public void IterationSetup()
        {
            Context = new TestDbContext();

            TestEntities = BenchmarkHelper.GenerateTestEntities<TestEntity>(EntityCount);
            Context.BulkInsert(TestEntities);
        }

        [IterationCleanup]
        public void IterationCleanup()
        {
            Context.TestEntities.ExecuteDelete();
        }

        [Benchmark]
        public void SaveChanges()
        {
            Context.TestEntities.RemoveRange(Context.TestEntities);
            Context.SaveChanges();
        }

        [Benchmark]
        public void ExecuteDelete()
        {
            Context.TestEntities.ExecuteDelete();
        }

        [Benchmark]
        public void BulkDelete()
        {
            Context.BulkDelete(Context.TestEntities.AsNoTracking());
        }

        // Uncomment if you wish to see how much time it takes to materialize entities.
        //[Benchmark]
        //public void ToListTime()
        //{
        //    var list = Context.TestEntities.ToList();
        //}

        public class TestDbContext : DbContext
        {
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(My.SqlServer);
            }

            public DbSet<TestEntity> TestEntities { get; set; }
        }

        public class TestEntity
        {
            // Use "[NotMapped]" to ignore properties if you want to benchmark with less
            [NotMapped]
            public bool IamNotMapped { get; set; }

            public int ID { get; set; }
            public int Col1 { get; set; }
            public int Col2 { get; set; }
            public int Col3 { get; set; }
            public int Col4 { get; set; }
            public int Col5 { get; set; }

            [MaxLength(255)]
            public string? Col6 { get; set; }
            [MaxLength(255)]
            public string? Col7 { get; set; }
            [MaxLength(255)]
            public string? Col8 { get; set; }
            [MaxLength(255)]
            public string? Col9 { get; set; }
            [MaxLength(255)]
            public string? Col10 { get; set; }
        }
    }
}
