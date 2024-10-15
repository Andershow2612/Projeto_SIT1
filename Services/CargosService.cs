using MySql.Data.MySqlClient;
using Projeto_SIT.Models;

namespace Projeto_SIT.Services {
    public class CargosService {
        private readonly Database _database;
        private readonly IConfiguration _configuration;
        public CargosService(Database database, IConfiguration configuration) {
            _database = database;
            _configuration = configuration;
        }

        public List<Cargosget> GetAll() {
            var list = new List<Cargosget>();
            using (var connection = _database.CreateConnection()) { 
                connection.Open();

                var query = "SELECT * FROM cargos";

                using (var command = new MySqlCommand(query, (MySqlConnection)connection)) {
                    var reader = command.ExecuteReader();
                    while (reader.Read()) {
                        list.Add(new Cargosget {
                            IdCargo = reader.GetInt32("idCargo"),
                            NomeCargo = reader.GetString("nomeCargo")
                        });
                    }
                    return list;
                }
            }
        }

        public List<CargoNome> GetCargoUser(string nomeCargo) { 
            var list = new List<CargoNome>();
            using (var connection = _database.CreateConnection()) {
                connection.Open();

                var query = @"SELECT u.idUser, u.userName, u.email
                              FROM usuarios u
                              INNER JOIN cargos c ON u.idCargo = c.idCargo
                              WHERE c.nomeCargo = @nomeCargo";

                var command = new MySqlCommand(query, (MySqlConnection)connection);
                command.Parameters.AddWithValue("@nomeCargo", nomeCargo);

                using (var reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        var users = new Users {
                            idUser = reader.GetInt32("idUser"),
                            UserName = reader.GetString("userName"),
                            Email = reader.GetString("email")
                        };
                        var cargo = new CargoNome {
                            NomeCargo = nomeCargo,
                            Users = new List<Users> { users }
                        };
                        list.Add(cargo);
                    }
                }
            }
            return list;
        }

        public bool register(PostCargos cargos) {
            using (var connection = _database.CreateConnection()) {
                connection.Open();

                var query = "INSERT INTO cargos(nomeCargo) VALUES(@nomeCargo)";

                using (var command = new MySqlCommand(query, (MySqlConnection)connection)) {
                    command.Parameters.AddWithValue("@nomeCargo", cargos.NomeCargo);

                    var result = command.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }

        public bool Delete(int id) {
            using (var connection = _database.CreateConnection()) {
                connection.Open();

                var query = "DELETE FROM cargos WHERE idCargo = @idCargo;";

                using (var command = new MySqlCommand(query, (MySqlConnection)connection)) {
                    command.Parameters.AddWithValue("@idCargo", id);

                    var result = command.ExecuteNonQuery();
                    return result > 0;
                }
            }
        } 
    }
}
