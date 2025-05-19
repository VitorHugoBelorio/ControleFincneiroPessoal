import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import axios from 'axios';

const VisualizarCategoria = () => {
    const { titulo } = useParams(); // capturando o t�tulo da categoria da URL
    const navigate = useNavigate();
    const [transacoes, setTransacoes] = useState([]);
    const [carregando, setCarregando] = useState(true);
    const [erro, setErro] = useState(null);

    const token = localStorage.getItem("token");
    const headers = token ? { Authorization: `Bearer ${token}` } : {};

    useEffect(() => {
        const buscarTransacoes = async () => {
            if (!token) {
                setErro("Usu�rio n�o autenticado.");
                setCarregando(false);
                return;
            }

            try {
                const response = await axios.get(`https://localhost:7277/v1/transacao/categoria/${titulo}`, {
                    headers
                });

                setTransacoes(response.data);
            } catch (err) {
                if (err.response) {
                    if (err.response.status === 404) {
                        setErro("Nenhuma transa��o encontrada para esta categoria.");
                    } else if (err.response.status === 401) {
                        setErro("Usu�rio n�o autorizado. Fa�a login novamente.");
                    } else {
                        setErro("Erro ao carregar transa��es.");
                    }
                } else {
                    setErro("Erro de conex�o. Verifique sua internet.");
                }
            } finally {
                setCarregando(false);
            }
        };

        buscarTransacoes();
    }, [titulo]);

    const handleCriarTransacao = () => {
        navigate(`/transacao/criar/${titulo}`);
    };

    const handleEditar = (id) => {
        navigate(`/transacao/editar/${titulo}/${id}`);
    };

    const handleExcluir = async (id) => {
        const confirmacao = window.confirm("Tem certeza que deseja excluir esta transa��o?");
        if (!confirmacao) return;

        try {
            await axios.delete(`https://localhost:7277/v1/transacao/${id}`, { headers });
            setTransacoes(transacoes.filter(t => t.id !== id));
        } catch (error) {
            alert("Erro ao excluir transa��o.");
            console.error(error);
        }
    };

    return (
        <div className="container mt-5">
            <div className="d-flex justify-content-between align-items-center mb-4">
                <h2 className="text-primary">Transa��es: {titulo}</h2>
                <button className="btn btn-success" onClick={handleCriarTransacao}>
                    + Nova Transa��o
                </button>
            </div>

            {carregando ? (
                <div className="alert alert-info">Carregando transa��es...</div>
            ) : erro ? (
                <div className="alert alert-danger">{erro}</div>
            ) : (
                <div className="table-responsive">
                    <table className="table table-bordered table-hover">
                        <thead className="table-light">
                            <tr>
                                <th>T�tulo</th>
                                <th>Quantia</th>
                                <th>Data</th>
                                <th>Tipo</th>
                                <th>A��es</th>
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
                                                {transacao.type ? 'Entrada' : 'Sa�da'}
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
                                        Nenhuma transa��o cadastrada para esta categoria.
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
