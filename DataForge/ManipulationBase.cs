using DBConnection;
using Microsoft.EntityFrameworkCore;

namespace DataBridge;

/// <summary>
/// Classe de base pour toutes les classes Manipulation.
/// Centralise l'initialisation du DbContext et évite la duplication
/// de la chaîne de connexion dans chaque classe.
/// </summary>
public abstract class ManipulationBase
{
    protected readonly RamssisCleaningContex ramssisCleaningContex;

    protected ManipulationBase()
    {
        ramssisCleaningContex = CreateContext();
    }

    /// <summary>
    /// Crée un nouveau DbContext isolé — à utiliser pour les opérations
    /// d'écriture afin d'éviter toute contamination du contexte partagé.
    /// </summary>
    protected RamssisCleaningContex CreateContext()
    {
        var options = new DbContextOptionsBuilder<RamssisCleaningContex>()
            .UseSqlServer(ConnectionConfig.ConnectionString)
            .Options;
        return new RamssisCleaningContex(options);
    }
}
