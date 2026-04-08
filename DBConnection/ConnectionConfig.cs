namespace DBConnection;

/// <summary>
/// Point unique de configuration de la chaîne de connexion.
/// Peut être surchargé au démarrage de l'application via :
///   ConnectionConfig.ConnectionString = builder.Configuration.GetConnectionString("Default");
/// </summary>
public static class ConnectionConfig
{
    public static string ConnectionString { get; set; } =
        "Server=DESKTOP-71ON71H\\SQLEXPRESS;Database=RamssisCleaningDB;Trusted_Connection=True;TrustServerCertificate=true;";
}
