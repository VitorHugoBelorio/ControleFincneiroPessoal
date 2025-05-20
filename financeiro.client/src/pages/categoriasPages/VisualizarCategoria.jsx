import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import axios from 'axios';

const VisualizarCategoria = () => {
    const { id } = useParams(); // agora corretamente pegando o ID da categoria
    const navigate = useNavigate();
    const [categoria, setCategoria] = useState(null);
    const [transacoes, setTransacoes] = useState([]);
    const [carregando, setCarregando] = useState(true);
    const [erro, setErro] = useState(null);

    const token = localStorage.getItem("token");
    const headers = token ? { Authorization: `Bearer ${token}` } : {};

    useEffect(() => {
        const buscarDados = async () => {
            if (!token) {
                setErro("Usuário não autenticado.");
                setCarregando(false);
                return;
            }

            try {
                // Buscar categoria pelo ID
                const categoriaResp = await axios.get(`https://localhost:7277/v1/categoria/${id}`, { headers });
                setCategoria(categoriaResp.data);

                // Buscar transações associadas à categoria
                const transacoesResp = await axios.get(`https://localhost:7277/v1/transacao/categoria/${id}`, { headers });
                setTransacoes(transacoesResp.data);
            } catch (err) {
                if (err.response) {
                    if (err.response.status === 404) {
                        setErro("Categoria ou transações não encontradas.");
                    } else if (err.response.status === 401) {
                        setErro("Usuário não autorizado. Faça login novamente.");
                    } else {
                        setErro("Erro ao carregar dados.");
                    }
                } else {
                    setErro("Erro de conexão. Verifique sua internet.");
                }
            } finally {
                setCarregando(false);
            }
        };

        buscarDados();
    }, [id]);

    const handleCriarTransacao = () => {
        navigate(`/transacoes/nova?categoriaId=${id}`);
    };

    const handleEditar = (transacaoId) => {
        navigate(`/transacoes/editar/${transacaoId}`);
    };

    const handleExcluir = async (transacaoId) => {
        const confirmacao = window.confirm("Tem certeza que deseja excluir esta transação?");
        if (!confirmacao) return;

        try {
            await axios.delete(`https://localhost:7277/v1/transacao/${transacaoId}`, { headers });
            setTransacoes(transacoes.filter(t => t.id !== transacaoId));
        } catch (error) {
            alert("Erro ao excluir transação.");
            console.error(error);
        }
    };

    return (
        <div className="container mt-5">
            <div className="d-flex justify-content-between align-items-center mb-4">
                <h2 className="text-primary">
                    Transações: {categoria ? categoria.titulo : "Carregando..."}
                </h2>
                <button className="btn btn-success" onClick={handleCriarTransacao}>
                    + Nova Transação
                </button>
            </div>

            {carregando ? (
                <div className="alert alert-info">Carregando dados...</div>
            ) : erro ? (
                <div className="alert alert-danger">{erro}</div>
            ) : (
                <div className="table-responsive">
                    <table className="table table-bordered table-hover">
                        <thead className="table-light">
                            <tr>
                                <th>Título</th>
                                <th>Quantia</th>
                                <th>Data</th>
                                <th>Tipo</th>
                                <th>Ações</th>
                            </tr>
                        </thead>
                        <tbody>
                            {transacoes.length > 0 ? (
                                transacoes.map(transacao => (
                                    <tr key={transacao.id}>
                                        <td>{transacao.titulo}</td>
                                        <td>R$ {transacao.quantia.toFixed(2)}</td>
                                        <td>{new Date(transacao.pagoOuRecebidoEm).toLocaleDateString()}</td>
                                        <td>
                                            <span className={`badge ${transacao.type ? 'bg-success' : 'bg-danger'}`}>
                                                {transacao.type ? 'Entrada' : 'Saída'}
                                            </span>
                                        </td>
                                        <td>
                                            <button
                                                className="btn btn-warning btn-sm me-2"
                                                onClick={() => handleEditar(transacao.id)}
                                            >
                                                Editar
                                            </button>
                                            <button
                                                className="btn btn-danger btn-sm"
                                                onClick={() => handleExcluir(transacao.id)}
                                            >
                                                Excluir
                                            </button>
                                        </td>
                                    </tr>
                                ))
                            ) : (
                                <tr>
                                    <td colSpan="5" className="text-center">
                                        Nenhuma transação cadastrada para esta categoria.
                                    </td>
                                </tr>
                            )}
                        </tbody>
                    </table>
                </div>
            )}
        </div>
    );
};

export default VisualizarCategoria;
