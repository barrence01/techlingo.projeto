package br.com.techlingo.DTO

data class CadastrarRequestBody(
    val nm_aluno: String,
    val dt_nascimento: String,
    val nr_cpf: String,
    val ds_email: String,
    val ds_senha: String,
    val ds_plano: String
)
