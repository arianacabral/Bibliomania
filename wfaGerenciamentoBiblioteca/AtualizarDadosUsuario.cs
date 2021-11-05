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
    public partial class AtualizarDadosUsuario : Form
    {
        public AtualizarDadosUsuario()
        {
            InitializeComponent();
            habilitarCampos();
        }

        private void AtualizarDadosUsuario_Load(object sender, EventArgs e)
        {
            // Atualizando o data grid view
            dataGridView1.DataSource = GDB_Biblioteca.getLeitor();

            // Alterando as larguras das colunas no data grid view
            dataGridView1.Columns[1].Width = 165;
            dataGridView1.Columns[2].Width = 130;
        }

        private void button1_Click(object sender, EventArgs e) // botão fechar
        {
            Close();
        }

        private void button4_Click(object sender, EventArgs e) // botão cancelar
        {
            limparCampos();
        }

        private void habilitarCampos()
        {
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            textBox6.Enabled = true;

            comboBox1.Enabled = true;

            maskedTextBox1.Enabled = true;
            maskedTextBox2.Enabled = true;
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

        private void button3_Click(object sender, EventArgs e)
        {
            int m_row = dataGridView1.SelectedRows[0].Index; // linha selecionada

            Leitor m_usuario = new Leitor(); // instanciando leitor

            // definindo os dados do leitor selecionado
            m_usuario.setNome(textBox1.Text);
            m_usuario.setCPF(maskedTextBox1.Text);
            m_usuario.setEndereco(textBox2.Text);
            m_usuario.setNumeroResidencia(textBox3.Text);
            m_usuario.setBairro(textBox4.Text);
            m_usuario.setCidade(textBox5.Text);
            m_usuario.setUF(comboBox1.Text);
            m_usuario.setTelefone(maskedTextBox2.Text);
            m_usuario.setEmail(textBox6.Text);

            if (textBox1.Text != "" & textBox2.Text != "" & textBox3.Text != "" & textBox4.Text != "" & textBox5.Text != "" & textBox6.Text != "" &
                comboBox1.Items.Count != 0 &
                maskedTextBox1.Text != "" & maskedTextBox2.Text != "")
            {
                DataTable m_data_table = new DataTable();
                m_data_table = GDB_Biblioteca.selectLeitor(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());

                GDB_Biblioteca.updateLeitor(m_usuario);

                dataGridView1.DataSource = GDB_Biblioteca.getLeitor();

                dataGridView1.CurrentCell = dataGridView1[0, m_row];

                limparCampos();
            }
            else
            {
                MessageBox.Show("Usuário não informado!");

                if (textBox1.Text == "")
                {
                    textBox1.Focus();
                }
                else if (maskedTextBox1.Text == "")
                {
                    maskedTextBox1.Focus();
                }
                else if (maskedTextBox2.Text == "")
                {
                    maskedTextBox2.Focus();
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
                else if (comboBox1.Items.Count == 0)
                {
                    comboBox1.Focus();
                }
                else
                {
                    textBox6.Focus();
                }
            }
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
