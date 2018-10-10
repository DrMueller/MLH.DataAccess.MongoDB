using Mmu.Mlh.DataAccess.Areas.DatabaseAccess;
using Mmu.Mlh.DataAccess.MongoDb.Areas.DataMapping.Services;
using Mmu.Mlh.DataAccess.MongoDb.Areas.DataMapping.Services.CoreMappings;
using Mmu.Mlh.DataAccess.MongoDb.Areas.DataMapping.Services.Implementation;
using Mmu.Mlh.DataAccess.MongoDb.Areas.DataModelRepositories.Implementation;
using Mmu.Mlh.DataAccess.MongoDb.Areas.DataModelRepositories.Servants;
using Mmu.Mlh.DataAccess.MongoDb.Areas.DataModelRepositories.Servants.Implementation;
using Mmu.Mlh.DataAccess.MongoDb.Areas.Factories;
using Mmu.Mlh.DomainExtensions.Areas.Factories;
using StructureMap;

namespace Mmu.Mlh.DataAccess.MongoDb.Infrastructure.DependencyInjection
{
    public class DataAccessMongoDbRegistry : Registry
    {
        public DataAccessMongoDbRegistry()
        {
            Scan(
                scanner =>
                {
                    scanner.AssemblyContainingType<DataAccessMongoDbRegistry>();
                    scanner.WithDefaultConventions();
                });

            For<IDataMapper>().Use<DataModelBaseDataMapper>();
            For<IDataMappingInitializationService>().Use<DataMappingInitializationService>().Singleton();
            For(typeof(IEntityIdFactory<string>)).Use(typeof(MongoDbEntityIdFactory)).Singleton();

            For<IMongoClientFactory>().Use<MongoClientFactory>().Singleton();
            For<IMongoDbAccess>().Use<MongoDbAccess>().Singleton();
            For(typeof(IMongoDbFilterDefinitionFactory<,>)).Use(typeof(MongoDbFilterDefinitionFactory<,>)).Singleton();
            For(typeof(IDataModelRepository<,>)).Use(typeof(MongoDbDataModelRepository<,>));
        }
    }
}