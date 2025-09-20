using Fluxo.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fluxo.Model
{
    public class Produto
    {
        public int IdProduto { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }

        public override string ToString()
        {
            return $"ID: {IdProduto}, Nome: {Nome}, Preço: {Preco}, Quantidade: {Quantidade}";
        }
    }
}
