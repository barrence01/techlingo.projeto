using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;

namespace techlingo.projeto.Models
{
    [Table("TL_CURSO")]
    public class tl_cursoModel
    {

        [Key]
        [Column("ID_CURSO")]
        public int id_curso { get; set; }

        [Required]
        [Column("NM_CURSO")]
        [MaxLength(80)]
        public string? nm_curso { get; set; }

        [Required]
        [Column("DS_DURACAO")]
        public int ds_duracao { get; set; }


        // Navegation Property
        // Um curso pode ter vários usuários
        [JsonIgnore] // Faz com que a declaração em Json do Alunos não seja obrigatória, ou seja, pode existir sem estar relacionado a algum Aluno
        public ICollection<tl_aluno_cursoModel>? Cursando { get; set; }
    }
}
