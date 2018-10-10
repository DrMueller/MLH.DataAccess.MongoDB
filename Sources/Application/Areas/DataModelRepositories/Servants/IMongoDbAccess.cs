using Mmu.Mlh.DataAccess.Areas.DataModeling.Models;
using MongoDB.Driver;

namespace Mmu.Mlh.DataAccess.MongoDb.Areas.DataModelRepositories.Servants
{
    internal interface IMongoDbAccess
    {
        IMongoCollection<T> GetDatabaseCollection<T, TId>()
            where T : DataModelBase<TId>;
    }
}