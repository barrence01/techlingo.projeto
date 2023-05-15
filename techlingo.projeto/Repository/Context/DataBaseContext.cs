using techlingo.projeto.Models;
using Microsoft.EntityFrameworkCore;
using techlingo.projeto.Models;
using System.Reflection.Metadata;

namespace techlingo.projeto.Repository.Context
{
    public class DataBaseContext : DbContext
    {
        // Propriedade que será responsável pelo acesso a tabela de Aluno
        public DbSet<tl_alunoModel> tl_aluno { get; set; }

        // Propriedade que será responsável pelo acesso a tabela de Plano
        public DbSet<tl_planoModel> tl_plano { get; set; }

        // Propriedade que será responsável pelo acesso a tabela de Curso
        public DbSet<tl_cursoModel> tl_curso { get; set; }

        // Propriedade que será responsável pelo acesso a tabela de aluno_curso(Relação N-N entre Aluno e Curso)
        public DbSet<tl_aluno_cursoModel> tl_aluno_cursoModel { get; set; }


        // Este campo sobrescreve o ModelBuilder para a construção das tabelas e relacionamentos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relação de muito - para - muitos entre Alunos e Cursos
            modelBuilder.Entity<tl_aluno_cursoModel>()
                .HasKey(ac => new { ac.alunoId, ac.cursoId });

            modelBuilder.Entity<tl_aluno_cursoModel>()
                .HasOne(ac => ac.tl_alunoModel)
                .WithMany(a => a.Cursando)
                .HasForeignKey(ac => ac.alunoId);

            modelBuilder.Entity<tl_aluno_cursoModel>()
                .HasOne(ac => ac.curso)
                .WithMany(c => c.Cursando)
                .HasForeignKey(ac => ac.cursoId);
                

        }


        public DataBaseContext(DbContextOptions options) : base(options)
        {
        }

        protected DataBaseContext()
        {
        }

    }
}