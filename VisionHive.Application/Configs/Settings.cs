
using VisionHive.Application.Configs;

namespace VisionHive.Application.Configs
{
    public class Settings
    {
        public SwaggerSettings Swagger { get; set; }
        public MongoDbSettings MongoDb { get; set; }
        public MySqlSettings ConnectionStrings { get; set; }
    }
}
