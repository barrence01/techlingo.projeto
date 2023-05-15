using System.ComponentModel.DataAnnotations.Schema;

namespace techlingo.projeto.DTO
{
    // Esta classe representa a relação de um para um entre a tabela Aluno e Plano
    // utilizada pelo AddPlano em tl_alunoController
    public class AdicionarPlanoAlunoDto
    {
        public int id_aluno {  get; set; }

        public int id_plano { get; set; }

    }
}
