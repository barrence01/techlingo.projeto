namespace techlingo.projeto.Controllers.DTO.Cursos
{
    public class AulaConcluirAulaRequestDTO
    {
        public string email { get; set; }
        public string senha { get; set; }

        public string nm_curso { get; set; }

        public int nr_aula { get; set; }
    }
}
