using Fluxo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fluxo.Interface
{
    public interface IProdutoDao
    {
        void InserirProduto(Produto produto);
        void AtualizarProduto(Produto produto);
        void DeletarProduto(Produto produto);
        List<Produto> ListarProdutos();
    }
}
