using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fluxo.Interface;
using Fluxo.Model;
using Npgsql;

namespace Fluxo.Controller
{
    public class ProdutoController
    {
        private readonly IProdutoDao _produtoDao;

        public ProdutoController(IProdutoDao produtoDao)
        {
            _produtoDao = produtoDao;
        }

        public void InserirProduto(Produto produto)
        {
            _produtoDao.InserirProduto(produto);
        }

        public void AtualizarProduto(Produto produto)
        {
            _produtoDao.AtualizarProduto(produto);
        }
        public void DeletarProduto(Produto produto)
        {
            _produtoDao.DeletarProduto(produto);
        }

        public List<Produto> ListarProdutos()
        {
            return _produtoDao.ListarProdutos();
        }
    }
}