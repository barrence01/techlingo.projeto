using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace techlingo.projeto.Migrations
{
    /// <inheritdoc />
    public partial class criacao1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TL_CURSOS",
                columns: table => new
                {
                    id_curso = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nm_curso = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    ds_curso = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    dt_criacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_CURSOS", x => x.id_curso);
                });

            migrationBuilder.CreateTable(
                name: "TL_PLANOS",
                columns: table => new
                {
                    id_plano = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nm_plano = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    vl_plano = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_PLANOS", x => x.id_plano);
                });

            migrationBuilder.CreateTable(
                name: "TL_AULAS",
                columns: table => new
                {
                    id_aula = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nr_aula = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    nm_aula = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    dt_criacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    id_curso = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_AULAS", x => x.id_aula);
                    table.ForeignKey(
                        name: "FK_TL_AULAS_TL_CURSOS_id_curso",
                        column: x => x.id_curso,
                        principalTable: "TL_CURSOS",
                        principalColumn: "id_curso",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TL_ALUNO",
                columns: table => new
                {
                    id_aluno = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nm_aluno = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    dt_nascimento = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    nr_cpf = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    dt_criacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    dt_alteracao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    ds_email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    ds_senha = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    id_plano = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_ALUNO", x => x.id_aluno);
                    table.ForeignKey(
                        name: "FK_TL_ALUNO_TL_PLANOS_id_plano",
                        column: x => x.id_plano,
                        principalTable: "TL_PLANOS",
                        principalColumn: "id_plano",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TL_AULA_CONTEUDO",
                columns: table => new
                {
                    id_aula_conteudo = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ds_titulo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    ds_link_video = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    ds_descricao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    id_aula = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_AULA_CONTEUDO", x => x.id_aula_conteudo);
                    table.ForeignKey(
                        name: "FK_TL_AULA_CONTEUDO_TL_AULAS_id_aula",
                        column: x => x.id_aula,
                        principalTable: "TL_AULAS",
                        principalColumn: "id_aula",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TL_ALUNO_CURSO",
                columns: table => new
                {
                    id_aluno_cursante = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    st_status = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    dt_entrada = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    dt_termino = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    id_curso = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    id_aluno = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_ALUNO_CURSO", x => x.id_aluno_cursante);
                    table.ForeignKey(
                        name: "FK_TL_ALUNO_CURSO_TL_ALUNO_id_aluno",
                        column: x => x.id_aluno,
                        principalTable: "TL_ALUNO",
                        principalColumn: "id_aluno",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TL_ALUNO_CURSO_TL_CURSOS_id_curso",
                        column: x => x.id_curso,
                        principalTable: "TL_CURSOS",
                        principalColumn: "id_curso",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TL_AULA_QUIZ",
                columns: table => new
                {
                    id_quiz = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ds_titulo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    ds_descricao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    ds_pergunta = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    ds_pergunta2 = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    ds_pergunta3 = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    ds_resposta = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    id_aula_conteudo = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_AULA_QUIZ", x => x.id_quiz);
                    table.ForeignKey(
                        name: "FK_TL_AULA_QUIZ_TL_AULA_CONTEUDO_id_aula_conteudo",
                        column: x => x.id_aula_conteudo,
                        principalTable: "TL_AULA_CONTEUDO",
                        principalColumn: "id_aula_conteudo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TL_AULA_CURSADA",
                columns: table => new
                {
                    id_aula_cursada = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    st_status = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    dt_termino = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    id_aluno_cursante = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    id_aula = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TL_AULA_CURSADA", x => x.id_aula_cursada);
                    table.ForeignKey(
                        name: "FK_TL_AULA_CURSADA_TL_ALUNO_CURSO_id_aluno_cursante",
                        column: x => x.id_aluno_cursante,
                        principalTable: "TL_ALUNO_CURSO",
                        principalColumn: "id_aluno_cursante",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TL_AULA_CURSADA_TL_AULAS_id_aula",
                        column: x => x.id_aula,
                        principalTable: "TL_AULAS",
                        principalColumn: "id_aula",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TL_ALUNO_id_plano",
                table: "TL_ALUNO",
                column: "id_plano");

            migrationBuilder.CreateIndex(
                name: "IX_TL_ALUNO_CURSO_id_aluno",
                table: "TL_ALUNO_CURSO",
                column: "id_aluno");

            migrationBuilder.CreateIndex(
                name: "IX_TL_ALUNO_CURSO_id_curso",
                table: "TL_ALUNO_CURSO",
                column: "id_curso");

            migrationBuilder.CreateIndex(
                name: "IX_TL_AULA_CONTEUDO_id_aula",
                table: "TL_AULA_CONTEUDO",
                column: "id_aula",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TL_AULA_CURSADA_id_aluno_cursante",
                table: "TL_AULA_CURSADA",
                column: "id_aluno_cursante");

            migrationBuilder.CreateIndex(
                name: "IX_TL_AULA_CURSADA_id_aula",
                table: "TL_AULA_CURSADA",
                column: "id_aula");

            migrationBuilder.CreateIndex(
                name: "IX_TL_AULA_QUIZ_id_aula_conteudo",
                table: "TL_AULA_QUIZ",
                column: "id_aula_conteudo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TL_AULAS_id_curso",
                table: "TL_AULAS",
                column: "id_curso");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TL_AULA_CURSADA");

            migrationBuilder.DropTable(
                name: "TL_AULA_QUIZ");

            migrationBuilder.DropTable(
                name: "TL_ALUNO_CURSO");

            migrationBuilder.DropTable(
                name: "TL_AULA_CONTEUDO");

            migrationBuilder.DropTable(
                name: "TL_ALUNO");

            migrationBuilder.DropTable(
                name: "TL_AULAS");

            migrationBuilder.DropTable(
                name: "TL_PLANOS");

            migrationBuilder.DropTable(
                name: "TL_CURSOS");
        }
    }
}
