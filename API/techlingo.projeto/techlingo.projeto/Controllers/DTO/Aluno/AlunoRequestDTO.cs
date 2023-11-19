using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace techlingo.projeto.Controllers.DTO.Aluno;

public class AlunoRequestDTO
{

    public string? nm_aluno { get; set; }

    public string? dt_nascimento { get; set; }
    public string? nr_cpf { get; set; }

    public string? ds_email { get; set; }

    public string? ds_senha { get; set; }

    public string? plano { get; set; }
}
  
