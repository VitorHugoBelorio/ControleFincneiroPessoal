import { BrowserRouter, Route, Routes } from "react-router-dom";

import Login from "../pages/Login"
import Home from "../pages/Home"

function minhasRotas() {
  return (
      <BrowserRouter>
          <Routes>
              <Route path="/" element={<Login />} />
              <Route path="/home" element={<Home /> } />
          </Routes>
      </BrowserRouter>
  );
}

export default minhasRotas;