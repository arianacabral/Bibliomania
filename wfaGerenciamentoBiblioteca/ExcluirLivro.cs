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
    public partial class ExcluirLivro : Form
    {
        public ExcluirLivro()
        {
            InitializeComponent();
            desabilitarCampos();
        }
        private void desabilitarCampos()
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;
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

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataTable m_data_table = new DataTable();
            m_data_table = GDB_Biblioteca.selectLivro(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());

            textBox1.Text = m_data_table.Rows[0].Field<string>("Título");
            textBox2.Text = m_data_table.Rows[0].Field<string>("Autores");
            textBox3.Text = m_data_table.Rows[0].Field<Int64>("Edição").ToString();
            textBox4.Text = m_data_table.Rows[0].Field<string>("Editora");
            textBox5.Text = m_data_table.Rows[0].Field<string>("Categoria");
            textBox6.Text = m_data_table.Rows[0].Field<Int64>("Nº Páginas").ToString();
            textBox7.Text = m_data_table.Rows[0].Field<string>("ISBN");

            DialogResult res = MessageBox.Show("Realmente deseja excluir?", "Excluir dados?", MessageBoxButtons.YesNo);

            if (res == DialogResult.Yes)
            {
                GDB_Biblioteca.deleteLivro(m_data_table.Rows[0].Field<string>("ISBN"));

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

        private void ExcluirLivro_Load(object sender, EventArgs e)
        {
            // Atualizando o data grid view
            dataGridView1.DataSource = GDB_Biblioteca.getLivro();
        }
    }
}
