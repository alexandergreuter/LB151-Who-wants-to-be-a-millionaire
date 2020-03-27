namespace LB_151.Data
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string LeaderboardCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CategoryCollectionName { get; set; }
    }

    public interface IDatabaseSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
        string LeaderboardCollectionName { get; set; }
        string CategoryCollectionName { get; set; }
    }
}