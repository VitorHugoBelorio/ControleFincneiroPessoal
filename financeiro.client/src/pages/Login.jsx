import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import Input from "../components/componentsLogin/Input";
import Button from "../components/componentsLogin/Button";

const Login = () => {
    const [email, setEmail] = useState("");
    const [senha, setSenha] = useState("");
    const navigate = useNavigate();

    const handleLogin = async (e) => {
        e.preventDefault();

        try {
            const response = await fetch("https://localhost:7277/v1/login", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({ email, senha })
            });

            if (response.ok) {
                const data = await response.json();
                console.log("Login realizado:", data);

                // Salvar token se quiser usar depois (ex: localStorage)
                // localStorage.setItem("token", data.token);

                // Redireciona para o painel
                navigate("/home");
            } else {
                const erro = await response.json();
                alert(erro.mensagem || "Erro no login");
            }
        } catch (error) {
            console.error("Erro ao conectar com o servidor:", error);
            alert("Não foi possível conectar com o servidor.");
        }
    };

    return (
        <div className="d-flex align-items-center justify-content-center vh-100 bg-light">
            <form
                onSubmit={handleLogin}
                className="bg-white p-5 rounded shadow-sm w-100"
                style={{ maxWidth: "400px" }}
            >
                <h2 className="text-center mb-4">Login</h2>
                <Input
                    label="E-mail"
                    type="email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    placeholder="seuemail@exemplo.com"
                />
                <Input
                    label="Senha"
                    type="password"
                    value={senha}
                    onChange={(e) => setSenha(e.target.value)}
                    placeholder="Digite sua senha"
                />
                <Button type="submit">Entrar</Button>
            </form>
        </div>
    );
};

export default Login;
