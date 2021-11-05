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
    public partial class CadastrarLeitor : Form
    {
        public CadastrarLeitor()
        {
            InitializeComponent();
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

        private void button4_Click(object sender, EventArgs e) // botão Cancelar
        {
            limparCampos();
        }

        private void button3_Click(object sender, EventArgs e) // Botão Salvar
        {
            if (textBox1.Text != "" & textBox2.Text != "" & textBox3.Text != "" & textBox4.Text != "" & textBox5.Text != "" & textBox6.Text != "" &
                comboBox1.Items.Count != 0 &
                maskedTextBox1.Text != "" & maskedTextBox2.Text != "")
            {
                Leitor m_usuario = new Leitor();

                m_usuario.setNome(textBox1.Text);
                m_usuario.setCPF(maskedTextBox1.Text);
                m_usuario.setEndereco(textBox2.Text);
                m_usuario.setNumeroResidencia(textBox3.Text);
                m_usuario.setBairro(textBox4.Text);
                m_usuario.setCidade(textBox5.Text);
                m_usuario.setUF(comboBox1.Text);
                m_usuario.setTelefone(maskedTextBox2.Text);
                m_usuario.setEmail(textBox6.Text);

                GDB_Biblioteca.insertLeitor(m_usuario);
                limparCampos();

            }
            else
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Informar Nome do usuário!");
                    textBox1.Focus();
                }
                else if (maskedTextBox1.Text == "")
                {
                    MessageBox.Show("Informar CPF do usuário!");
                    maskedTextBox1.Focus();
                }
                else if (textBox2.Text == "")
                {
                    MessageBox.Show("Informar a Rua/Avenida!");
                    textBox2.Focus();
                }
                else if (textBox3.Text == "")
                {
                    MessageBox.Show("Informar o Número da Residência!");
                    textBox3.Focus();
                }
                else if (textBox4.Text == "")
                {
                    MessageBox.Show("Informar o Bairro!");
                    textBox4.Focus();
                }
                else if (textBox5.Text == "")
                {
                    MessageBox.Show("Informar a Cidade!");
                    textBox5.Focus();
                }
                else if (comboBox1.Items.Count == 0)
                {
                    MessageBox.Show("Informar o Estado!");
                    comboBox1.Focus();
                }
                else if (maskedTextBox2.Text == "")
                {
                    MessageBox.Show("Informar Telefone de contato do usuário!");
                    maskedTextBox2.Focus();
                }
                else
                {
                    MessageBox.Show("Informar o e-mail do usuário!");
                    textBox6.Focus();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) // botão fechar
        {
            Close();
        }
    }
}
