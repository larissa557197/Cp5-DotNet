using MongoDB.Driver;

namespace VisionHive.Infrastructure.Mongo
{
    public class MongoContext
    {
        public IMongoDatabase Database { get; }
        public MongoContext(MongoOptions options)
        {
            var client = new MongoClient(options.ConnectionString);
            Database = client.GetDatabase(options.Database);
        }
    }
}
