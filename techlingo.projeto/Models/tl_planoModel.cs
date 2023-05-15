using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace techlingo.projeto.Models
{
    [Table("TL_PLANO")]
    public class tl_planoModel
    {
        [Key]
        [Column("ID_PLANO")] // Plano 0 - Padrão // Plano 1 - Inicial // Plano 2 - Standard // Plano 3 - Premium
        public int id_plano { get; set; }

        [Required]
        [Column("NM_PLANO")]
        [MaxLength(80)] 
        public string? nm_plano { get; set; }

        [Required]
        [Column("VL_PLANO", TypeName = "decimal(18,2)")]
        public decimal vl_plano { get; set; }


    }

}
