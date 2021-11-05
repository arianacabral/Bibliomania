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
    public partial class CadastrarLivro : Form
    {
        public CadastrarLivro()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) // botão fechar
        {
            Close();
        }

        private void button4_Click(object sender, EventArgs e) // botão cancelar
        {
            limparCampos();
        }

        private void limparCampos()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" & textBox2.Text != "" & textBox3.Text != "" & textBox4.Text != "" &
                textBox5.Text != "" & textBox6.Text != "" & textBox7.Text != "")
            {
                Acervo m_livro = new Acervo();

                m_livro.setTitulo(textBox1.Text);
                m_livro.setISBN(textBox7.Text);
                m_livro.setAutores(textBox2.Text);
                m_livro.setEditora(textBox4.Text);
                m_livro.setEdicao(int.Parse(textBox3.Text));
                m_livro.setCategoria(textBox5.Text);
                m_livro.setNumPaginas(int.Parse(textBox6.Text));

                GDB_Biblioteca.insertLivro(m_livro);

                limparCampos();

            }
            else
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Informar Título do livro!");
                    textBox1.Focus();
                }
                else if (textBox2.Text == "")
                {
                    MessageBox.Show("Informar Autor(es)!");
                    textBox2.Focus();
                }
                else if (textBox3.Text == "")
                {
                    MessageBox.Show("Informar a Edição!");
                    textBox3.Focus();
                }
                else if (textBox4.Text == "")
                {
                    MessageBox.Show("Informar a Editora!");
                    textBox4.Focus();
                }
                else if (textBox5.Text == "")
                {
                    MessageBox.Show("Informar a Categoria!");
                    textBox5.Focus();
                }
                else if (textBox6.Text == "")
                {
                    MessageBox.Show("Informar o Número de Páginas!");
                    textBox6.Focus();
                }
                else
                {
                    MessageBox.Show("Informar o ISBN!");
                    textBox6.Focus();
                }
            }
        }
    }
}
