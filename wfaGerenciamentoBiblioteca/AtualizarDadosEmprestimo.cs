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
    public partial class AtualizarDadosEmprestimo : Form
    {
        int id = -1;

        public AtualizarDadosEmprestimo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
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

            maskedTextBox1.Clear();
            maskedTextBox2.Clear();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            limparCampos();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int m_row = dataGridView1.SelectedRows[0].Index; // linha selecionada

            Leitor m_usuario = new Leitor(); // instanciando leitor

            DataTable leitor = GDB_Biblioteca.selectLeitor(maskedTextBox1.Text); 

            // definindo os dados do leitor selecionado
            m_usuario.setNome(leitor.Rows[0].Field<string>("nome"));
            m_usuario.setCPF(leitor.Rows[0].Field<string>("cpf"));
            m_usuario.setEndereco(leitor.Rows[0].Field<string>("endereco"));
            m_usuario.setNumeroResidencia(leitor.Rows[0].Field<string>("numero"));
            m_usuario.setBairro(leitor.Rows[0].Field<string>("bairro"));
            m_usuario.setCidade(leitor.Rows[0].Field<string>("cidade"));
            m_usuario.setUF(leitor.Rows[0].Field<string>("uf"));
            m_usuario.setTelefone(leitor.Rows[0].Field<string>("telefone"));
            m_usuario.setEmail(leitor.Rows[0].Field<string>("email"));

            Acervo m_livro = new Acervo(); // instanciando livro

            DataTable livro = GDB_Biblioteca.selectLivro(textBox6.Text);

            // definindo os dados do livro selecionado
            m_livro.setTitulo(livro.Rows[0].Field<string>("Título"));
            m_livro.setISBN(livro.Rows[0].Field<string>("ISBN"));
            m_livro.setAutores(livro.Rows[0].Field<string>("Autores"));
            m_livro.setEditora(livro.Rows[0].Field<string>("Editora"));
            m_livro.setEdicao(int.Parse(livro.Rows[0].Field<Int64>("Edição").ToString()));
            m_livro.setCategoria(livro.Rows[0].Field<string>("Categoria"));
            m_livro.setNumPaginas(int.Parse(livro.Rows[0].Field<Int64>("Nº Páginas").ToString()));

            // definindo os dados do livro selecionado
            m_livro.setTitulo(textBox1.Text);
            m_livro.setISBN(textBox6.Text);

            DadosEmprestimo m_emprestimo = new DadosEmprestimo();

            m_emprestimo.setLeitor(m_usuario);
            m_emprestimo.setLivro(m_livro);

            // validando a data de devolução 

            if(textBox3.Text != "")
            {
                if ((DateTime.Parse(textBox3.Text) < DateTime.Parse(m_emprestimo.setToday())) | (DateTime.Parse(textBox3.Text) < DateTime.Parse(m_emprestimo.set7dias())))
                {
                    if(DateTime.Parse(textBox3.Text) < DateTime.Parse(m_emprestimo.setToday()))
                    {
                        MessageBox.Show("Data inválida!");
                    }
                    else
                    {
                        MessageBox.Show("Devolução prevista para menos de 7 dias úteis!");
                    }

                    textBox3.Text = m_emprestimo.set7dias();
                    textBox3.Focus();
                    return;
                }
                else
                {
                    m_emprestimo.setDataDevolucao(textBox3.Text);
                }

            }

            // validando a data de empréstimo

            if (textBox4.Text != "")
            {
                if (DateTime.Parse(textBox4.Text) != DateTime.Parse(m_emprestimo.setToday()))
                {
                    MessageBox.Show("Data inválida!");

                    textBox4.Text = m_emprestimo.setToday();
                    textBox4.Focus();
                    return;
                }
                else
                {
                    m_emprestimo.setDataEmprestimo(textBox4.Text);
                }
         
            }

            if (textBox1.Text != "" & textBox2.Text != "" &
                textBox5.Text != "" & textBox6.Text != "")
            {
                if(id != -1)
                {
                    DataTable m_data_table = new DataTable();
                    m_data_table = GDB_Biblioteca.selectEmprestimo(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());

                    GDB_Biblioteca.updateEmprestimo(m_emprestimo, id);

                    dataGridView1.DataSource = GDB_Biblioteca.getEmprestimo();

                    dataGridView1.CurrentCell = dataGridView1[0, m_row];

                    limparCampos();
                }
                else
                {
                    MessageBox.Show("Dados do empréstimo não informados!");
                }

            }
            else
            {
                MessageBox.Show("Dados do empréstimo não informados!");

                if(textBox1.Text == "")
                {
                    MessageBox.Show("Informar o título do livro!");
                    textBox1.Focus();
                }
                else if (textBox6.Text == "")
                {
                    MessageBox.Show("Informar o ISBN do livro!");
                    textBox6.Focus();
                }
                else if (textBox2.Text == "")
                {
                    MessageBox.Show("Informar o nome do leitor!");
                    textBox2.Focus();
                }
                else if (textBox5.Text == "")
                {
                    MessageBox.Show("Informar o e-mail do leitor!");
                    textBox5.Focus();
                }
                else if (textBox3.Text == "")
                {
                    MessageBox.Show("Informar a data de empréstimo!");
                    textBox3.Focus();
                }
                else if (textBox4.Text == "")
                {
                    MessageBox.Show("Informar a data de devolução!");
                    textBox4.Focus();
                }
                else if (maskedTextBox1.Text == "")
                {
                    MessageBox.Show("Informar o CPF do leitor!");
                    maskedTextBox1.Focus();
                }
                else
                {
                    MessageBox.Show("Informar o telefone do leitor!");
                    maskedTextBox2.Focus();
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView m_data_grid_view = (DataGridView)sender;

            if (m_data_grid_view.SelectedRows.Count > 0)
            {
                DataTable m_data_table = new DataTable();
                m_data_table = GDB_Biblioteca.selectEmprestimo(m_data_grid_view.SelectedRows[0].Cells[0].Value.ToString());

                id = int.Parse(m_data_table.Rows[0].Field<Int64>("id_emprestimo").ToString());

                DataTable leitor = GDB_Biblioteca.selectLeitor(m_data_table.Rows[0].Field<string>("cpf"));
                DataTable livro = GDB_Biblioteca.selectLivro(m_data_table.Rows[0].Field<string>("ISBN"));

                textBox1.Text = livro.Rows[0].Field<string>("Título");
                textBox6.Text = m_data_table.Rows[0].Field<string>("ISBN");
                textBox2.Text = leitor.Rows[0].Field<string>("nome");
                maskedTextBox1.Text = m_data_table.Rows[0].Field<string>("cpf");
                maskedTextBox2.Text = leitor.Rows[0].Field<string>("telefone");
                textBox5.Text = leitor.Rows[0].Field<string>("email");
                textBox4.Text = m_data_table.Rows[0].Field<string>("data_emprestimo");
                textBox3.Text = m_data_table.Rows[0].Field<string>("data_devolucao");
                textBox7.Text = m_data_table.Rows[0].Field<Int64>("id_emprestimo").ToString();
                
            }
        }

        private void AtualizarDadosEmprestimo_Load(object sender, EventArgs e)
        {
            // Atualizando o data grid view
            dataGridView1.DataSource = GDB_Biblioteca.getEmprestimo();
        }

    }
}
