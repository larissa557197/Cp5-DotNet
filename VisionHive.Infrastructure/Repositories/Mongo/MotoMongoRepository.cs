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
        // Registro do serializer compatível com qualquer versão
        try
        {
            BsonSerializer.RegisterSerializer(typeof(Guid), new GuidSerializer(GuidRepresentation.Standard));
        }
        catch
        {
            // ignora caso já tenha sido registrado
        }

        _collection = database.GetCollection<Moto>("Motos");
    }


    public async Task<Moto> CreateAsync(Moto moto)
    {
        await _collection.InsertOneAsync(moto);
        return moto;
    }


    public async Task<List<Moto>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }
}