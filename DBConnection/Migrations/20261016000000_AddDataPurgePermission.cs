using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    /// <summary>Permission « data.purge » (Suppression définitive) dans le catalogue global des permissions.
    /// Aucune attribution automatique : à donner à un groupe depuis Administration → Groupes.</summary>
    public partial class AddDataPurgePermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM [AppPermissions] WHERE [Key] = N'data.purge')
                    INSERT INTO [AppPermissions] ([Key], [Label], [Module])
                    VALUES (N'data.purge', N'Suppression définitive', N'Application');
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE FROM [AppPermissions] WHERE [Key] = N'data.purge';");
        }
    }
}
