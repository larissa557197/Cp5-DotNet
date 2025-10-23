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

    public async Task<Filial> CreateAsync(Filial filial)
    {
        await _collection.InsertOneAsync(filial);
        return filial;
    }

    public async Task<List<Filial>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }
    
    
}