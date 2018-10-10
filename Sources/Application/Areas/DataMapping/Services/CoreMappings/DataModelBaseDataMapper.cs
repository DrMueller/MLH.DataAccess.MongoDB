using Mmu.Mlh.DataAccess.Areas.DataModeling.Models;
using MongoDB.Bson.Serialization;

namespace Mmu.Mlh.DataAccess.MongoDb.Areas.DataMapping.Services.CoreMappings
{
    internal class DataModelBaseDataMapper : IDataMapper
    {
        public void InitializeDataMapping()
        {
            BsonClassMap.RegisterClassMap<DataModelBase<string>>(
                f =>
                {
                    f.AutoMap();
                    f.MapIdMember(m => m.Id);
                    f.MapMember(m => m.DataModelTypeName);
                });
        }
    }
}