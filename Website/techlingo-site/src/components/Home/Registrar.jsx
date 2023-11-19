import React, { useState } from "react";
import API_DOMAIN from '../../config';
import { useNavigate } from "react-router-dom";


class cadastroRequest {
  constructor(nm_aluno, dt_nascimento, nr_cpf, ds_email, ds_senha, ds_plano) {
    this.nm_aluno = nm_aluno;
    this.dt_nascimento = dt_nascimento;
    this.nr_cpf = nr_cpf;
    this.ds_email = ds_email;
    this.ds_senha = ds_senha;
    this.ds_plano = ds_plano;
  }
}


function Registrar() {
  const [nome, setNome] = useState('');
  const [nascimento, setNascimento] = useState('');
  const [cpf, setCpf] = useState('');
  const [email, setEmail] = useState('');
  const [senha, setSenha] = useState('');
  const [plano, setPlano] = useState('Basic');

  const navigate = useNavigate();


  const handleRegistrar = () => {



    const registroRequestJSON = JSON.stringify(new cadastroRequest(nome, nascimento, cpf, email, senha, plano));
    console.log(registroRequestJSON);


      fetch(`${API_DOMAIN}/api/Aluno/CadastrarAluno`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: registroRequestJSON
      })
      .then(response => {
        if(response.status === 200){
          alert("Usuário cadastrado com sucesso!")
          navigate('/');
        }else{
          alert('Não foi possível cadastrar o usuário');
        }
      })
     .catch(error => {
      console.log(error);
      alert('Não foi possível cadastrar o usuário');
     });
  }

  const handleDateChange = (e) => {
    // Remove quaisquer barras que o usuário digitar
    const limparBarra = e.target.value.replace(/\//g, '');

    console.log(limparBarra);
    // Adiciona as barras via regex
    const dataFormatada = limparBarra
      .replace(/^(\d{2})/, '$1/') // Adiciona a primeira barra após o segundo dígito
      .replace(/(\d{2})(\d{2})/, '$1/$2'); // Adiciona a segunda barra após o quarto dígito

    setNascimento(dataFormatada);
  };

  const handleSetPlano = (e) => {
    setPlano(e.target.value);
  }
  

  return (
    
    <div className="login-container">
      <div className="login-box">
        <h2>Registrar</h2>
        <form>
          <div className="user-box">
          <label>Nome</label>
            <input type="text" name="" required="" 
            onChange={(e) => setNome(e.target.value)}/>
          </div>
          <br/>
          <div className="user-box">
            <label>Data de Nascimento</label>
            <input type="text" id="dateInput" value={nascimento} placeholder="DD/MM/AAAA" name="" required="" maxLength={10}
            onChange={handleDateChange} />
          </div>
          <br/>
          <div className="user-box">
            <label>CPF</label>
            <input type="text" name="" required=""
            onChange={(e) => setCpf(e.target.value)}/>
          </div>
          <br/>
          <div className="user-box">
            <label>Email</label>
            <input type="text" name="" required="" 
            onChange={(e) => setEmail(e.target.value)}/>
          </div>
          <br/>
          <div className="user-box">
            <label>Senha</label>
            <input type="password" name="" required="" 
            onChange={(e) => setSenha(e.target.value)}/>
          </div>
          <br/>
          <div className="user-box">
            <label>Plano</label>
            <select
              id="Planos"
              value={plano}
              onChange={handleSetPlano}
            >
              <option value="Basic">Basic</option>
              <option value="Intermediate">Intermediate</option>
              <option value="Gold">Gold</option>
              <option value="Platinum">Platinum</option>
              <option value="Scholar">Scholar</option>
            </select>
          </div>
          <br/>
          <button type="button" onClick={handleRegistrar}>
            Registrar
          </button>
        </form>
      </div>
    </div>
  );
}

export default Registrar;
