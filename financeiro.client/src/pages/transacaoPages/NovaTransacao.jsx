import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useNavigate, useSearchParams } from 'react-router-dom';

const NovaTransacao = () => {
    const [categorias, setCategorias] = useState([]);
    const [form, setForm] = useState({
        titulo: "",
        quantia: "",
        type: true,
        pagoOuRecebidoEm: "",
        categoriaId: ""
    });
    const [searchParams] = useSearchParams();
    const categoriaIdParam = searchParams.get("categoriaId");
    const navigate = useNavigate();

    const token = localStorage.getItem("token");
    const headers = token ? { Authorization: `Bearer ${token}` } : {};

    useEffect(() => {
        const buscarCategorias = async () => {
            try {
                const res = await axios.get("https://localhost:7277/v1/categoria", { headers });
                setCategorias(res.data);

                if (categoriaIdParam) {
                    setForm(prev => ({ ...prev, categoriaId: categoriaIdParam }));
                }
            } catch (err) {
                console.error("Erro ao buscar categorias:", err);
            }
        };

        buscarCategorias();
    }, []);

    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;
        setForm({
            ...form,
            [name]: type === "checkbox" ? checked : value,
        });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        // Prepara o payload no formato que o backend espera
        /*
        const payload = {
            titulo: form.titulo.trim(),
            quantia: parseFloat(form.quantia),
            type: form.type,
            pagoOuRecebidoEm: form.pagoOuRecebidoEm
                ? new Date(form.pagoOuRecebidoEm).toISOString()
                : null,
            categoriaId: parseInt(form.categoriaId)
        };
        */
        try {
            await axios.post(`https://localhost:7277/v1/transacao/categoria/id/${form.categoriaId}`, form, { headers });
            alert("Transação criada com sucesso!");
            navigate(`/categorias/visualizar/${form.categoriaId}`);
        } catch (err) {
            console.error("Erro ao criar transação:", err);
            alert("Erro ao criar transação.");
        }
    };

    return (
        <div className="container mt-4">
            <h2>Nova Transação</h2>
            <form onSubmit={handleSubmit}>
                <div className="mb-3">
                    <label className="form-label">Título</label>
                    <input type="text" className="form-control" name="titulo" value={form.titulo} onChange={handleChange} required />
                </div>
                <div className="mb-3">
                    <label className="form-label">Quantia</label>
                    <input type="number" className="form-control" name="quantia" value={form.quantia} onChange={handleChange} required />
                </div>
                <div className="mb-3">
                    <label className="form-label">Data</label>
                    <input type="date" className="form-control" name="pagoOuRecebidoEm" value={form.pagoOuRecebidoEm} onChange={handleChange} required />
                </div>
                <div className="mb-3">
                    <label className="form-label">Categoria</label>
                    <select className="form-select" name="categoriaId" value={form.categoriaId} onChange={handleChange} required>
                        <option value="">Selecione uma categoria</option>
                        {categorias?.map(c => (
                            <option key={c.id} value={c.id}>{c.titulo}</option>
                        ))}
                    </select>
                </div>
                <div className="mb-3 form-check">
                    <input type="checkbox" className="form-check-input" name="type" checked={form.type} onChange={handleChange} />
                    <label className="form-check-label">Entrada?</label>
                </div>
                <button type="submit" className="btn btn-primary">Salvar</button>
            </form>
        </div>
    );
};

export default NovaTransacao;
