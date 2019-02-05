using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Mmu.Mlh.DataAccess.Areas.DatabaseAccess;
using Mmu.Mlh.DataAccess.Areas.DataModeling.Models;
using Mmu.Mlh.DataAccess.MongoDb.Areas.DataModelRepositories.Servants;
using MongoDB.Driver;

namespace Mmu.Mlh.DataAccess.MongoDb.Areas.DataModelRepositories.Implementation
{
    internal class MongoDbDataModelRepository<T, TId> : IDataModelRepository<T, TId>
        where T : AggregateRootDataModel<TId>
    {
        private readonly IMongoDbFilterDefinitionFactory<T, TId> _filterDefinitionFactory;
        private readonly IMongoDbAccess _mongoDbAccess;

        public MongoDbDataModelRepository(IMongoDbAccess mongoDbAccess, IMongoDbFilterDefinitionFactory<T, TId> filterDefinitionFactory)
        {
            _mongoDbAccess = mongoDbAccess;
            _filterDefinitionFactory = filterDefinitionFactory;
        }

        public Task DeleteAsync(TId id)
        {
            var collection = _mongoDbAccess.GetDatabaseCollection<T, TId>();
            return collection.DeleteOneAsync(f => f.Id.Equals(id));
        }

        public async Task<IReadOnlyCollection<T>> LoadAsync(Expression<Func<T, bool>> predicate)
        {
            return await LoadByExpressionAsync(predicate);
        }

        public async Task<T> LoadSingleAsync(Expression<Func<T, bool>> predicate)
        {
            var allResults = await LoadAsync(predicate);
            var result = allResults.SingleOrDefault();
            return result;
        }

        public async Task<T> SaveAsync(T aggregateRoot)
        {
            var collection = _mongoDbAccess.GetDatabaseCollection<T, TId>();

            var filter = _filterDefinitionFactory.CreateFilterDefinition(f => f.Id.Equals(aggregateRoot.Id));
            var updateOptions = new FindOneAndReplaceOptions<T> { IsUpsert = true, ReturnDocument = ReturnDocument.After };
            var result = await collection.FindOneAndReplaceAsync(filter, aggregateRoot, updateOptions);
            return result;
        }

        private async Task<IReadOnlyCollection<T>> LoadByExpressionAsync(Expression<Func<T, bool>> predicate)
        {
            var collection = _mongoDbAccess.GetDatabaseCollection<T, TId>();

            var filter = _filterDefinitionFactory.CreateFilterDefinition(predicate);
            var result = await collection.Find(filter).ToListAsync();
            return result;
        }
    }
}