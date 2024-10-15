namespace Projeto_SIT.Models {
    public class Filiais {
        public int IdFilial { get; set; }
        public string? NomeFilial { get; set; }
        public string? UF { get; set; }
        public string? Cidade { get; set; }
        public string? Rua { get; set; }
        public string? Numero { get; set; }
    }

    public class PostFiliais {
        public string? NomeFilial { get; set; }
        public string? UF { get; set; }
        public string? Cidade { get; set; }
        public string? Rua { get; set; }
        public string? Numero { get; set; }
    }
}
