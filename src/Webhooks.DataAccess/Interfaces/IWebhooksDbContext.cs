using MongoDB.Driver;

namespace Webhooks.DataAccess.Interfaces
{
    public interface IWebhooksDbContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
        void AddCommand(Func<Task> func);
        Task<int> SaveChangesAsync();
    }
}
