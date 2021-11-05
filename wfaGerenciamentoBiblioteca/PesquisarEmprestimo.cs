using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wfaGerenciamentoBiblioteca
{
    public partial class PesquisarEmprestimo : Form
    {
        public PesquisarEmprestimo()
        {
            InitializeComponent();
        }

        private void limparCampos()
        {
            comboBox1.SelectedIndex = -1;
            textBox1.Clear();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                if (comboBox1.Text == "Todos")
                {
                    textBox1.Clear();
                    dataGridView1.DataSource = GDB_Biblioteca.getEmprestimo();
                    limparCampos();
                    comboBox1.Focus();
                }
                else
                {
                    if (textBox1.Text != "")
                    {
                        switch (comboBox1.Text)
                        {
                            case "ISBN do livro":
                                dataGridView1.DataSource = GDB_Biblioteca.selectEmprestimoByISBN(textBox1.Text);
                                limparCampos();
                                comboBox1.Focus();
                                break;
                            case "Título do livro":
                                dataGridView1.DataSource = GDB_Biblioteca.selectEmprestimoByTitulo(textBox1.Text);
                                limparCampos();
                                comboBox1.Focus();
                                break;
                            case "Nome do leitor":
                                dataGridView1.DataSource = GDB_Biblioteca.selectEmprestimoByNomeLeitor(textBox1.Text);
                                limparCampos();
                                comboBox1.Focus();
                                break;
                            default: // CPF
                                dataGridView1.DataSource = GDB_Biblioteca.selectEmprestimoByCPFLeitor(textBox1.Text);
                                limparCampos();
                                comboBox1.Focus();
                                break;
                        }

                    }
                    else
                    {
                        MessageBox.Show("Informar o item de busca!");
                        textBox1.Focus();

                    }
                }
            }
            else
            {
                MessageBox.Show("Informar o tipo de busca!");
                comboBox1.Focus();
            }
        }
    }
}
