import 'bootstrap/dist/css/bootstrap.min.css';

function Home() {
    // Dados fictícios — substitua com dados reais vindos de API, contexto ou props
    const categorias = ['Alimentação', 'Transporte', 'Lazer', 'Investimentos'];
    const saldoMesAtual = 2750.50;

    return (
        <div>
            {/* Navbar */}
            <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
                <div className="container-fluid">
                    <a className="navbar-brand" href="#">Controle Financeiro</a>
                    <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="collapse navbar-collapse" id="navbarNav">
                        <ul className="navbar-nav ms-auto">
                            <li className="nav-item">
                                <a className="nav-link" href="#">Criar uma categoria</a>
                            </li>
                            <li className="nav-item">
                                <a className="nav-link" href="#">Criar uma transação</a>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>

            {/* Conteúdo principal */}
            <div className="container mt-5">
                <h1 className="text-center mb-4">Bem-vindo ao seu Controle Financeiro</h1>

                <div className="row">
                    {/* Bloco de Categorias */}
                    <div className="col-md-6 mb-4">
                        <div className="card shadow">
                            <div className="card-header bg-primary text-white">
                                Categorias
                            </div>
                            <ul className="list-group list-group-flush">
                                {categorias.map((categoria, index) => (
                                    <li className="list-group-item" key={index}>{categoria}</li>
                                ))}
                            </ul>
                        </div>
                    </div>

                    {/* Bloco de Saldo do Mês */}
                    <div className="col-md-6 mb-4">
                        <div className="card shadow">
                            <div className="card-header bg-success text-white">
                                Saldo do Mês Atual
                            </div>
                            <div className="card-body">
                                <h3 className="card-title">R$ {saldoMesAtual.toFixed(2)}</h3>
                                <p className="card-text">Este é o seu saldo acumulado em abril de 2025.</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default Home;
