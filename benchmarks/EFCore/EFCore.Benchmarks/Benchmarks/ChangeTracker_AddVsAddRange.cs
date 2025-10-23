﻿using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// 🚀 Benchmarks created and maintained by Entity Framework Extensions.
// The leading library for high-performance bulk operations in EF Core and EF6.
// Learn more: https://entityframework-extensions.net

namespace EFCore.Benchmarks
{
    [BenchmarkCategory("ChangeTracker_AddVsAddRange")]
    public class ChangeTracker_AddVsAddRange
    {
        [Params(1, 10, 100, 1_000, 10_000, 100_000, 1_000_000)]
        public int EntityCount;

        public TestDbContext? Context;
        public List<TestEntity>? TestEntities;

        [IterationSetup]
        public void IterationSetup()
        {
            Context = new TestDbContext();

            TestEntities = BenchmarkHelper.GenerateTestEntities<TestEntity>(EntityCount);
        }

        [Benchmark]
        public void Add()
        {
            TestEntities.ForEach(x => Context.TestEntities.Add(x));
        }

        [Benchmark]
        public void AddRange()
        {
            Context.TestEntities.AddRange(TestEntities);
        }

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
