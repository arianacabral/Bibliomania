using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Collections;

namespace wfaGerenciamentoBiblioteca
{
    public partial class Form1 : Form
    {
        //int nEmprestimos;
        int nLeitores;
        int nLivros;
        int nEmprestimos;

        string[] categoria = new string[GDB_Biblioteca.selectEmprestimoByCategoria().Rows.Count];
        string[] quantidade = new string[GDB_Biblioteca.selectEmprestimoByCategoria().Rows.Count];

        public Form1()
        {
            InitializeComponent();
            
            for(int i = 0; i < categoria.Length; i++)
            {
                var stringArr = GDB_Biblioteca.selectEmprestimoByCategoria().Rows[i].ItemArray.Select(x => x.ToString()).ToArray();
                categoria[i] = stringArr[0];
                quantidade[i] = stringArr[1];
            }
        }  

        private void cadastrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            CadastrarLeitor novoLeitor = new CadastrarLeitor();
            novoLeitor.ShowDialog();
            atualizarQtdLeitores();
            this.Visible = true;

        }

        private void atualizarDadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            AtualizarDadosUsuario atualizarLeitor = new AtualizarDadosUsuario();
            atualizarLeitor.ShowDialog();
            this.Visible = true;

        }

        private void excluirUsuárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            ExcluirLeitor excluirLeitor = new ExcluirLeitor();
            excluirLeitor.ShowDialog();
            atualizarQtdLeitores();
            this.Visible = true;

        }

        private void consultarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            PesquisarLeitores buscarLeitor = new PesquisarLeitores();
            buscarLeitor.ShowDialog();
            this.Visible = true;

        }

        private void cadastrarLivroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            CadastrarLivro novoLivro = new CadastrarLivro();
            novoLivro.ShowDialog();
            atualizarQtdLivros();
            this.Visible = true;

        }

        private void atualizarDadosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Hide();
            AtualizarDadosAcervo atualizarAcervo = new AtualizarDadosAcervo();
            atualizarAcervo.ShowDialog();
            this.Visible = true;
        }

        private void excluirLivroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            ExcluirLivro excluirLivro = new ExcluirLivro();
            excluirLivro.ShowDialog();
            atualizarQtdLivros();
            this.Visible = true;
        }

        private void consultarAcervoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            PesquisarLivro buscarLivro = new PesquisarLivro();
            buscarLivro.ShowDialog();
            this.Visible = true;
        }

        private void incluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            CadastrarEmprestimo novoEmprestimo = new CadastrarEmprestimo();
            novoEmprestimo.ShowDialog();
            atualizarQtdEmprestimos();
            this.Visible = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            atualizarQtdLeitores();
            atualizarQtdLivros();
            atualizarQtdEmprestimos();
        }
        private void atualizarQtdLeitores()
        {
            nLeitores = GDB_Biblioteca.getLeitor().Rows.Count;
            label4.Text = nLeitores.ToString();
        }
        private void atualizarQtdLivros()
        {
            nLivros = GDB_Biblioteca.getLivro().Rows.Count;
            label1.Text = nLivros.ToString();
        }
        private void atualizarQtdEmprestimos()
        {
            nEmprestimos = GDB_Biblioteca.getEmprestimo().Rows.Count;
            label6.Text = nEmprestimos.ToString();
        }

        private void consultarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Hide();
            AtualizarDadosEmprestimo atualizarEmprestimo = new AtualizarDadosEmprestimo();
            atualizarEmprestimo.ShowDialog();
            this.Visible = true;
        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            ExcluirEmprestimo excluirEmprestimo = new ExcluirEmprestimo();
            excluirEmprestimo.ShowDialog();
            atualizarQtdEmprestimos();
            this.Visible = true;
        }

        private void consultarEmpréstimosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            PesquisarEmprestimo buscarEmprestimo = new PesquisarEmprestimo();
            buscarEmprestimo.ShowDialog();
            this.Visible = true;
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sobreToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Biblioteca sobre = new Biblioteca();
            sobre.ShowDialog();
            this.Visible = true;
        }

        private void suporteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Suporte suporte = new Suporte();
            suporte.ShowDialog();
            this.Visible = true;
        }
    }
}
