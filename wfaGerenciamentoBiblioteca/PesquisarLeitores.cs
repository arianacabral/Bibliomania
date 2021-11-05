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
    public partial class PesquisarLeitores : Form
    {
        public PesquisarLeitores()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if(comboBox1.Items.Count != 0)
            {
                if(comboBox1.Text == "Todos")
                {
                    textBox1.Clear();
                    dataGridView1.DataSource = GDB_Biblioteca.getLeitor();
                }
                else
                {
                    if(textBox1.Text != "")
                    {
                        switch (comboBox1.Text)
                        {
                            case "Nome":
                                dataGridView1.DataSource = GDB_Biblioteca.selectLeitorByNome(textBox1.Text);
                                break;
                            case "UF":
                                if(textBox1.Text.Length != 2)
                                {
                                    MessageBox.Show("Informe apenas a sigla da UF!");
                                }
                                else
                                {
                                    dataGridView1.DataSource = GDB_Biblioteca.selectLeitorByUF(textBox1.Text.ToUpper());
                                }

                                break;
                            default:
                                if(textBox1.Text.Length == 11)
                                {
                                    dataGridView1.DataSource = GDB_Biblioteca.selectLeitorByCPF(textBox1.Text);
                                }
                                else if(textBox1.Text.Length == 11)
                                {
                                    MessageBox.Show("Informe o CPF sem pontos e hífen! \nExemplo: 00000000000");
                                }
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
