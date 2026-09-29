using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBConnection.Migrations
{
    /// <summary>
    /// Une note peut être liée à plusieurs entités : table NoteLinks (remplace l'usage de
    /// Notes.LinkedEntityType/LinkedEntityId, qui restent en base mais ne sont plus lues).
    /// Les liens existants sont recopiés.
    /// </summary>
    public partial class AddNoteLinks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF OBJECT_ID(N'NoteLinks') IS NULL
                BEGIN
                    CREATE TABLE [NoteLinks] (
                        [NoteLinkId] UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_NoteLinks PRIMARY KEY,
                        [NoteId]     UNIQUEIDENTIFIER NOT NULL
                            CONSTRAINT FK_NoteLinks_Notes_NoteId REFERENCES [Notes]([NoteId]) ON DELETE CASCADE,
                        [EntityType] NVARCHAR(20)     NOT NULL,
                        [EntityId]   UNIQUEIDENTIFIER NOT NULL
                    );
                    CREATE INDEX IX_NoteLinks_EntityType_EntityId ON [NoteLinks]([EntityType], [EntityId]);
                    CREATE UNIQUE INDEX IX_NoteLinks_NoteId_EntityType_EntityId ON [NoteLinks]([NoteId], [EntityType], [EntityId]);
                END
            ");
            // Recopie des liens uniques existants (dynamique : les colonnes peuvent ne pas exister sur une base fraîche).
            migrationBuilder.Sql(@"
                IF COL_LENGTH(N'Notes', N'LinkedEntityId') IS NOT NULL
                    EXEC(N'
                        INSERT INTO [NoteLinks] ([NoteLinkId], [NoteId], [EntityType], [EntityId])
                        SELECT NEWID(), n.[NoteId], n.[LinkedEntityType], n.[LinkedEntityId]
                        FROM [Notes] n
                        WHERE n.[LinkedEntityType] IS NOT NULL AND n.[LinkedEntityId] IS NOT NULL
                          AND NOT EXISTS (SELECT 1 FROM [NoteLinks] l
                                          WHERE l.[NoteId] = n.[NoteId]
                                            AND l.[EntityType] = n.[LinkedEntityType]
                                            AND l.[EntityId] = n.[LinkedEntityId]);');
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF OBJECT_ID(N'NoteLinks') IS NOT NULL DROP TABLE [NoteLinks];");
        }
    }
}
