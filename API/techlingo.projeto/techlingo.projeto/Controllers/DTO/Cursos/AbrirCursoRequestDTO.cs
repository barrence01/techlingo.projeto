using techlingo.projeto.Controllers.DTO.Aluno;
using techlingo.projeto.Models.Cursos;

namespace techlingo.projeto.Controllers.DTO.Cursos
{
    public class AbrirCursoRequestDTO
    {
        public string email { get; set; }
        public string senha { get; set; }

        // "Back-End" "Front-End" "Database" "Lógica de Programação" "Cloud Computing";
        public string nm_curso { get; set; }
    }
}
