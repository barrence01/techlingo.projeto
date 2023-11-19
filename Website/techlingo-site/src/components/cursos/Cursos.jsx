import React, { useState, useEffect } from "react";
import API_DOMAIN from '../../config';
import { useNavigate } from "react-router-dom";
import "./Cursos.css";

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


function Cursos() {

    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    const navigate = useNavigate();

    const handleCurso = (element) => {
        console.log("Dentro do handle: " + element);


        const cursoRequestJSON = JSON.stringify(new cursoRequest(username, password, element));
        console.log(cursoRequestJSON);


        fetch(`${API_DOMAIN}/api/Cursos/abrirCurso`, {
            method: 'POST',
            headers: {
            'Content-Type': 'application/json'
            },
            body: cursoRequestJSON
        })
        .then(response =>  response.json())
        .then (data => {

            console.log("Curso aberto com sucesso");
            const jsonString = data;
            console.log(jsonString);

            sessionStorage.setItem("json", JSON.stringify(jsonString));
            sessionStorage.setItem("nm_curso", element);
            navigate('/aula');
        })
        .catch(error => {
        console.log(error);
        });

    };

    //useEffect to get data from session storage
    useEffect(() => {
        const username = sessionStorage.getItem("username");
        const password = sessionStorage.getItem("password");
        setUsername(username);
        setPassword(password);
    }, []);

    return (
        <div>
            <h1>Cursos</h1>
            <div className="box-botoes">
                <input
                    type="button"
                    className="curso-selecionado"
                    value='Back-End'
                    onClick={(e) => handleCurso(e.target.value)}
                />
                <input
                    type="button"
                    className="curso-selecionado"
                    value='Front-End'
                    onClick={(e) => handleCurso(e.target.value)}
                />
                <input
                    type="button"
                    className="curso-selecionado"
                    value='Database'
                    onClick={(e) => handleCurso(e.target.value)}
                />
                <input
                    type="button"
                    className="curso-selecionado"
                    value='Lógica de Programação'
                    onClick={(e) => handleCurso(e.target.value)}
                />
                <input
                    type="button"
                    className="curso-selecionado"
                    value='Cloud Computing'
                    onClick={(e) => handleCurso(e.target.value)}
                />
            </div>
        </div>
    );
}

export default Cursos;