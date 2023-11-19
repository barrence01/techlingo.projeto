package br.com.techlingo

import ApiService
import android.content.Context
import android.os.Bundle
import android.provider.Settings.Global.getString
import android.net.Uri
import android.provider.Settings
import android.text.Editable
import android.text.TextWatcher
import android.util.Log
import android.widget.ArrayAdapter
import android.widget.Button
import android.widget.EditText
import android.widget.Spinner
import android.widget.Toast
import androidx.activity.ComponentActivity
import br.com.techlingo.DTO.AbrirAulaRequestBody
import br.com.techlingo.DTO.AbrirAulaResponseBody
import br.com.techlingo.DTO.CadastrarRequestBody
import br.com.techlingo.DTO.ListarPlanosResponseBody
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory


class CadastroActivity : ComponentActivity() {

    private lateinit var spinner: Spinner


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_cadastro)

        val baseUrl = getString(R.string.api_baseurl)
        val dataRepository = DataRepository(baseUrl)



        dataRepository.fetchData { items ->
            if (items != null) {
                spinner = findViewById(R.id.lista_planos)
                val adapter = ArrayAdapter(this, android.R.layout.simple_spinner_item, items.planos)
                adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item)
                spinner.adapter = adapter
            } else {
                // Handle error or failure
            }
        }

        val btn_cadastrar = findViewById(R.id.btn_cadastrar) as Button
        val spinner_planos = findViewById(R.id.lista_planos) as Spinner

        val dt_nascimento = findViewById(R.id.txtDataAniversarioCadastro) as EditText

        dt_nascimento.addTextChangedListener(object: TextWatcher {
            override fun beforeTextChanged(p0: CharSequence?, p1: Int, p2: Int, p3: Int) {
                //
            }

            override fun onTextChanged(p0: CharSequence?, p1: Int, p2: Int, p3: Int) {
                //
            }

            override fun afterTextChanged(s: Editable?) {
                val text = s.toString()
                Log.i("text", text)
                if (text.length == 2 && text[1] != '/') {
                    dt_nascimento.setText(StringBuilder(text).insert(2, "/"))
                    dt_nascimento.setSelection(3)
                } else if (text.length == 5 && text[4] != '/') {
                    dt_nascimento.setText(StringBuilder(text).insert(5, "/"))
                    dt_nascimento.setSelection(6)
                }
            }
        })


        btn_cadastrar.setOnClickListener {
            val baseUrl = getString(R.string.api_baseurl)

            val nm_aluno = findViewById(R.id.txtNomeCompleto) as EditText
            val dt_nascimento = findViewById(R.id.txtDataAniversarioCadastro) as EditText
            val nr_cpf = findViewById(R.id.txtNrCPF) as EditText
            val ds_email = findViewById(R.id.txtEmailCadastro) as EditText
            val ds_senha = findViewById(R.id.txtSenhaCadastro) as EditText

            val resultado = validarCampos(dt_nascimento.text.toString(), nr_cpf.text.toString(), ds_email.text.toString(), ds_senha.text.toString(), nm_aluno.text.toString())

            if(resultado)
                cadastrarUsuario(spinner_planos, baseUrl, nm_aluno.text.toString(), dt_nascimento.text.toString(), nr_cpf.text.toString(), ds_email.text.toString(), ds_senha.text.toString(), verificarPlanoSelecionado(spinner_planos))
        }




}

    fun cadastrarUsuario(spinner: Spinner, baseUrl: String, nm_aluno: String, dt_nascimento: String, nr_cpf: String, ds_email: String, ds_senha: String, ds_plano: String) {

        // Defina a URL base do seu servidor
        // 10.0.2.2 é o localhost dentro do emulador android
        // Também deve ser adicionado AndroidManifest.xml -> <uses-permission android:name="android.permission.INTERNET" />
        // Também é necessário criar a classe ApiService.kt
        // para que o Retrofit possa fazer a solicitação HTTP
        // pelos metodos criados na interface


        // Crie uma instância Retrofit
        val retrofit = Retrofit.Builder()
            .baseUrl(baseUrl)
            .addConverterFactory(GsonConverterFactory.create())
            .build()

        val apiService = retrofit.create(ApiService::class.java)

        val cadastrarRequestBody = CadastrarRequestBody(nm_aluno, dt_nascimento, nr_cpf, ds_email, ds_senha, ds_plano)

        // Faça a solicitação de login
        val call = apiService.cadastrarAluno(cadastrarRequestBody)

        call.enqueue(object : Callback<Unit> {
            override fun onResponse(
                call: retrofit2.Call<Unit>, response: retrofit2.Response<Unit>) {
                if (response.isSuccessful) {
                    Toast.makeText(
                        this@CadastroActivity,
                        "Cadastro realizado com sucesso",
                        Toast.LENGTH_SHORT
                    ).show()
                    finish()

                } else {
                    Toast.makeText(
                        this@CadastroActivity,
                        "Dados incorretos. Email já existe.",
                        Toast.LENGTH_SHORT
                    ).show()
                }
            }

            override fun onFailure(call: retrofit2.Call<Unit>, t: Throwable) {
                // Exibe uma mensagem de erro para o usuário
                val errorMessage = "Falha no request. Verifique a conexão com a internet."
                Toast.makeText(this@CadastroActivity, errorMessage, Toast.LENGTH_SHORT).show()

                // Log do erro no console(logcat)
                Log.e("API Request Error", errorMessage, t)
            }
        })

    }

    fun validarCampos(dt_nascimento: String, nr_cpf: String, ds_email: String, ds_senha: String, nome:String): Boolean {
        if (dt_nascimento.isEmpty() || nr_cpf.isEmpty() || ds_email.isEmpty() || ds_senha.isEmpty() || nome.isEmpty()) {
            Toast.makeText(
                this@CadastroActivity,
                "Preencha todos os campos",
                Toast.LENGTH_SHORT
            ).show()
            return false
        }

        if(dt_nascimento.length != 10) {
            Toast.makeText(
                this@CadastroActivity,
                "Data de nascimento inválida. Deve estar no formato dd/mm/aaaa",
                Toast.LENGTH_SHORT
            ).show()
            return false
        }

        val regex = """^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/\d{4}$""".toRegex()
        val result = regex.matches(dt_nascimento)
        if(!result){
            Toast.makeText(
                this@CadastroActivity,
                "Data de nascimento inválida. Deve estar no formato dd/mm/aaaa",
                Toast.LENGTH_SHORT
            ).show()
            return false
        }


        if(nr_cpf.length != 11) {
            Toast.makeText(
                this@CadastroActivity,
                "CPF inválido. Deve conter 11 dígitos",
                Toast.LENGTH_SHORT
            ).show()
            return false
        }


        return true


    }



}

private fun verificarPlanoSelecionado(spinner: Spinner): String {
        val planoSelecionado = spinner.selectedItem.toString()
        Log.i("planoSelecionado", planoSelecionado)
        val valor = planoSelecionado.split(" - ")
        return valor[0];
    }




