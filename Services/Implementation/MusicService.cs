using ASPCoreWebApplication.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace ASPCoreWebApplication.Services.Implementation
{
    public class MusicService : IMusicService
    {
        private readonly IMongoCollection<Category> _musicCollection;

        public MusicService(IOptions<MusicStoreDatabaseSettings> musicStoreDatabaseSettings)
        {
            // инициализируем клиент, бд и коллекции
            var mongoClient = new MongoClient(musicStoreDatabaseSettings.Value.ConnectionString);
            
            // берём ссылку на бд
            var mongoDatabase = mongoClient.GetDatabase(
                musicStoreDatabaseSettings.Value.DatabaseName);

            _musicCollection = mongoDatabase.GetCollection<Category>(
                musicStoreDatabaseSettings.Value.MusicCollectionName);
        }

        public async Task<List<Category>> GetAllAsync(string? title, string? sortBy, string? sortDirection)
        {
            FilterDefinition<Category> filter = FilterDefinition<Category>.Empty; // описание поиска как db.collection.find({ . . . })

            if (!string.IsNullOrWhiteSpace(title)) // если title не задан - не фильтруем
            {
                var escaped = Regex.Escape(title); // экранирование (чтобы метасимволы воспринимались как элемент строки)
                var regex = new BsonRegularExpression(escaped, "i"); // независимость регистров

                filter = Builders<Category>.Filter.Regex(a => a.AlbumName, regex);

            }

            SortDefinition<Category> sort;
            
            // для сравнения в нижнем регистре в независимости от входных данных
            sortBy = sortBy?.ToLowerInvariant();
            sortDirection = sortDirection?.ToLowerInvariant();

            bool desc = sortDirection == "desc";
            
            switch (sortBy)
            {
                case "price":
                    sort = desc
                        ? Builders<Category>.Sort.Descending(a => a.Price)
                        : Builders<Category>.Sort.Ascending(a => a.Price);
                    break;

                case "author":
                    sort = desc
                        ? Builders<Category>.Sort.Descending(a => a.Author)
                        : Builders<Category>.Sort.Ascending(a => a.Author);
                    break;

                default:
                    sort = desc
                        ? Builders<Category>.Sort.Descending(a => a.AlbumName)
                        : Builders<Category>.Sort.Ascending(a => a.AlbumName);
                    break;
            }

            return await _musicCollection
                .Find(filter)
                .Sort(sort)
                .ToListAsync();
        }

        public async Task<Category?> GetAsync(string id) =>
            await _musicCollection
            .Find(x => x.Id == id)
            .FirstOrDefaultAsync();

        public async Task CreateAsync(Category newCategory) =>
            await _musicCollection
            .InsertOneAsync(newCategory);

        public async Task UpdateAsync(string id, Category updatedCategory) =>
            await _musicCollection
            .ReplaceOneAsync(x => x.Id == id, updatedCategory);

        public async Task RemoveAsync(string id) =>
            await _musicCollection
            .DeleteOneAsync(x => x.Id == id);

        public async Task<List<Category>> SearchByTitleAsync(string? title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                // если ничего удаётся найти, то просто GetAllAsync
                return await _musicCollection.Find(_ => true).ToListAsync(); 
            }

            // регистронезависимость
            var regex = new BsonRegularExpression(title, "i");
            var filter = Builders<Category>.Filter.Regex(a => a.AlbumName, regex);

            return await _musicCollection.Find(filter).ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return (int)await _musicCollection.CountDocumentsAsync(_ => true);
        }


    }
}
