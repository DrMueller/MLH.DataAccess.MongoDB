using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization;

namespace Mmu.Mlh.DataAccess.MongoDb.Areas.DataMapping.Services.Implementation
{
    internal class DataMappingInitializationService : IDataMappingInitializationService
    {
        private readonly IReadOnlyCollection<IDataMapper> _dataMappers;
        private readonly object _lock = new object();

        public DataMappingInitializationService(IDataMapper[] dataMappers)
        {
            _dataMappers = dataMappers;
        }

        public void AssureMappingsAreInitialized()
        {
            if (BsonClassMap.GetRegisteredClassMaps().Any())
            {
                return;
            }

            lock (_lock)
            {
                if (BsonClassMap.GetRegisteredClassMaps().Any())
                {
                    return;
                }

                foreach (var dataMapper in _dataMappers)
                {
                    dataMapper.InitializeDataMapping();
                }
            }
        }
    }
}