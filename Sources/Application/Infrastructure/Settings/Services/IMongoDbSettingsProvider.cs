using Mmu.Mlh.DataAccess.MongoDb.Infrastructure.Settings.Models;

namespace Mmu.Mlh.DataAccess.MongoDb.Infrastructure.Settings.Services
{
    public interface IMongoDbSettingsProvider
    {
        MongoDbSettings ProvideMongoDbSettings();
    }
}