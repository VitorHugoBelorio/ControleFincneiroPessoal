namespace Financeiro.Server.Configuracoes
{
    public class Configuracao
    {
        public const int DefaultStatusCode = 200;
        public const int DefaultPageNumber = 1;
        public const int DefaultPageSize = 25;

        // public static string BackendUrl { get; set; } = "";
        public static string FrontendUrl { get; set; } = "https://localhost:56109";
    }
}
