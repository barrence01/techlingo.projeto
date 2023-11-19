using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using techlingo.projeto.Controllers.DTO.Plano;
using techlingo.projeto.Models.Aluno;

namespace techlingo.projeto.Models.Planos
{
    [Table("TL_PLANOS")]
    public class PlanoModel
    {

        [Key]
        [Column("id_plano")]
        public int id_plano { get; set; }

        [Column("nm_plano")]
        public string? nm_plano { get; set; }

        [Column("vl_plano")]
        public decimal? vl_plano { get; set; }



        // Relacionamento
        [JsonIgnore]
        public ICollection<AlunoModel?> aluno { get; set; }

        public PlanoModel()
        {
            
        }

        public PlanoModel(PlanoRequestDTO planoModel)
        {
            this.nm_plano = planoModel.nm_plano;
            this.vl_plano = planoModel.vl_plano;
        }
    }
}
