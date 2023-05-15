using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace techlingo.projeto.Models
{
    [Keyless]
    [Table("TL_ALUNO_CURSO")]
    public class tl_aluno_cursoModel
    {
        [JsonIgnore]
        public tl_alunoModel tl_alunoModel { get; set; }
        [Column("ID_ALUNO")]
        [JsonIgnore]
        public int alunoId { get; set; }

        public tl_cursoModel curso { get; set; }
        [JsonIgnore]
        [Column("ID_CURSO")]
        public int cursoId { get; set; }

        [Column("NR_NOTA")]
        public int nr_nota { get; set; } = 0;

        [Column("ST_STATUS")]
        [MaxLength(20)]
        public string? st_status { get; set; }

        [Column("DT_CONCLUSAO")]
        public DateTime? dt_conclusao { get; set; }

    }
}
