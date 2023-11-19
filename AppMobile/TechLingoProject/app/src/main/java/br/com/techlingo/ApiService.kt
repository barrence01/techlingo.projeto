import br.com.techlingo.DTO.AbrirAulaRequestBody
import br.com.techlingo.DTO.AbrirAulaResponseBody
import br.com.techlingo.DTO.AbrirCursoRequestBody
import br.com.techlingo.DTO.AbrirCursoResponseBody
import br.com.techlingo.DTO.CadastrarRequestBody
import br.com.techlingo.DTO.ConcluirAulaRequestBody
import br.com.techlingo.DTO.ConcluirCursoRequestBody
import br.com.techlingo.DTO.ConcluirCursoResposeBody
import br.com.techlingo.DTO.ListarPlanosResponseBody
import br.com.techlingo.DTO.LoginRequestBody
import retrofit2.Call
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.POST

interface ApiService {
    @POST("/api/Aluno/LoginAluno")
    fun loginAluno(@Body requestBody: LoginRequestBody): Call<Unit>

    @POST("/api/Cursos/abrirCurso")
    fun abrirCurso(@Body requestBody: AbrirCursoRequestBody): Call<AbrirCursoResponseBody>

    @POST("/api/Cursos/abrirAula")
    fun abrirAula(@Body requestBody: AbrirAulaRequestBody): Call<AbrirAulaResponseBody>

    @POST("/api/Cursos/concluirAula")
    fun concluirAula(@Body requestBody: ConcluirAulaRequestBody): Call<Unit>

    @POST("/api/Cursos/concluirCurso")
    fun concluirCurso(@Body requestBody: ConcluirCursoRequestBody): Call<ConcluirCursoResposeBody>

    @GET("/api/Plano/ListaDePlanos")
    fun listarPlanos(): Call<ListarPlanosResponseBody>

    @POST("/api/Aluno/CadastrarAluno")
    fun cadastrarAluno(@Body requestBody: CadastrarRequestBody): Call<Unit>
}
