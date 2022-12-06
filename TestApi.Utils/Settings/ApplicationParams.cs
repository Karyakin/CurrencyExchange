namespace TestApi.Utils.Settings;

public static class ApplicationParams
{
    private static string? _host;
    public static string? Host
    {
        get => _host;
        set => _host = value ?? throw new InvalidOperationException("Unable to complete operation. The SwaggerHost cannot be obtained.");
    }
    
    private static string? _port;
    public static string? Port
    {
        get => _port;
        set => _port = value ?? throw new InvalidOperationException("Unable to complete operation. The SwaggerPort cannot be obtained.");
    }
    
    private static string? _hostSsl;
    public static string? HostSsl
    {
        get => _hostSsl;
        set => _hostSsl = value ?? throw new InvalidOperationException("Unable to complete operation. The HostSsl cannot be obtained.");
    }
    
    private static string? _portSsl;
    public static string? PortSsl
    {
        get => _portSsl;
        set => _portSsl = value ?? throw new InvalidOperationException("Unable to complete operation. The PortSsl cannot be obtained.");
    }
    private static string? _nationalBankBaseUrl;
    
    public static string? NationalBankBaseUrl
    {
        get => _nationalBankBaseUrl;
        set => _nationalBankBaseUrl = value ?? throw new InvalidOperationException("Unable to complete operation. The NationalBankBaseUrl cannot be obtained.");
    }
    
    public static bool IsDevelopment { get; set; }
}