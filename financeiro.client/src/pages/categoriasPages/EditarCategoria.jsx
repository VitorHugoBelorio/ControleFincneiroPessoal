import { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import axios from "axios";

export default function EditarCategoria() {
    const [titulo, setTitulo] = useState("");
    const [descricao, setDescricao] = useState("");
    const { id } = useParams();
    const navigate = useNavigate();

    const token = localStorage.getItem("token");
    const headers = { Authorization: `Bearer ${token}` };

    useEffect(() => {
        axios.get(`https://localhost:5227/v1/categoria/${id}`, { headers })
            .then(res => {
                setTitulo(res.data.titulo);
                setDescricao(res.data.descricao || "");
            })
            .catch(err => {
                console.error("Erro ao carregar categoria:", err);
            });
    }, [id]);

    const atualizar = async (e) => {
        e.preventDefault();
        try {
            await axios.put(`https://localhost:7277/v1/categoria/${id}`, { id, titulo, descricao }, { headers });
            navigate("/categorias");
        } catch (err) {
            console.error("Erro ao atualizar categoria:", err);
        }
    };

    return (
        <div className="container mt-4">
            <h2>Editar Categoria</h2>
            <form onSubmit={atualizar}>
                <div className="mb-3">
                    <label className="form-label">Título</label>
                    <input type="text" className="form-control" value={titulo} onChange={(e) => setTitulo(e.target.value)} required />
                </div>
                <div className="mb-3">
                    <label className="form-label">Descrição</label>
                    <textarea className="form-control" value={descricao} onChange={(e) => setDescricao(e.target.value)} />
                </div>
                <button type="submit" className="btn btn-success">Atualizar</button>
                <button type="button" className="btn btn-secondary ms-2" onClick={() => navigate("/categorias")}>
                    Cancelar
                </button>
            </form>
        </div>
    );
}
