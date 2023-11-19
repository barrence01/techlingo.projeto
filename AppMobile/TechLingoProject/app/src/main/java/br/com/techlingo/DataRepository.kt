package br.com.techlingo

import ApiService
import android.util.Log
import br.com.techlingo.DTO.ListarPlanosResponseBody
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

class DataRepository(baseUrl: String) {
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

    fun fetchData(callback: (ListarPlanosResponseBody) -> Unit) {
        val call = apiService.listarPlanos()

        call.enqueue(object : Callback<ListarPlanosResponseBody> {
            override fun onResponse(call: Call<ListarPlanosResponseBody>, response: Response<ListarPlanosResponseBody>) {
                if (response.isSuccessful) {
                    val items = response.body()
                    Log.i("DataRepository", items?.planos.toString())
                    callback(items!!)
                } else {
                }
            }

            override fun onFailure(call: Call<ListarPlanosResponseBody>, t: Throwable) {
            }
        })
    }
}