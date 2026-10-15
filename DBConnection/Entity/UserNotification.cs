using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBConnection.Entity
{
    // Notification affichée dans TimeGuard (cloche de la barre de navigation) pour un employé :
    // invitation, modification, annulation, réponse à une invitation, rappel d'événement…
    [Table("UserNotifications")]
    public class UserNotification
    {
        public Guid   NotificationId { get; set; }
        public Guid   TenantId       { get; set; }
        /// <summary>Destinataire (EmployeeId).</summary>
        public Guid   EmployeeId     { get; set; }
        /// <summary>« invitation », « update », « cancel », « response » ou « reminder ».</summary>
        public string Kind           { get; set; } = string.Empty;
        public string Title          { get; set; } = string.Empty;
        public string? Body          { get; set; }
        /// <summary>Adresse de la page à ouvrir dans TimeGuard (ex. « /calendrier?event=… »).</summary>
        public string? Link          { get; set; }
        /// <summary>Événement concerné (sans clé étrangère : la notification reste si l'événement est supprimé).</summary>
        public Guid?  EventId        { get; set; }
        public DateTime  CreatedAt   { get; set; }   // UTC
        /// <summary>Lue le (UTC) ; null = non lue.</summary>
        public DateTime? ReadAt      { get; set; }
    }
}
