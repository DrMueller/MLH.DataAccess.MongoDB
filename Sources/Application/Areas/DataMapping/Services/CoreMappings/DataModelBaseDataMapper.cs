﻿using Mmu.Mlh.DataAccess.Areas.DataModeling.Models;
using MongoDB.Bson.Serialization;

namespace Mmu.Mlh.DataAccess.MongoDb.Areas.DataMapping.Services.CoreMappings
{
    internal class DataModelBaseDataMapper : IDataMapper
    {
        public void InitializeDataMapping()
        {
            // Why EntityDataModel instead of AggregateRootDataModel?
            // Because the AggregateRootDataModel is a EntityDataModel and we need to map the top level type
            // That said, the Repositories still only communicate via AggregateRootDataModel
            BsonClassMap.RegisterClassMap<EntityDataModel<string>>(
                f =>
                {
                    f.AutoMap();
                    f.MapIdMember(m => m.Id);
                    f.MapMember(m => m.DataModelTypeName);
                });
        }
    }
}