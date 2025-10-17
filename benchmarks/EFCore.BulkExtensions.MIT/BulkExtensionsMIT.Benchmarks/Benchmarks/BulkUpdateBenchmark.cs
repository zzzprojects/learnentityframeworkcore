using BenchmarkDotNet.Attributes;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EFCore.Benchmarks
{
    [BenchmarkCategory("BulkUpdateBenchmark")]
    public class BulkUpdateBenchmark : ProgramBenchmarks
    {
        [IterationSetup]
        public void IterationSetup()
        {
            Context = new TestDbContext(ProviderKind);
            Context.EnsureSetup();

            TestEntities = BenchmarkHelper.GenerateTestEntities(EntityCount);

			// Seed the table with entities that will be updated during the benchmark
			var Context2 = new TestDbContext(ProviderKind);
			Context2.TestEntities.AddRange(TestEntities);
			Context2.SaveChanges();
            //Context.BulkInsert(TestEntities, options =>
            //{
            //    options.SetOutputIdentity = true;
            //});
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
            Context.TestEntities.UpdateRange(TestEntities);
            Context.SaveChanges();
        }

        [Benchmark]
        public void BulkUpdate()
        {
            Context.BulkUpdate(TestEntities);
        }
    }
}
