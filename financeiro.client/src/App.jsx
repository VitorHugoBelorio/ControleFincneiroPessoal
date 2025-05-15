import { Routes, Route, Navigate } from "react-router-dom";
import Login from "./pages/Login";
import Categorias from "./pages/Categorias";

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
        </Routes>
    );
}

export default App;
