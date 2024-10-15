using MySql.Data.MySqlClient;
using Projeto_SIT.Models;

namespace Projeto_SIT.Services {
    public class EquipamentoService {
        private readonly Database _database;
        private readonly IConfiguration _configuration;
        public EquipamentoService(Database database, IConfiguration configuration) {
            _database = database;
            _configuration = configuration;
        }

        public List<Equipamento> GetEquip() { 
            var list = new List<Equipamento>();

            using (var connection = _database.CreateConnection()) { 
                connection.Open();

                var query = @"SELECT e.idEquipamento, e.NumeroDeSerie, e.Modelo, e.Marca, e.Tipo, e.Cor, e.DataDeAquisicao,
                               a.nomeArmazenamento, a.descricao, f.nomeFilial, f.UF, f.Cidade
                            FROM 
                                equipamentos e
                            LEFT JOIN 
                                armazenamento a ON e.idArmazenamento = a.idArmazenamento
                            LEFT JOIN 
                                filiais f ON e.idFilial = f.idFilial;";
            
                var command = new MySqlCommand(query, (MySqlConnection)connection);
                var reader = command.ExecuteReader();
                while (reader.Read()) {
                    list.Add(new Equipamento {
                        IdEquipamento = reader.GetInt32("idEquipamento"),
                        NumeroDeSerie = reader.GetInt32("NumeroDeSerie"),
                        Modelo = reader.GetString("Modelo"),
                        Marca = reader.GetString("Marca"),
                        Tipo = reader.GetString("Tipo"),
                        Cor = reader.GetString("Cor"),
                        DataDeAquisicao = reader.GetDateTime("DataDeAquisicao"),
                        Armazenamento = new ArmazenamentoEquip {
                            NomeArmazenamento = reader.GetString("nomeArmazenamento"),
                            Descricao = reader.GetString("descricao")
                        },
                        Filial = new Filial {
                            NomeFilial = reader.GetString("nomeFilial"),
                            UF = reader.GetString("UF"),
                            Cidade = reader.GetString("Cidade")
                        }
                    });
                }
                return list;
            }
        }

        public Equipamento getById(int id) {
            Equipamento equipamento = null;

            using (var connection = _database.CreateConnection()) { 
                connection.Open();

                var query = @"SELECT e.idEquipamento, e.NumeroDeSerie, e.Modelo, e.Marca, e.Tipo, e.Cor, e.DataDeAquisicao,
                               a.nomeArmazenamento, a.descricao, f.nomeFilial, f.UF, f.Cidade
                            FROM 
                                equipamentos e
                            LEFT JOIN 
                                armazenamento a ON e.idArmazenamento = a.idArmazenamento
                            LEFT JOIN 
                                filiais f ON e.idFilial = f.idFilial
                            WHERE e.idEquipamento = @idEquipamento";

                var command = new MySqlCommand(query, (MySqlConnection)connection);
                command.Parameters.AddWithValue("@idEquipamento", id);
                var reader = command.ExecuteReader();
                if (reader.Read()) {
                    equipamento = new Equipamento {
                        IdEquipamento = reader.GetInt32("idEquipamento"),
                        NumeroDeSerie = reader.GetInt32("NumeroDeSerie"),
                        Modelo = reader.GetString("Modelo"),
                        Marca = reader.GetString("Marca"),
                        Tipo = reader.GetString("Tipo"),
                        Cor = reader.GetString("Cor"),
                        DataDeAquisicao = reader.GetDateTime("DataDeAquisicao"),
                        Armazenamento = new ArmazenamentoEquip {
                            NomeArmazenamento = reader.GetString("nomeArmazenamento"),
                            Descricao = reader.GetString("descricao")
                        },
                        Filial = new Filial {
                            NomeFilial = reader.GetString("nomeFilial"),
                            UF = reader.GetString("UF"),
                            Cidade = reader.GetString("Cidade")
                        }
                    };
                }
                return equipamento;
            }
        }

        public bool RegistarEquip(PostEquipamento equip) {
            using (var connection = _database.CreateConnection()) { 
                connection.Open();

                var query = @"INSERT INTO equipamentos (NumeroDeSerie, Modelo, Marca, Tipo, Cor, idArmazenamento, idFilial)
                              VALUES (@NumeroDeSerie, @Modelo, @Marca, @Tipo, @Cor, @idArmazenamento, @idFilial)";

                using(var command = new MySqlCommand(query, (MySqlConnection)connection)) {
                    command.Parameters.AddWithValue("@NumeroDeSerie", equip.NumeroDeSerie);
                    command.Parameters.AddWithValue("@Modelo", equip.Modelo);
                    command.Parameters.AddWithValue("@Marca", equip.Marca);
                    command.Parameters.AddWithValue("@Tipo", equip.Tipo);
                    command.Parameters.AddWithValue("@Cor", equip.Cor);
                    command.Parameters.AddWithValue("@idArmazenamento", equip.idArmazenamento);
                    command.Parameters.AddWithValue("@idFilial", equip.idFilial);
                    
                    var result = command.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }

        public void UpdateEquip(int idEquipamento, int novoNumeroSerie, string novoModelo, string novaMarca, string novoTipo, string novaCor, int novoArmazenamento, int novaFilial) {
            using (var connection = _database.CreateConnection()) {
                connection.Open();

                var query = @"UPDATE equipamentos 
                              SET NumeroDeSerie = @NumeroDeSerie, Modelo = @Modelo, Marca = @Marca, Tipo = @Tipo,
                              Cor = @Cor, idArmazenamento = @idArmazenamento, idFilial = @idFilial
                              WHERE idEquipamento = @idEquipamento";

                using (var command = new MySqlCommand(query, (MySqlConnection)connection)) {
                    command.Parameters.AddWithValue("@NumeroDeSerie", novoNumeroSerie);
                    command.Parameters.AddWithValue("@Modelo", novoModelo);
                    command.Parameters.AddWithValue("@Marca", novaMarca);
                    command.Parameters.AddWithValue("@Tipo", novoTipo);
                    command.Parameters.AddWithValue("@Cor", novaCor);
                    command.Parameters.AddWithValue("@idArmazenamento", novoArmazenamento);
                    command.Parameters.AddWithValue("@idFilial", novaFilial);
                    command.Parameters.AddWithValue("@idEquipamento", idEquipamento);

                    command.ExecuteNonQuery();
                }
            }
        }

        public bool deleteEquip(int id) {
            using (var connection = _database.CreateConnection()) {
                connection.Open();

                var query = "DELETE FROM equipamentos WHERE idEquipamento = @idEquipamento";

                using (var command = new MySqlCommand(query, (MySqlConnection)connection)) {
                    command.Parameters.AddWithValue("@idEquipamento", id);

                    var result = command.ExecuteNonQuery();
                    return result > 0; 
                }
            }
        }
    }
}
