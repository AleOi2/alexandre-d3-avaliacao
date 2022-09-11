using System;
using alexandre_d3_avaliacao.Interfaces;
using alexandre_d3_avaliacao.Models;
using MySql.Data.MySqlClient;
using accessing_db.StaticFunction;
namespace accessing_db.Repositories
{
    internal class UserRepository : IUser
    {
        /// <summary>
        /// 
        /// </summary>
        private const string path = "csv/register.csv";
        private readonly string stringConexao = System.Environment.GetEnvironmentVariable("mysql_connection");        
        
        public UserRepository()
        {
            Utils.CreateFolderFile(path);
        }

        public void Create(User newUser)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(stringConexao))
                {
                // SQL Injection
                // string queryInsert = $"INSERT INTO Users (id, email, senha, nome) VALUES ('{newUser.Id}', '{newUser.Email}', '{newUser.Senha}', {newUser.Nome})";
                string queryInsert = "INSERT INTO usuario (email, senha, nome) VALUES (@email, @senha, @nome)";
                string encryptedPass = Utils.Encrypt(newUser.Senha);
                using (MySqlCommand cmd = new MySqlCommand(queryInsert, con))
                    {
                        cmd.Parameters.AddWithValue("@email", newUser.Email);
                        cmd.Parameters.AddWithValue("@senha", encryptedPass);
                        cmd.Parameters.AddWithValue("@nome", newUser.Nome);

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                Console.WriteLine("Usuário adiconado");
            }
            catch (System.Exception)
            {
                throw;
            }

        }

        public void Delete(string idUser)
        {
            throw new NotImplementedException();
        }

        public List<User> ReadAll()
        {
            List<User> listUsers = new();
            MySqlConnection conn = new MySqlConnection(stringConexao);

            try
            {
                conn.Open();
                string querySelect = "SELECT * FROM usuario";
                MySqlCommand cmd = new MySqlCommand(querySelect, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    User user = new()
                    {
                        Id =         Convert.ToInt32(rdr[0]),
                        Email =      rdr[1].ToString(),
                        Senha =      rdr[2].ToString(),
                        Nome =       rdr[3].ToString(),
                    };
                    listUsers.Add(user);
                }                
            }
            catch (System.Exception)
            {
                Console.WriteLine("Error");   
                throw;
            }

            return listUsers;
        }

        public void Update(User User)
        {
            throw new NotImplementedException();
        }

        public User ValidateUser(string email, string password){
            User findedUser = null;
            Console.WriteLine("stringConexao");
            Console.WriteLine(stringConexao);
            MySqlConnection conn = new MySqlConnection(stringConexao);
            try
            {
                conn.Open();
                string querySelect = $"SELECT * FROM usuario where email = '{email}'";
                MySqlCommand cmd = new MySqlCommand(querySelect, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    string decryptedPassword = Utils.Decrypt(rdr[2].ToString());
                    if (decryptedPassword == password) {
                        User user = new()
                        {
                            Id    =  Convert.ToInt32(rdr[0]),
                            Email =  rdr[1].ToString(),
                            Senha =  rdr[2].ToString(),
                            Nome  =  rdr[3].ToString(),
                        };
                        findedUser = user;
                        Console.WriteLine("Usuário validado "+rdr[1]+" - "+rdr[3]);
                    }
                    break;


                }
            }
            catch (System.Exception)
            {
                Console.WriteLine("Error");   
                throw;
            }
            return findedUser;
        }

        public string PrepareLoginLineCSV(User user, DateTime accessTime){
            return $"Usuário {user.Nome} logou em {accessTime.ToString("dd/MM/yyyy HH:mm:ss")}";
        }

        public string PrepareLogoutLineCSV(User user, DateTime accessTime){
            return $"Usuário {user.Nome} deslogou {accessTime.ToString("dd/MM/yyyy HH:mm:ss")}";
        }

        public void registerUser(User user, DateTime time, bool login){
            if (login){
                string[] line = { PrepareLoginLineCSV(user, time) };
                File.AppendAllLines(path, line);
            } else if(!login){
                string[] line = { PrepareLogoutLineCSV(user, time) };
                File.AppendAllLines(path, line);
            }

        }
    }
}
