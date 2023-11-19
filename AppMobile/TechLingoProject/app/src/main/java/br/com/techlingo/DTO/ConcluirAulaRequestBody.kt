package br.com.techlingo.DTO

data class ConcluirAulaRequestBody (
    val email: String,
    val senha: String,
    val nm_curso: String,
    val nr_aula: Int
)