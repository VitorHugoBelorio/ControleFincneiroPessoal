import { useEffect, useState } from "react";
import axios from "axios";

export default function Categorias() {
    const [categorias, setCategorias] = useState([]);
    const [titulo, setTitulo] = useState("");
    const [descricao, setDescricao] = useState("");
    const [editando, setEditando] = useState(null);

    const token = localStorage.getItem("token");

    const headers = {
        Authorization: `Bearer ${token}`,
    };

    useEffect(() => {
        carregarCategorias();
    }, []);

    const carregarCategorias = async () => {
        try {
            const res = await axios.get("http://localhost:5227/v1/categoria", { headers });
            setCategorias(res.data);
        } catch (err) {
            console.error("Erro ao buscar categorias:", err);
        }
    };

    const salvarCategoria = async (e) => {
        e.preventDefault();
        const categoria = { titulo, descricao };

        try {
            if (editando) {
                await axios.put(`http://localhost:5227/v1/categoria/${editando}`, { ...categoria, id: editando }, { headers });
            } else {
                await axios.post("http://localhost:5227/v1/categoria", categoria, { headers });
            }

            setTitulo("");
            setDescricao("");
            setEditando(null);
            carregarCategorias();
        } catch (err) {
            console.error("Erro ao salvar categoria:", err);
            alert("Erro ao salvar categoria.");
        }
    };

    const excluirCategoria = async (id) => {
        if (!window.confirm("Deseja realmente excluir esta categoria?")) return;

        try {
            await axios.delete(`http://localhost:5227/v1/categoria/${id}`, {
                data: { id },
                headers,
            });
            carregarCategorias();
        } catch (err) {
            console.error("Erro ao excluir categoria:", err);
            alert("Erro ao excluir categoria.");
        }
    };

    const iniciarEdicao = (categoria) => {
        setTitulo(categoria.titulo);
        setDescricao(categoria.descricao || "");
        setEditando(categoria.id);
    };

    const cancelarEdicao = () => {
        setTitulo("");
        setDescricao("");
        setEditando(null);
    };

    return (
        <div className="container mt-4">
            <h2>Categorias</h2>
            <form onSubmit={salvarCategoria} className="mb-4">
                <div className="mb-3">
                    <label className="form-label">Título</label>
                    <input
                        type="text"
                        className="form-control"
                        value={titulo}
                        onChange={(e) => setTitulo(e.target.value)}
                        required
                    />
                </div>
                <div className="mb-3">
                    <label className="form-label">Descrição</label>
                    <textarea
                        className="form-control"
                        value={descricao}
                        onChange={(e) => setDescricao(e.target.value)}
                    />
                </div>
                <button type="submit" className="btn btn-success">
                    {editando ? "Atualizar" : "Criar"}
                </button>
                {editando && (
                    <button
                        type="button"
                        className="btn btn-secondary ms-2"
                        onClick={cancelarEdicao}
                    >
                        Cancelar
                    </button>
                )}
            </form>

            <table className="table table-bordered">
                <thead>
                    <tr>
                        <th>Título</th>
                        <th>Descrição</th>
                        <th>Ações</th>
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
                                    onClick={() => iniciarEdicao(cat)}
                                >
                                    Editar
                                </button>
                                <button
                                    className="btn btn-danger btn-sm"
                                    onClick={() => excluirCategoria(cat.id)}
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
