using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace wfaGerenciamentoBiblioteca
{
    public partial class CadastrarEmprestimo : Form
    {
        Leitor leitor = new Leitor();
        Acervo livro = new Acervo();
        DadosEmprestimo emprestimo = new DadosEmprestimo();

        // Relatório em PDF

        bool relatorio;

        String strPath;
        FolderBrowserDialog objFBD;
        Document doc;
        string dados;
        Paragraph paragrafo_leitor;
        Paragraph paragrafo_livro;

        public CadastrarEmprestimo()
        {
            InitializeComponent();

            relatorio = false;
            
            generateDate();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "" & textBox1.Text != "")
            {
                switch (comboBox1.Text)
                {
                    case "Nome":
                        dataGridView1.DataSource = GDB_Biblioteca.selectLeitorByNome(textBox1.Text);
                        break;
                    default:
                        if (textBox1.Text.Length == 11)
                        {
                            dataGridView1.DataSource = GDB_Biblioteca.selectLeitorByCPF(textBox1.Text);
                        }
                        else if (textBox1.Text.Length != 11)
                        {
                            MessageBox.Show("Informe o CPF sem pontos e hífen! \nExemplo: 00000000000");
                        }
                        break;
                }
            }
            else
            {
                if (comboBox1.Text == "")
                {
                    MessageBox.Show("Informar o tipo de busca!");
                    comboBox1.Focus();
                }
                else
                {
                    MessageBox.Show("Informar o item de busca!");
                    textBox1.Focus();
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

                leitor.setNome(m_data_table.Rows[0].Field<string>("nome"));
                leitor.setCPF(m_data_table.Rows[0].Field<string>("cpf"));
                leitor.setEndereco(m_data_table.Rows[0].Field<string>("endereco"));
                leitor.setNumeroResidencia(m_data_table.Rows[0].Field<string>("numero"));
                leitor.setBairro(m_data_table.Rows[0].Field<string>("bairro"));
                leitor.setCidade(m_data_table.Rows[0].Field<string>("cidade"));
                leitor.setUF(m_data_table.Rows[0].Field<string>("uf"));
                leitor.setTelefone(m_data_table.Rows[0].Field<string>("telefone"));
                leitor.setEmail(m_data_table.Rows[0].Field<string>("email"));

                emprestimo.setLeitor(leitor);

                textBox2.Text = " Nome: " + emprestimo.getLeitor().getNome() + " - CPF: " + emprestimo.getLeitor().getCPF();

                limparCampos();
                comboBox2.Focus();

            }
        }

        private void limparCampos()
        {
            comboBox1.SelectedIndex = -1;
            textBox1.Clear();
            comboBox2.SelectedIndex = -1;
            textBox3.Clear();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text != "" & textBox3.Text != "")
            {
                switch (comboBox2.Text)
                {
                    case "Título":
                        dataGridView2.DataSource = GDB_Biblioteca.selectLivroByTitulo(textBox3.Text);
                        limparCampos();
                        break;
                    case "Autor(es)":
                        dataGridView2.DataSource = GDB_Biblioteca.selectLivroByAutor(textBox3.Text);
                        limparCampos();
                        break;
                    case "Editora":
                        dataGridView2.DataSource = GDB_Biblioteca.selectLivroByEditora(textBox3.Text);
                        limparCampos();
                        break;
                    default:
                        dataGridView2.DataSource = GDB_Biblioteca.selectLivroByISBN(textBox3.Text);
                        limparCampos();
                        break;
                }
            }
            else
            {
                if (comboBox2.Text == "")
                {
                    MessageBox.Show("Informar o tipo de busca!");
                    comboBox2.Focus();
                }
                else
                {
                    MessageBox.Show("Informar o item de busca!");
                    textBox3.Focus();
                }
            }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView m_data_grid_view = (DataGridView)sender;

            if (m_data_grid_view.SelectedRows.Count > 0)
            {
                DataTable m_data_table = new DataTable();
                m_data_table = GDB_Biblioteca.selectLivro(m_data_grid_view.SelectedRows[0].Cells[0].Value.ToString());

                int m_row = dataGridView2.SelectedRows[0].Index; // linha selecionada

                livro.setTitulo(m_data_table.Rows[0].Field<string>("Título"));
                livro.setAutores(m_data_table.Rows[0].Field<string>("Autores"));
                livro.setEdicao(int.Parse(m_data_table.Rows[0].Field<Int64>("Edição").ToString()));
                livro.setEditora(m_data_table.Rows[0].Field<string>("Editora"));
                livro.setCategoria(m_data_table.Rows[0].Field<string>("Categoria"));
                livro.setNumPaginas(int.Parse(m_data_table.Rows[0].Field<Int64>("Nº Páginas").ToString()));
                livro.setISBN(m_data_table.Rows[0].Field<string>("ISBN"));

                emprestimo.setLivro(livro);

                textBox4.Text = " Título: " + emprestimo.getLivro().getTitulo() + " - ISBN: " +
                    emprestimo.getLivro().getISBN() + " - Autor(es): " + emprestimo.getLivro().getAutores();

                limparCampos();
            }
        }


        private void generateDate()
        {
            textBox5.Text = emprestimo.getDataEmprestimo();
            textBox6.Text = emprestimo.getDataDevolucao();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (emprestimo.getLeitor().getNome() != "" && emprestimo.getLivro().getISBN() != "")
            {
                GDB_Biblioteca.insertEmprestimo(emprestimo);
                limparCampos();
                relatorio = true;
            }
            else
            {
                MessageBox.Show("Inserir dados do empréstimo!");
                comboBox1.Focus();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(relatorio)
            {
                // Gerando atestado de solicitação de matrícula

                objFBD = new FolderBrowserDialog();

                objFBD.ShowDialog();

                strPath = objFBD.SelectedPath + @"\Comprovante_de_emprestimo.pdf"; // caminho onde será salvo o arquivo 

                doc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);//criando e estipulando o tipo da folha usada
                doc.SetMargins(40, 40, 40, 80);//estibulando o espaçamento das margens que queremos
                doc.AddCreationDate();//adicionando as configuracoes

                //criando o arquivo pdf em branco, passando como parâmetro a variável doc criada acima e a variavel caminho
                //tambem criada acima.
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(strPath, FileMode.Create));

                doc.Open();

                // imagem 
                string strpath = System.Environment.CurrentDirectory.Replace("\\bin\\Debug", "\\src");
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(strpath + @"\comprovante.png");
                logo.ScaleToFit(420f, 360f);
                logo.SetAbsolutePosition(110f, 700f);
                logo.SpacingAfter = 1f;
                logo.SpacingBefore = 10f;
                logo.Alignment = Element.ALIGN_LEFT;

                dados = "";

                // paragráfo com os dados do leitor
                paragrafo_leitor = new Paragraph(dados, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, (int)System.Drawing.FontStyle.Bold));
                //etipulando o alinhamneto
                paragrafo_leitor.Alignment = Element.ALIGN_JUSTIFIED;
                //adicioando texto
                paragrafo_leitor.Add("\n\n\n\n\n\n\n\n\nIdentificação\n");
                // modificando a fonte 
                paragrafo_leitor.Font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, (int)System.Drawing.FontStyle.Regular);
                //adicioando texto
                paragrafo_leitor.Add("Nome: " + emprestimo.getLeitor().getNome() + "\n" + "CPF: " + emprestimo.getLeitor().getCPF() +
                    "\n" + "Telefone de contato: " + emprestimo.getLeitor().getTelefone() + "\n" + "e-mail: " + emprestimo.getLeitor().getEmail() + "\n\n");


                //criando o paragráfo com os dados do livro
                paragrafo_livro = new Paragraph(dados, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, (int)System.Drawing.FontStyle.Bold));
                //etipulando o alinhamneto
                paragrafo_livro.Alignment = Element.ALIGN_JUSTIFIED;
                //adicioando texto
                paragrafo_livro.Add("\n\nItens emprestados\n");
                // modificando a fonte 
                paragrafo_livro.Font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, (int)System.Drawing.FontStyle.Regular);
                //adicioando texto
                paragrafo_livro.Add("Título: " + emprestimo.getLivro().getTitulo() + "\n" + "ISBN: " + emprestimo.getLivro().getISBN() + "\n" +
                    "Autor(es): " + emprestimo.getLivro().getAutores() + "\n\n" +
                    "Data do empréstimo: " + emprestimo.getDataEmprestimo() + "\n" + "Data de devolução/Renovação: " + 
                    emprestimo.getDataDevolucao() +
                    "\n\n\n\n" + String.Format("Documento gerado em {0}.", DateTime.Now));

                // Acidionado itens ao documento
                doc.Add(logo);
                doc.Add(paragrafo_leitor);
                doc.Add(paragrafo_livro);

                //fechando documento para que as alterações sejam salvas
                doc.Close();

                MessageBox.Show("Comprovanto salvo!");

            }
            else
            {
                MessageBox.Show("Solicitar empréstimo!");
            }
        }

    }
}
