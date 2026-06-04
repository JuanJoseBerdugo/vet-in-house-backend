using Supabase;

namespace VetInHouse.Services;

public class SupabaseService
{
    public Client AnonClient { get; }
    public Client AdminClient { get; }

    public SupabaseService(IConfiguration config)
    {
        var url = config["Supabase:Url"] ?? throw new InvalidOperationException("Supabase:Url no configurado");
        var anonKey = config["Supabase:AnonKey"] ?? throw new InvalidOperationException("Supabase:AnonKey no configurado");
        var serviceKey = config["Supabase:ServiceRoleKey"] ?? throw new InvalidOperationException("Supabase:ServiceRoleKey no configurado");

        AnonClient = new Client(url, anonKey, new SupabaseOptions
        {
            AutoRefreshToken = false,
            AutoConnectRealtime = false
        });

        AdminClient = new Client(url, serviceKey, new SupabaseOptions
        {
            AutoRefreshToken = false,
            AutoConnectRealtime = false
        });
    }

    public async Task InitAsync()
    {
        await AnonClient.InitializeAsync();
        await AdminClient.InitializeAsync();
    }
}
