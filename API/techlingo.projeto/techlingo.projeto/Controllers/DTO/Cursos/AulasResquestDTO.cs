using System.ComponentModel.DataAnnotations.Schema;

namespace techlingo.projeto.Controllers.DTO.Cursos
{
    public class AulasResquestDTO
    {
        public int? nr_aula { get; set; }

        public string? nm_aula { get; set; }

        public DateTime? dt_criacao { get; set; }

        public int id_curso { get; set; }
    }
}
