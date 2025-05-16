import { useParams, useNavigate } from "react-router-dom";
import axios from "axios";
import { useEffect, useState } from "react";

export default function ExcluirCategoria() {
    const { id } = useParams();
    const [categoria, setCategoria] = useState(null);
    const navigate = useNavigate();

    const token = localStorage.getItem("token");
    const headers = { Authorization: `Bearer ${token}` };

    useEffect(() => {
        axios.get(`https://localhost:7277/v1/categoria/${id}`, { headers })
            .then(res => setCategoria(res.data))
            .catch(err => {
                console.error("Erro ao buscar categoria:", err);
                navigate("/categorias");
            });
    }, [id]);

    const excluir = async () => {
        try {
            await axios.delete(`https://localhost:7277/v1/categoria/${id}`, {
                data: { id },
                headers,
            });
            navigate("/categorias");
        } catch (err) {
            console.error("Erro ao excluir categoria:", err);
            alert("Erro ao excluir.");
        }
    };

    if (!categoria) return <div className="container mt-4">Carregando...</div>;

    return (
        <div className="container mt-4">
            <h2>Confirmar Exclusão</h2>
            <p>Tem certeza que deseja excluir a categoria <strong>{categoria.titulo}</strong>?</p>
            <button className="btn btn-danger me-2" onClick={excluir}>Sim, excluir</button>
            <button className="btn btn-secondary" onClick={() => navigate("/categorias")}>Cancelar</button>
        </div>
    );
}
