using Mmu.Mlh.DataAccess.Areas.DataModeling.Models;
using Mmu.Mlh.DataAccess.MongoDb.Infrastructure.Settings.Models;
using Mmu.Mlh.DataAccess.MongoDb.Infrastructure.Settings.Services;
using MongoDB.Driver;

namespace Mmu.Mlh.DataAccess.MongoDb.Areas.DataModelRepositories.Servants.Implementation
{
    internal class MongoDbAccess : IMongoDbAccess
    {
        private readonly IMongoClientFactory _mongoClientFactory;
        private readonly MongoDbSettings _mongoDbSettings;

        public MongoDbAccess(
            IMongoClientFactory mongoClientFactory,
            IMongoDbSettingsProvider databaseSettingsProvider)
        {
            _mongoClientFactory = mongoClientFactory;
            _mongoDbSettings = databaseSettingsProvider.ProvideMongoDbSettings();
        }

        public IMongoCollection<T> GetDatabaseCollection<T, TId>()
            where T : AggregateRootDataModel<TId>
        {
            var db = GetDatabase();
            var result = db.GetCollection<T>(_mongoDbSettings.CollectionName);

            return result;
        }

        private IMongoDatabase GetDatabase()
        {
            var mongoClient = _mongoClientFactory.Create();
            var database = mongoClient.GetDatabase(_mongoDbSettings.DatabaseName);
            return database;
        }
    }
}