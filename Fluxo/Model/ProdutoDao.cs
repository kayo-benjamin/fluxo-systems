using Fluxo.Interface;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fluxo.Model
{
    public class ProdutoDao : IProdutoDao
    {
        public void InserirProduto(Produto produto)
        {
            string query = "INSERT INTO produto (nome, preco, quantidade) VALUES (@nome, @preco, @quantidade)";

            try
            {
                using (var conn = new NpgsqlConnection(AppSettings.ConnectionString))
                {
                    conn.Open();

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("nome", produto.Nome);
                        cmd.Parameters.AddWithValue("preco", produto.Preco);
                        cmd.Parameters.AddWithValue("quantidade", produto.Quantidade);

                        cmd.ExecuteNonQuery();
                    }
                    Console.WriteLine("Produto inserido com sucesso.");
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Erro ao inserir produto: {ex.Message}");
                throw;
            }
        }
        public void AtualizarProduto(Produto produto)
        {
            string query = "UPDATE produto SET nome = @nome, preco = @preco, quantidade = @quantidade WHERE id_produto = @idProduto";
            try
            {
                using (var conn = new NpgsqlConnection(AppSettings.ConnectionString))
                {
                    conn.Open();

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("id_produto", produto.IdProduto);
                        cmd.Parameters.AddWithValue("nome", produto.Nome);
                        cmd.Parameters.AddWithValue("preco", produto.Preco);
                        cmd.Parameters.AddWithValue("quantidade", produto.Quantidade);

                        int linhasAfetadas = cmd.ExecuteNonQuery();

                        if (linhasAfetadas > 0)
                        {
                            Console.WriteLine("Produto atualizado com sucesso.");
                        }
                        else
                        {
                            Console.WriteLine("Produto não encontrado.");
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Erro ao atualizar produto: {ex.Message}");
                throw;
            }
        }
        public void DeletarProduto(Produto produto)
        {
            string query = "DELETE FROM produto WHERE id_produto = @id_produto";
            try
            {
                using (var conn = new NpgsqlConnection(AppSettings.ConnectionString))
                {
                    conn.Open();

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("id_produto", produto.IdProduto);

                        int linhasAfetadas = cmd.ExecuteNonQuery();

                        if (linhasAfetadas > 0)
                        {
                            Console.WriteLine("Produto deletado com sucesso.");
                        }
                        else
                        {
                            Console.WriteLine("Produto não encontrado.");
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Erro ao deletar produto: {ex.Message}");
                throw;
            }
        }
  
        List<Produto> IProdutoDao.ListarProdutos()
        {
            var produtos = new List<Produto>();
            string query = "SELECT id_produto, nome, preco, quantidade FROM produto";

            try
            {
                using (var conn = new NpgsqlConnection(AppSettings.ConnectionString))
                {
                    conn.Open();

                    using (var cmd = new NpgsqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var produto = new Produto()
                            {
                                IdProduto = reader.GetInt32(0),
                                Nome = reader.GetString(1),
                                Preco= reader.GetDecimal(2),
                                Quantidade = reader.GetInt32(3),
                            };

                            produtos.Add(produto);

                        }
                       Console.WriteLine("Produtos listados com sucesso.");
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Erro ao listar produtos: {ex.Message}");
                throw;
            }
            return produtos;
        }
    }
}
