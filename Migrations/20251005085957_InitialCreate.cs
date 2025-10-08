using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateofbirth = table.Column<DateTime>(type: "date", nullable: false),
                    class_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.id);
                    table.ForeignKey(
                        name: "FK_Students_Classes_class_id",
                        column: x => x.class_id,
                        principalTable: "Classes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_class_id",
                table: "Students",
                column: "class_id");

            migrationBuilder.Sql(@"
                ALTER TABLE Students
                ADD CONSTRAINT CHK_Student_DateOfBirth CHECK (YEAR(dateofbirth) < YEAR(GETDATE()))");

            migrationBuilder.Sql(@"
                CREATE TRIGGER TRG_ResetIdStudent
                ON Students
                AFTER DELETE
                AS
                BEGIN
                    IF NOT EXISTS (SELECT 1 FROM Students)
                        DBCC CHECKIDENT ('Students', RESEED, 0);
                END
            ");

            migrationBuilder.Sql(@"
                CREATE TRIGGER TRG_ResetIdClass
                ON Classes
                AFTER DELETE
                AS
                BEGIN
                    IF NOT EXISTS (SELECT 1 FROM Classes)
                        DBCC CHECKIDENT ('Classes', RESEED, 0);
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS TRG_ResetStudentId");
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS TRG_ResetClassId");
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Classes");
        }
    }
}
