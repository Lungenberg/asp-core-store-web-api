namespace ASPCoreWebApplication.Models
{
    public class MusicStoreDatabaseSettings
    {
        public string ConnectionsString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string MusicCollectionName { get; set; } = null!;
    }
}
