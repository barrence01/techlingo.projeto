package br.com.techlingo

import ApiService
import android.content.Context
import android.content.Intent
import android.graphics.Color
import android.net.Uri
import android.os.Bundle
import android.util.Log
import android.widget.Button
import android.widget.ImageView
import android.widget.RadioButton
import android.widget.RadioGroup
import android.widget.TextView
import android.widget.Toast
import androidx.activity.ComponentActivity
import br.com.techlingo.DTO.AbrirAulaRequestBody
import br.com.techlingo.DTO.AbrirAulaResponseBody
import br.com.techlingo.DTO.ConcluirAulaRequestBody
import br.com.techlingo.DTO.ConcluirCursoRequestBody
import br.com.techlingo.DTO.ConcluirCursoResposeBody
import retrofit2.Callback
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

class CursosAulaActivity : ComponentActivity() {

    private var concluido = false

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_curso_aula);

        val sharedPreferences = getSharedPreferences(getString(R.string.preferencias), MODE_PRIVATE)
        val email = sharedPreferences.getString("email", "email") ?: "Não obtido"
        val senha = sharedPreferences.getString("senha", "senha") ?: "Não obtido"
        val nm_curso = sharedPreferences.getString("nm_curso", "nm_curso") ?: "Não obtido"
        val dt_entrada = sharedPreferences.getString("curso_dt_entrada", "dt_entrada") ?: "Não obtido"
        val st_status = sharedPreferences.getString("curso_st_status", "st_status") ?: "Não obtido"
        val curso_aulas = sharedPreferences.getString("curso_aulas", "curso_aulas") ?: "Não obtido"
        val nr_aula = sharedPreferences.getInt("nr_aula", 0) ?: 0
        val qtd_aulas = sharedPreferences.getInt("qtd_aulas", 0) ?: 0

        val ds_titulo = sharedPreferences.getString("aula_ds_titulo", "ds_titulo") ?: "Não obtido"
        val ds_link_video = sharedPreferences.getString("aula_ds_link_video", "ds_link_video") ?: "Não obtido"
        val ds_descricao_quiz = sharedPreferences.getString("aula_ds_descricao_quiz", "ds_descricao_quiz") ?: "Não obtido"
        val ds_pergunta1 = sharedPreferences.getString("aula_ds_pergunta1", "ds_pergunta1") ?: "Não obtido"
        val ds_pergunta2 = sharedPreferences.getString("aula_ds_pergunta2", "ds_pergunta2") ?: "Não obtido"
        val ds_pergunta3 = sharedPreferences.getString("aula_ds_pergunta3", "ds_pergunta3") ?: "Não obtido"
        val ds_resposta = sharedPreferences.getString("aula_ds_resposta", "ds_resposta") ?: "Não obtido"


        val txt_nm_curso = findViewById(R.id.txt_nome_curso) as TextView
        val txt_aula_titulo = findViewById(R.id.txt_aula_titulo) as TextView
        val txt_descricao_quiz = findViewById(R.id.txt_quiz_descricao) as TextView
        val radioGroup = findViewById(R.id.radioGroup) as RadioGroup
        val radio_pergunta1 = findViewById(R.id.btn_quiz_1) as RadioButton
        val radio_pergunta2 = findViewById(R.id.btn_quiz_2) as RadioButton
        val radio_pergunta3 = findViewById(R.id.btn_quiz_3) as RadioButton

        val btn_prox_aula = findViewById(R.id.btn_prox_aula) as Button


        val icone_aula = findViewById(R.id.iconeAula) as ImageView




        when (nm_curso) {
            "Back-End" -> {
                icone_aula.setImageResource(R.drawable.backend);
            }
            "Front-End" -> {
                icone_aula.setImageResource(R.drawable.front_end_img);
            }
            "Database" -> {
                icone_aula.setImageResource(R.drawable.database_img);
            }
            "Lógica de Programação" -> {
                icone_aula.setImageResource(R.drawable.logica);
            }
            "Cloud Computing" -> {
                icone_aula.setImageResource(R.drawable.cloud);
            }
            else -> {
                icone_aula.setImageResource(R.drawable.backend);
            }
        }


        //QUIZ
        txt_nm_curso.text = nm_curso
        txt_aula_titulo.text = ds_titulo
        txt_descricao_quiz.text = ds_descricao_quiz
        radio_pergunta1.text = ds_pergunta1
        radio_pergunta2.text = ds_pergunta2
        radio_pergunta3.text = ds_pergunta3
        radioGroup.setOnCheckedChangeListener{ group, checkedId ->

            val radio: RadioButton = findViewById(checkedId)
            if (radio.text == ds_resposta) {
                radio.setTextColor(Color.GREEN)
                concluirAula(email, senha, nm_curso, nr_aula)
                concluido = true;
            }
            else {
                radio.setTextColor(Color.RED)
            }
        }

        val imagem = findViewById(R.id.aula_imagem) as ImageView
        imagem.setOnClickListener{
            try {
                val intent = Intent(Intent.ACTION_VIEW, Uri.parse(ds_link_video))
                startActivity(intent)
            }
            catch (e: Exception) {
                e.printStackTrace()
            }
        }


        btn_prox_aula.setOnClickListener(){
            if (concluido) {
                if (nr_aula < qtd_aulas) {
                    avancarTela(email, senha, nm_curso, nr_aula + 1)
                }
                else {
                    concluirCurso(email, senha, nm_curso)
                }
            }
            else {
                Toast.makeText(
                    this@CursosAulaActivity,
                    "Você precisa concluir a aula para avançar",
                    Toast.LENGTH_SHORT
                ).show()
            }
        }
    }

    private fun concluirAula(email:String, senha:String, nm_curso:String, nr_aula:Int){
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

        val concluirAulaRequestBody = ConcluirAulaRequestBody(email, senha, nm_curso, nr_aula)

        val call = apiService.concluirAula(concluirAulaRequestBody)
        call.enqueue(object : Callback<Unit> {
            override fun onResponse(
                call: retrofit2.Call<Unit>, response: retrofit2.Response<Unit>) {
                if (response.isSuccessful) {

                } else {

                }
            }

            override fun onFailure(call: retrofit2.Call<Unit>, t: Throwable) {
                // Exibe uma mensagem de erro para o usuário
                val errorMessage = "Falha no request. Verifique a conexão com a internet."
                Toast.makeText(this@CursosAulaActivity, errorMessage, Toast.LENGTH_SHORT).show()

                // Log do erro no console(logcat)s
                Log.e("API Request Error", errorMessage, t)
            }
        })

    }

    private fun concluirCurso(email:String, senha:String, nm_curso:String){
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

        val concluirCursoRequestBody = ConcluirCursoRequestBody(email, senha, nm_curso)

        val call = apiService.concluirCurso(concluirCursoRequestBody)
        call.enqueue(object : Callback<ConcluirCursoResposeBody> {
            override fun onResponse(
                call: retrofit2.Call<ConcluirCursoResposeBody>, response: retrofit2.Response<ConcluirCursoResposeBody>) {
                if (response.isSuccessful) {
                    val responseBody = response.body()


                    Toast.makeText(
                        this@CursosAulaActivity,
                        responseBody?.resultado,
                        Toast.LENGTH_SHORT
                    ).show()

                    abrirTelaCurso()
                } else {

                    Toast.makeText(
                        this@CursosAulaActivity,
                        "Falha ao ir a próxima aula",
                        Toast.LENGTH_SHORT
                    ).show()
                }
            }

            override fun onFailure(call: retrofit2.Call<ConcluirCursoResposeBody>, t: Throwable) {
                // Exibe uma mensagem de erro para o usuário
                val errorMessage = "Falha no request. Verifique a conexão com a internet."
                Toast.makeText(this@CursosAulaActivity, errorMessage, Toast.LENGTH_SHORT).show()

                // Log do erro no console(logcat)s
                Log.e("API Request Error", errorMessage, t)
            }
        })

    }

    private fun avancarTela(email:String, senha:String, nm_curso:String, nr_aula:Int) {
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

        val abrirAulaRequestBody = AbrirAulaRequestBody(email, senha, nm_curso, nr_aula)

        val call = apiService.abrirAula(abrirAulaRequestBody)

        call.enqueue(object : Callback<AbrirAulaResponseBody> {
            override fun onResponse(
                call: retrofit2.Call<AbrirAulaResponseBody>, response: retrofit2.Response<AbrirAulaResponseBody>) {
                if (response.isSuccessful) {
                    val responseBody = response.body()
                    Log.i("Resposta",response.body().toString())

                    if (responseBody != null) {
//                        Toast.makeText(
//                            this@CursosAulaActivity,
//                            "Aula aberta com sucesso",
//                            Toast.LENGTH_SHORT
//                        ).show()
                    }

                    abrirAula()

                    salvarPref(responseBody!!,nr_aula)
                } else {
                    Toast.makeText(
                        this@CursosAulaActivity,
                        "Erro ao abrir aula",
                        Toast.LENGTH_SHORT
                    ).show()
                }
            }

            override fun onFailure(call: retrofit2.Call<AbrirAulaResponseBody>, t: Throwable) {
                // Exibe uma mensagem de erro para o usuário
                val errorMessage = "Falha no request. Verifique a conexão com a internet."
                Toast.makeText(this@CursosAulaActivity, errorMessage, Toast.LENGTH_SHORT).show()

                // Log do erro no console(logcat)
                Log.e("API Request Error", errorMessage, t)
            }
        })


    }

    private fun abrirAula() {
        val intent = Intent(this, this::class.java)
        startActivity(intent)
    }

    private fun abrirTelaCurso() {
        val intent = Intent(this, ListaCursosActivity::class.java)
        intent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP)
        startActivity(intent)
    }


    private fun salvarPref(aula:AbrirAulaResponseBody, nr_aula:Int){
        val sharedPreferences = getSharedPreferences(getString(R.string.preferencias), Context.MODE_PRIVATE)
        val editor = sharedPreferences.edit()

        editor.putString("aula_ds_titulo", aula.ds_titulo)
        editor.putString("aula_ds_link_video", aula.ds_link_video)
        editor.putString("aula_ds_descricao", aula.ds_descricao)
        editor.putString("aula_ds_descricao_quiz", aula.ds_descricao_quiz)
        editor.putString("aula_ds_pergunta1", aula.ds_pergunta1)
        editor.putString("aula_ds_pergunta2", aula.ds_pergunta2)
        editor.putString("aula_ds_pergunta3", aula.ds_pergunta3)
        editor.putString("aula_ds_resposta", aula.ds_resposta)
        editor.putInt("nr_aula", nr_aula?:0)
        editor.apply()

    }

}


