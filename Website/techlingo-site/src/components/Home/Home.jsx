import React, { useState } from "react";
import API_DOMAIN from '../../config';
import { useNavigate } from "react-router-dom";
import "./Home.css";


class loginRequest {
  constructor(email, senha) {
      this.email = email;
      this.senha = senha;
  }
}

function Home() {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');

  const [errorMessage, setErrorMessage] = useState('');
  const navigate = useNavigate();

  const handleLogin = () => {
    // Implemente aqui a lógica de autenticação
    console.log(`Usuário: ${username}, Senha: ${password}`);

    const loginRequestJSON = JSON.stringify(new loginRequest(username, password));
    console.log(loginRequestJSON);


      fetch(`${API_DOMAIN}/api/Aluno/LoginAluno`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: loginRequestJSON
      })
      .then(response => {
        if(response.status === 200){
          sessionStorage.setItem("password", password);
          sessionStorage.setItem("username", username);
          navigate('/cursos');
        }else{
          setErrorMessage('Usuário ou senha inválidos');
        }
      })
     .catch(error => {
      console.log(error);
      setErrorMessage('Não foi possível se conectar ao servidor');
     });
    
    
  };

  

  return (
    <div className="login-container">
      <div className="login-box">
      {errorMessage && <div className="error-message" style={{color: 'red'}}>{errorMessage}</div>}
        <h2>Login</h2>
        <form>
          <label htmlFor="username">Usuário:</label>
          <input
            type="text"
            id="username"
            className="input-home"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
          />

          <label htmlFor="password">Senha:</label>
          <input
            type="password"
            id="password"
            className="input-home"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
          <br/>
          <button type="button" onClick={handleLogin}>
            Entrar
          </button>
        </form>
        <br/>
        <button type="button" onClick={() => navigate("/registrar")}>
            Registrar
          </button>
      </div>
    </div>
  );
}

export default Home;
