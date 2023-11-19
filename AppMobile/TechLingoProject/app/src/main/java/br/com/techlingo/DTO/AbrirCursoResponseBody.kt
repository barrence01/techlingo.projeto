package br.com.techlingo.DTO


data class AbrirCursoResponseBody (
    val dt_entrada: String,
    val st_status: String,
    val aulas: Map<String, String>,
    val qtd_aulas: Int
)