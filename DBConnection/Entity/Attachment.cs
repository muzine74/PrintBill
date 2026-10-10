using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBConnection.Entity
{
    // Pièce jointe d'une note ou d'une entrée de l'historique de communication.
    // Le fichier est sur disque : Billing\{Fournisseur}\attachments\{OwnerType}\{OwnerId}\{FileName}.
    [Table("Attachments")]
    public class Attachment
    {
        public Guid   AttachmentId { get; set; }
        public Guid   TenantId     { get; set; }
        /// <summary>« note » ou « communication ».</summary>
        public string OwnerType    { get; set; } = string.Empty;
        /// <summary>NoteId ou CommunicationId selon OwnerType (pas de clé étrangère : deux tables possibles).</summary>
        public Guid   OwnerId      { get; set; }
        public string FileName     { get; set; } = string.Empty;   // nom sur disque
        public string OriginalName { get; set; } = string.Empty;   // nom d'origine
        public long   Size         { get; set; }                   // octets
        public DateTime UploadedAt { get; set; }   // UTC
        public string?  UploadedBy { get; set; }
    }
}
