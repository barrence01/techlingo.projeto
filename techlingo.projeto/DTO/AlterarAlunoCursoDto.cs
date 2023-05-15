using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using techlingo.projeto.Models;

namespace techlingo.projeto.DTO
{
    // Esta classe é uma mascara para somente os dados do aluno
    public class AlterarAlunoCursoDto
    {

        public int id_aluno { get; set; }

        public int id_curso { get; set; }

        public int nr_nota { get; set; } = 0;

        public string? st_status { get; set; }

        public DateTime? dt_conclusao { get; set; }

    }
}
