using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using techlingo.projeto.Controllers.DTO.Cursos;

namespace techlingo.projeto.Models.Cursos
{
    [Table("TL_AULA_QUIZ")]
    public class AulaQuizModel
    {

        [Key]
        [Column("id_quiz")]
        public int id_quiz { get; set; }

        [Column("ds_titulo")]
        public string? ds_titulo { get; set; }

        [Column("ds_descricao")]
        public string? ds_descricao { get; set; }

        [Column("ds_pergunta")]
        public string? ds_pergunta { get; set; }

        [Column("ds_pergunta2")]
        public string? ds_pergunta2 { get; set; }

        [Column("ds_pergunta3")]
        public string? ds_pergunta3 { get; set; }

        [Column("ds_resposta")]
        public string? ds_resposta { get; set; }


        // Relacionamentos

        [Column("id_aula_conteudo")]
        public int id_aula_conteudo { get; set; }
        [JsonIgnore]
        public AulaConteudoModel? conteudo { get; set; }


        public AulaQuizModel()
        {

        }

        public AulaQuizModel(ConteudoQuizRequestDTO conteudoQuizRequest)
        {
            this.ds_titulo = conteudoQuizRequest.ds_titulo;
            this.ds_descricao = conteudoQuizRequest.ds_descricao;
            this.ds_pergunta = conteudoQuizRequest.ds_pergunta;
            this.ds_pergunta2 = conteudoQuizRequest.ds_pergunta2;
            this.ds_pergunta3 = conteudoQuizRequest.ds_pergunta3;
            this.id_aula_conteudo = conteudoQuizRequest.id_aula_conteudo;
        }

        public void EditInfo(AulaQuizModel conteudoQuizModel)
        {
            this.ds_titulo = conteudoQuizModel.ds_titulo;
            this.ds_descricao = conteudoQuizModel.ds_descricao;
            this.ds_pergunta = conteudoQuizModel.ds_pergunta;
            this.ds_pergunta2 = conteudoQuizModel.ds_pergunta2;
            this.ds_pergunta3 = conteudoQuizModel.ds_pergunta3;
        }
    }
}
