
  
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using MySqlConnector;
namespace Biblioteca.Models
{
    public class UsuarioBanco
    {
        private const string dadosConexao = "Database=Biblioteca; Data Source= localhost;User Id= root";

        public void Insert(Usuario novoUsuario)
        {
            MySqlConnection Conexao = new MySqlConnection(dadosConexao);

            Conexao.Open();

            string query = "INSERT INTO Usuario(Nome,Login, Senha) VALUES (@Nome, @Login, @Senha)";

            MySqlCommand comando = new MySqlCommand(query,Conexao);

            comando.Parameters.AddWithValue("@Nome", novoUsuario.Nome);
            comando.Parameters.AddWithValue("@Login", novoUsuario.Login);
            comando.Parameters.AddWithValue("@Senha", novoUsuario.Senha);
            comando.ExecuteNonQuery();
            Conexao.Close();
        }

        public List<Usuario> Query()
        {
            MySqlConnection Conexao = new MySqlConnection(dadosConexao);
            Conexao.Open();
            string query = "SELECT * FROM Usuario";
            MySqlCommand comando = new MySqlCommand(query, Conexao);
            MySqlDataReader reader = comando.ExecuteReader();

            List<Usuario> lista = new List<Usuario>();
            while (reader.Read())
            {
                Usuario user = new Usuario();
                user.Id = reader.GetInt32("id");

                if(!reader.IsDBNull(reader.GetOrdinal("Nome")))
                {
                    user.Nome = reader.GetString("Nome");
                }
                if(!reader.IsDBNull(reader.GetOrdinal("Login")))
                {
                    user.Login = reader.GetString("Login");
                }
                if(!reader.IsDBNull(reader.GetOrdinal("Senha")))
                {
                    user.Senha = reader.GetString("Senha");
                }
                lista.Add(user);
                
            }
            Conexao.Close();
            return lista;
        }

        public Usuario QueryLogin(Usuario usuario)
        {
            MySqlConnection Conexao = new MySqlConnection(dadosConexao);
            Conexao.Open();
            string sql = " SELECT * FROM Usuario WHERE login = @Login AND senha = @Senha";

            MySqlCommand comandoQuery = new MySqlCommand(sql, Conexao);
            comandoQuery.Parameters.AddWithValue("@Login",usuario.Login );
            comandoQuery.Parameters.AddWithValue("@Senha",usuario.Senha );
            MySqlDataReader reader = comandoQuery.ExecuteReader();
            Usuario novoUsuario = null;
            if(reader.Read())
            {
                novoUsuario = new Usuario();
                novoUsuario.Id = reader.GetInt32("Id");

                if(!reader.IsDBNull(reader.GetOrdinal("Nome")))
                    novoUsuario.Nome = reader.GetString("Nome");

                if(!reader.IsDBNull(reader.GetOrdinal("Login")))
                    novoUsuario.Login = reader.GetString("Login");

                if(!reader.IsDBNull(reader.GetOrdinal("Senha")))
                    novoUsuario.Senha= reader.GetString("Senha");

            }
            Conexao.Close();
            return novoUsuario;

        }
         public void Atualizar(Usuario usuario)
        {
            MySqlConnection conexao = new MySqlConnection(dadosConexao);
            conexao.Open();
            string sql = "update Usuario set Nome = @Nome, Login = @Login, Senha = @Senha, Nome = @Nome, Tipo = @Tipo where Id = @Id";
            MySqlCommand comando = new MySqlCommand(sql,conexao);
            comando.Parameters.AddWithValue("@Nome",usuario.Nome);
            comando.Parameters.AddWithValue("@Tipo",usuario.Tipo);
            comando.Parameters.AddWithValue("@Senha",usuario.Senha);
            comando.Parameters.AddWithValue("@Login",usuario.Login);
            comando.Parameters.AddWithValue("@Id",usuario.Id);
            comando.ExecuteNonQuery();
            conexao.Close();
        }
    
        public void Remover(int Id)
        {
            MySqlConnection conexao = new MySqlConnection(dadosConexao);
            conexao.Open();
            string sql = "delete from Usuario where Id = @Id";
            MySqlCommand comando = new MySqlCommand(sql,conexao);
            comando.Parameters.AddWithValue("@Id", Id);
            comando.ExecuteNonQuery();
            conexao.Close();
        }
        
        public Usuario ConsultaPorId(int Id)
        {
            MySqlConnection conexao = new MySqlConnection(dadosConexao);
            conexao.Open();
            string sql = "select * from Usuario  where Id= @Id";
            MySqlCommand comando = new MySqlCommand(sql,conexao);
            comando.Parameters.AddWithValue("@Id",Id);
            MySqlDataReader reader = comando.ExecuteReader();
            Usuario user = null;
            if(reader.Read())
            {
                user = new Usuario();
                user.Id = reader.GetInt32("Id");
                user.Tipo = reader.GetInt32("Tipo");
                
                if(!reader.IsDBNull(reader.GetOrdinal("Nome")))
                    user.Nome = reader.GetString("Nome");
                if(!reader.IsDBNull(reader.GetOrdinal("Login")))
                    user.Login = reader.GetString("Login");
                if(!reader.IsDBNull(reader.GetOrdinal("Senha")))
                    user.Senha = reader.GetString("Senha");
            }
            conexao.Close();
            return user;
        }
    }
}