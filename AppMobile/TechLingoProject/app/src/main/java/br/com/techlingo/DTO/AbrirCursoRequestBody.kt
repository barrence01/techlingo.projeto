package br.com.techlingo.DTO

data class AbrirCursoRequestBody (
    val email: String,
    val senha: String,
    val nm_curso: String
)