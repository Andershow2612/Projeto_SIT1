namespace Projeto_SIT.Models {
    public class Equipamento {
        public int IdEquipamento { get; set; }
        public int NumeroDeSerie { get; set; }
        public string? Modelo { get; set; }
        public string? Marca { get; set; }
        public string? Tipo { get; set; }
        public string? Cor { get; set; }
        public DateTime? DataDeAquisicao { get; set; }
        public ArmazenamentoEquip? Armazenamento { get; set; }
        public Filial? Filial { get; set; }
    }

    public class ArmazenamentoEquip { 
        public string? NomeArmazenamento {  get; set; }
        public string? Descricao { get; set; }
    }

    public class Filial { 
        public string? NomeFilial { get; set; }
        public string? UF { get; set; }
        public string? Cidade { get; set; }
    }

    public class PostEquipamento {
        public int NumeroDeSerie { get; set; }
        public string? Modelo { get; set; }
        public string? Marca { get; set; }
        public string? Tipo { get; set; }
        public string? Cor { get; set; }
        public int idArmazenamento { get; set; }
        public int idFilial { get; set; }
    }

    public class UpdateEquip {
        public int NumeroDeSerie { get; set; }
        public string? Modelo { get; set; }
        public string? Marca { get; set; }
        public string? Tipo { get; set; }
        public string? Cor { get; set; }
        public int idArmazenamento { get; set; }
        public int idFilial { get; set; }
    }
}
