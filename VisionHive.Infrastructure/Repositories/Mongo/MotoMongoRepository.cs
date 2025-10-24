using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using VisionHive.Domain.Entities;

namespace VisionHive.Infrastructure.Repositories.Mongo;

public class MotoMongoRepository
{
    private readonly IMongoCollection<Moto> _collection;

    public MotoMongoRepository(IMongoDatabase database)
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

        _collection = database.GetCollection<Moto>("Motos");
    }


    // CREATE
    public async Task<Moto> CreateAsync(Moto moto)
    {
        await _collection.InsertOneAsync(moto);
        return moto;
    }


    // READ - Todos
    public async Task<List<Moto>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }
    
    // READ - Por ID
    public async Task<Moto> GetByIdAsync(Guid id)
    {
        // Busca tanto pelo campo Id quanto pelo _id binário (compatível com UUID('...'))
        var filter = Builders<Moto>.Filter.Or(
            Builders<Moto>.Filter.Eq(f => f.Id, id),
            Builders<Moto>.Filter.Eq("_id", new BsonBinaryData(id, GuidRepresentation.Standard))
        );
        
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }
    
    // UPDATE
    public async Task<bool> UpdateAsync(Moto moto)
    {
        var filter = Builders<Moto>.Filter.Eq(m => m.Id, moto.Id);
        var result = await _collection.ReplaceOneAsync(filter, moto);
        return result.ModifiedCount > 0;
    }
    
    // DELETE
    public async Task<bool> DeleteAsync(Guid id)
    {
        var result = await _collection.DeleteOneAsync(m => m.Id == id);
        return result.DeletedCount > 0;
    }
}