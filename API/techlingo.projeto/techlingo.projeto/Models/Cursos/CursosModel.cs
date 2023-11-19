using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using techlingo.projeto.Controllers.DTO.Cursos;
using techlingo.projeto.Models.Aluno;

namespace techlingo.projeto.Models.Cursos
{
    [Table("TL_CURSOS")]
    public class CursosModel
    {
        [Key]
        [Column("id_curso")]
        public int id_curso { get; set; }

        [Column("nm_curso")]
        public string? nm_curso { get; set; }

        [Column("ds_curso")]
        public string? ds_curso { get; set; }

        [Column("dt_criacao")]
        public DateTime? dt_criacao { get; set; }




        // Relacionamento
        [JsonIgnore]
        public ICollection<AlunoCursosCursadosModel>? cursantes { get; set; }

        // Relacionamento
        public ICollection<AulasModel>? aulas { get; set; }


        public CursosModel()
        {
            
        }

        public CursosModel(CursosRequestDTO cursoModel)
        {
            this.nm_curso = cursoModel.nm_curso;
            this.ds_curso = cursoModel.ds_curso;
            this.dt_criacao = DateTime.Now;
        }

        public void EditInfo (CursosModel cursosModel)
        {
            this.nm_curso = cursosModel.nm_curso;
            this.ds_curso = cursosModel.ds_curso;

        }
    }
}
