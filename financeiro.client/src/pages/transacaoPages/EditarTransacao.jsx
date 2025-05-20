import { useState, useEffect } from 'react';

export default function EditarTransacao({ transacao, categorias, onSubmit }) {
    const [form, setForm] = useState({ ...transacao });

    useEffect(() => {
        if (transacao) {
            setForm({ ...transacao });
        }
    }, [transacao]);

    function handleChange(e) {
        const { name, value, type } = e.target;
        setForm({ ...form, [name]: type === 'checkbox' ? e.target.checked : value });
    }

    function handleSubmit(e) {
        e.preventDefault();
        onSubmit(form);
    }

    return (
        <div className="p-4 max-w-md mx-auto">
            <h2 className="text-xl font-bold mb-4">Editar Transação</h2>
            <form onSubmit={handleSubmit} className="space-y-4">
                <input name="titulo" value={form.titulo} onChange={handleChange} required className="w-full p-2 border rounded" />
                <input name="quantia" type="number" step="0.01" value={form.quantia} onChange={handleChange} required className="w-full p-2 border rounded" />
                <input name="pagoOuRecebidoEm" type="date" value={form.pagoOuRecebidoEm?.slice(0, 10) || ''} onChange={handleChange} className="w-full p-2 border rounded" />
                <label className="flex items-center gap-2">
                    <input name="type" type="checkbox" checked={form.type} onChange={handleChange} />
                    É Receita?
                </label>
                <select name="categoriaId" value={form.categoriaId} onChange={handleChange} className="w-full p-2 border rounded">
                    {categorias.map(c => <option key={c.id} value={c.id}>{c.nome}</option>)}
                </select>
                <button type="submit" className="bg-blue-600 text-white px-4 py-2 rounded">Salvar Alterações</button>
            </form>
        </div>
    );
}
