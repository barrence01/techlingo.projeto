using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using techlingo.projeto.Models.Aluno;
using techlingo.projeto.Models.Cursos;
using techlingo.projeto.Models.Planos;

namespace techlingo.projeto.Repository.Context
{
    public class DataBaseContext : DbContext
    {

        public DbSet<AlunoModel> Alunos { get; set; }
        public DbSet<PlanoModel> Planos { get; set; }
        public DbSet<AlunoCursosCursadosModel> AlunoCursosCursados { get; set; }
        public DbSet<AlunoAulasCursadasModel> AlunoAulasCursadas { get; set; }
        public DbSet<CursosModel> Cursos { get; set; }
        public DbSet<AulasModel> Aulas { get; set; }
        public DbSet<AulaConteudoModel> AulaConteudo { get; set; }
        public DbSet<AulaQuizModel> AulaQuiz { get; set; }



        protected override void ConfigureConventions(ModelConfigurationBuilder configuration)
        {
           
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Plano
            modelBuilder.Entity<AlunoModel>()
                .HasOne(e => e.plano)
                .WithMany(p => p.aluno)
                .HasForeignKey(m => m.id_plano);


            // Cursantes
            modelBuilder.Entity<AlunoCursosCursadosModel>()
                .HasOne(e => e.aluno)
                .WithMany(c => c.cursosCursados)
                .HasForeignKey(m => m.id_aluno);

            modelBuilder.Entity<AlunoCursosCursadosModel>()
                .HasOne(e => e.curso)
                .WithMany(c => c.cursantes)
                .HasForeignKey(m => m.id_curso);


            // Aulas
            modelBuilder.Entity<AulasModel>()
                .HasOne(e => e.curso)
                .WithMany(c => c.aulas)
                .HasForeignKey(m => m.id_curso);


            // Aulas Cursadas
            modelBuilder.Entity<AlunoAulasCursadasModel>()
                .HasOne(e => e.aula)
                .WithMany(m => m.alunos)
                .HasForeignKey(c => c.id_aula);

            modelBuilder.Entity<AlunoAulasCursadasModel>()
                .HasOne(e => e.cursante)
                .WithMany(m => m.aulas_cursadas)
                .HasForeignKey(c => c.id_aluno_cursante);


            // Aula conteudo
            modelBuilder.Entity<AulaConteudoModel>()
                .HasOne(e => e.aula)
                .WithOne(m => m.conteudo)
                .HasForeignKey<AulaConteudoModel>(c => c.id_aula);


            // Aula quiz
            modelBuilder.Entity<AulaQuizModel>()
                .HasOne(e => e.conteudo)
                .WithOne(m => m.quiz)
                .HasForeignKey<AulaQuizModel>(c => c.id_aula_conteudo);




        }
        public DataBaseContext(DbContextOptions options) : base(options)
        {

        }

        protected DataBaseContext()
        {
        }
    }
}
