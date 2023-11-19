package br.com.techlingo.DTO

data class AbrirAulaRequestBody (
    val email: String,
    val senha: String,
    val nm_curso: String,
    val nr_aula: Int
)