using Fluxo.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fluxo.Utils
{
    public static class ValidarProduto
    {
        public static ValidationResult ValidarDados(Produto produto)
        {
            var result = new ValidationResult();

            if (produto == null)
            {
                result.AddError("Produto não pode ser nulo.");
                return result;
            }

            // Validação do Nome
            if (string.IsNullOrWhiteSpace(produto.Nome))
            {
                result.AddError("Nome do produto é obrigatório.");
            }
            else if (produto.Nome.Length < 2)
            {
                result.AddError("Nome do produto deve ter pelo menos 2 caracteres.");
            }
            else if (produto.Nome.Length > 45)
            {
                result.AddError("Nome do produto não pode exceder 45 caracteres.");
            }

            // Validação do Preço
            if (produto.Preco < 0)
            {
                result.AddError("Preço não pode ser negativo.");
            }
            else if (produto.Preco > 999999.99m)
            {
                result.AddError("Preço não pode exceder R$ 999.999,99.");
            }

            // Validação da Quantidade
            if (produto.Quantidade < 0)
            {
                result.AddError("Quantidade não pode ser negativa.");
            }
            else if (produto.Quantidade > 999999)
            {
                result.AddError("Quantidade não pode exceder 999.999 unidades.");
            }

            return result;
        }
    }

    public class ValidationResult
    {
        private readonly List<string> _errors = new List<string>();

        public bool IsValid => !_errors.Any();
        public IReadOnlyList<string> Errors => _errors.AsReadOnly();

        public void AddError(string error)
        {
            if (!string.IsNullOrWhiteSpace(error))
            {
                _errors.Add(error);
            }
        }

        public string GetErrorsAsString()
        {
            return string.Join(Environment.NewLine, _errors);
        }
    }
}