using BenchmarkDotNet.Attributes;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Benchmarks
{
    [BenchmarkCategory("BulkUpdateWithGraphBenchmark")]
    public class BulkUpdateWithGraphBenchmark : ProgramBenchmarks
    {
        [IterationSetup]
        public void IterationSetup()
        {
            Context = new TestDbContext(ProviderKind);
            Context.EnsureSetup();

            TestEntities = BenchmarkHelper.GenerateTestEntitiesWithGraph(EntityCount, IncludeGraphChildCount);

            // Seed the table with entities that will be updated during the benchmark
            Context.BulkInsert(TestEntities, options =>
            {
                options.SetOutputIdentity = true;
                options.IncludeGraph = true;
            });

            // Change at least one column value
            TestEntities.ForEach(x => x.Col1 += 1);
            TestEntities.SelectMany(x => x.ChildEntities).ToList().ForEach(x => x.Col1 += 1);
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
            // Remove all entities to reset the table for the next iteration
            Context.TestEntities.UpdateRange(TestEntities);
            Context.SaveChanges();
        }

        [Benchmark]
        public void BulkUpdate()
        {
            Context.BulkUpdate(TestEntities, options => { options.IncludeGraph = true; });
        }
    }
}
