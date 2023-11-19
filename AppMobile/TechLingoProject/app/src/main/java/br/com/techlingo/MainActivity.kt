package br.com.techlingo

import ApiService
import android.content.Context
import android.content.Intent
import android.os.Bundle
import android.util.Log
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import androidx.activity.ComponentActivity
import br.com.techlingo.DTO.LoginRequestBody
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory


class MainActivity : ComponentActivity() {


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_login)


        // Define a URL base do seu servidor
        // 10.0.2.2 é o localhost dentro do emulador android
        // Também deve ser adicionado AndroidManifest.xml -> <uses-permission android:name="android.permission.INTERNET" />
        // Também é necessário criar a classe ApiService.kt
        // para que o Retrofit possa fazer a solicitação HTTP
        // pelos metodos criados na interface

        // obtem a 'api_baseurl' do arquivo 'strings.xml'
        val baseUrl = getString(R.string.api_baseurl)

        // Cria uma instância Retrofit
        val retrofit = Retrofit.Builder()
            .baseUrl(baseUrl)
            .addConverterFactory(GsonConverterFactory.create())
            .build()

        val apiService = retrofit.create(ApiService::class.java)


        val btn_entrar = findViewById(R.id.btnLogin) as Button
        val txt_email = findViewById(R.id.txtEmail) as EditText
        val txt_senha = findViewById(R.id.txtSenha) as EditText

        val btnNoAccount = findViewById(R.id.btnNoAccount) as Button

        btn_entrar.setOnClickListener{
            val email = txt_email.text.toString()
            val senha = txt_senha.text.toString()
            //val email = "email@email.com"
            //val senha = "123"

            // Cria um objeto LoginRequestBody com os dados
            val loginRequestBody = LoginRequestBody(email, senha)


            // Faz a solicitação de login
            val call = apiService.loginAluno(loginRequestBody)


            call.enqueue(object : Callback<Unit> {
                override fun onResponse(call: Call<Unit>, response: Response<Unit>) {
                    if (response.isSuccessful) {
                        // Manipula a resposta bem-sucedida aqui
                        //val apiResponse = response.body()
                        exibirMensagemDeSucesso()

                        // Salve o email e a senha do usuário nas preferências compartilhadas
                        val sharedPreferences = getSharedPreferences(getString(R.string.preferencias), Context.MODE_PRIVATE)
                        val editor = sharedPreferences.edit()
                        editor.putString("email", email)
                        editor.putString("senha", senha)
                        editor.apply()

                        // Inicia a ListaCursosActivity após o login
                        avancarTela()

                    } else {
                        // Lidar com erros de resposta aqui
                        exibirMensagemDeErro()
                        val errorCode = response.code()
                        Log.i("Codigo de retorno",errorCode.toString())
                    }
                }

                override fun onFailure(call: Call<Unit>, t: Throwable) {
                    // Exibe uma mensagem de erro para o usuário
                    val errorMessage = "Falha no request. Verifique a conexão com a internet."
                    Toast.makeText(this@MainActivity, errorMessage, Toast.LENGTH_SHORT).show()

                    // Log do erro no console(logcat)
                    Log.e("API Request Error", errorMessage, t)
                }

            })
        }

        btnNoAccount.setOnClickListener{
            val intent = Intent(this, CadastroActivity::class.java)
            startActivity(intent)
        }
    }



    private fun exibirMensagemDeSucesso() {
        Toast.makeText(this, "Login bem-sucedido!", Toast.LENGTH_SHORT).show()
    }

    private fun exibirMensagemDeErro() {
        Toast.makeText(this, "Email ou senha incorreto.", Toast.LENGTH_SHORT).show()
    }


    private fun avancarTela() {
        val intent = Intent(this, ListaCursosActivity::class.java)
        startActivity(intent)
    }
}