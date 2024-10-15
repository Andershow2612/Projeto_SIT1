using MySql.Data.MySqlClient;
using Projeto_SIT.Models;
using System.Data;

namespace Projeto_SIT.Services {
    public class UserService {
        private readonly Database _database;
        private readonly IConfiguration _configuration;
        public UserService(Database database, IConfiguration configuration) {
            _database = database;
            _configuration = configuration;
        }

        public List<User> GetUsers() {
            var usersList = new List<User>();

            using (var connection = _database.CreateConnection()) {
                connection.Open();

                var query = @"SELECT u.idUser, u.userName, u.email, u.telefone, u.password, u.cidade, u.data_criacao, 
                              f.nomeFilial, f.UF, f.Cidade, c.nomeCargo
                              FROM usuarios u
                              JOIN filiais f ON u.idFilial = f.idFilial
                              JOIN cargos c ON u.idCargo = c.idCargo
                              ORDER BY u.idUser ASC;";

                using (var command = new MySqlCommand(query, (MySqlConnection)connection)) {
                    using (var reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            var user = new User {
                                IdUser = reader.GetInt32("idUser"),
                                UserName = reader.GetString("userName"),
                                Email = reader.GetString("email"),
                                Telefone = reader.GetString("telefone"),
                                Password = reader.GetString("password"),
                                Cidade = reader.GetString("cidade"),
                                DataCriacao = reader.GetDateTime("data_criacao"),
                                Filial = new GetFilial {
                                    NomeFilial = reader.GetString("nomeFilial"),
                                    Uf = reader.GetString("UF"),
                                    Cidade = reader.GetString("Cidade")
                                },
                                Cargo = new Cargo {
                                    NomeCargo = reader.GetString("nomeCargo")
                                }
                            };
                            usersList.Add(user);
                        }
                    }
                }
            }
            return usersList;
        }

        public List<User> GetUsersEquipamentos() {
            var usersList = new List<User>();

            using (var connection = _database.CreateConnection()) {
                connection.Open();

                var query = @"SELECT u.idUser, u.userName, u.email, u.telefone, u.password, u.cidade, u.data_criacao, 
                              f.nomeFilial, f.UF, f.Cidade, 
                              e.idEquipamento, e.NumeroDeSerie, e.Modelo, e.Marca, e.Tipo, e.Cor, e.DataDeAquisicao,
                              c.nomeCargo
                              FROM usuarios u
                              JOIN filiais f ON u.idFilial = f.idFilial
                              LEFT JOIN equipamentos e ON u.idEquipamento = e.idEquipamento
                              JOIN cargos c ON u.idCargo = c.idCargo
                              ORDER BY u.idUser ASC;";

                using (var command = new MySqlCommand(query, (MySqlConnection)connection)) {
                    using (var reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            var user = new User {
                                IdUser = reader.GetInt32("idUser"),
                                UserName = reader.GetString("userName"),
                                Email = reader.GetString("email"),
                                Telefone = reader.GetString("telefone"),
                                Password = reader.GetString("password"),
                                Cidade = reader.GetString("cidade"),
                                DataCriacao = reader.GetDateTime("data_criacao"),
                                Filial = new GetFilial {
                                    NomeFilial = reader.GetString("nomeFilial"),
                                    Uf = reader.GetString("UF"),
                                    Cidade = reader.GetString("Cidade")
                                },
                                Equipamento = new EquipamentoGet {
                                    IdEquipamento = reader.GetInt32("idEquipamento"),
                                    NumeroDeSerie = reader.GetInt32("NumeroDeSerie"),
                                    Modelo = reader.GetString("Modelo"),
                                    Marca = reader.GetString("Marca"),
                                    Tipo = reader.GetString("Tipo"),
                                    Cor = reader.GetString("Cor"),
                                    DataDeAquisicao = reader.IsDBNull("DataDeAquisicao") ? (DateTime?)null : reader.GetDateTime("DataDeAquisicao")
                                },
                                Cargo = new Cargo {
                                    NomeCargo = reader.GetString("nomeCargo")
                                }
                            };
                            usersList.Add(user);
                        }
                    }
                }
            }
            return usersList;
        }

        //arrumar esse serviço.
        public CargoUser GetUserCargo(int id) { 
            using (var connection = _database.CreateConnection()) { 
                connection.Open();

                var query = @"SELECT u.idUser, u.userName, f.nomeCargo
                              FROM usuarios u
                              JOIN cargos f ON u.idCargo = f.idCargo
                              WHERE u.idUser = @idUser;";

                using (var command = new MySqlCommand(query, (MySqlConnection)connection)) { 
                    command.Parameters.AddWithValue("@idUser", id);

                    var reader = command.ExecuteReader();
                    if (reader.Read()) {
                        var users = new CargoUser {
                            IdUser = reader.GetInt32("idUser"),
                            UserName = reader.GetString("userName"),
                            Cargo = new Cargo {
                                NomeCargo = reader.GetString("nomeCargo")
                            }
                        };
                        return users;
                    }
                }
                return null;
            }
        }
        
        public User GetUserId(int idUser) {
            using (var connection = _database.CreateConnection()) {
                connection.Open();

                var query = @"SELECT u.idUser, u.userName, u.email, u.telefone, u.password, u.cidade, u.data_criacao, 
                              f.nomeFilial, f.UF, f.Cidade, 
                              e.idEquipamento, e.NumeroDeSerie, e.Modelo, e.Marca, e.Tipo, e.Cor, e.DataDeAquisicao,
                              c.idCargo, c.nomeCargo
                              FROM usuarios u
                              JOIN filiais f ON u.idFilial = f.idFilial
                              LEFT JOIN equipamentos e ON u.idEquipamento = e.idEquipamento
                              JOIN cargos c ON u.idCargo = c.idCargo
                              WHERE u.idUser = @idUser;";

                using (var command = new MySqlCommand(query, (MySqlConnection)connection)) {
                    command.Parameters.AddWithValue("@idUser", idUser);

                    using (var reader = command.ExecuteReader()) {
                        if (reader.Read()) { 
                            var users = new User{
                                IdUser = reader.GetInt32("idUser"),
                                UserName = reader.GetString("userName"),
                                Email = reader.GetString("email"),
                                Telefone = reader.GetString("telefone"),
                                Password = reader.GetString("password"),
                                Cidade = reader.GetString("cidade"),
                                DataCriacao = reader.GetDateTime("data_criacao"),
                                Filial = new GetFilial {
                                    NomeFilial = reader.GetString("nomeFilial"),
                                    Uf = reader.GetString("UF"),
                                    Cidade = reader.GetString("Cidade")
                                },
                                Equipamento = new EquipamentoGet {
                                    IdEquipamento = reader.GetInt32("idEquipamento"),
                                    NumeroDeSerie = reader.GetInt32("NumeroDeSerie"),
                                    Modelo = reader.GetString("Modelo"),
                                    Marca = reader.GetString("Marca"),
                                    Tipo = reader.GetString("Tipo"),
                                    Cor = reader.GetString("Cor"),
                                    DataDeAquisicao = reader.IsDBNull("DataDeAquisicao") ? (DateTime?)null : reader.GetDateTime("DataDeAquisicao")
                                },
                                Cargo = new Cargo {
                                    NomeCargo = reader.GetString("nomeCargo")
                                }
                            };
                            return users;
                        }
                        return null;
                    }
                }
            }
        }

        public bool Registrar(PostUser user) { 
            try {
                using (var connection = _database.CreateConnection()) {
                    connection.Open();

                    var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

                    var query = @"INSERT INTO usuarios (userName, email, telefone, password, cidade, idFilial, idEquipamento, idCargo)
                                  VALUES (@userName, @email, @telefone, @password, @cidade, @idFilial, @idEquipamento, @idCargo)";

                    using (var command = new MySqlCommand(query, (MySqlConnection)connection)) {
                        command.Parameters.AddWithValue("@userName", user.UserName);
                        command.Parameters.AddWithValue("@email", user.Email);
                        command.Parameters.AddWithValue("@telefone", user.Telefone);
                        command.Parameters.AddWithValue("@password", hashedPassword);
                        command.Parameters.AddWithValue("@cidade", user.Cidade);
                        command.Parameters.AddWithValue("@idFilial", user.IdFilial);
                        command.Parameters.AddWithValue("@idEquipamento", user.IdEquipamento);
                        command.Parameters.AddWithValue("@idCargo", user.idCargo);

                        var result = command.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }catch (MySqlException ex) {
                if (ex.Number == 1062) {
                    // Código 1062 = chave duplicada
                    throw new Exception("Esse usuário já existe!");
                }
                throw;
            }
        }

        public void UpdateUser(int idUser, string novoUserName, string novoEmail, string novoTelefone, string novoPassword, string novaCidade, int novoSetor) {
            using (var connection = _database.CreateConnection()) {
                connection.Open();

                // Verificação de duplicidade de nome
                var checkQuery = @"SELECT COUNT(*) 
                                   FROM usuarios 
                                   WHERE userName = @userName AND idUser != @idUser";

                using (var checkCommand = new MySqlCommand(checkQuery, (MySqlConnection)connection)) {
                    checkCommand.Parameters.AddWithValue("@userName", novoUserName);
                    checkCommand.Parameters.AddWithValue("@idUser", idUser);

                    int userExists = Convert.ToInt32(checkCommand.ExecuteScalar());
                    if (userExists > 0) {
                        throw new Exception("Já existe um usuário com esse nome.");
                    }
                }

                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(novoPassword);

                var query = @"UPDATE usuarios
                              SET userName = @userName, email = @email, telefone = @telefone, password = @password, cidade = @cidade, idFilial = @idFilial
                              WHERE idUser = @idUser";

                using (var command = new MySqlCommand(query, (MySqlConnection)connection)) {
                    command.Parameters.AddWithValue("@userName", novoUserName);
                    command.Parameters.AddWithValue("@email", novoEmail);
                    command.Parameters.AddWithValue("@telefone", novoTelefone);
                    command.Parameters.AddWithValue("@password", hashedPassword);
                    command.Parameters.AddWithValue("@cidade", novaCidade);
                    command.Parameters.AddWithValue("@idFilial", novoSetor);
                    command.Parameters.AddWithValue("@idUser", idUser);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0) {
                        throw new Exception("Nenhum usuário foi atualizado.");
                    }
                }
            }
        }

        public bool Delete(int idUser) {
            using (var connection = _database.CreateConnection()) {
                connection.Open();

                var query = "DELETE FROM usuarios WHERE idUser = @idUser;";

                using (var command = new MySqlCommand(query, (MySqlConnection)connection)) {
                    command.Parameters.AddWithValue("@idUser", idUser);

                    int result = command.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }
    }
}