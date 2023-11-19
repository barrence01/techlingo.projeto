package br.com.techlingo.DTO

data class ConcluirCursoRequestBody (
    val email: String,
    val senha: String,
    val nm_curso: String
)