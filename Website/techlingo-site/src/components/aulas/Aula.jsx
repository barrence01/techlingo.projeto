import React, { useState, useEffect } from "react";
import API_DOMAIN from '../../config';
import { useNavigate } from "react-router-dom";
import "./Aula.css";

class cursoRequest {
    constructor(email, senha, nm_curso) {
        this.email = email;
        this.senha = senha;
        this.nm_curso = nm_curso
    }
  }

  class cursoResponse {
    constructor(dt_entrada, st_status, aulas, qtd_aulas) {
        this.dt_entrada = dt_entrada;
        this.st_status = st_status;
        this.aulas = aulas;
        this.qtd_aulas = qtd_aulas;
    }
  }


function Aula() {

    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [nm_curso, setNm_curso] = useState('');
    const [jsonString, setJsonString] = useState([]);
    const [aulasArray, setAulasArray] = useState([]);

    const navigate = useNavigate();

   

    //useEffect to get data from session storage
    useEffect(() => {
        const username = sessionStorage.getItem("username");
        const password = sessionStorage.getItem("password");
        const nm_curso = sessionStorage.getItem("nm_curso");
        const json = sessionStorage.getItem("json");
        const jsonString = JSON.parse(json);
        // Extract the "aulas" into an array
        const aulasArray = Object.keys(jsonString.aulas).map(aula => {
            return { nome: aula, status: jsonString.aulas[aula] };
        });



        setUsername(username);
        setPassword(password);
        setNm_curso(nm_curso);
        setJsonString(jsonString);
        setAulasArray(aulasArray);
        console.log("Username: " + username);
        console.log("Password: " + password);
        console.log("Nm_curso: " + nm_curso);
        console.log("Json: " + json);
        console.log("Aulas array: " + aulasArray);

        sessionStorage.setItem("curso_selecionado", nm_curso);
    }, []);

    return (
        <div className="aulas-cursos">
            <h1>Curso: {nm_curso}</h1>
            <h3>Status: {jsonString.st_status}</h3>
            <h3>Data de entrada: {jsonString.dt_entrada}</h3>
            <h2 className="titulo-aulas">Aulas:</h2>
            <div>
                {aulasArray.map((aula, index) => {
                    return (
                        <div key={index}>
                            <h3>Aula: {aula.nome}</h3>
                            <h3>Status: {aula.status}</h3>
                            <button onClick={() => navigate('/conteudo/'+(index+1))}>Abrir aula</button>
                        </div>
                    );
                })}
            </div>
        </div>
    );
}

export default Aula;