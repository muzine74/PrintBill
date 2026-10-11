using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBConnection.Entity
{
    // Téléphonie d'une entreprise (Administration → Configuration → Téléphonie) : compte Twilio utilisé
    // pour passer des appels depuis TimeGuard. Une ligne par entreprise.
    [Table("TelephonyConfigs")]
    public class TelephonyConfig
    {
        public Guid    TenantId       { get; set; }
        public bool    Enabled        { get; set; }
        /// <summary>Compte (ou sous-compte) Twilio : AC…</summary>
        public string? AccountSid     { get; set; }
        /// <summary>Jeton d'authentification du compte, chiffré : sert à vérifier que les requêtes viennent de Twilio.</summary>
        public string? AuthToken      { get; set; }
        /// <summary>Clé d'API : SK…</summary>
        public string? ApiKeySid      { get; set; }
        /// <summary>Secret de la clé d'API, chiffré.</summary>
        public string? ApiKeySecret   { get; set; }
        /// <summary>Application TwiML : AP…</summary>
        public string? TwimlAppSid    { get; set; }
        /// <summary>Numéro affiché aux personnes appelées, au format E.164.</summary>
        public string? CallerNumber   { get; set; }
        /// <summary>Adresse publique du site (https://…), d'où Twilio appelle l'API.</summary>
        public string? PublicBaseUrl  { get; set; }
        /// <summary>« off » ou « all ».</summary>
        public string  RecordingMode  { get; set; } = "off";
        /// <summary>Message lu à la personne appelée quand l'appel est enregistré.</summary>
        public string? RecordingNotice { get; set; }
        /// <summary>Nombre de jours de conservation des enregistrements ; 0 = sans limite.</summary>
        public int     RetentionDays  { get; set; }
        public DateTime UpdatedAt     { get; set; }   // UTC
    }
}
