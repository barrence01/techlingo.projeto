using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace techlingo.projeto.Migrations
{
    /// <inheritdoc />
    public partial class criacao_update4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DS_SENHA",
                table: "TL_ALUNO",
                type: "NVARCHAR2(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(30)",
                oldMaxLength: 30,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DS_SENHA",
                table: "TL_ALUNO",
                type: "NVARCHAR2(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(30)",
                oldMaxLength: 30);
        }
    }
}
