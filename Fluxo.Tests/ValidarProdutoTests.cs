using Fluxo.Model;
using Fluxo.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fluxo.Tests
{
    [TestClass]
    public class ValidarProdutoTests
    {
        [TestMethod]
        public void ValidarDados_ProdutoValido_DeveRetornarSucesso()
        {
            // Arrange
            var produto = new Produto
            {
                Nome = "Produto Teste",
                Preco = 10.50m,
                Quantidade = 5
            };

            // Act
            var resultado = ValidarProduto.ValidarDados(produto);

            // Assert
            Assert.IsTrue(resultado.IsValid);
            Assert.AreEqual(0, resultado.Errors.Count);
        }

        [TestMethod]
        public void ValidarDados_ProdutoNulo_DeveRetornarErro()
        {
            // Arrange
            Produto produto = null;

            // Act
            var resultado = ValidarProduto.ValidarDados(produto);

            // Assert
            Assert.IsFalse(resultado.IsValid);
            Assert.IsTrue(resultado.Errors.Count > 0);
            Assert.AreEqual("Produto não pode ser nulo.", resultado.Errors[0]);
        }

        [TestMethod]
        public void ValidarDados_NomeVazio_DeveRetornarErro()
        {
            // Arrange
            var produto = new Produto
            {
                Nome = "",
                Preco = 10.50m,
                Quantidade = 5
            };

            // Act
            var resultado = ValidarProduto.ValidarDados(produto);

            // Assert
            Assert.IsFalse(resultado.IsValid);
            Assert.IsTrue(resultado.Errors.Any(e => e.Contains("Nome do produto é obrigatório")));
        }

        [TestMethod]
        public void ValidarDados_NomeMuitoCurto_DeveRetornarErro()
        {
            // Arrange
            var produto = new Produto
            {
                Nome = "A",
                Preco = 10.50m,
                Quantidade = 5
            };

            // Act
            var resultado = ValidarProduto.ValidarDados(produto);

            // Assert
            Assert.IsFalse(resultado.IsValid);
            Assert.IsTrue(resultado.Errors.Any(e => e.Contains("pelo menos 2 caracteres")));
        }

        [TestMethod]
        public void ValidarDados_NomeMuitoLongo_DeveRetornarErro()
        {
            // Arrange
            var produto = new Produto
            {
                Nome = new string('A', 46), // 46 caracteres
                Preco = 10.50m,
                Quantidade = 5
            };

            // Act
            var resultado = ValidarProduto.ValidarDados(produto);

            // Assert
            Assert.IsFalse(resultado.IsValid);
            Assert.IsTrue(resultado.Errors.Any(e => e.Contains("não pode exceder 45 caracteres")));
        }

        [TestMethod]
        public void ValidarDados_PrecoNegativo_DeveRetornarErro()
        {
            // Arrange
            var produto = new Produto
            {
                Nome = "Produto Teste",
                Preco = -10.50m,
                Quantidade = 5
            };

            // Act
            var resultado = ValidarProduto.ValidarDados(produto);

            // Assert
            Assert.IsFalse(resultado.IsValid);
            Assert.IsTrue(resultado.Errors.Any(e => e.Contains("Preço não pode ser negativo")));
        }

        [TestMethod]
        public void ValidarDados_PrecoMuitoAlto_DeveRetornarErro()
        {
            // Arrange
            var produto = new Produto
            {
                Nome = "Produto Teste",
                Preco = 1000000.00m,
                Quantidade = 5
            };

            // Act
            var resultado = ValidarProduto.ValidarDados(produto);

            // Assert
            Assert.IsFalse(resultado.IsValid);
            Assert.IsTrue(resultado.Errors.Any(e => e.Contains("não pode exceder R$ 999.999,99")));
        }

        [TestMethod]
        public void ValidarDados_QuantidadeNegativa_DeveRetornarErro()
        {
            // Arrange
            var produto = new Produto
            {
                Nome = "Produto Teste",
                Preco = 10.50m,
                Quantidade = -1
            };

            // Act
            var resultado = ValidarProduto.ValidarDados(produto);

            // Assert
            Assert.IsFalse(resultado.IsValid);
            Assert.IsTrue(resultado.Errors.Any(e => e.Contains("Quantidade não pode ser negativa")));
        }

        [TestMethod]
        public void ValidarDados_QuantidadeMuitoAlta_DeveRetornarErro()
        {
            // Arrange
            var produto = new Produto
            {
                Nome = "Produto Teste",
                Preco = 10.50m,
                Quantidade = 1000000
            };

            // Act
            var resultado = ValidarProduto.ValidarDados(produto);

            // Assert
            Assert.IsFalse(resultado.IsValid);
            Assert.IsTrue(resultado.Errors.Any(e => e.Contains("não pode exceder 999.999 unidades")));
        }

        [TestMethod]
        public void ValidarDados_MultiplosErros_DeveRetornarTodos()
        {
            // Arrange
            var produto = new Produto
            {
                Nome = "",
                Preco = -10.50m,
                Quantidade = -1
            };

            // Act
            var resultado = ValidarProduto.ValidarDados(produto);

            // Assert
            Assert.IsFalse(resultado.IsValid);
            Assert.IsTrue(resultado.Errors.Count >= 3); // Pelo menos 3 erros
        }
    }
}