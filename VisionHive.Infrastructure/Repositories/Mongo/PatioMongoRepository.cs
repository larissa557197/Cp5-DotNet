using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using VisionHive.Domain.Entities;

namespace VisionHive.Infrastructure.Repositories.Mongo;

public class PatioMongoRepository
{
    
    private readonly IMongoCollection<Patio> _collection;

    public PatioMongoRepository(IMongoDatabase database)
    {
        //  Garante compatibilidade com UUID binário padrão do Mongo
        try
        {
            BsonSerializer.RegisterSerializer(typeof(Guid), new GuidSerializer(GuidRepresentation.Standard));
        }
        catch
        {
            // ignora se já tiver sido registrado (versões antigas não têm verificação)
        }

        _collection = database.GetCollection<Patio>("Patios");
    }

    // CREATE
    public async Task<Patio> CreateAsync(Patio patio)
    {
        await _collection.InsertOneAsync(patio);
        return patio;
    }

    
    // READ - Todos
    public async Task<List<Patio>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }
    
    // READ - Por ID
    public async Task<Patio> GetByIdAsync(Guid id)
    {
        // Busca tanto pelo campo Id quanto pelo _id binário (compatível com UUID('...'))
        var filter = Builders<Patio>.Filter.Or(
            Builders<Patio>.Filter.Eq(f => f.Id, id),
            Builders<Patio>.Filter.Eq("_id", new BsonBinaryData(id, GuidRepresentation.Standard))
        );
        
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }
    
    // UPDATE
    public async Task<bool> UpdateAsync(Patio patio)
    {
        var filter = Builders<Patio>.Filter.Eq(p => p.Id, patio.Id);
        var result = await _collection.ReplaceOneAsync(filter, patio);
        return result.ModifiedCount > 0;
        
    }
    
    // DELETE
    public async Task<bool> DeleteAsync(Guid id)
    {
        var result = await _collection.DeleteOneAsync(f => f.Id == id);
        return result.DeletedCount > 0;
    }
    
}