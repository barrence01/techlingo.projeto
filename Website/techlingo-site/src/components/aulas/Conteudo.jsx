import React, { useState, useEffect } from "react";
import API_DOMAIN from '../../config';
import { useParams } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import "./Conteudo.css";



class abrirAulaRequest {
    constructor(email, senha, nm_curso, nr_aula) {
        this.email = email;
        this.senha = senha;
        this.nm_curso = nm_curso;
        this.nr_aula = nr_aula;
    }
  }

  class abrirAulaResponse {
    constructor(ds_titulo, ds_link_video, ds_descricao, ds_descricao_quiz, ds_pergunta1, ds_pergunta2, ds_pergunta3, ds_resposta) {
        this.ds_titulo = ds_titulo;
        this.ds_link_video = ds_link_video;
        this.ds_descricao = ds_descricao;
        this.ds_descricao_quiz = ds_descricao_quiz;
        this.ds_pergunta1 = ds_pergunta1;
        this.ds_pergunta2 = ds_pergunta2;
        this.ds_pergunta3 = ds_pergunta3;
        this.ds_resposta = ds_resposta;
    }
  }

  class concluirAulaRequestBody {
    constructor(email, senha, nm_curso, nr_aula) {
        this.email = email;
        this.senha = senha;
        this.nm_curso = nm_curso;
        this.nr_aula = nr_aula;
    }
  
  }

  class concluirCursoRequestBody {
    constructor(email, senha, nm_curso) {
        this.email = email;
        this.senha = senha;
        this.nm_curso = nm_curso;
    }
  
  }




function Conteudo() {

    const { id } = useParams();
    
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [curso_selecionado, setCurso_selecionado] = useState('');
    const navigate = useNavigate();

    //useState do conteudo da aula
    const [ds_titulo, setDs_titulo] = useState('');
    const [ds_link_video, setDs_link_video] = useState('');
    const [ds_descricao, setDs_descricao] = useState('');
    const [ds_descricao_quiz, setDs_descricao_quiz] = useState('');
    const [ds_pergunta1, setDs_pergunta1] = useState('');
    const [ds_pergunta2, setDs_pergunta2] = useState('');
    const [ds_pergunta3, setDs_pergunta3] = useState('');
    const [ds_resposta, setDs_resposta] = useState('');

    //Resposta quiz
    const [selectedOption, setSelectedOption] = useState('nenhum')  
    const [errorMessage, setErrorMessage] = useState('');

    
    //useEffect to get data from session storage
    useEffect(() => {
        const username = sessionStorage.getItem("username");
        const password = sessionStorage.getItem("password");
        const curso_selecionado = sessionStorage.getItem("curso_selecionado");



        setUsername(username);
        setPassword(password);
        setCurso_selecionado(curso_selecionado);
        console.log("Username: " + username);
        console.log("Password: " + password);
        console.log("curso_selecionado: " + curso_selecionado);


        const abrirRequestJSON = JSON.stringify(new abrirAulaRequest(username, password, curso_selecionado, Number(id)));
        console.log(abrirRequestJSON);

        fetch(`${API_DOMAIN}/api/Cursos/abrirAula`, {
            method: 'POST',
            headers: {
            'Content-Type': 'application/json'
            },
            body: abrirRequestJSON
        })
        .then(response =>  response.json())
        .then (data => {
            
            console.log("Aula aberta com sucesso");
            const jsonString = data;
            console.log(jsonString);

            setDs_titulo(jsonString.ds_titulo);
            setDs_link_video(jsonString.ds_link_video);
            setDs_descricao(jsonString.ds_descricao);
            setDs_descricao_quiz(jsonString.ds_descricao_quiz);
            setDs_pergunta1(jsonString.ds_pergunta1);
            setDs_pergunta2(jsonString.ds_pergunta2);
            setDs_pergunta3(jsonString.ds_pergunta3);
            setDs_resposta(jsonString.ds_resposta);

        })
        .catch(error => {
        console.log(error);
        });

    }, []);


    const handleQuiz = () => {

        console.log("Resposta: " + selectedOption);

        if(selectedOption === 'nenhum'){
            alert("Selecione uma resposta!");
        }

        if(selectedOption === ds_resposta){
            alert("Resposta correta!");
            handleConcluirAula();
            
        } else {
            setErrorMessage("Resposta incorreta!");
            //alert("Resposta incorreta!");
        }

        
    }

    const handleConcluirAula = () => {

        const concluirAulaRequestJSON = JSON.stringify(new concluirAulaRequestBody(username, password, curso_selecionado, Number(id)));
        console.log(concluirAulaRequestJSON);

        fetch(`${API_DOMAIN}/api/Cursos/concluirAula`, {
            method: 'POST',
            headers: {
            'Content-Type': 'application/json'
            },
            body: concluirAulaRequestJSON
        })
        .then(response => {
            if(response.status === 200){
                handleConcluirCurso();
            }else{
              //setErrorMessage('Usuário ou senha inválidos');
            }
          })
         .catch(error => {
          console.log(error);
          setErrorMessage('Não foi possível se conectar ao servidor');
         });

    }

    const handleConcluirCurso = () => { 

        const concluirCursoRequestJSON = JSON.stringify(new concluirCursoRequestBody(username, password, curso_selecionado));
        console.log(concluirCursoRequestJSON);

        fetch(`${API_DOMAIN}/api/Cursos/concluirCurso`, {
            method: 'POST',
            headers: {
            'Content-Type': 'application/json'
            },
            body: concluirCursoRequestJSON
        })
        .then(response =>  response.json())
        .then (data => {

            const jsonString = data;
            console.log(jsonString);

            if(jsonString.resultado === "Curso concluido com sucesso"){
                alert("Curso concluído com sucesso!");
            }
            navigate('/cursos');
        })
         .catch(error => {
          console.log(error);
          setErrorMessage('Não foi possível se conectar ao servidor');
         });
    }



    
    function onValueChange(event){
        
        setSelectedOption(event.target.value)
    }


    return (
        <div>
            <h1>Conteúdo</h1>
            
            <div className="box-aula-conteudo">
                <h2>{ds_titulo}</h2>
                <iframe width="560" height="315" src={ds_link_video} title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"></iframe>
                <h3>{ds_descricao}</h3>
                <br/>
                <h4>{ds_descricao_quiz}</h4>
                {errorMessage && <div className="error-message" style={{color: 'red'}}>{errorMessage}</div>}
                <form className="form-conteudo">
                    <div className="radio">
                        <label>
                            <input type="radio" name="quiz" value={ds_pergunta1} 
                            onChange={onValueChange} />
                            {ds_pergunta1}
                        </label>
                    </div>
                    <div className="radio">
                        <label>
                            <input type="radio" name="quiz" value={ds_pergunta2}
                            onChange={onValueChange} />
                            {ds_pergunta2}
                        </label>
                    </div>
                    <div className="radio">
                        <label>
                            <input type="radio" name="quiz" value={ds_pergunta3}
                            onChange={onValueChange} />
                            {ds_pergunta3}
                        </label>
                    </div>
                    <button type="button" onClick={handleQuiz}>
                        Submeter
                    </button>
                </form>
            </div>
        </div>
    );
}

export default Conteudo;