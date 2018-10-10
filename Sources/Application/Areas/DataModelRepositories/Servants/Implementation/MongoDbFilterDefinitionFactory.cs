using System;
using System.Linq.Expressions;
using Mmu.Mlh.DataAccess.Areas.DataModeling.Models;
using MongoDB.Driver;

namespace Mmu.Mlh.DataAccess.MongoDb.Areas.DataModelRepositories.Servants.Implementation
{
    internal class MongoDbFilterDefinitionFactory<T, TId> : IMongoDbFilterDefinitionFactory<T, TId>
        where T : DataModelBase<TId>
    {
        public FilterDefinition<T> CreateFilterDefinition(Expression<Func<T, bool>> predicate)
        {
            var entityTypeFilter = CreateEntityTypeFilterDefinition();
            var predicateFilter = new ExpressionFilterDefinition<T>(predicate);
            return new FilterDefinitionBuilder<T>().And(entityTypeFilter, predicateFilter);
        }

        private static FilterDefinition<T> CreateEntityTypeFilterDefinition()
        {
            var entityTypeFilter = new ExpressionFilterDefinition<T>(x => x.DataModelTypeName == typeof(T).FullName);
            return entityTypeFilter;
        }
    }
}
