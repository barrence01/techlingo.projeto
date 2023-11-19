package br.com.techlingo

import ApiService
import android.content.Context
import android.content.Intent
import android.os.Bundle
import android.util.Log
import android.widget.Button
import android.widget.Toast
import androidx.activity.ComponentActivity
import br.com.techlingo.DTO.AbrirCursoRequestBody
import br.com.techlingo.DTO.AbrirCursoResponseBody
import retrofit2.Callback
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

class ListaCursosActivity : ComponentActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.list_course);

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

        val btn_backend = findViewById(R.id.btn_backend) as Button
        val btn_frontend = findViewById(R.id.btn_frontend) as Button
        val btn_database = findViewById(R.id.btn_database) as Button
        val btn_logica = findViewById(R.id.btn_logica_programacao) as Button
        val btn_cloud = findViewById(R.id.btn_cloud) as Button

        val sharedPreferences = getSharedPreferences(getString(R.string.preferencias), Context.MODE_PRIVATE)
        val email = sharedPreferences.getString("email", "email") ?: "Não obtido"
        val senha = sharedPreferences.getString("senha", "senha") ?: "Não obtido"

        btn_backend.setOnClickListener{

            val nm_curso = "Back-End";

            val abrirCursoRequestBody = AbrirCursoRequestBody(email, senha, nm_curso)

            val call = apiService.abrirCurso(abrirCursoRequestBody)
            call.enqueue(object : Callback<AbrirCursoResponseBody> {
                override fun onResponse(
                    call: retrofit2.Call<AbrirCursoResponseBody>, response: retrofit2.Response<AbrirCursoResponseBody>) {
                    if (response.isSuccessful) {
                        val responseBody = response.body()
                        Log.i("Resposta",response.body().toString())

                        if (responseBody != null) {
//                            Toast.makeText(
//                                this@ListaCursosActivity,
//                                "Curso aberto com sucesso",
//                                Toast.LENGTH_SHORT
//                            ).show()
                        }

                        salvarPref(nm_curso, responseBody)
                        avancarTela()

                    } else {
                        Toast.makeText(
                            this@ListaCursosActivity,
                            "Erro ao abrir curso",
                            Toast.LENGTH_SHORT
                        ).show()
                    }
                }

                override fun onFailure(call: retrofit2.Call<AbrirCursoResponseBody>, t: Throwable) {
                    // Exibe uma mensagem de erro para o usuário
                    val errorMessage = "Falha no request. Verifique a conexão com a internet."
                    Toast.makeText(this@ListaCursosActivity, errorMessage, Toast.LENGTH_SHORT).show()

                    // Log do erro no console(logcat)
                    Log.e("API Request Error", errorMessage, t)
                }
            })

        }

        btn_frontend.setOnClickListener{

            val nm_curso = "Front-End";

            val abrirCursoRequestBody = AbrirCursoRequestBody(email, senha, nm_curso)

            val call = apiService.abrirCurso(abrirCursoRequestBody)
            call.enqueue(object : Callback<AbrirCursoResponseBody> {
                override fun onResponse(
                    call: retrofit2.Call<AbrirCursoResponseBody>, response: retrofit2.Response<AbrirCursoResponseBody>) {
                    if (response.isSuccessful) {
                        val responseBody = response.body()
                        Log.i("Resposta",response.body().toString())

                        if (responseBody != null) {
                            Toast.makeText(
                                this@ListaCursosActivity,
                                "Curso aberto com sucesso",
                                Toast.LENGTH_SHORT
                            ).show()
                        }

                        salvarPref(nm_curso, responseBody)
                        avancarTela()
                    } else {
                        Toast.makeText(
                            this@ListaCursosActivity,
                            "Erro ao abrir curso",
                            Toast.LENGTH_SHORT
                        ).show()
                    }
                }

                override fun onFailure(call: retrofit2.Call<AbrirCursoResponseBody>, t: Throwable) {
                    // Exibe uma mensagem de erro para o usuário
                    val errorMessage = "Falha no request. Verifique a conexão com a internet."
                    Toast.makeText(this@ListaCursosActivity, errorMessage, Toast.LENGTH_SHORT).show()

                    // Log do erro no console(logcat)s
                    Log.e("API Request Error", errorMessage, t)
                }
            })

        }

        btn_database.setOnClickListener{

            val nm_curso = "Database";

            val abrirCursoRequestBody = AbrirCursoRequestBody(email, senha, nm_curso)

            val call = apiService.abrirCurso(abrirCursoRequestBody)
            call.enqueue(object : Callback<AbrirCursoResponseBody> {
                override fun onResponse(
                    call: retrofit2.Call<AbrirCursoResponseBody>, response: retrofit2.Response<AbrirCursoResponseBody>) {
                    if (response.isSuccessful) {
                        val responseBody = response.body()
                        Log.i("Resposta",response.body().toString())

                        if (responseBody != null) {
                            Toast.makeText(
                                this@ListaCursosActivity,
                                "Curso aberto com sucesso",
                                Toast.LENGTH_SHORT
                            ).show()
                        }

                        salvarPref(nm_curso, responseBody)
                        avancarTela()

                    } else {
                        Toast.makeText(
                            this@ListaCursosActivity,
                            "Erro ao abrir curso",
                            Toast.LENGTH_SHORT
                        ).show()
                    }
                }

                override fun onFailure(call: retrofit2.Call<AbrirCursoResponseBody>, t: Throwable) {
                    // Exibe uma mensagem de erro para o usuário
                    val errorMessage = "Falha no request. Verifique a conexão com a internet."
                    Toast.makeText(this@ListaCursosActivity, errorMessage, Toast.LENGTH_SHORT).show()

                    // Log do erro no console(logcat)s
                    Log.e("API Request Error", errorMessage, t)
                }
            })

        }

        btn_logica.setOnClickListener{

            val nm_curso = "Lógica de Programação";

            val abrirCursoRequestBody = AbrirCursoRequestBody(email, senha, nm_curso)

            val call = apiService.abrirCurso(abrirCursoRequestBody)
            call.enqueue(object : Callback<AbrirCursoResponseBody> {
                override fun onResponse(
                    call: retrofit2.Call<AbrirCursoResponseBody>, response: retrofit2.Response<AbrirCursoResponseBody>) {
                    if (response.isSuccessful) {
                        val responseBody = response.body()
                        Log.i("Resposta",response.body().toString())

                        if (responseBody != null) {
                            Toast.makeText(
                                this@ListaCursosActivity,
                                "Curso aberto com sucesso",
                                Toast.LENGTH_SHORT
                            ).show()
                        }

                        salvarPref(nm_curso, responseBody)
                        avancarTela()

                    } else {
                        Toast.makeText(
                            this@ListaCursosActivity,
                            "Erro ao abrir curso",
                            Toast.LENGTH_SHORT
                        ).show()
                    }
                }

                override fun onFailure(call: retrofit2.Call<AbrirCursoResponseBody>, t: Throwable) {
                    // Exibe uma mensagem de erro para o usuário
                    val errorMessage = "Falha no request. Verifique a conexão com a internet."
                    Toast.makeText(this@ListaCursosActivity, errorMessage, Toast.LENGTH_SHORT).show()

                    // Log do erro no console(logcat)s
                    Log.e("API Request Error", errorMessage, t)
                }
            })

        }

        btn_cloud.setOnClickListener{

            val nm_curso = "Cloud Computing";

            val abrirCursoRequestBody = AbrirCursoRequestBody(email, senha, nm_curso)

            // Faça a solicitação de login
            val call = apiService.abrirCurso(abrirCursoRequestBody)
            call.enqueue(object : Callback<AbrirCursoResponseBody> {
                override fun onResponse(
                    call: retrofit2.Call<AbrirCursoResponseBody>, response: retrofit2.Response<AbrirCursoResponseBody>) {
                    if (response.isSuccessful) {
                        val responseBody = response.body()
                        Log.i("Resposta",response.body().toString())

                        if (responseBody != null) {
                            Toast.makeText(
                                this@ListaCursosActivity,
                                "Curso aberto com sucesso",
                                Toast.LENGTH_SHORT
                            ).show()
                        }
                        salvarPref(nm_curso, responseBody)
                        avancarTela()

                    } else {
                        Toast.makeText(
                            this@ListaCursosActivity,
                            "Erro ao abrir curso",
                            Toast.LENGTH_SHORT
                        ).show()
                    }
                }

                override fun onFailure(call: retrofit2.Call<AbrirCursoResponseBody>, t: Throwable) {
                    // Exibe uma mensagem de erro para o usuário
                    val errorMessage = "Falha no request. Verifique a conexão com a internet."
                    Toast.makeText(this@ListaCursosActivity, errorMessage, Toast.LENGTH_SHORT).show()

                    // Log do erro no console(logcat)s
                    Log.e("API Request Error", errorMessage, t)
                }
            })

        }



    }

    private fun salvarPref(nm_curso:String, response:AbrirCursoResponseBody?){

        // formato do aulas key = nome da aula e chave = status da aula
        // aulas={Aula 1=Em andamento, Aula 2=Em andamento}
        // salvar aulas no shared preferences
        // salvar o nome do curso no shared preferences

        val aulas = ArrayList<String>()

        response?.aulas?.forEach {
            Log.i("Aula",it.toString())
            aulas.add(it.toString())
        }

        val aulasString = aulas.joinToString(separator = ",")
        val dt_entrada = response?.dt_entrada.toString();
        val st_status = response?.st_status.toString();

        val sharedPreferences = getSharedPreferences(getString(R.string.preferencias), Context.MODE_PRIVATE)
        val editor = sharedPreferences.edit()
        editor.putString("curso_dt_entrada", dt_entrada)
        editor.putString("curso_st_status", st_status)
        editor.putString("curso_aulas", aulasString)
        editor.putString("nm_curso", nm_curso)
        editor.putString("qtd_aulas", response?.qtd_aulas.toString())
        editor.apply()
    }
    private fun avancarTela() {
        val intent = Intent(this, CursosActivity::class.java)
        startActivity(intent)
    }
}


