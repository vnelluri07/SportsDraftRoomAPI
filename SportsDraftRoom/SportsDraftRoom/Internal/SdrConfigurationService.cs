namespace SportsDraftRoom.Internal;

public class SdrConfigurationService : ISdrConfigurationService
{
    private readonly IConfiguration _configuration;

    public SdrConfigurationService(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public string SdrConnection => _configuration.GetConnectionString("SdrConnectionString") ?? throw new ArgumentNullException(nameof(SdrConnection), "Connection string can not be null");
}
