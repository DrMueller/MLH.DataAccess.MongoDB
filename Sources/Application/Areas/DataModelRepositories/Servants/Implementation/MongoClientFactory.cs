using System.Security.Authentication;
using Mmu.Mlh.DataAccess.MongoDb.Areas.DataMapping.Services;
using Mmu.Mlh.DataAccess.MongoDb.Infrastructure.Settings.Models;
using Mmu.Mlh.DataAccess.MongoDb.Infrastructure.Settings.Services;
using MongoDB.Driver;

namespace Mmu.Mlh.DataAccess.MongoDb.Areas.DataModelRepositories.Servants.Implementation
{
    internal class MongoClientFactory : IMongoClientFactory
    {
        private readonly MongoDbSettings _databaseSettings;

        public MongoClientFactory(IMongoDbSettingsProvider mongoDbSettingsProvider, IDataMappingInitializationService dataMappingInitializationService)
        {
            dataMappingInitializationService.AssureMappingsAreInitialized();
            _databaseSettings = mongoDbSettingsProvider.ProvideMongoDbSettings();
        }

        public MongoClient Create()
        {
            var clientSettings = new MongoClientSettings
            {
                Server = new MongoServerAddress(_databaseSettings.Host, _databaseSettings.Port),
                UseSsl = _databaseSettings.UseSsl
            };

            if (clientSettings.UseSsl)
            {
                clientSettings.SslSettings = new SslSettings
                {
                    EnabledSslProtocols = SslProtocols.Tls12
                };
            }

            var identity = new MongoInternalIdentity(_databaseSettings.DatabaseName, _databaseSettings.UserName);
            var evidence = new PasswordEvidence(_databaseSettings.Password);
            clientSettings.Credential = new MongoCredential("SCRAM-SHA-1", identity, evidence);

            var mongoClient = new MongoClient(clientSettings);
            return mongoClient;
        }
    }
}