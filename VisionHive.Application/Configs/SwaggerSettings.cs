

namespace VisionHive.Application.Configs
{
    public sealed class SwaggerSettings
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public SwaggerContact Contact { get; set; }
        public List<SwaggerServer> Servers { get; set; }
    }
    
    public sealed class SwaggerContact
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public sealed class SwaggerServer
    {
        public string Url { get; set; }
        public string Description { get; set; }
    }
}
