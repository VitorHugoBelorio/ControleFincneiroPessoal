import { useParams, useNavigate } from "react-router-dom";
import axios from "axios";
import { useEffect, useState } from "react";

export default function ExcluirTransacao() {
    const { id } = useParams();
    const [transacao, setTransacao] = useState(null);
    const navigate = useNavigate();

    const token = localStorage.getItem("token");
    const headers = { Authorization: `Bearer ${token}` };

    useEffect(() => {
        axios.get(`https://localhost:7277/v1/transacao/${id}`, { headers })
            .then(res => setTransacao(res.data))
            .catch(err => {
                console.error("Erro ao buscar transação:", err);
                navigate("/transacoes");
            });
    }, [id]);

    const excluir = async () => {
        try {
            await axios.delete(`https://localhost:7277/v1/transacao/${id}`, {
                data: { id },
                headers,
            });
            navigate("/transacoes");
        } catch (err) {
            console.error("Erro ao excluir transação:", err);
            alert("Erro ao excluir.");
        }
    };

    if (!transacao) return <div className="container mt-4">Carregando...</div>;

    return (
        <div className="container mt-4">
            <h2>Confirmar Exclusão</h2>
            <p>
                Tem certeza que deseja excluir a transação <strong>{transacao.titulo}</strong>
                no valor de <strong>R$ {transacao.quantia.toFixed(2)}</strong>?
            </p>
            <button className="btn btn-danger me-2" onClick={excluir}>Sim, excluir</button>
            <button className="btn btn-secondary" onClick={() => navigate("/transacoes")}>Cancelar</button>
        </div>
    );
}
