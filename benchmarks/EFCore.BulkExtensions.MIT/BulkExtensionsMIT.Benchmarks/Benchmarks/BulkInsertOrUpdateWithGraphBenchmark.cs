using BenchmarkDotNet.Attributes;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Benchmarks
{
    [BenchmarkCategory("BulkInsertOrUpdateWithGraphBenchmark")]
    public class BulkInsertOrUpdateWithGraphBenchmark : ProgramBenchmarks
    {
        [IterationSetup]
        public void IterationSetup()
        {
            Context = new TestDbContext(ProviderKind);
            Context.EnsureSetup();

            // First half: Inserted now so they will be updated during BulkMerge
            TestEntities = BenchmarkHelper.GenerateTestEntitiesWithGraph(EntityCount / 2, IncludeGraphChildCount);
            Context.BulkInsert(TestEntities, options =>
            {
                options.SetOutputIdentity = true;
                options.IncludeGraph = true;
            });

            // Change at least one column value
            TestEntities.ForEach(x => x.Col1 += 1);
            TestEntities.SelectMany(x => x.ChildEntities).ToList().ForEach(x => x.Col1 += 1);

            // Second half: Added to the list so they will be inserted during BulkMerge
            TestEntities.AddRange(BenchmarkHelper.GenerateTestEntitiesWithGraph(EntityCount / 2, IncludeGraphChildCount));
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
            // Doesn't work
            Context.BulkInsertOrUpdate(TestEntities, options => {
                options.SetOutputIdentity = true;
                options.IncludeGraph = true;
            });
        }
    }
}
