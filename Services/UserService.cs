using ArtechApi.Configurations;
using ArtechApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ArtechApi.Services;

public class UserService
{
    private readonly IMongoCollection<User> _userCollection;

    public UserService(IOptions<DatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
        var mongoDb = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName); // Connect to db
        _userCollection = mongoDb.GetCollection<User>(databaseSettings.Value.CollectionName); // Connect to collection
    }

    // Create
    public async Task CreateAsync(User user) =>
        await _userCollection.InsertOneAsync(user);
    
    // Read
    public async Task<List<User>> GetAsync() =>
        await _userCollection.Find(user => true).ToListAsync();


    public async Task<User> GetAsync(string id) =>
        await _userCollection.Find(user => user.Id == id).FirstOrDefaultAsync();

    // Update
    public async Task UpdateAsync(User updatedUser) =>
        await _userCollection.ReplaceOneAsync(user => user.Id == updatedUser.Id, updatedUser); // Replaces entire record

    // Delete
    public async Task RemoveAsync(string id) =>
        await _userCollection.DeleteOneAsync(user => user.Id == id);
}