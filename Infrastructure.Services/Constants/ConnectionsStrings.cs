namespace Infrastructure.Services.Constants
{
    public class ConnectionsStrings
    {
        public const string SqlServer = "Server={0};Database={1};User Id={2};Password={3};Encrypt=False;MultipleActiveResultSets=True;";
        public const string SqlServerUnsafe = "Server={0};Database={1};Encrypt=False;MultipleActiveResultSets=True;";

        public const string Redis = "{0},password={1},ssl=False,abortConnect=False";
        public const string RabbitMq = "amqps://{2}:{3}@{0}:{1}{4}";
    }
}