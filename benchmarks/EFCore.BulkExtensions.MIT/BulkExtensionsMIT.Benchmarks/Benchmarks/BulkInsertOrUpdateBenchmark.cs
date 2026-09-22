using BenchmarkDotNet.Attributes;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Benchmarks
{
    [BenchmarkCategory("BulkInsertOrUpdateBenchmark")]
    public class BulkInsertOrUpdateBenchmark : ProgramBenchmarks
    {
        [IterationSetup]
        public void IterationSetup()
        {
            Context = new TestDbContext(ProviderKind);
            Context.EnsureSetup();

            // First half: Inserted now so they will be updated during BulkMerge
            TestEntities = BenchmarkHelper.GenerateTestEntities(EntityCount / 2);
            Context.BulkInsert(TestEntities, options =>
            {
                options.SetOutputIdentity = true;
            });

            // Change at least one column value
            TestEntities.ForEach(x => x.Col1 += 1);

            // Second half: Added to the list so they will be inserted during BulkMerge
            TestEntities.AddRange(BenchmarkHelper.GenerateTestEntities(EntityCount / 2));
        }

        [IterationCleanup]
        public void IterationCleanup()
        {
            // Remove all entities to reset the table for the next iteration
            Context.TestChildEntities.ExecuteDelete();
            Context.TestEntities.ExecuteDelete();
        }

        [Benchmark]
        public void SaveChanges()
        {
            Context.TestEntities.UpdateRange(TestEntities.Where(x => x.ID > 0));
            Context.TestEntities.AddRange(TestEntities.Where(x => x.ID == 0));
            Context.SaveChanges();
        }

        [Benchmark]
        public void BulkInsertOrUpdate()
        {
            Context.BulkInsertOrUpdate(TestEntities);
        }
    }
}
