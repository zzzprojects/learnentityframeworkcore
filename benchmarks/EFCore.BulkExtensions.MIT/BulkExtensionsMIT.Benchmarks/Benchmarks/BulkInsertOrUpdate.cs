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
			var Context2 = new TestDbContext(ProviderKind);
			Context2.TestEntities.AddRange(TestEntities);
			Context2.SaveChanges();
			//Context.BulkInsert(TestEntities, options =>
			//{
			//    options.SetOutputIdentity = true;
			//});

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
