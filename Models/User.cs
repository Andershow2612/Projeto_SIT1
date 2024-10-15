using System.ComponentModel.DataAnnotations;

namespace Projeto_SIT.Models {
    public class User {
        public int IdUser { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public string? Password { get; set; }
        public string? Cidade { get; set; }
        public DateTime DataCriacao { get; set; }
        public GetFilial? Filial { get; set; }
        public EquipamentoGet? Equipamento { get; set; }
        public Cargo? Cargo { get; set; }
    }

    public class GetFilial {
        public string? NomeFilial { get; set; }
        public string? Uf { get; set; }
        public string? Cidade { get; set; }
    }

    public class EquipamentoGet {
        public int? IdEquipamento { get; set; }
        public int? NumeroDeSerie { get; set; }
        public string? Modelo { get; set; }
        public string? Marca { get; set; }
        public string? Tipo { get; set; }
        public string? Cor { get; set; }
        public DateTime? DataDeAquisicao { get; set; }
    }

    public class Cargo {
        public string? NomeCargo { get; set; }
    }

    public class PostUser{
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public string? Password { get; set; }
        public string? Cidade { get; set; }
        public int IdFilial { get; set; }
        public int IdEquipamento { get; set; }
        public int idCargo { get; set; }
    }

    public class UserLogin1 {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }
    }

    public class UserResponse {
        public int IdUser { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public string? Cidade { get; set; }
        public DateTime DataCriacao { get; set; }
        public string? Token { get; set; }
    }

    public class UserUpdate {
        public int IdUser { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public string? Password { get; set; }
        public string? Cidade { get; set; }
        public int IdFilial { get; set; }
    }

    public class CargoUser { 
        public int IdUser { get; set; }
        public string? UserName { get; set; }
        public Cargo? Cargo { get; set; }
    }
}
