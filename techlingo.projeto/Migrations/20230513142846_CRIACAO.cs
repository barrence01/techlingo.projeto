using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace techlingo.projeto.Migrations
{
    /// <inheritdoc />
    public partial class CRIACAO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TL_CURSO",
                columns: table => new
                {
                    ID_CURSO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NM_CURSO = table.Column<string>(type: "NVARCHAR2(80)", maxLength: 80, nullable: false),
                    DS_DURACAO = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_CURSO", x => x.ID_CURSO);
                });

            migrationBuilder.CreateTable(
                name: "TL_PLANO",
                columns: table => new
                {
                    ID_PLANO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NM_PLANO = table.Column<string>(type: "NVARCHAR2(80)", maxLength: 80, nullable: false),
                    VL_PLANO = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_PLANO", x => x.ID_PLANO);
                });

            migrationBuilder.CreateTable(
                name: "TL_ALUNO",
                columns: table => new
                {
                    ID_ALUNO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NM_ALUNO = table.Column<string>(type: "NVARCHAR2(80)", maxLength: 80, nullable: false),
                    NR_CPF = table.Column<string>(type: "NVARCHAR2(14)", maxLength: 14, nullable: false),
                    DS_EMAIL = table.Column<string>(type: "NVARCHAR2(80)", maxLength: 80, nullable: false),
                    DT_NASCIMENTO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    FK_ID_PLANO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_ALUNO", x => x.ID_ALUNO);
                    table.ForeignKey(
                        name: "FK_TL_ALUNO_TL_PLANO_FK_ID_PLANO",
                        column: x => x.FK_ID_PLANO,
                        principalTable: "TL_PLANO",
                        principalColumn: "ID_PLANO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TL_ALUNO_CURSO",
                columns: table => new
                {
                    ID_ALUNO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ID_CURSO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    NR_NOTA = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ST_STATUS = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_ALUNO_CURSO", x => new { x.ID_ALUNO, x.ID_CURSO });
                    table.ForeignKey(
                        name: "FK_TL_ALUNO_CURSO_TL_ALUNO_ID_ALUNO",
                        column: x => x.ID_ALUNO,
                        principalTable: "TL_ALUNO",
                        principalColumn: "ID_ALUNO",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TL_ALUNO_CURSO_TL_CURSO_ID_CURSO",
                        column: x => x.ID_CURSO,
                        principalTable: "TL_CURSO",
                        principalColumn: "ID_CURSO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TL_ALUNO_FK_ID_PLANO",
                table: "TL_ALUNO",
                column: "FK_ID_PLANO");

            migrationBuilder.CreateIndex(
                name: "IX_TL_ALUNO_CURSO_ID_CURSO",
                table: "TL_ALUNO_CURSO",
                column: "ID_CURSO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TL_ALUNO_CURSO");

            migrationBuilder.DropTable(
                name: "TL_ALUNO");

            migrationBuilder.DropTable(
                name: "TL_CURSO");

            migrationBuilder.DropTable(
                name: "TL_PLANO");
        }
    }
}
