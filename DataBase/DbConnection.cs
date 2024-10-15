using System.Data;
using MySql.Data.MySqlClient;

public class Database {
    private readonly string _connectionString;

    public Database(IConfiguration configuration) {
        // Garante que a string de conexão não será nula, e lança uma exceção caso seja.
        _connectionString = configuration.GetConnectionString("DefaultConnection")
                            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
    }

    public IDbConnection CreateConnection() {
        return new MySqlConnection(_connectionString);
    }
}
