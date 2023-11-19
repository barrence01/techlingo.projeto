import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import API_DOMAIN from "../../config";

import "./Menu.css";


export default function Menu() {


  return (
    <div>
      <header className="header">
        <h1>
          <Link to='/' className="menu-titulo"
            >Techlingo
           </Link>
          </h1>
        <nav id="menu-topo">
        </nav>
      </header>
    </div>
  );
}
