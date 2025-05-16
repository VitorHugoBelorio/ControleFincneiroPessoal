import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";

export default function Categorias() {
    const [categorias, setCategorias] = useState([]);
    const navigate = useNavigate();

    const token = localStorage.getItem("token");
    const headers = { Authorization: `Bearer ${token}` };

    useEffect(() => {
        carregarCategorias();
    }, []);

    const carregarCategorias = async () => {
        try {
            const res = await axios.get("https://localhost:7277/v1/categoria", { headers });
            setCategorias(res.data);
        } catch (err) {
            console.error("Erro ao buscar categorias:", err);
        }
    };

    return (
        <div className="container mt-4">
            <h2>Categorias</h2>
            <button className="btn btn-success mb-3" onClick={() => navigate("/categorias/nova")}>
                Nova Categoria
            </button>

            <table className="table table-bordered">
                <thead>
                    <tr>
                        <th>Titulo</th>
                        <th>Descricao</th>
                        <th>Acoes</th>
                    </tr>
                </thead>
                <tbody>
                    {categorias.map((cat) => (
                        <tr key={cat.id}>
                            <td>{cat.titulo}</td>
                            <td>{cat.descricao}</td>
                            <td>
                                <button
                                    className="btn btn-primary btn-sm me-2"
                                    onClick={() => navigate(`/categorias/editar/${cat.id}`)}
                                >
                                    Editar
                                </button>
                                <button
                                    className="btn btn-danger btn-sm"
                                    onClick={() => navigate(`/categorias/excluir/${cat.id}`)}
                                >
                                    Excluir
                                </button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}
