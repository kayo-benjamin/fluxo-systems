using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fluxo.Model;
using Fluxo.Interface;
using Fluxo.Controller;

namespace Fluxo.View
{
    public partial class frmProdutos : Form
    {
        private readonly ProdutoController _produtoController;
        public frmProdutos()
        {
            InitializeComponent();
            this.Icon = new Icon("C:\\Users\\kayov\\source\\repos\\Fluxo\\Fluxo\\logo_fluxo.ico");

            IProdutoDao produtoDao = new ProdutoDao();
            _produtoController = new ProdutoController(produtoDao);
        }
        private void frmProdutos_Load(object sender, EventArgs e)
        {
            CarregarProdutos();
        }

        private void CarregarProdutos()
        {
            List<Produto> produtos = _produtoController.ListarProdutos();
            dgvProdutos.DataSource = null;
            dgvProdutos.DataSource = produtos;
        }

        private void LimparCampos()
        {
            txtIdProduto.Clear();
            txtNome.Clear();
            txtPreco.Clear();
            txtQuantidade.Clear();
            txtNome.Focus();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            string precoTextoLimpo = txtPreco.Text.Replace("R$", "").Trim();
            if (!decimal.TryParse(precoTextoLimpo, out decimal precoConvertido))
            {
                MessageBox.Show("O valor do campo 'Preço' é inválido. Por favor, digite apenas números.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(txtQuantidade.Text, out int quantidadeConvertida))
            {
                MessageBox.Show("O valor do campo 'Quantidade' é inválido. Por favor, digite apenas números inteiros.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("O campo 'Nome' não pode estar vazio.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                var produto = new Produto
                {
                    Nome = txtNome.Text,
                    Preco = precoConvertido,
                    Quantidade = quantidadeConvertida
                };

                // Se o campo ID tiver algo, significa que estamos editando
                if (!string.IsNullOrEmpty(txtIdProduto.Text))
                {
                    produto.IdProduto = int.Parse(txtIdProduto.Text);
                    _produtoController.AtualizarProduto(produto);
                    MessageBox.Show("Produto atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else // Senão, é um produto novo
                {
                    _produtoController.InserirProduto(produto);
                    MessageBox.Show("Produto cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LimparCampos();
                CarregarProdutos();
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor, verifique se os campos 'Preço' e 'Quantidade' são válidos.", "Erro de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvProdutos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvProdutos.Rows[e.RowIndex];
                txtIdProduto.Text = row.Cells["IdProduto"].Value.ToString();
                txtNome.Text = row.Cells["Nome"].Value.ToString();
                txtPreco.Text = row.Cells["Preco"].Value.ToString();
                txtQuantidade.Text = row.Cells["Quantidade"].Value.ToString();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdProduto.Text))
            {
                MessageBox.Show("Por favor, selecione um produto para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmacao = MessageBox.Show("Tem certeza que deseja excluir este produto?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacao == DialogResult.Yes)
            {
                try
                {
                    var produto = new Produto
                    {
                        IdProduto = int.Parse(txtIdProduto.Text)
                    };

                    _produtoController.DeletarProduto(produto);
                    MessageBox.Show("Produto excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LimparCampos();
                    CarregarProdutos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocorreu um erro ao excluir: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
