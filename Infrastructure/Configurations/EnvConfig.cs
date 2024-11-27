namespace DompifyAPI.Infrastructure.Configurations
{
    public class EnvConfig
    {
        // Application Configuration
        public static string AppName => Environment.GetEnvironmentVariable("APP_NAME")!.ToString();
        public static string AppVersion => Environment.GetEnvironmentVariable("APP_VERSION")!.ToString();
        public static string AppDebug => Environment.GetEnvironmentVariable("APP_DEBUG")!.ToString();

        // Server Configuration
        public static string ServerHost => Environment.GetEnvironmentVariable("SERVER_HOST")!.ToString();

        // Database Configuration
        public static string DbHost => Environment.GetEnvironmentVariable("DB_HOST")!.ToString();
        public static string DbPort => Environment.GetEnvironmentVariable("DB_PORT")!.ToString();
        public static string DbName => Environment.GetEnvironmentVariable("DB_NAME")!.ToString();
        public static string DbUser => Environment.GetEnvironmentVariable("DB_USER")!.ToString();
        public static string DbPassword => Environment.GetEnvironmentVariable("DB_PASSWORD")!.ToString();
    }
}
