using MongoDB.Driver;

namespace Mmu.Mlh.DataAccess.MongoDb.Areas.DataModelRepositories.Servants
{
    internal interface IMongoClientFactory
    {
        MongoClient Create();
    }
}