using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace techlingo.projeto.Migrations
{
    /// <inheritdoc />
    public partial class criacao_update3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "VL_PLANO",
                table: "TL_PLANO",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "ST_STATUS",
                table: "TL_ALUNO_CURSO",
                type: "NVARCHAR2(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DS_SENHA",
                table: "TL_ALUNO",
                type: "NVARCHAR2(30)",
                maxLength: 30,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DS_SENHA",
                table: "TL_ALUNO");

            migrationBuilder.AlterColumn<decimal>(
                name: "VL_PLANO",
                table: "TL_PLANO",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "ST_STATUS",
                table: "TL_ALUNO_CURSO",
                type: "NVARCHAR2(2000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(20)",
                oldMaxLength: 20,
                oldNullable: true);
        }
    }
}
