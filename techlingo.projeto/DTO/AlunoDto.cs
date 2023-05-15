using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using techlingo.projeto.Models;

namespace techlingo.projeto.DTO
{
    // Esta classe é uma mascara para somente os dados do aluno
    public class AlunoDto
    {
        public int id_aluno { get; set; }

        public string? nm_aluno { get; set; }

        public string? nr_cpf { get; set; }

        public string? ds_email { get; set; }

        public string? ds_senha { get; set; }

        public DateTime dt_nascimento { get; set; }

    }
}
