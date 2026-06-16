using System.Text.Json;

namespace DBConnection.Entity
{
    public class AppGroup
    {
        public int    GroupId         { get; set; }
        public string Name            { get; set; } = string.Empty;
        public string Description     { get; set; } = string.Empty;
        public string PermissionsJson { get; set; } = "[]";
        public Guid   TenantId        { get; set; }

        public ICollection<AppGroupEmployee> GroupEmployees { get; set; } = [];

        public List<string> GetPermissions() =>
            JsonSerializer.Deserialize<List<string>>(PermissionsJson) ?? [];

        public void SetPermissions(IEnumerable<string> perms) =>
            PermissionsJson = JsonSerializer.Serialize(perms.Distinct().ToList());
    }

    public class AppGroupEmployee
    {
        public int   GroupId    { get; set; }
        public Guid  EmployeeId { get; set; }
        public AppGroup Group   { get; set; } = null!;
    }

    public class AppUserRole
    {
        public int    CredentialId { get; set; }
        public string Role         { get; set; } = "USER";
    }
}
