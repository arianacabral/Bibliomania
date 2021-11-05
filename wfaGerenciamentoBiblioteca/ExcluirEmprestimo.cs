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
    public partial class ExcluirEmprestimo : Form
    {
        int id = -1;

        public ExcluirEmprestimo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ExcluirEmprestimo_Load(object sender, EventArgs e)
        {
            // Atualizando o data grid view
            dataGridView1.DataSource = GDB_Biblioteca.getEmprestimo();
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
            DataTable m_data_table = new DataTable();
            m_data_table = GDB_Biblioteca.selectEmprestimo(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());

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

            DialogResult res = MessageBox.Show("Realmente deseja excluir?", "Excluir dados?", MessageBoxButtons.YesNo);

            if (res == DialogResult.Yes)
            {
                GDB_Biblioteca.deleteEmprestimo(int.Parse(m_data_table.Rows[0].Field<Int64>("id_emprestimo").ToString()));

                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);

                limparCampos();

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
    }
}
