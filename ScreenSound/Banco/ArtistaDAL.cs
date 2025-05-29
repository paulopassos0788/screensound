using Microsoft.Data.SqlClient;
using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Banco;

internal class ArtistaDAL
{
    public IEnumerable<Artista> Listar()
    {
        var lista = new List<Artista>();
        using var connection = new Connection().ObterConexao();
        connection.Open();

        string sql = "SELECT * FROM Artistas";
        SqlCommand command = new SqlCommand(sql, connection);
        using SqlDataReader dataReader = command.ExecuteReader();

        while (dataReader.Read())
        {
            string nomeArtista = Convert.ToString(dataReader["Nome"]);
            string bioArtista = Convert.ToString(dataReader["Bio"]);
            int idArtista = Convert.ToInt32(dataReader["Id"]);
            Artista artista = new Artista(nomeArtista, bioArtista)
            {
                Id = idArtista
            };
            lista.Add(artista);
        }
        return lista;
    }

    public void Adicionar(Artista artista)
    {
        var lista = new List<Artista>();
        using var connection = new Connection().ObterConexao();
        connection.Open();

        string sql = "INSERT INTO Artistas (Nome, Bio, FotoPerfil) VALUES (@nome, @bio, @fotoPerfil)";
        SqlCommand command = new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@nome", artista.Nome);
        command.Parameters.AddWithValue("@bio", artista.Bio);
        command.Parameters.AddWithValue("@fotoPerfil", artista.FotoPerfil);

        int retorno = command.ExecuteNonQuery();
        if (retorno > 0)
        {
            Console.WriteLine("Artista adicionado com sucesso!");
        }
        else
        {
            Console.WriteLine("Erro ao adicionar artista.");
        }
    }

    public void Atualizar(Artista artista, int id)
    {
        using var connection = new Connection().ObterConexao();
        connection.Open();
        string sql = "UPDATE Artistas SET Nome = @nome, Bio = @bio WHERE Id = @id";
        SqlCommand command = new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@nome", artista.Nome);
        command.Parameters.AddWithValue("@bio", artista.Bio);
        command.Parameters.AddWithValue("@id", id);

        int retorno = command.ExecuteNonQuery();

        if (retorno > 0)
        {
            Console.WriteLine("Artista atualizado com sucesso!");
        }
        else
        {
            Console.WriteLine("Erro ao atualizar artista.");
        }
    }

    public void Excluir(int id)
    {
        using var connection = new Connection().ObterConexao();
        connection.Open();
        string sql = "DELETE FROM Artistas WHERE Id = @id";
        SqlCommand command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);
        int retorno = command.ExecuteNonQuery();
        if (retorno > 0)
        {
            Console.WriteLine("Artista excluído com sucesso!");
        }
        else
        {
            Console.WriteLine("Erro ao excluir artista.");
        }
    }
}
