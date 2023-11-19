using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text.Json.Serialization;
using techlingo.projeto.Controllers.DTO.Aluno;
using techlingo.projeto.Models.Planos;

namespace techlingo.projeto.Models.Aluno
{
    [Table("TL_ALUNO")]
    public class AlunoModel
    {

        [Key]
        [Column("id_aluno")]
        public int id_aluno { get; set; }

        [Column("nm_aluno")]
        public string? nm_aluno { get; set; }

        [Column("dt_nascimento")]
        public DateTime? dt_nascimento { get; set; }

        [Column("nr_cpf")]
        public string? nr_cpf { get; set; }

        [Column("dt_criacao")]
        public DateTime? dt_criacao { get; set; }

        [Column("dt_alteracao")]
        public DateTime? dt_alteracao { get; set; }

        [Column("ds_email")]
        public string? ds_email { get; set; }

        [Column("ds_senha")]
        public string? ds_senha { get; set; }


        // Relacionamentos
        [Column("id_plano")]
        public int id_plano { get; set; }

        [JsonIgnore]
        public PlanoModel? plano { get; set; }

        public ICollection<AlunoCursosCursadosModel>? cursosCursados { get; set; }



        public AlunoModel()
        {
           
        }

        public AlunoModel(AlunoRequestDTO alunoModel, int id_plano)
        {
            this.nm_aluno = alunoModel.nm_aluno;
            DateTime dt = DateTime.ParseExact(alunoModel.dt_nascimento, "d/MM/yyyy", CultureInfo.InvariantCulture);
            this.dt_nascimento = dt;
            this.nr_cpf = alunoModel.nr_cpf;
            this.ds_email = alunoModel.ds_email;
            this.ds_senha = alunoModel.ds_senha;
            this.dt_criacao = DateTime.Now;
            this.dt_alteracao = DateTime.Now;

            this.id_plano = id_plano;
        }

        public void EditInfo (AlunoModel alunoModel)
        {
            this.nm_aluno = alunoModel.nm_aluno;
            this.dt_nascimento = alunoModel.dt_nascimento;
            this.nr_cpf = alunoModel.nr_cpf;
            this.ds_email = alunoModel.ds_email;
            this.ds_senha = alunoModel.ds_senha;
            this.dt_alteracao = DateTime.Now;


            this.id_plano = alunoModel.id_plano;
        }

        public void criarExemplo(string nome, string email, string senha, int plano)
        {
            this.nm_aluno = nome;
            this.dt_nascimento = DateTime.Now;
            this.nr_cpf = "11144478965";
            this.ds_email = email;
            this.ds_senha = senha;
            this.dt_criacao = DateTime.Now;
            this.dt_alteracao = DateTime.Now;

            this.id_plano = plano;
        }


    }
}
