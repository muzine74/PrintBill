using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBConnection.Entity
{
    // Comptes reliés (« Mes entreprises ») : une personne qui a un identifiant dans plusieurs entreprises les relie
    // pour basculer de l'une à l'autre sans se reconnecter. Les identifiants d'un même LinkGroupId sont reliés
    // entre eux ; un identifiant appartient au plus à un groupe. Dans chaque entreprise, les droits restent ceux
    // de l'identifiant de cette entreprise (ses groupes) : un lien ne donne aucun droit de plus.
    [Table("CredentialLinks")]
    public class CredentialLink
    {
        /// <summary>EmployeeCredentials.EmployeeCredentialId (une ligne par identifiant relié).</summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int      CredentialId { get; set; }
        public Guid     LinkGroupId  { get; set; }
        /// <summary>Entreprise de l'identifiant (copie, pour contrôler qu'un groupe n'a qu'un compte par entreprise).</summary>
        public Guid     TenantId     { get; set; }
        /// <summary>Empreinte du mot de passe de l'identifiant au moment où il a été relié : si le mot de passe change
        /// ensuite, la bascule vers ce compte est refusée tant qu'il n'est pas relié de nouveau.</summary>
        public string   PasswordStamp { get; set; } = string.Empty;
        public DateTime CreatedAt    { get; set; }   // UTC
        public string?  CreatedBy    { get; set; }
    }
}
