using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using VisionHive.Domain.Entities;

namespace VisionHive.Infrastructure.Repositories.Mongo;

public class PatioMongoRepository
{
    //
    private readonly IMongoCollection<Patio> _collection;

    public PatioMongoRepository(IMongoDatabase database)
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

        _collection = database.GetCollection<Patio>("Patios");
    }

    public async Task<Patio> CreateAsync(Patio patio)
    {
        await _collection.InsertOneAsync(patio);
        return patio;
    }

    public async Task<List<Patio>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }
    
}