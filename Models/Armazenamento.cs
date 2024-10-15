namespace Projeto_SIT.Models {
    public class Armazenamento {
        public int IdArmazenamento { get; set; }
        public string? NomeArmazenamento { get; set; }
        public string? Descricao { get; set; }
        public int CapacidadeMaxima { get; set; }
        public filialGet? Filial { get; set; }
    }
    public class filialGet {
        public string? NomeFilial { get; set; }
    }

    public class PostArmazenamento {
        public string? NomeArmazenamento { get; set; }
        public string? Descricao { get; set; }
        public int CapacidadeMaxima { get; set; }
        public int IdFilial { get; set; }
    }

    public class UpdateArmz {
        public int IdArmazenamento { get; set; }
        public string? NomeArmazenamento { get; set; }
        public string? Descricao { get; set; }
        public int CapacidadeMaxima { get; set; }
        public int IdFilial { get; set; }
    }
}
