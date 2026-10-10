using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBConnection.Entity
{
    // Historique de communication d'une compagnie ou d'un employé (page « Communications ») :
    // courriels envoyés par TimeGuard (enregistrés automatiquement) et entrées saisies à la main
    // (appel, rencontre, courriel externe, texto…).
    [Table("Communications")]
    public class Communication
    {
        public Guid   CommunicationId { get; set; }
        public Guid   TenantId        { get; set; }
        /// <summary>« company » ou « employee ».</summary>
        public string TargetType      { get; set; } = string.Empty;
        /// <summary>CompanyId ou EmployeeId selon TargetType (pas de clé étrangère : deux tables possibles).</summary>
        public Guid   TargetId        { get; set; }
        public DateTime OccurredAt    { get; set; }   // UTC
        /// <summary>« email », « call », « meeting », « sms » ou « other ».</summary>
        public string Channel         { get; set; } = string.Empty;
        /// <summary>« out » (vers la compagnie / l'employé) ou « in » (reçu).</summary>
        public string Direction       { get; set; } = string.Empty;
        /// <summary>Destinataires du courriel ou interlocuteur.</summary>
        public string? Contact        { get; set; }
        public string  Subject        { get; set; } = string.Empty;
        public string? Body           { get; set; }
        public string? Attachment     { get; set; }
        /// <summary>Vrai = enregistré par TimeGuard lors d'un envoi (non modifiable).</summary>
        public bool    IsAutomatic    { get; set; }
        /// <summary>Envoi automatique échoué : message d'erreur ; null = réussi.</summary>
        public string? Error          { get; set; }
        public DateTime CreatedAt     { get; set; }   // UTC
        public string?  CreatedBy     { get; set; }
    }
}
