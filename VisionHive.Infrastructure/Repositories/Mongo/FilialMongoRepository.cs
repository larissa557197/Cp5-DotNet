using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using VisionHive.Domain.Entities;

namespace VisionHive.Infrastructure.Repositories.Mongo;

public class FilialMongoRepository
{
    private readonly IMongoCollection<Filial> _collection;

    public FilialMongoRepository(IMongoDatabase database)
    {
        // Registro do serializer compatível com qualquer versão
        try
        {
            BsonSerializer.RegisterSerializer(typeof(Guid), new GuidSerializer(GuidRepresentation.Standard));
        }
        catch
        {
            // ignora caso já tenha sido registrado
        }

        _collection = database.GetCollection<Filial>("Filiais");
    }

    // CREATE 
    public async Task<Filial> CreateAsync(Filial filial)
    {
        await _collection.InsertOneAsync(filial);
        return filial;
    }
    
    // READ - Todos
    public async Task<List<Filial>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }
    
    // READ - por ID
    public async Task<Filial> GetByIdAsync(Guid id)
    {
        return await _collection.Find(f => f.Id == id).FirstOrDefaultAsync();
    }
    
    // UPDATE
    public async Task<bool> UpdateAsync(Filial filial)
    {
        var filter = Builders<Filial>.Filter.Eq(f => f.Id, filial.Id);
        var result  = await _collection.ReplaceOneAsync(filter, filial);
        return result.ModifiedCount > 0;
    }
    
    // DELETE
    public async Task<bool> DeleteAsync(Guid id)
    {
        var result = await _collection.DeleteOneAsync(f => f.Id == id);
        return result.DeletedCount > 0;
    }
}