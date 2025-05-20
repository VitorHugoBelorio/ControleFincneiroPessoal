import { Routes, Route, Navigate } from "react-router-dom";
import Login from "./pages/Login";

// Categorias
import Categorias from "./pages/categoriasPages/Categorias";
import NovaCategoria from "./pages/categoriasPages/NovaCategoria";
import EditarCategoria from "./pages/categoriasPages/EditarCategoria";
import ExcluirCategoria from "./pages/categoriasPages/ExcluirCategoria";
import VisualizarCategoria from "./pages/categoriasPages/VisualizarCategoria";

// Transações
import NovaTransacao from "./pages/transacaoPages/NovaTransacao";
import EditarTransacao from "./pages/transacaoPages/EditarTransacao";
import ExcluirTransacao from "./pages/transacaoPages/ExcluirTransacao";

const PrivateRoute = ({ children }) => {
    const token = localStorage.getItem("token");
    return token ? children : <Navigate to="/" />;
};

function App() {
    return (
        <Routes>
            {/* Rota pública */}
            <Route path="/" element={<Login />} />

            {/* Rotas protegidas - Categorias */}
            <Route
                path="/categorias"
                element={
                    <PrivateRoute>
                        <Categorias />
                    </PrivateRoute>
                }
            />
            <Route
                path="/categorias/nova"
                element={
                    <PrivateRoute>
                        <NovaCategoria />
                    </PrivateRoute>
                }
            />
            <Route
                path="/categorias/editar/:id"
                element={
                    <PrivateRoute>
                        <EditarCategoria />
                    </PrivateRoute>
                }
            />
            <Route
                path="/categorias/excluir/:id"
                element={
                    <PrivateRoute>
                        <ExcluirCategoria />
                    </PrivateRoute>
                }
            />
            <Route
                path="/categorias/visualizar/:id"
                element={
                    <PrivateRoute>
                        <VisualizarCategoria />
                    </PrivateRoute>
                }
            />

            {/* Rotas protegidas - Transações */}
            <Route
                path="/transacoes/nova"
                element={
                    <PrivateRoute>
                        <NovaTransacao />
                    </PrivateRoute>
                }
            />
            <Route
                path="/transacoes/editar/:id"
                element={
                    <PrivateRoute>
                        <EditarTransacao />
                    </PrivateRoute>
                }
            />
            <Route
                path="/transacoes/excluir/:id"
                element={
                    <PrivateRoute>
                        <ExcluirTransacao />
                    </PrivateRoute>
                }
            />
        </Routes>
    );
}

export default App;
