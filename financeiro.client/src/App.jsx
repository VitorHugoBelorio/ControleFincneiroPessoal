import { Routes, Route, Navigate } from "react-router-dom";
import Login from "./pages/Login";
import Categorias from "./pages/categoriasPages/Categorias";
import NovaCategoria from "./pages/categoriasPages/NovaCategoria";
import EditarCategoria from "./pages/categoriasPages/EditarCategoria";
import ExcluirCategoria from "./pages/categoriasPages/ExcluirCategoria";

const PrivateRoute = ({ children }) => {
    const token = localStorage.getItem("token");
    return token ? children : <Navigate to="/" />;
};

function App() {
    return (
        <Routes>
            <Route path="/" element={<Login />} />

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
        </Routes>
    );
}

export default App;
