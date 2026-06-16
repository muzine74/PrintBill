namespace DBConnection.Entity
{
    public class AppPermission
    {
        public int    PermissionId { get; set; }
        public string Key          { get; set; } = string.Empty;
        public string Label        { get; set; } = string.Empty;
        public string Module       { get; set; } = string.Empty;
    }
}
