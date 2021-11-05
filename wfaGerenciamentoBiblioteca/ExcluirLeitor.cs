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
    public partial class ExcluirLeitor : Form
    {
        public ExcluirLeitor()
        {
            InitializeComponent();
        }

        private void ExcluirLeitor_Load(object sender, EventArgs e)
        {
            desabilitarCampos();

            // Atualizando o data grid view
            dataGridView1.DataSource = GDB_Biblioteca.getLeitor();

            // Alterando as larguras das colunas no data grid view
            dataGridView1.Columns[1].Width = 165;
            dataGridView1.Columns[2].Width = 130;
        }

        private void desabilitarCampos()
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;

            comboBox1.Enabled = false;

            maskedTextBox1.Enabled = false;
            maskedTextBox2.Enabled = false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataTable m_data_table = new DataTable();
            m_data_table = GDB_Biblioteca.selectLeitor(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());

            textBox1.Text = m_data_table.Rows[0].Field<string>("nome");
            maskedTextBox1.Text = m_data_table.Rows[0].Field<string>("cpf");
            textBox2.Text = m_data_table.Rows[0].Field<string>("endereco");
            textBox3.Text = m_data_table.Rows[0].Field<string>("numero");
            textBox4.Text = m_data_table.Rows[0].Field<string>("bairro");
            textBox5.Text = m_data_table.Rows[0].Field<string>("cidade");
            comboBox1.Text = m_data_table.Rows[0].Field<string>("uf");
            maskedTextBox2.Text = m_data_table.Rows[0].Field<string>("telefone");
            textBox6.Text = m_data_table.Rows[0].Field<string>("email");

            DialogResult res = MessageBox.Show("Realmente deseja excluir?", "Excluir dados?", MessageBoxButtons.YesNo);

            if (res == DialogResult.Yes)
            {
                GDB_Biblioteca.deleteLeitor(m_data_table.Rows[0].Field<string>("cpf"));

                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);

                limparCampos();

            }
        }

        private void limparCampos()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();

            comboBox1.SelectedIndex = -1;

            maskedTextBox1.Clear();
            maskedTextBox2.Clear();

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView m_data_grid_view = (DataGridView)sender;

            if (m_data_grid_view.SelectedRows.Count > 0)
            {
                DataTable m_data_table = new DataTable();
                m_data_table = GDB_Biblioteca.selectLeitor(m_data_grid_view.SelectedRows[0].Cells[0].Value.ToString());

                textBox1.Text = m_data_table.Rows[0].Field<string>("nome");
                maskedTextBox1.Text = m_data_table.Rows[0].Field<string>("cpf");
                textBox2.Text = m_data_table.Rows[0].Field<string>("endereco");
                textBox3.Text = m_data_table.Rows[0].Field<string>("numero");
                textBox4.Text = m_data_table.Rows[0].Field<string>("bairro");
                textBox5.Text = m_data_table.Rows[0].Field<string>("cidade");
                comboBox1.Text = m_data_table.Rows[0].Field<string>("uf");
                maskedTextBox2.Text = m_data_table.Rows[0].Field<string>("telefone");
                textBox6.Text = m_data_table.Rows[0].Field<string>("email");

            }
        }
    }
}
