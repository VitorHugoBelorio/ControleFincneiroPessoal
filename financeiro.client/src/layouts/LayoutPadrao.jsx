// src/layouts/LayoutPadrao.jsx
import { Outlet, Link } from 'react-router-dom';

export default function LayoutPadrao() {
    return (
        <div>
            <nav className="navbar navbar-expand navbar-dark bg-dark px-4">
                <Link to="/categorias" className="navbar-brand">Financeiro</Link>
                <div className="ml-auto">
                    <button
                        className="btn btn-outline-light"
                        onClick={() => {
                            localStorage.removeItem('token');
                            window.location.href = '/login';
                        }}
                    >
                        Sair
                    </button>
                </div>
            </nav>

            <div className="container mt-4">
                <Outlet />
            </div>
        </div>
    );
}
