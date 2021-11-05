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
    public partial class AtualizarDadosAcervo : Form
    {
        public AtualizarDadosAcervo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) // botão de fechar
        {
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
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
            int m_row = dataGridView1.SelectedRows[0].Index; // linha selecionada

            Acervo m_livro = new Acervo(); // instanciando livro

            // definindo os dados do livro selecionado
            m_livro.setTitulo(textBox1.Text);
            m_livro.setISBN(textBox7.Text);
            m_livro.setAutores(textBox2.Text);
            m_livro.setEditora(textBox4.Text);
            m_livro.setEdicao(int.Parse(textBox3.Text));
            m_livro.setCategoria(textBox5.Text);
            m_livro.setNumPaginas(int.Parse(textBox6.Text));

            if (textBox1.Text != "" & textBox2.Text != "" & textBox3.Text != "" & textBox4.Text != "" & textBox5.Text != "" & textBox6.Text != "" & textBox7.Text != "")
            {
                DataTable m_data_table = new DataTable();
                m_data_table = GDB_Biblioteca.selectLivro(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());

                GDB_Biblioteca.updateLivro(m_livro);

                dataGridView1.DataSource = GDB_Biblioteca.getLivro();

                dataGridView1.CurrentCell = dataGridView1[0, m_row];

                limparCampos();

            }
            else
            {
                MessageBox.Show("Livro não informado!");

                if (textBox1.Text == "")
                {
                    textBox1.Focus();
                }
                else if (textBox2.Text == "")
                {
                    textBox2.Focus();
                }
                else if (textBox3.Text == "")
                {
                    textBox3.Focus();
                }
                else if (textBox4.Text == "")
                {
                    textBox4.Focus();
                }
                else if (textBox5.Text == "")
                {
                    textBox5.Focus();
                }
                else if (textBox6.Text == "")
                {
                    textBox6.Focus();
                }
                else
                {
                    textBox7.Focus();
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView m_data_grid_view = (DataGridView)sender;

            if (m_data_grid_view.SelectedRows.Count > 0)
            {
                DataTable m_data_table = new DataTable();
                m_data_table = GDB_Biblioteca.selectLivro(m_data_grid_view.SelectedRows[0].Cells[0].Value.ToString());

                
                textBox1.Text = m_data_table.Rows[0].Field<string>("Título");
                textBox2.Text = m_data_table.Rows[0].Field<string>("Autores");
                textBox3.Text = m_data_table.Rows[0].Field<Int64>("Edição").ToString();
                textBox4.Text = m_data_table.Rows[0].Field<string>("Editora");
                textBox5.Text = m_data_table.Rows[0].Field<string>("Categoria");
                textBox6.Text = m_data_table.Rows[0].Field<Int64>("Nº Páginas").ToString();
                textBox7.Text = m_data_table.Rows[0].Field<string>("ISBN");
                
            }
        }

        private void AtualizarDadosAcervo_Load(object sender, EventArgs e)
        {
            // Atualizando o data grid view
            dataGridView1.DataSource = GDB_Biblioteca.getLivro();
        }
    }
}
