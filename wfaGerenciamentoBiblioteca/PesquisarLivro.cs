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
    public partial class PesquisarLivro : Form
    {
        public PesquisarLivro()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                if (comboBox1.Text == "Todos")
                {
                    textBox1.Clear();
                    dataGridView1.DataSource = GDB_Biblioteca.getLivro();
                    limparCampos();
                    comboBox1.Focus();
                }
                else
                {
                    if (textBox1.Text != "")
                    {
                        switch (comboBox1.Text)
                        {
                            case "Título":
                                dataGridView1.DataSource = GDB_Biblioteca.selectLivroByTitulo(textBox1.Text);
                                limparCampos();
                                comboBox1.Focus();
                                break;
                            case "Categoria":
                                dataGridView1.DataSource = GDB_Biblioteca.selectLivroByCategoria(textBox1.Text);
                                limparCampos();
                                comboBox1.Focus();
                                break;
                            case "Autor(es)":
                                dataGridView1.DataSource = GDB_Biblioteca.selectLivroByAutor(textBox1.Text);
                                limparCampos();
                                comboBox1.Focus();
                                break;
                            case "Editora":
                                dataGridView1.DataSource = GDB_Biblioteca.selectLivroByEditora(textBox1.Text);
                                limparCampos();
                                comboBox1.Focus();
                                break;
                            default:
                                dataGridView1.DataSource = GDB_Biblioteca.selectLivroByISBN(textBox1.Text);
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

        private void limparCampos()
        {
            comboBox1.SelectedIndex = -1;
            textBox1.Clear();
        }
    }
}
