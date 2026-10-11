using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBConnection.Entity
{
    // Boîte de courriel d'une entreprise (Administration → Configuration → Courriels reçus) : boîte
    // Microsoft 365 lue par TimeGuard pour ajouter à l'historique de communication les courriels
    // reçus des contacts des compagnies. Une ligne par entreprise.
    [Table("MailboxConfigs")]
    public class MailboxConfig
    {
        public Guid    TenantId       { get; set; }
        public bool    Enabled        { get; set; }
        /// <summary>Identifiant de l'annuaire Microsoft 365 (« Directory (tenant) ID »).</summary>
        public string? DirectoryId    { get; set; }
        /// <summary>Identifiant de l'application inscrite (« Application (client) ID »).</summary>
        public string? ClientId       { get; set; }
        /// <summary>Secret de l'application, chiffré.</summary>
        public string? ClientSecret   { get; set; }
        /// <summary>Adresse de la boîte lue (info@entreprise.com).</summary>
        public string? Mailbox        { get; set; }
        /// <summary>Date de réception du dernier courriel examiné : la lecture suivante reprend après.</summary>
        public DateTime? LastReceivedAt { get; set; }   // UTC
        public DateTime? LastCheckedAt  { get; set; }   // UTC
        /// <summary>Dernière lecture échouée : message d'erreur ; null = réussie.</summary>
        public string? LastError      { get; set; }
        public DateTime UpdatedAt     { get; set; }   // UTC
    }
}
