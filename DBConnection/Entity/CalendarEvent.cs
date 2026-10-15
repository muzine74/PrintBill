using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBConnection.Entity
{
    // Événement du calendrier (page « Calendrier ») : événement personnel, rendez-vous ou rencontre.
    // L'organisateur le voit toujours ; les participants invités le voient dans leur propre calendrier.
    [Table("CalendarEvents")]
    public class CalendarEvent
    {
        public Guid   EventId     { get; set; }
        public Guid   TenantId    { get; set; }
        public string Title       { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Location    { get; set; }
        /// <summary>« event » (événement), « appointment » (rendez-vous) ou « meeting » (rencontre).</summary>
        public string Kind        { get; set; } = "event";
        public DateTime StartsAt  { get; set; }   // UTC
        public DateTime EndsAt    { get; set; }   // UTC
        /// <summary>Journée entière : StartsAt / EndsAt couvrent des journées complètes (heure locale).</summary>
        public bool   AllDay      { get; set; }
        /// <summary>Employé qui a créé l'événement (EmployeeId).</summary>
        public Guid   OrganizerId { get; set; }
        /// <summary>Numéro de version de l'invitation (fichier .ics) : augmenté à chaque changement de date ou de lieu.</summary>
        public int    Sequence    { get; set; }
        public DateTime CreatedAt { get; set; }   // UTC
        public DateTime UpdatedAt { get; set; }   // UTC
        public string?  CreatedBy { get; set; }

        public virtual ICollection<CalendarAttendee> Attendees { get; set; } = new List<CalendarAttendee>();
        public virtual ICollection<CalendarReminder> Reminders { get; set; } = new List<CalendarReminder>();
    }

    // Participant invité à un événement (l'organisateur n'y figure pas).
    [Table("CalendarAttendees")]
    public class CalendarAttendee
    {
        public Guid   EventId     { get; set; }
        public Guid   EmployeeId  { get; set; }
        /// <summary>« pending » (sans réponse), « accepted », « tentative » ou « declined ».</summary>
        public string Response    { get; set; } = "pending";
        public DateTime  InvitedAt   { get; set; }   // UTC
        public DateTime? RespondedAt { get; set; }   // UTC

        public virtual CalendarEvent? Event { get; set; }
    }

    // Rappel automatique d'un événement, envoyé à l'organisateur et aux participants qui n'ont pas refusé.
    [Table("CalendarReminders")]
    public class CalendarReminder
    {
        public Guid   ReminderId    { get; set; }
        public Guid   EventId       { get; set; }
        public Guid   TenantId      { get; set; }
        /// <summary>Délai avant le début de l'événement, en minutes (0 = au début).</summary>
        public int    MinutesBefore { get; set; }
        /// <summary>« app » (notification dans TimeGuard), « email » ou « both ».</summary>
        public string Channel       { get; set; } = "app";
        /// <summary>Moment d'envoi (UTC) = début − délai ; recalculé quand l'événement est déplacé.</summary>
        public DateTime  DueAt      { get; set; }
        /// <summary>Envoyé le (UTC) ; null = pas encore envoyé.</summary>
        public DateTime? SentAt     { get; set; }

        public virtual CalendarEvent? Event { get; set; }
    }
}
