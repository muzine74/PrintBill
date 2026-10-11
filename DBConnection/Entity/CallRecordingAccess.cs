using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBConnection.Entity
{
    // Trace des écoutes : qui a écouté l'enregistrement de quel appel, et quand.
    [Table("CallRecordingAccesses")]
    public class CallRecordingAccess
    {
        public Guid     CallRecordingAccessId { get; set; }
        public Guid     TenantId        { get; set; }
        public Guid     CommunicationId { get; set; }
        public DateTime ListenedAt      { get; set; }   // UTC
        public string?  ListenedBy      { get; set; }
    }
}
