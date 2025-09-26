using System;
using System.Collections.Generic;
using Fluxo.Interface;
using Fluxo.Model;
using Fluxo.Utils;

namespace Fluxo.Controller
{
    public class ProdutoController
    {
        private readonly IProdutoDao _produtoDao;

        public ProdutoController(IProdutoDao produtoDao)
        {
            _produtoDao = produtoDao ?? throw new ArgumentNullException(nameof(produtoDao));
        }

        public OperationResult InserirProduto(Produto produto)
        {
            try
            {
                Logger.LogInfo("Controller: Iniciando inserção de produto");
                
                // Validação dos dados
                var validationResult = ValidarProduto.ValidarDados(produto);
                if (!validationResult.IsValid)
                {
                    Logger.LogWarning($"Validação falhou: {validationResult.GetErrorsAsString()}");
                    return OperationResult.Failure(validationResult.GetErrorsAsString());
                }

                _produtoDao.InserirProduto(produto);
                Logger.LogInfo("Controller: Produto inserido com sucesso");
                return OperationResult.Success("Produto inserido com sucesso!");
            }
            catch (ApplicationException ex)
            {
                Logger.LogError("Controller: Erro na inserção de produto", ex);
                return OperationResult.Failure($"Erro ao inserir produto: {ex.Message}");
            }
            catch (Exception ex)
            {
                Logger.LogError("Controller: Erro inesperado na inserção", ex);
                return OperationResult.Failure("Erro inesperado. Consulte o administrador do sistema.");
            }
        }

        public OperationResult AtualizarProduto(Produto produto)
        {
            try
            {
                Logger.LogInfo($"Controller: Iniciando atualização do produto ID {produto?.IdProduto}");
                
                // Validação dos dados
                var validationResult = ValidarProduto.ValidarDados(produto);
                if (!validationResult.IsValid)
                {
                    Logger.LogWarning($"Validação falhou: {validationResult.GetErrorsAsString()}");
                    return OperationResult.Failure(validationResult.GetErrorsAsString());
                }

                // Validação adicional para atualização
                if (produto.IdProduto <= 0)
                {
                    Logger.LogWarning("ID do produto inválido para atualização");
                    return OperationResult.Failure("ID do produto é obrigatório para atualização.");
                }

                _produtoDao.AtualizarProduto(produto);
                Logger.LogInfo("Controller: Produto atualizado com sucesso");
                return OperationResult.Success("Produto atualizado com sucesso!");
            }
            catch (InvalidOperationException ex)
            {
                Logger.LogWarning($"Controller: Produto não encontrado para atualização: {ex.Message}");
                return OperationResult.Failure("Produto não encontrado.");
            }
            catch (ApplicationException ex)
            {
                Logger.LogError("Controller: Erro na atualização de produto", ex);
                return OperationResult.Failure($"Erro ao atualizar produto: {ex.Message}");
            }
            catch (Exception ex)
            {
                Logger.LogError("Controller: Erro inesperado na atualização", ex);
                return OperationResult.Failure("Erro inesperado. Consulte o administrador do sistema.");
            }
        }

        public OperationResult DeletarProduto(Produto produto)
        {
            try
            {
                Logger.LogInfo($"Controller: Iniciando deleção do produto ID {produto?.IdProduto}");
                
                if (produto == null || produto.IdProduto <= 0)
                {
                    Logger.LogWarning("Produto ou ID inválido para deleção");
                    return OperationResult.Failure("Produto válido é obrigatório para deleção.");
                }

                _produtoDao.DeletarProduto(produto);
                Logger.LogInfo("Controller: Produto deletado com sucesso");
                return OperationResult.Success("Produto deletado com sucesso!");
            }
            catch (InvalidOperationException ex)
            {
                Logger.LogWarning($"Controller: Produto não encontrado para deleção: {ex.Message}");
                return OperationResult.Failure("Produto não encontrado.");
            }
            catch (ApplicationException ex)
            {
                Logger.LogError("Controller: Erro na deleção de produto", ex);
                return OperationResult.Failure($"Erro ao deletar produto: {ex.Message}");
            }
            catch (Exception ex)
            {
                Logger.LogError("Controller: Erro inesperado na deleção", ex);
                return OperationResult.Failure("Erro inesperado. Consulte o administrador do sistema.");
            }
        }

        public OperationResult<List<Produto>> ListarProdutos()
        {
            try
            {
                Logger.LogInfo("Controller: Iniciando listagem de produtos");
                
                var produtos = _produtoDao.ListarProdutos();
                Logger.LogInfo($"Controller: Listagem concluída com {produtos.Count} produtos");
                
                return OperationResult<List<Produto>>.Success(produtos, "Produtos listados com sucesso!");
            }
            catch (ApplicationException ex)
            {
                Logger.LogError("Controller: Erro na listagem de produtos", ex);
                return OperationResult<List<Produto>>.Failure($"Erro ao listar produtos: {ex.Message}");
            }
            catch (Exception ex)
            {
                Logger.LogError("Controller: Erro inesperado na listagem", ex);
                return OperationResult<List<Produto>>.Failure("Erro inesperado. Consulte o administrador do sistema.");
            }
        }
    }

    // Classe auxiliar para padronizar retornos de operações
    public class OperationResult
    {
        public bool IsSuccess { get; protected set; }
        public string Message { get; protected set; }

        protected OperationResult(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public static OperationResult Success(string message = "Operação realizada com sucesso.")
        {
            return new OperationResult(true, message);
        }

        public static OperationResult Failure(string message)
        {
            return new OperationResult(false, message);
        }
    }

    public class OperationResult<T> : OperationResult
    {
        public T Data { get; private set; }

        private OperationResult(bool isSuccess, string message, T data = default(T)) : base(isSuccess, message)
        {
            Data = data;
        }

        public static OperationResult<T> Success(T data, string message = "Operação realizada com sucesso.")
        {
            return new OperationResult<T>(true, message, data);
        }

        public static new OperationResult<T> Failure(string message)
        {
            return new OperationResult<T>(false, message);
        }
    }
}