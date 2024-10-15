using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using Projeto_SIT.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Projeto_SIT.Services {
    public class AuthService {
        private readonly Database _database;
        private readonly IConfiguration _configuration;
        public AuthService(Database database, IConfiguration configuration) {
            _database = database;
            _configuration = configuration;
        }
        public UserResponse? Login(UserLogin1 login) {
            using (var connection = _database.CreateConnection()) {
                connection.Open();

                var query = "SELECT * FROM usuarios WHERE Username = @userName";
                using (var command = new MySqlCommand(query, (MySqlConnection)connection)) {
                    command.Parameters.AddWithValue("@Username", login.UserName);

                    using (var reader = command.ExecuteReader()) {
                        if (!reader.Read()) {
                            return null; // Usuário não encontrado
                        }

                        var user = new User {
                            IdUser = reader.GetInt32("idUser"),
                            UserName = reader.GetString("Username"),
                            Password = reader.GetString("Password"), // A senha hasheada
                            Email = reader.GetString("email"),
                            Telefone = reader.GetString("telefone"),
                            Cidade = reader.GetString("cidade"),
                        };

                        reader.Close();

                        // Verifica se a senha fornecida corresponde à senha hasheada armazenada
                        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(login.Password, user.Password);
                        if (!isPasswordValid) {
                            return null; // Senha inválida
                        }

                        // Gera o token JWT
                        var token = GenerateJwtToken(user);

                        // Retorna o objeto UserResponse com os dados do usuário e o token
                        var userResponse = new UserResponse {
                            IdUser = user.IdUser,
                            UserName = user.UserName,
                            Email = user.Email,
                            Telefone = user.Telefone,
                            Cidade = user.Cidade,
                            DataCriacao = user.DataCriacao,
                            Token = token // Token gerado
                        };

                        return userResponse;
                    }
                }
            }
        }

        public string GenerateJwtToken(User user) {
            // Validação de que a chave JWT e o emissor não são nulos
            var jwtKey = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key is not configured.");
            var jwtIssuer = _configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("JWT Issuer is not configured.");

            // Verificar o nome user é nulo
            var userName = user.UserName ?? throw new InvalidOperationException("UserName cannot be null.");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtKey);  // Agora temos certeza que jwtKey não é nulo
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, userName) }), // Garantindo que userName não é nulo
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = jwtIssuer,
                Audience = jwtIssuer
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
