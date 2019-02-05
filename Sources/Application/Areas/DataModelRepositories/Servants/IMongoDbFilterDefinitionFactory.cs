using System;
using System.Linq.Expressions;
using Mmu.Mlh.DataAccess.Areas.DataModeling.Models;
using MongoDB.Driver;

namespace Mmu.Mlh.DataAccess.MongoDb.Areas.DataModelRepositories.Servants
{
    internal interface IMongoDbFilterDefinitionFactory<T, TId>
        where T : AggregateRootDataModel<TId>
    {
        FilterDefinition<T> CreateFilterDefinition(Expression<Func<T, bool>> predicate);
    }
}