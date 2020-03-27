namespace LB_151.Data
{
    public class AdminSettings : IAdminSettings
    {
        public string Password { get; set; }
        public int ExpirationTime { get; set; }
        public int AuthenticationStringLength { get; set; }
    }

    public interface IAdminSettings
    {
        public string Password { get; set; }
        public int ExpirationTime { get; set; }
        public int AuthenticationStringLength { get; set; }
    }
}