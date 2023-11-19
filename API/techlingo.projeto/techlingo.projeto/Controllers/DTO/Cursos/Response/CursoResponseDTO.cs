namespace techlingo.projeto.Controllers.DTO.Cursos.Response
{
    public class CursoResponseDTO
    {
        public String? dt_entrada { get; set; }

        public string st_status { get; set; }

        public Dictionary<string,string> aulas { get; set; }

        public int qtd_aulas { get; set; }
    }
}
