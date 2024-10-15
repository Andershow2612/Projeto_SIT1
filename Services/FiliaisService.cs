using MySql.Data.MySqlClient;
using Projeto_SIT.Models;

namespace Projeto_SIT.Services {
    public class FiliaisService {
        private readonly Database _database;
        private readonly IConfiguration _configuration;
        public FiliaisService(Database database, IConfiguration configuration) {
            _database = database;
            _configuration = configuration;
        }

        public List<Filiais> getAll() { 
            var lista = new List<Filiais>();

            using (var connection = _database.CreateConnection()) { 
                connection.Open();

                var query = "SELECT * FROM filiais";

                var command = new MySqlCommand(query, (MySqlConnection)connection);
                var reader = command.ExecuteReader();
                while (reader.Read()) {
                    lista.Add(new Filiais {
                        IdFilial = reader.GetInt32("idFilial"),
                        NomeFilial = reader.GetString("nomeFilial"),
                        UF = reader.GetString("UF"),
                        Cidade = reader.GetString("Cidade"),
                        Rua = reader.GetString("Rua"),
                        Numero = reader.GetString("Numero")
                    });
                }
                return lista;
            }
        }

        public Filiais getId(int id) {
            using (var connection = _database.CreateConnection()) {
                connection.Open();

                var query = "SELECT idFilial, nomeFilial, UF, Cidade, Rua, Numero FROM filiais WHERE idFilial = @idFilial;";

                using (var command = new MySqlCommand(query, (MySqlConnection)connection)) {
                    command.Parameters.AddWithValue("@idFilial", id);

                    using (var reader = command.ExecuteReader()) {
                        if (reader.Read()) {
                            var filiais = new Filiais {
                                IdFilial = reader.GetInt32("idFilial"),
                                NomeFilial = reader.GetString("NomeFilial"),
                                UF = reader.GetString("UF"),
                                Cidade = reader.GetString("Cidade"),
                                Rua = reader.GetString("Rua"),
                                Numero = reader.GetString("Numero"),
                            };
                            return filiais;
                        }
                        return null;
                    }
                }
            }
        }

        public bool registar(PostFiliais filial) {
            using (var connection = _database.CreateConnection()) {
                connection.Open();

                var query = @"INSERT INTO filiais(nomeFilial, UF, Cidade, Rua, numero) 
                                 VALUES (@nomeFilial, @UF, @Cidade, @Rua, @numero)";

                using (var command = new MySqlCommand(query, (MySqlConnection)connection)) {
                    command.Parameters.AddWithValue("@nomeFilial", filial.NomeFilial);
                    command.Parameters.AddWithValue("@UF", filial.UF);
                    command.Parameters.AddWithValue("@Cidade", filial.Cidade);
                    command.Parameters.AddWithValue("@Rua", filial.Rua);
                    command.Parameters.AddWithValue("@numero", filial.Numero);

                    var result = command.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }

        public void UpdateFilial(int idFilial, string novoNomeFilial, string novoUF, string novaCidade, string novaRua, string novoNumero) {
            using (var connection = _database.CreateConnection()) {
                connection.Open();

                var query = @"UPDATE filiais 
                      SET nomeFilial = @nomeFilial, UF = @UF, Cidade = @Cidade, Rua = @Rua, Numero = @Numero
                      WHERE idFilial = @idFilial";

                using (var command = new MySqlCommand(query, (MySqlConnection)connection)) {
                    command.Parameters.AddWithValue("@idFilial", idFilial);
                    command.Parameters.AddWithValue("@nomeFilial", novoNomeFilial);
                    command.Parameters.AddWithValue("@UF", novoUF);
                    command.Parameters.AddWithValue("@Cidade", novaCidade);
                    command.Parameters.AddWithValue("@Rua", novaRua);
                    command.Parameters.AddWithValue("@Numero", novoNumero);

                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
        }

        public bool deletar(int idFilial) {
            using(var connection = _database.CreateConnection()) {
                connection.Open();

                var query = "DELETE FROM filiais WHERE idFilial = @idFilial";

                using (var command = new MySqlCommand(query, (MySqlConnection)connection)) {
                    command.Parameters.AddWithValue("@idFilial", idFilial);

                    int result = command.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }
    }
}
