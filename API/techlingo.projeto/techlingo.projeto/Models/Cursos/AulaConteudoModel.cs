using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using techlingo.projeto.Controllers.DTO.Cursos;

namespace techlingo.projeto.Models.Cursos
{
    [Table("TL_AULA_CONTEUDO")]
    public class AulaConteudoModel
    {

        [Key]
        [Column("id_aula_conteudo")]
        public int id_aula_conteudo { get; set; }

        [Column("ds_titulo")]
        public string? ds_titulo { get; set; }

        [Column("ds_link_video")]
        public string? ds_link_video { get; set; }

        [Column("ds_descricao")]
        public string? ds_descricao { get; set; }


        // Relacionamento

        [Column("id_aula")]
        public int id_aula { get; set; }

        [JsonIgnore]
        public AulasModel? aula { get; set; }


        public AulaQuizModel? quiz { get; set; }

        public AulaConteudoModel()
        {

        }

        public AulaConteudoModel(ConteudoAulaRequestDTO conteudoAulaRequest)
        {
            this.ds_titulo = conteudoAulaRequest.ds_titulo;
            this.ds_link_video = conteudoAulaRequest.ds_link_video;
            this.ds_descricao = conteudoAulaRequest.ds_descricao;
            this.id_aula = conteudoAulaRequest.id_aula;
        }

        public void EditInfo(AulaConteudoModel conteudoAulaModel)
        {
            this.ds_titulo = conteudoAulaModel.ds_titulo;
            this.ds_link_video = conteudoAulaModel.ds_link_video;
            this.ds_descricao = conteudoAulaModel.ds_descricao;
        }
    }
}
