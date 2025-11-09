using ASPCoreWebApplication.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Security.Cryptography.X509Certificates;

namespace ASPCoreWebApplication.Services
{
    public class MusicService
    {
        private readonly IMongoCollection<Category> _musicCollection;

        public MusicService(IOptions<MusicStoreDatabaseSettings> musicStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(musicStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                musicStoreDatabaseSettings.Value.DatabaseName);

            _musicCollection = mongoDatabase.GetCollection<Category>(
                musicStoreDatabaseSettings.Value.MusicCollectionName);
        }

        public async Task<List<Category>> GetAsync() =>
            await _musicCollection.Find(_ => true).ToListAsync();

        public async Task<Category?> GetAsync(string id) =>
            await _musicCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Category newCategory) =>
            await _musicCollection.InsertOneAsync(newCategory);

        public async Task UpdateAsync(string id, Category updatedCategory) =>
            await _musicCollection.ReplaceOneAsync(x => x.Id == id, updatedCategory);

        public async Task RemoveAsync(string id) =>
            await _musicCollection.DeleteOneAsync(x => x.Id == id);

    }
}
