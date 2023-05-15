using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace techlingo.projeto.Migrations
{
    /// <inheritdoc />
    public partial class criacao_update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DT_CONCLUSAO",
                table: "TL_CURSO");

            migrationBuilder.AlterColumn<decimal>(
                name: "VL_PLANO",
                table: "TL_PLANO",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DT_CONCLUSAO",
                table: "TL_ALUNO_CURSO",
                type: "TIMESTAMP(7)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DT_CONCLUSAO",
                table: "TL_ALUNO_CURSO");

            migrationBuilder.AlterColumn<decimal>(
                name: "VL_PLANO",
                table: "TL_PLANO",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DT_CONCLUSAO",
                table: "TL_CURSO",
                type: "TIMESTAMP(7)",
                nullable: true);
        }
    }
}
