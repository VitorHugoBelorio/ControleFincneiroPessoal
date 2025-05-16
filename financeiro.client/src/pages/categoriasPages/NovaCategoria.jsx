import { useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";

export default function NovaCategoria() {
    const [titulo, setTitulo] = useState("");
    const [descricao, setDescricao] = useState("");
    const navigate = useNavigate();

    const token = localStorage.getItem("token");
    const headers = { Authorization: `Bearer ${token}` };

    const salvar = async (e) => {
        e.preventDefault();
        try {
            await axios.post("https://localhost:7277/v1/categoria", { titulo, descricao }, { headers });
            navigate("/categorias");
        } catch (err) {
            console.error("Erro ao salvar categoria:", err);
            alert("Erro ao salvar categoria.");
        }
    };

    return (
        <div className="container mt-4">
            <h2>Nova Categoria</h2>
            <form onSubmit={salvar}>
                <div className="mb-3">
                    <label className="form-label">Título</label>
                    <input type="text" className="form-control" value={titulo} onChange={(e) => setTitulo(e.target.value)} required />
                </div>
                <div className="mb-3">
                    <label className="form-label">Descrição</label>
                    <textarea className="form-control" value={descricao} onChange={(e) => setDescricao(e.target.value)} />
                </div>
                <button type="submit" className="btn btn-success">Salvar</button>
                <button type="button" className="btn btn-secondary ms-2" onClick={() => navigate("/categorias")}>
                    Cancelar
                </button>
            </form>
        </div>
    );
}
