using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parameter.API.Migrations
{
    /// <inheritdoc />
    public partial class parameter_id_type_changed_ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // === VergiParametreleri ===
            // 1) Geçici GUID kolon ekle (default NEWSEQUENTIALID)
            migrationBuilder.AddColumn<Guid>(
                name: "NewId",
                table: "VergiParametreleri",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWSEQUENTIALID()");

            // 2) Eski PK'yi kaldır
            migrationBuilder.DropPrimaryKey(
                name: "PK_VergiParametreleri",
                table: "VergiParametreleri");

            // 3) Eski int Id kolonunu kaldır
            migrationBuilder.DropColumn(
                name: "Id",
                table: "VergiParametreleri");

            // 4) NewId -> Id olarak yeniden adlandır
            migrationBuilder.RenameColumn(
                name: "NewId",
                table: "VergiParametreleri",
                newName: "Id");

            // 5) Yeni PK'yi ata
            migrationBuilder.AddPrimaryKey(
                name: "PK_VergiParametreleri",
                table: "VergiParametreleri",
                column: "Id");

            // === GelirVergisiDilimleri ===
            migrationBuilder.AddColumn<Guid>(
                name: "NewId",
                table: "GelirVergisiDilimleri",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWSEQUENTIALID()");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GelirVergisiDilimleri",
                table: "GelirVergisiDilimleri");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "GelirVergisiDilimleri");

            migrationBuilder.RenameColumn(
                name: "NewId",
                table: "GelirVergisiDilimleri",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GelirVergisiDilimleri",
                table: "GelirVergisiDilimleri",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // === VergiParametreleri (geri dönüş: GUID -> int identity) ===
            migrationBuilder.AddColumn<int>(
                name: "OldIntId",
                table: "VergiParametreleri",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VergiParametreleri",
                table: "VergiParametreleri");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "VergiParametreleri");

            migrationBuilder.RenameColumn(
                name: "OldIntId",
                table: "VergiParametreleri",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VergiParametreleri",
                table: "VergiParametreleri",
                column: "Id");

            // === GelirVergisiDilimleri (geri dönüş) ===
            migrationBuilder.AddColumn<int>(
                name: "OldIntId",
                table: "GelirVergisiDilimleri",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GelirVergisiDilimleri",
                table: "GelirVergisiDilimleri");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "GelirVergisiDilimleri");

            migrationBuilder.RenameColumn(
                name: "OldIntId",
                table: "GelirVergisiDilimleri",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GelirVergisiDilimleri",
                table: "GelirVergisiDilimleri",
                column: "Id");
        }
    }
}
