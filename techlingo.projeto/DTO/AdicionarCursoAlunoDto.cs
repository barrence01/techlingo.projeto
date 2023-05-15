using System.ComponentModel.DataAnnotations.Schema;

namespace techlingo.projeto.DTO
{
    // Esta classe representa a relação de muitos para muitos entre a tabela Aluno e Curso
    // utilizada pelo AddCurso em tl_alunoController
    public class AdicionarCursoAlunoDto
    {
        public int id_aluno {  get; set; }

        public int id_curso { get; set; }

    }
}
