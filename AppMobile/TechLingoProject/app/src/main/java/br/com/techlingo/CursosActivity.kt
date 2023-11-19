package br.com.techlingo

import ApiService
import android.content.Context
import android.content.Intent
import android.graphics.Color
import android.os.Bundle
import android.util.Log
import android.view.View
import android.widget.Button
import android.widget.ImageView
import android.widget.TextView
import android.widget.Toast
import androidx.activity.ComponentActivity
import br.com.techlingo.DTO.AbrirAulaRequestBody
import br.com.techlingo.DTO.AbrirAulaResponseBody
import retrofit2.Callback
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory


class CursosActivity : ComponentActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_curso);

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


        val sharedPreferences = getSharedPreferences(getString(R.string.preferencias), MODE_PRIVATE)
        val email = sharedPreferences.getString("email", "email") ?: "Não obtido"
        val senha = sharedPreferences.getString("senha", "senha") ?: "Não obtido"
        val nm_curso = sharedPreferences.getString("nm_curso", "nm_curso") ?: "Não obtido"
        val dt_entrada = sharedPreferences.getString("curso_dt_entrada", "dt_entrada") ?: "Não obtido"
        val st_status = sharedPreferences.getString("curso_st_status", "st_status") ?: "Não obtido"
        val curso_aulas = sharedPreferences.getString("curso_aulas", "") ?: ""
        val qtd_aulas_string = sharedPreferences.getString("qtd_aulas", "qtd_aulas") ?: "Não obtido"
        val qtd_aulas = Integer.parseInt(qtd_aulas_string)
        Log.i("qtd_aulas",curso_aulas)

        val txt_nm_curso = findViewById(R.id.txt_nome_curso) as TextView
        val txt_data_entrada = findViewById(R.id.txt_curso_data_entrada) as TextView
        val txt_status = findViewById(R.id.txt_curso_status) as TextView

        txt_nm_curso.text = nm_curso
        txt_data_entrada.text = dt_entrada
        txt_status.text = st_status
        when (st_status) {
            "Concluido" -> {
                txt_status.setTextColor(Color.GREEN)
            }
            "Em andamento" -> {
                txt_status.setTextColor(Color.RED)
            }
            else -> {
                txt_status.text = ""
            }
        }

        val lista_aulas = curso_aulas.split(",")

        val btn_aula1 = findViewById(R.id.btn_aula1) as Button
        val btn_aula2 = findViewById(R.id.btn_aula2) as Button
        val btn_aula3 = findViewById(R.id.btn_aula3) as Button
        val btn_aula4 = findViewById(R.id.btn_aula4) as Button
        val st_aula1 = findViewById(R.id.txt_status_aula1) as TextView
        val st_aula2 = findViewById(R.id.txt_status_aula2) as TextView
        val st_aula3 = findViewById(R.id.txt_status_aula3) as TextView
        val st_aula4 = findViewById(R.id.txt_status_aula4) as TextView
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

        btn_aula1.isEnabled = false;
        btn_aula2.isEnabled = false;
        btn_aula3.isEnabled = false;
        btn_aula4.isEnabled = false;



        try {
            if (!lista_aulas.isEmpty()) {
                btn_aula1.isEnabled = true
                val aula = lista_aulas[0].split("=")
                btn_aula1.text = "   " + aula[0]
                st_aula1.text = aula[1]
                mudarCorStatus(aula[1], st_aula1)
            }
        } catch (e: Exception) {
            if (qtd_aulas < 1) {
                btn_aula1.isEnabled = false
                st_aula1.text = "Indisponível"
            } else {
                btn_aula1.isEnabled = true
                btn_aula1.text = "   Aula 01"
                st_aula1.text = "Não iniciado"
                mudarCorStatus("", st_aula1)
            }
        }
        try{
        if (!lista_aulas[1].isNullOrBlank()) {
            btn_aula2.isEnabled = true
            val aula = lista_aulas[1].split("=")
            btn_aula2.text = "   "+aula[0]
            st_aula2.text = aula[1]
            mudarCorStatus(aula[1], st_aula2)
        }
        } catch (e: Exception) {
            if(qtd_aulas < 2) {
                btn_aula2.isEnabled = false
                st_aula2.text = "Indisponível"
            }
            else {
                btn_aula2.isEnabled = true
                btn_aula2.text = "   Aula 02"
                st_aula2.text = "Não iniciado"
                mudarCorStatus("", st_aula2)
            }
        }
        try{
        if (!lista_aulas[2].isNullOrBlank()) {
            btn_aula3.isEnabled = true
            val aula = lista_aulas[2].split("=")
            btn_aula3.text = "   "+aula[0]
            st_aula3.text = aula[1]
            mudarCorStatus(aula[1], st_aula3)
        }
        } catch (e: Exception) {
            if(qtd_aulas < 3) {
                btn_aula3.isEnabled = false
                st_aula3.text = "Indisponível"
            }
            else {
                btn_aula3.isEnabled = true
                btn_aula3.text = "   Aula 03"
                st_aula3.text = "Não iniciado"
                mudarCorStatus("", st_aula3)
            }
        }
        try{
        if (!lista_aulas[3].isNullOrBlank()) {
            btn_aula4.isEnabled = true
            val aula = lista_aulas[3].split("=")
            btn_aula4.text = "   "+aula[0]
            st_aula4.text = aula[1]
            mudarCorStatus(aula[1], st_aula4)
        }
        } catch (e: Exception) {
            if(qtd_aulas < 4) {
                btn_aula4.isEnabled = false
                st_aula4.text = "Indisponível"
            }
            else {
                btn_aula4.isEnabled = true
                btn_aula4.text = "   Aula 04"
                st_aula4.text = "Não iniciado"
                mudarCorStatus("", st_aula4)
            }
        }

        btn_aula1.setOnClickListener{
            val abrirAulaRequestBody = AbrirAulaRequestBody(email, senha, nm_curso, 1)

            val call = apiService.abrirAula(abrirAulaRequestBody)

            call.enqueue(object : Callback<AbrirAulaResponseBody> {
                override fun onResponse(
                    call: retrofit2.Call<AbrirAulaResponseBody>, response: retrofit2.Response<AbrirAulaResponseBody>) {
                    if (response.isSuccessful) {
                        val responseBody = response.body()
                        Log.i("Resposta",response.body().toString())

                        if (responseBody != null) {
//                            Toast.makeText(
//                                this@CursosActivity,
//                                "Aula aberta com sucesso",
//                                Toast.LENGTH_SHORT
//                            ).show()
                        }

                        salvarPref(responseBody!!,1, qtd_aulas)
                        avancarTela()
                    } else {
                        Toast.makeText(
                            this@CursosActivity,
                            "Erro ao abrir aula",
                            Toast.LENGTH_SHORT
                        ).show()
                    }
                }

                override fun onFailure(call: retrofit2.Call<AbrirAulaResponseBody>, t: Throwable) {
                    // Exibe uma mensagem de erro para o usuário
                    val errorMessage = "Falha no request. Verifique a conexão com a internet."
                    Toast.makeText(this@CursosActivity, errorMessage, Toast.LENGTH_SHORT).show()

                    // Log do erro no console(logcat)s
                    Log.e("API Request Error", errorMessage, t)
                }
            })
        }

        btn_aula2.setOnClickListener{
            val abrirAulaRequestBody = AbrirAulaRequestBody(email, senha, nm_curso, 2)

            val call = apiService.abrirAula(abrirAulaRequestBody)

            call.enqueue(object : Callback<AbrirAulaResponseBody> {
                override fun onResponse(
                    call: retrofit2.Call<AbrirAulaResponseBody>, response: retrofit2.Response<AbrirAulaResponseBody>) {
                    if (response.isSuccessful) {
                        val responseBody = response.body()
                        Log.i("Resposta",response.body().toString())

                        if (responseBody != null) {
//                            Toast.makeText(
//                                this@CursosActivity,
//                                "Aula aberta com sucesso",
//                                Toast.LENGTH_SHORT
//                            ).show()
                        }

                        salvarPref(responseBody!!,2, qtd_aulas)
                        avancarTela()
                    } else {
                        Toast.makeText(
                            this@CursosActivity,
                            "Erro ao abrir aula",
                            Toast.LENGTH_SHORT
                        ).show()
                    }
                }

                override fun onFailure(call: retrofit2.Call<AbrirAulaResponseBody>, t: Throwable) {
                    // Exibe uma mensagem de erro para o usuário
                    val errorMessage = "Falha no request. Verifique a conexão com a internet."
                    Toast.makeText(this@CursosActivity, errorMessage, Toast.LENGTH_SHORT).show()

                    // Log do erro no console(logcat)s
                    Log.e("API Request Error", errorMessage, t)
                }
            })

        }

        btn_aula3.setOnClickListener{
            val abrirAulaRequestBody = AbrirAulaRequestBody(email, senha, nm_curso, 3)

            val call = apiService.abrirAula(abrirAulaRequestBody)

            call.enqueue(object : Callback<AbrirAulaResponseBody> {
                override fun onResponse(
                    call: retrofit2.Call<AbrirAulaResponseBody>, response: retrofit2.Response<AbrirAulaResponseBody>) {
                    if (response.isSuccessful) {
                        val responseBody = response.body()
                        Log.i("Resposta",response.body().toString())

                        if (responseBody != null) {
//                            Toast.makeText(
//                                this@CursosActivity,
//                                "Aula aberta com sucesso",
//                                Toast.LENGTH_SHORT
//                            ).show()
                        }

                        salvarPref(responseBody!!,3, qtd_aulas)
                        avancarTela()
                    } else {
                        Toast.makeText(
                            this@CursosActivity,
                            "Erro ao abrir aula",
                            Toast.LENGTH_SHORT
                        ).show()
                    }
                }

                override fun onFailure(call: retrofit2.Call<AbrirAulaResponseBody>, t: Throwable) {
                    // Exibe uma mensagem de erro para o usuário
                    val errorMessage = "Falha no request. Verifique a conexão com a internet."
                    Toast.makeText(this@CursosActivity, errorMessage, Toast.LENGTH_SHORT).show()

                    // Log do erro no console(logcat)s
                    Log.e("API Request Error", errorMessage, t)
                }
            })
        }

        btn_aula4.setOnClickListener{
            val abrirAulaRequestBody = AbrirAulaRequestBody(email, senha, nm_curso, 4)

            val call = apiService.abrirAula(abrirAulaRequestBody)

            call.enqueue(object : Callback<AbrirAulaResponseBody> {
                override fun onResponse(
                    call: retrofit2.Call<AbrirAulaResponseBody>, response: retrofit2.Response<AbrirAulaResponseBody>) {
                    if (response.isSuccessful) {
                        val responseBody = response.body()
                        Log.i("Resposta",response.body().toString())

                        if (responseBody != null) {
//                            Toast.makeText(
//                                this@CursosActivity,
//                                "Aula aberta com sucesso",
//                                Toast.LENGTH_SHORT
//                            ).show()
                        }

                        salvarPref(responseBody!!,4, qtd_aulas)
                        avancarTela()
                    } else {
                        Toast.makeText(
                            this@CursosActivity,
                            "Erro ao abrir aula",
                            Toast.LENGTH_SHORT
                        ).show()
                    }
                }

                override fun onFailure(call: retrofit2.Call<AbrirAulaResponseBody>, t: Throwable) {
                    // Exibe uma mensagem de erro para o usuário
                    val errorMessage = "Falha no request. Verifique a conexão com a internet."
                    Toast.makeText(this@CursosActivity, errorMessage, Toast.LENGTH_SHORT).show()

                    // Log do erro no console(logcat)s
                    Log.e("API Request Error", errorMessage, t)
                }
            })



        }

    }

    override fun onRestart() {
        super.onRestart()
        finish()
    }


    private fun mudarCorStatus(st_status:String, txt:TextView) {
        when (st_status) {
            "Concluido" -> {
                txt.setTextColor(Color.GREEN)
            }
            "Não concluido" -> {
                txt.setTextColor(Color.RED)
            }
            else -> {
                txt.setTextColor(Color.BLACK)
            }
        }
    }

    private fun salvarPref(aula:AbrirAulaResponseBody, nr_aula:Int, qtd_aulas:Int){
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
        editor.putInt("qtd_aulas", qtd_aulas?:0)
        editor.apply()

    }

    private fun avancarTela() {
        val intent = Intent(this, CursosAulaActivity::class.java)
        startActivity(intent)
    }


}


