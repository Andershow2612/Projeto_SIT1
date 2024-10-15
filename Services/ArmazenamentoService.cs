using MySql.Data.MySqlClient;
using Projeto_SIT.Models;

namespace Projeto_SIT.Services {
    public class ArmazenamentoService {
        private readonly Database _database;
        private readonly IConfiguration _configuration;
        public ArmazenamentoService(Database database, IConfiguration configuration) {
            _database = database;
            _configuration = configuration;
        }

        public List<Armazenamento> getArmazenamento() { 
            var lista = new List<Armazenamento>();

            using (var connection = _database.CreateConnection()) { 
                connection.Open();

                var query = @"SELECT a.idArmazenamento, a.nomeArmazenamento, a.descricao, a.capacidadeMaxima, f.nomeFilial
                              FROM armazenamento a
                              JOIN filiais f ON a.idFilial = f.idFilial
                              ORDER BY a.idArmazenamento ASC;";

                var command = new MySqlCommand(query, (MySqlConnection)connection);
                var reader = command.ExecuteReader();
                while (reader.Read()) {
                    lista.Add(new Armazenamento {
                        IdArmazenamento = reader.GetInt32("idArmazenamento"),
                        NomeArmazenamento = reader.GetString("nomeArmazenamento"),
                        Descricao = reader.GetString("descricao"),
                        CapacidadeMaxima = reader.GetInt32("capacidadeMaxima"),
                        Filial = new filialGet {
                            NomeFilial = reader.GetString("nomeFilial")
                        },
                    });
                }
            return lista;
            }
        }

        public Armazenamento? getId(int id) {
            using (var connection = _database.CreateConnection()) {
                connection.Open();

                var query = @"SELECT a.idArmazenamento, a.nomeArmazenamento, a.descricao, a.capacidadeMaxima, f.nomeFilial
                              FROM armazenamento a
                              JOIN filiais f ON a.idFilial = f.idFilial
                              WHERE a.idArmazenamento = @idArmazenamento;";

                var command = new MySqlCommand(query, (MySqlConnection)connection);
                command.Parameters.AddWithValue("@idArmazenamento", id);

                var reader = command.ExecuteReader();
                if (reader.Read()) {
                    var armz = new Armazenamento {
                        IdArmazenamento = reader.GetInt32("idArmazenamento"),
                        NomeArmazenamento = reader.GetString("nomeArmazenamento"),
                        Descricao = reader.GetString("descricao"),
                        CapacidadeMaxima = reader.GetInt32("capacidadeMaxima"),
                        Filial = new filialGet {
                            NomeFilial = reader.GetString("nomeFilial")
                        }
                    };
                    return armz;
                }
                return null;
            }
        }

        public bool Registrar(PostArmazenamento equip) {
            using (var connection = _database.CreateConnection()) {
                connection.Open();

                var query = @"INSERT INTO armazenamento(nomeArmazenamento, descricao, capacidadeMaxima, idFilial)
                              VALUES (@nomeArmazenamento, @descricao, @capacidadeMaxima, @idFilial)";

                using(var command = new MySqlCommand(query, (MySqlConnection)connection)) {
                    command.Parameters.AddWithValue("@nomeArmazenamento", equip.NomeArmazenamento);
                    command.Parameters.AddWithValue("@descricao", equip.Descricao);
                    command.Parameters.AddWithValue("@capacidadeMaxima", equip.CapacidadeMaxima);
                    command.Parameters.AddWithValue("@idFilial", equip.IdFilial);
                    
                    var result = command.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }

        public void UpdateArmz(int idArmz, string novoNome, string novaDescricao, int novaCapacidade, int novaFilial) {
            using (var connection = _database.CreateConnection()) { 
                connection.Open();

                var query = @"UPDATE armazenamento
                              SET  nomeArmazenamento = @nomeArmazenamento, descricao = @descricao, capacidadeMaxima = @capacidadeMaxima, idFilial = @idFilial
                              WHERE idArmazenamento = @idArmazenamento";

                using (var command = new MySqlCommand(query, (MySqlConnection)connection)) {
                    command.Parameters.AddWithValue("@nomeArmazenamento", novoNome);
                    command.Parameters.AddWithValue("@descricao", novaDescricao);
                    command.Parameters.AddWithValue("@capacidadeMaxima", novaCapacidade);
                    command.Parameters.AddWithValue("@idFilial", novaFilial);
                    command.Parameters.AddWithValue("@idArmazenamento", idArmz);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0) {
                        throw new Exception("Nenhum usuário foi atualizado.");
                    }
                }
            }
        }

        public bool DeleteArmz(int id) {
            using(var connection = _database.CreateConnection()) {
                connection.Open();

                var query = @"DELETE FROM armazenamento
                              WHERE idArmazenamento = @idArmazenamento";

                using (var command = new MySqlCommand(query, (MySqlConnection)connection)) {
                    command.Parameters.AddWithValue("@idArmazenamento", id);

                    var result = command.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }

    }
}
