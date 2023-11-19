using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using techlingo.projeto.Models.Cursos;

namespace techlingo.projeto.Models.Aluno
{
    [Table("TL_AULA_CURSADA")]
    public class AlunoAulasCursadasModel
    {
        [Key]
        [Column("id_aula_cursada")]
        public int id_aula_cursada { get; set; }

        [Column("st_status")]
        public string? st_status { get; set; } // Concluido // Não concluido // Não iniciado

        [Column("dt_termino")]
        public DateTime? dt_termino { get; set; }


        // Relacionamento

        [Column("id_aluno_cursante")]
        public int id_aluno_cursante { get; set; }
        [JsonIgnore]
        public AlunoCursosCursadosModel? cursante { get; set; }

        [Column("id_aula")]
        public int id_aula { get; set; }
        [JsonIgnore]
        public AulasModel? aula { get; set; }


        public AlunoAulasCursadasModel()
        {

        }

        public AlunoAulasCursadasModel(AlunoAulasCursadasModel alunoAulasCursadasModel)
        {
            this.st_status = "Não concluido";
            this.id_aluno_cursante = alunoAulasCursadasModel.id_aluno_cursante;
            this.id_aula = alunoAulasCursadasModel.id_aula;
        }

        public void EditInfo(bool termino)
        {
            if (termino)
            {
                this.st_status = "Concluido";
                this.dt_termino = DateTime.Now;
            } else
            {
                this.st_status = "Não concluido";
                this.dt_termino = null;
            }
        }
    }
}
