using Mmu.Mlh.DomainExtensions.Areas.Factories;
using MongoDB.Bson;

namespace Mmu.Mlh.DataAccess.MongoDb.Areas.Factories
{
    public class MongoDbEntityIdFactory : IEntityIdFactory<string>
    {
        public string CreateEntityId()
        {
            return ObjectId.GenerateNewId().ToString();
        }
    }
}