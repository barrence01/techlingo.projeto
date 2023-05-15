using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace techlingo.projeto.Models
{
    [Table("TL_ALUNO")]
    public class tl_alunoModel
    {

        [HiddenInput]
        [Key]
        [Column("ID_ALUNO")]
        public int id_aluno { get; set; }

        [Display(Name = "Nome do Cliente")]
        [Required(ErrorMessage = "O nome do cliente é obrigatório")]
        [MaxLength(80, ErrorMessage = "O tamanho máximo para o campo nome é de 80 caracteres.")]
        [MinLength(5, ErrorMessage = "Digite um nome com 5 ou mais caracteres")]
        [Column("NM_ALUNO")]
        public string? nm_aluno { get; set; }

        [Required(ErrorMessage = "CPF é obrigatório!")]
        [Display(Name = "CPF")]
        [MaxLength(14)]
        [Column("NR_CPF")]
        public string? nr_cpf { get; set; }

        [Required(ErrorMessage = "EMAIL é obrigatório!")]
        [MaxLength(80)]
        [Column("DS_EMAIL")]
        public string? ds_email { get; set; }

        [Required(ErrorMessage = "SENHA é obrigatório!")]
        [MaxLength(30)]
        [JsonIgnore]
        [Column("DS_SENHA")]
        public string? ds_senha { get; set; }

        [Display(Name = "Data de Nascimento")]
        [Required(ErrorMessage = "Data de nascimento é obrigatório")]
        [DataType(DataType.Date, ErrorMessage = "Data de nascimento incorreta")]
        [Column("DT_NASCIMENTO")]
        public DateTime dt_nascimento { get; set; }

        // Navegation Property
        // Um usuário pode ter vários cursos
        public ICollection<tl_aluno_cursoModel>? Cursando { get; set; }


        //Navegation Property
        // Relação de 1-1 com plano
        [Column("FK_ID_PLANO")]
        [JsonIgnore]
        public int planoId { get; set; }
        public tl_planoModel? plano { get; set; }


        public tl_alunoModel()
        {
        }

        public tl_alunoModel(int Id_aluno, string Nm_aluno, string Nr_cpf, string Ds_email)
        {
            id_aluno = Id_aluno;
            nm_aluno = Nm_aluno;
            nr_cpf = Nr_cpf;
            ds_email = Ds_email;
        }

    }
}
