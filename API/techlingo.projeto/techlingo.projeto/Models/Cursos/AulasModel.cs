using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using techlingo.projeto.Controllers.DTO.Cursos;
using techlingo.projeto.Models.Aluno;

namespace techlingo.projeto.Models.Cursos
{
    [Table("TL_AULAS")]
    public class AulasModel
    {
        [Key]
        [Column("id_aula")]
        public int id_aula { get; set; }

        [Column("nr_aula")]
        public int? nr_aula { get; set; }

        [Column("nm_aula")]
        public string? nm_aula { get; set; }

        [Column("dt_criacao")]
        public DateTime? dt_criacao { get; set; }


        // Relacionamento

        [Column("id_curso")]
        public int id_curso { get; set; }
        [JsonIgnore]
        public CursosModel? curso { get; set; }

        [JsonIgnore]
        public ICollection<AlunoAulasCursadasModel>? alunos { get; set; }

        public AulaConteudoModel? conteudo { get; set; }

        public AulasModel()
        {
            
        }

        public AulasModel(AulasResquestDTO aulasModel)
        {
            this.nr_aula = aulasModel.nr_aula;
            this.nm_aula = aulasModel.nm_aula;
            this.dt_criacao = DateTime.Now;
            this.id_curso = aulasModel.id_curso;
        }

        public void EditInfo (AulasModel aulasModel)
        {
            this.nr_aula = aulasModel.nr_aula;
            this.nm_aula = aulasModel.nm_aula;
        }
    }
}
