namespace Projeto_SIT.Models {
    public class Cargosget {
        public int IdCargo { get; set; }
        public string? NomeCargo { get; set; }
    }

    public class CargoNome {
        public int IdCargo { get; set; }
        public string? NomeCargo { get; set; }
        public List<Users>? Users { get; set; }
    }
    public class Users {
        public int idUser { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
    }

    public class PostCargos {
        public string? NomeCargo { get; set; }
    }
}
