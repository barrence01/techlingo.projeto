using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using techlingo.projeto.Controllers.DTO.Aluno;
using techlingo.projeto.Models.Cursos;

namespace techlingo.projeto.Models.Aluno
{
    [Table("TL_ALUNO_CURSO")]
    public class AlunoCursosCursadosModel
    {
        [Key]
        [Column("id_aluno_cursante")]
        public int id_aluno_cursante { get; set; }

        [Column("st_status")]
        public string? st_status { get; set; } // Em andamento // Concluido

        [Column("dt_entrada")]
        public DateTime? dt_entrada { get; set; }

        [Column("dt_termino")]
        public DateTime? dt_termino { get; set; }


        // Relacionamento

        [Column("id_curso")]
        public int id_curso { get; set; }
        [JsonIgnore]
        public CursosModel? curso { get; set; }

        [Column("id_aluno")]
        public int id_aluno { get; set; }
        [JsonIgnore]
        public AlunoModel? aluno { get; set; }


        public ICollection<AlunoAulasCursadasModel>? aulas_cursadas { get; set; }



        public AlunoCursosCursadosModel()
        {

        }

        public AlunoCursosCursadosModel(AlunoCursosCursadosRequestDTO alunoCursosCursadosRequest)
        {
            this.st_status = "Em andamento";
            this.dt_entrada = DateTime.Now;
            this.id_curso = alunoCursosCursadosRequest.id_curso;
            this.id_aluno = alunoCursosCursadosRequest.id_aluno;
        }

        public AlunoCursosCursadosModel(int id_aluno, int id_curso)
        {
            this.st_status = "Em andamento";
            this.dt_entrada = DateTime.Now;
            this.id_curso = id_curso;
            this.id_aluno = id_aluno;
        }

        public void EditInfo(bool termino)
        {
            if (termino)
            {
                this.st_status = "Concluido";
                this.dt_termino = DateTime.Now;
            }

        }
    }
}
