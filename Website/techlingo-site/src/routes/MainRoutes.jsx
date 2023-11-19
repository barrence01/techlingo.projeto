import React from "react";
import { Routes, Route } from "react-router-dom";

import Home from "../components/Home/Home"
import Cursos from "../components/cursos/Cursos"
import Aula from "../components/aulas/Aula"
import Conteudo from "../components/aulas/Conteudo"
import Registrar from "../components/Home/Registrar"


export default function MainRoutes() {
  return (
    <>
    <Routes>
      <Route path="/" element={<Home/>} />
      <Route path="/cursos" element={<Cursos/>} />
      <Route path="/aula" element={<Aula/>} />
      <Route path="/conteudo/:id" element={<Conteudo/>} />
      <Route path="/registrar" element={<Registrar/>} />
    </Routes>
    </>
  );
}