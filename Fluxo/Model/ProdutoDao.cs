using Fluxo.Interface;
using Fluxo.Utils;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace Fluxo.Model
{
    public class ProdutoDao : IProdutoDao
    {
        public void InserirProduto(Produto produto)
        {
            const string query = "INSERT INTO produto (nome, preco, quantidade) VALUES (@nome, @preco, @quantidade)";
            
            Logger.LogInfo($"Iniciando inserção do produto: {produto.Nome}");

            try
            {
                using (var conn = new NpgsqlConnection(AppSettings.ConnectionString))
                {
                    conn.Open();
                    Logger.LogInfo("Conexão com banco de dados estabelecida");

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.Add(new NpgsqlParameter("nome", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = produto.Nome });
                        cmd.Parameters.Add(new NpgsqlParameter("preco", NpgsqlTypes.NpgsqlDbType.Numeric) { Value = produto.Preco });
                        cmd.Parameters.Add(new NpgsqlParameter("quantidade", NpgsqlTypes.NpgsqlDbType.Integer) { Value = produto.Quantidade });

                        int linhasAfetadas = cmd.ExecuteNonQuery();
                        
                        if (linhasAfetadas > 0)
                        {
                            Logger.LogInfo($"Produto '{produto.Nome}' inserido com sucesso");
                        }
                        else
                        {
                            Logger.LogWarning("Nenhuma linha foi afetada durante a inserção");
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError($"Erro de banco de dados ao inserir produto '{produto.Nome}'", ex);
                throw new ApplicationException($"Erro ao inserir produto: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Erro inesperado ao inserir produto '{produto.Nome}'", ex);
                throw new ApplicationException($"Erro inesperado: {ex.Message}", ex);
            }
        }

        public void AtualizarProduto(Produto produto)
        {
            const string query = "UPDATE produto SET nome = @nome, preco = @preco, quantidade = @quantidade WHERE id_produto = @idProduto";
            
            Logger.LogInfo($"Iniciando atualização do produto ID: {produto.IdProduto}");

            try
            {
                using (var conn = new NpgsqlConnection(AppSettings.ConnectionString))
                {
                    conn.Open();
                    Logger.LogInfo("Conexão com banco de dados estabelecida");

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.Add(new NpgsqlParameter("nome", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = produto.Nome });
                        cmd.Parameters.Add(new NpgsqlParameter("preco", NpgsqlTypes.NpgsqlDbType.Numeric) { Value = produto.Preco });
                        cmd.Parameters.Add(new NpgsqlParameter("quantidade", NpgsqlTypes.NpgsqlDbType.Integer) { Value = produto.Quantidade });
                        cmd.Parameters.Add(new NpgsqlParameter("idProduto", NpgsqlTypes.NpgsqlDbType.Integer) { Value = produto.IdProduto });

                        int linhasAfetadas = cmd.ExecuteNonQuery();

                        if (linhasAfetadas > 0)
                        {
                            Logger.LogInfo($"Produto ID {produto.IdProduto} atualizado com sucesso");
                        }
                        else
                        {
                            Logger.LogWarning($"Produto com ID {produto.IdProduto} não foi encontrado para atualização");
                            throw new InvalidOperationException($"Produto com ID {produto.IdProduto} não encontrado");
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError($"Erro de banco de dados ao atualizar produto ID {produto.IdProduto}", ex);
                throw new ApplicationException($"Erro ao atualizar produto: {ex.Message}", ex);
            }
            catch (InvalidOperationException)
            {
                throw; // Re-lança a exceção de produto não encontrado
            }
            catch (Exception ex)
            {
                Logger.LogError($"Erro inesperado ao atualizar produto ID {produto.IdProduto}", ex);
                throw new ApplicationException($"Erro inesperado: {ex.Message}", ex);
            }
        }

        public void DeletarProduto(Produto produto)
        {
            const string query = "DELETE FROM produto WHERE id_produto = @id_produto";
            
            Logger.LogInfo($"Iniciando deleção do produto ID: {produto.IdProduto}");

            try
            {
                using (var conn = new NpgsqlConnection(AppSettings.ConnectionString))
                {
                    conn.Open();
                    Logger.LogInfo("Conexão com banco de dados estabelecida");

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.Add(new NpgsqlParameter("id_produto", NpgsqlTypes.NpgsqlDbType.Integer) { Value = produto.IdProduto });

                        int linhasAfetadas = cmd.ExecuteNonQuery();

                        if (linhasAfetadas > 0)
                        {
                            Logger.LogInfo($"Produto ID {produto.IdProduto} deletado com sucesso");
                        }
                        else
                        {
                            Logger.LogWarning($"Produto com ID {produto.IdProduto} não foi encontrado para deleção");
                            throw new InvalidOperationException($"Produto com ID {produto.IdProduto} não encontrado");
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError($"Erro de banco de dados ao deletar produto ID {produto.IdProduto}", ex);
                throw new ApplicationException($"Erro ao deletar produto: {ex.Message}", ex);
            }
            catch (InvalidOperationException)
            {
                throw; // Re-lança a exceção de produto não encontrado
            }
            catch (Exception ex)
            {
                Logger.LogError($"Erro inesperado ao deletar produto ID {produto.IdProduto}", ex);
                throw new ApplicationException($"Erro inesperado: {ex.Message}", ex);
            }
        }

        public List<Produto> ListarProdutos()
        {
            var produtos = new List<Produto>();
            const string query = "SELECT id_produto, nome, preco, quantidade FROM produto ORDER BY nome";
            
            Logger.LogInfo("Iniciando listagem de produtos");

            try
            {
                using (var conn = new NpgsqlConnection(AppSettings.ConnectionString))
                {
                    conn.Open();
                    Logger.LogInfo("Conexão com banco de dados estabelecida");

                    using (var cmd = new NpgsqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var produto = new Produto()
                            {
                                IdProduto = reader.GetInt32("id_produto"),
                                Nome = reader.GetString("nome"),
                                Preco = reader.GetDecimal("preco"),
                                Quantidade = reader.GetInt32("quantidade")
                            };

                            produtos.Add(produto);
                        }
                        
                        Logger.LogInfo($"Listagem concluída: {produtos.Count} produtos encontrados");
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                Logger.LogError("Erro de banco de dados ao listar produtos", ex);
                throw new ApplicationException($"Erro ao listar produtos: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                Logger.LogError("Erro inesperado ao listar produtos", ex);
                throw new ApplicationException($"Erro inesperado: {ex.Message}", ex);
            }

            return produtos;
        }
    }
}