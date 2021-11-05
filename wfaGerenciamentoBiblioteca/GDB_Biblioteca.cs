using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace wfaGerenciamentoBiblioteca
{
    class GDB_Biblioteca
    {
        // CONEXÃO COM O BANCO DE DADOS
        private static SQLiteConnection conexao;

        // nome do banco de dados
        private static string nameBD = "database_biblioteca.db";

        // diretório de trabalho
        private static string strpath = System.Environment.CurrentDirectory;

        // caminho com o nome do arquivo
        private static string strpathBD = strpath + @"\database\" + nameBD;

        // método para fazer a conexão com o banco de dados
        private static SQLiteConnection conexaoBancoDados()
        {
            conexao = new SQLiteConnection("Data Source = " + strpathBD); // declarando e instanciando a conexão com o banco de dados

            conexao.Open(); // fazendo a conexão com o banco de dados

            return (conexao);
        }

        // método INSERT

        public static void insertLeitor(Leitor leitor)
        {
            if (existeCPF(leitor))
            {
                MessageBox.Show("Usuário já cadastrado!");
                return;
            }

            try
            {
                var cmd = conexaoBancoDados().CreateCommand();
                cmd.CommandText = "INSERT INTO leitores (CPF, nome, telefone, email, endereco, numero, bairro, cidade, uf) VALUES (@cpf, @nome, @telefone, @email, @endereco, @numero, @bairro, @cidade, @uf)";

                cmd.Parameters.AddWithValue("@cpf", leitor.getCPF());
                cmd.Parameters.AddWithValue("@nome", leitor.getNome());
                cmd.Parameters.AddWithValue("@telefone", leitor.getTelefone());
                cmd.Parameters.AddWithValue("@email", leitor.getEmail());
                cmd.Parameters.AddWithValue("@endereco", leitor.getEndereco());
                cmd.Parameters.AddWithValue("@numero", leitor.getNumeroResidencia());
                cmd.Parameters.AddWithValue("@bairro", leitor.getBairro());
                cmd.Parameters.AddWithValue("@cidade", leitor.getCidade());
                cmd.Parameters.AddWithValue("@uf", leitor.getUF());

                cmd.ExecuteNonQuery();

                MessageBox.Show("Novo usuário cadastrado!");

                conexaoBancoDados().Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao inserir novo usuário!");
                throw ex;
            }
        }

        public static void insertLivro(Acervo livro)
        {
            if (existeISBN(livro))
            {
                MessageBox.Show("Livro já cadastrado!");
                return;
            }

            try
            {
                var cmd = conexaoBancoDados().CreateCommand();
                cmd.CommandText = "INSERT INTO livros (isbn, titulo, categoria, autores, editora, edicao, npags) VALUES (@isbn, @titulo, @categoria, @autores, @editora, @edicao, @npags)";

                cmd.Parameters.AddWithValue("@isbn", livro.getISBN());
                cmd.Parameters.AddWithValue("@titulo", livro.getTitulo());
                cmd.Parameters.AddWithValue("@categoria", livro.getCategoria());
                cmd.Parameters.AddWithValue("@autores", livro.getAutores());
                cmd.Parameters.AddWithValue("@editora", livro.getEditora());
                cmd.Parameters.AddWithValue("@edicao", livro.getEdicao());
                cmd.Parameters.AddWithValue("@npags", livro.getNumPaginas());


                cmd.ExecuteNonQuery();

                MessageBox.Show("Novo livro adicionado ao acervo!");

                conexaoBancoDados().Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao inserir livro ao acervo!");
                throw ex;
            }
        }

        public static void insertEmprestimo(DadosEmprestimo emprestimo)
        {
            if (existeEmprestimo(emprestimo))
            {
                MessageBox.Show("Empréstimo já cadastrado!");
                return;
            }


            try
            {
                var cmd = conexaoBancoDados().CreateCommand();
                cmd.CommandText = "INSERT INTO emprestimos (ISBN,cpf,data_emprestimo,data_devolucao) VALUES (@isbn, @cpf, @data_emprestimo, @data_devolucao)";

                cmd.Parameters.AddWithValue("@isbn", emprestimo.getLivro().getISBN());
                cmd.Parameters.AddWithValue("@cpf", emprestimo.getLeitor().getCPF());
                cmd.Parameters.AddWithValue("@data_emprestimo", emprestimo.getDataEmprestimo());
                cmd.Parameters.AddWithValue("@data_devolucao", emprestimo.getDataDevolucao());

                cmd.ExecuteNonQuery();

                MessageBox.Show("Empréstimo realizado com sucesso!");

                conexaoBancoDados().Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar empréstimo!");
                throw ex;
            }
        }

        // função para validar CPF informado
        public static bool existeCPF(Leitor leitor)
        {
            SQLiteDataAdapter data_adapter = null;
            DataTable data_table = new DataTable();

            // buscando CPF no banco de dados
            var cmd = conexaoBancoDados().CreateCommand();
            cmd.CommandText = "SELECT CPF FROM leitores WHERE CPF = '" + leitor.getCPF() + "'";

            // verificando o número de retornos da QUERY
            data_adapter = new SQLiteDataAdapter(cmd.CommandText, conexaoBancoDados());

            data_adapter.Fill(data_table);

            if (data_table.Rows.Count > 0)
            {
                conexaoBancoDados().Close();
                return (true);
            }
            else
            {
                conexaoBancoDados().Close();
                return (false);
            }
        }

        // função para validar livro informado
        public static bool existeISBN(Acervo livro)
        {
            SQLiteDataAdapter data_adapter = null;
            DataTable data_table = new DataTable();

            // buscando ISBN no banco de dados
            var cmd = conexaoBancoDados().CreateCommand();
            cmd.CommandText = "SELECT ISBN FROM livros WHERE ISBN = '" + livro.getISBN() + "'";

            // verificando o número de retornos da QUERY
            data_adapter = new SQLiteDataAdapter(cmd.CommandText, conexaoBancoDados());

            data_adapter.Fill(data_table);

            if (data_table.Rows.Count > 0)
            {
                conexaoBancoDados().Close();
                return (true);
            }
            else
            {
                conexaoBancoDados().Close();
                return (false);
            }
        }

        // função para validar empréstimo informado
        public static bool existeEmprestimo(DadosEmprestimo emprestimo)
        {
            SQLiteDataAdapter data_adapter = null;
            DataTable data_table = new DataTable();

            // buscando ISBN no banco de dados
            var cmd = conexaoBancoDados().CreateCommand();

            cmd.CommandText = "SELECT * FROM emprestimos WHERE ISBN = " + emprestimo.getLivro().getISBN() + " AND CPF = '" + emprestimo.getLeitor().getCPF() + "'";

            // verificando o número de retornos da QUERY
            data_adapter = new SQLiteDataAdapter(cmd.CommandText, conexaoBancoDados());

            data_adapter.Fill(data_table);

            if (data_table.Rows.Count > 0)
            {
                conexaoBancoDados().Close();
                return (true);
            }
            else
            {
                conexaoBancoDados().Close();
                return (false);
            }
        }

        // método SELECT * FROM table
        public static DataTable getLeitor()
        {
            SQLiteDataAdapter data_adapter = null;
            DataTable data_table = new DataTable();

            try
            {
                using (var cmd = conexaoBancoDados().CreateCommand())
                {
                    cmd.CommandText = "SELECT CPF as 'CPF', nome as 'Nome', email as 'e-mail', telefone as 'Telefone', endereco as 'Av./Rua', numero as 'Número', bairro as 'Bairro', cidade as 'Cidade', uf as 'UF' FROM leitores";

                    data_adapter = new SQLiteDataAdapter(cmd.CommandText, conexaoBancoDados());

                    data_adapter.Fill(data_table);

                    conexaoBancoDados().Close();

                    return (data_table);
                }
            }
            catch (Exception ex)
            {
                conexaoBancoDados().Close();
                throw ex;
            }

        }

        public static DataTable getLivro()
        {
            SQLiteDataAdapter data_adapter = null;
            DataTable data_table = new DataTable();

            try
            {
                using (var cmd = conexaoBancoDados().CreateCommand())
                {
                    cmd.CommandText = "SELECT isbn as 'ISBN', titulo as 'Título', categoria as 'Categoria', autores as 'Autor(es)', editora as 'Editora', edicao as 'Edição', npags as 'Nº Páginas' FROM livros ORDER BY titulo ASC";

                    data_adapter = new SQLiteDataAdapter(cmd.CommandText, conexaoBancoDados());

                    data_adapter.Fill(data_table);

                    conexaoBancoDados().Close();

                    return (data_table);
                }
            }
            catch (Exception ex)
            {
                conexaoBancoDados().Close();
                throw ex;
            }

        }

        public static DataTable getEmprestimo()
        {
            SQLiteDataAdapter data_adapter = null;
            DataTable data_table = new DataTable();

            try
            {
                using (var cmd = conexaoBancoDados().CreateCommand())
                {
                    cmd.CommandText = "SELECT emprestimos.id_emprestimo as 'ID Empréstimo', emprestimos.ISBN as 'ISBN', livros.titulo as 'Título', emprestimos.cpf as 'CPF', leitores.nome as 'Nome', leitores.telefone as 'Telefone', leitores.email as 'e-mail', data_emprestimo as 'Data de Empréstimo', data_devolucao as 'Data de Devolução' FROM emprestimos INNER JOIN leitores ON leitores.CPF = emprestimos.cpf, livros ON livros.isbn = emprestimos.ISBN ORDER BY id_emprestimo";

                    data_adapter = new SQLiteDataAdapter(cmd.CommandText, conexaoBancoDados());

                    data_adapter.Fill(data_table);

                    conexaoBancoDados().Close();

                    return (data_table);
                }
            }
            catch (Exception ex)
            {
                conexaoBancoDados().Close();
                throw ex;
            }

        }

        // método SELECT 
        public static DataTable selectLeitor(String CPF)
        {
            SQLiteDataAdapter data_adapter = null;
            DataTable data_table = new DataTable();

            try
            {
                using (var cmd = conexaoBancoDados().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM leitores WHERE CPF = '" + CPF + "'";

                    data_adapter = new SQLiteDataAdapter(cmd.CommandText, conexaoBancoDados());

                    data_adapter.Fill(data_table);

                    conexaoBancoDados().Close();

                    return (data_table);
                }
            }
            catch (Exception ex)
            {
                conexaoBancoDados().Close();
                throw ex;
            }

        }

        public static DataTable selectLivro(string ISBN)
        {
            SQLiteDataAdapter data_adapter = null;
            DataTable data_table = new DataTable();

            try
            {
                using (var cmd = conexaoBancoDados().CreateCommand())
                {
                    cmd.CommandText = "SELECT isbn as 'ISBN', titulo as 'Título', categoria as 'Categoria', autores as 'Autores', editora as 'Editora', edicao as 'Edição', npags as 'Nº Páginas' FROM livros WHERE ISBN =  '" + ISBN + "'";

                    data_adapter = new SQLiteDataAdapter(cmd.CommandText, conexaoBancoDados());

                    data_adapter.Fill(data_table);

                    conexaoBancoDados().Close();

                    return (data_table);
                }
            }
            catch (Exception ex)
            {
                conexaoBancoDados().Close();
                throw ex;
            }

        }

        public static DataTable selectEmprestimo(String id)
        {
            SQLiteDataAdapter data_adapter = null;
            DataTable data_table = new DataTable();

            try
            {
                using (var cmd = conexaoBancoDados().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM emprestimos WHERE id_emprestimo = '" + id + "'";

                    data_adapter = new SQLiteDataAdapter(cmd.CommandText, conexaoBancoDados());

                    data_adapter.Fill(data_table);

                    conexaoBancoDados().Close();

                    return (data_table);
                }
            }
            catch (Exception ex)
            {
                conexaoBancoDados().Close();
                throw ex;
            }

        }

        // método UPDATE
        public static void updateLeitor(Leitor leitor)
        {
            SQLiteDataAdapter data_adapter = null;
            DataTable data_table = new DataTable();

            try
            {
                using (var cmd = conexaoBancoDados().CreateCommand())
                {
                    cmd.CommandText = "UPDATE leitores SET nome = '" + leitor.getNome() +
                         "', telefone ='" +
                         leitor.getTelefone() + "', email = '" + leitor.getEmail() + "', endereco = '" +
                         leitor.getEndereco() + "', numero = '" + leitor.getNumeroResidencia() +
                         "', bairro = '" + leitor.getBairro() + "', cidade = '" + leitor.getCidade() +
                         "', uf = '" + leitor.getUF() + "' WHERE CPF = '" + leitor.getCPF() + "'";

                    data_adapter = new SQLiteDataAdapter(cmd.CommandText, conexaoBancoDados());

                    cmd.ExecuteNonQuery(); // executando a QUERY

                    MessageBox.Show("Dados do usuário atualizados com sucesso!");

                    conexaoBancoDados().Close();

                }
            }
            catch (Exception ex)
            {
                conexaoBancoDados().Close();
                throw ex;
            }

        }

        public static void updateLivro(Acervo livro)
        {
            SQLiteDataAdapter data_adapter = null;
            DataTable data_table = new DataTable();

            try
            {
                using (var cmd = conexaoBancoDados().CreateCommand())
                {
                    cmd.CommandText = "UPDATE livros SET isbn = '" + livro.getISBN() +
                         "', titulo ='" +
                         livro.getTitulo() + "', categoria = '" + livro.getCategoria() + "', autores = '" +
                         livro.getAutores() + "', editora = '" + livro.getEditora() +
                         "', edicao = " + livro.getEdicao() + ", npags = " + livro.getNumPaginas() +
                         " WHERE isbn = '" + livro.getISBN() + "'";

                    data_adapter = new SQLiteDataAdapter(cmd.CommandText, conexaoBancoDados());

                    cmd.ExecuteNonQuery(); // executando a QUERY

                    MessageBox.Show("Dados do livro atualizados com sucesso!");

                    conexaoBancoDados().Close();

                }
            }
            catch (Exception ex)
            {
                conexaoBancoDados().Close();
                throw ex;
            }

        }

        public static void updateEmprestimo(DadosEmprestimo emprestimo, int id)
        {
            if (!existeISBN(emprestimo.getLivro()) | !existeCPF(emprestimo.getLeitor()))
            {
                if (!existeISBN(emprestimo.getLivro()))
                {
                    MessageBox.Show("Livro não pertencente ao acervo!");
                }
                else
                {
                    MessageBox.Show("Usuário não cadastrado!");
                }

                return;
            }

            SQLiteDataAdapter data_adapter = null;
            DataTable data_table = new DataTable();

            try
            {
                using (var cmd = conexaoBancoDados().CreateCommand())
                {
                    cmd.CommandText = "UPDATE emprestimos SET ISBN = '" + emprestimo.getLivro().getISBN() + "', " +
                        "cpf = '" + emprestimo.getLeitor().getCPF() + "', " +
                        "data_emprestimo = '" + emprestimo.getDataEmprestimo() + "', " + 
                        "data_devolucao = '" + emprestimo.getDataDevolucao() + "' WHERE id_emprestimo = '" + id + "'";

                    data_adapter = new SQLiteDataAdapter(cmd.CommandText, conexaoBancoDados());

                    cmd.ExecuteNonQuery(); // executando a QUERY

                    MessageBox.Show("Dados do empréstimo atualizados com sucesso!");

                    conexaoBancoDados().Close();

                }
            }
            catch (Exception ex)
            {
                conexaoBancoDados().Close();
                throw ex;
            }

        }

        // método DELETE
        public static void deleteLeitor(string cpf)
        {
            SQLiteDataAdapter data_adapter = null;
            DataTable data_table = new DataTable();

            try
            {
                using (var cmd = conexaoBancoDados().CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM leitores WHERE CPF = '" + cpf + "'";

                    data_adapter = new SQLiteDataAdapter(cmd.CommandText, conexaoBancoDados());

                    cmd.ExecuteNonQuery(); // executando a QUERY

                    MessageBox.Show("Dados excluídos com sucesso!");

                    conexaoBancoDados().Close();

                }
            }
            catch (Exception ex)
            {
                conexaoBancoDados().Close();
                throw ex;
            }

        }

        public static void deleteLivro(string isbn)
        {
            SQLiteDataAdapter data_adapter = null;
            DataTable data_table = new DataTable();

            try
            {
                using (var cmd = conexaoBancoDados().CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM livros WHERE isbn = '" + isbn + "'";

                    data_adapter = new SQLiteDataAdapter(cmd.CommandText, conexaoBancoDados());

                    cmd.ExecuteNonQuery(); // executando a QUERY

                    MessageBox.Show("Dados excluídos com sucesso!");

                    conexaoBancoDados().Close();

                }
            }
            catch (Exception ex)
            {
                conexaoBancoDados().Close();
                throw ex;
            }

        }

        public static void deleteEmprestimo(int id)
        {
            SQLiteDataAdapter data_adapter = null;
            DataTable data_table = new DataTable();

            try
            {
                using (var cmd = conexaoBancoDados().CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM emprestimos WHERE id_emprestimo = " + id;

                    data_adapter = new SQLiteDataAdapter(cmd.CommandText, conexaoBancoDados());

                    cmd.ExecuteNonQuery(); // executando a QUERY

                    MessageBox.Show("Dados excluídos com sucesso!");

                    conexaoBancoDados().Close();

                }
            }
            catch (Exception ex)
            {
                conexaoBancoDados().Close();
                throw ex;
            }

        }

        // método para pesquisar pelo CPF
        public static DataTable selectLeitorByCPF(String CPF)
        {
            SQLiteDataAdapter data_adapter = null;
            DataTable data_table = new DataTable();

            try
            {
                using (var cmd = conexaoBancoDados().CreateCommand())
                {
                    cmd.CommandText = "SELECT CPF as 'CPF', nome as 'Nome', email as 'e-mail', telefone as 'Telefone', endereco as 'Av./Rua', numero as 'Número', bairro as 'Bairro', cidade as 'Cidade', uf as 'UF' FROM leitores WHERE CPF LIKE '%" + CPF + "%'";

                    data_adapter = new SQLiteDataAdapter(cmd.CommandText, conexaoBancoDados());

                    data_adapter.Fill(data_table);

                    conexaoBancoDados().Close();

                    return (data_table);
                }
            }
            catch (Exception ex)
            {
                conexaoBancoDados().Close();
                throw ex;
            }

        }

        // método para pesquisar pelo Nome
        public static DataTable selectLeitorByNome(string nome)
        {
            SQLiteDataAdapter data_adapter = null;
            DataTable data_table = new DataTable();

            try
            {
                using (var cmd = conexaoBancoDados().CreateCommand())
                {
                    cmd.CommandText = "SELECT CPF as 'CPF', nome as 'Nome', email as 'e-mail', telefone as 'Telefone', endereco as 'Av./Rua', numero as 'Número', bairro as 'Bairro', cidade as 'Cidade', uf as 'UF' FROM leitores WHERE nome LIKE '%" + nome + "%'";

                    data_adapter = new SQLiteDataAdapter(cmd.CommandText, conexaoBancoDados());

                    data_adapter.Fill(data_table);

                    conexaoBancoDados().Close();

                    return (data_table);
                }
            }
            catch (Exception ex)
            {
                conexaoBancoDados().Close();
                throw ex;
            }

        }

        // método para pesquisar pela UF
        public static DataTable selectLeitorByUF(string uf)
        {
            SQLiteDataAdapter data_adapter = null;
            DataTable data_table = new DataTable();

            try
            {
                using (var cmd = conexaoBancoDados().CreateCommand())
                {
                    cmd.CommandText = "SELECT CPF as 'CPF', nome as 'Nome', email as 'e-mail', telefone as 'Telefone', endereco as 'Av./Rua', numero as 'Número', bairro as 'Bairro', cidade as 'Cidade', uf as 'UF' FROM leitores WHERE uf LIKE '%" + uf + "%'";

                    data_adapter = new SQLiteDataAdapter(cmd.CommandText, conexaoBancoDados());

                    data_adapter.Fill(data_table);

                    conexaoBancoDados().Close();

                    return (data_table);
                }
            }
            catch (Exception ex)
            {
                conexaoBancoDados().Close();
                throw ex;
            }

        }

        // método para pesquisar pelo título do LIVRO
        public static DataTable selectLivroByTitulo(string titulo)
        {
            SQLiteDataAdapter data_adapter = null;
            DataTable data_table = new DataTable();

            try
            {
                using (var cmd = conexaoBancoDados().CreateCommand())
                {
                    cmd.CommandText = "SELECT isbn as 'ISBN', titulo as 'Título', categoria as 'Categoria', autores as 'Autores', editora as 'Editora', edicao as 'Edição', npags as 'Nº Páginas' FROM livros WHERE titulo LIKE '%" + titulo + "%' ORDER BY titulo ASC";

                    data_adapter = new SQLiteDataAdapter(cmd.CommandText, conexaoBancoDados());

                    data_adapter.Fill(data_table);

                    conexaoBancoDados().Close();

                    return (data_table);
                }
            }
            catch (Exception ex)
            {
                conexaoBancoDados().Close();
                throw ex;
            }

        }

        // método para pesquisar pela categoria do LIVRO
        public static DataTable selectLivroByCategoria(string categoria)
        {
            SQLiteDataAdapter data_adapter = null;
            DataTable data_table = new DataTable();

            try
            {
                using (var cmd = conexaoBancoDados().CreateCommand())
                {
                    cmd.CommandText = "SELECT isbn as 'ISBN', titulo as 'Título', categoria as 'Categoria', autores as 'Autores', editora as 'Editora', edicao as 'Edição', npags as 'Nº Páginas' FROM livros WHERE categoria LIKE '%" + categoria + "%' ORDER BY titulo ASC";

                    data_adapter = new SQLiteDataAdapter(cmd.CommandText, conexaoBancoDados());

                    data_adapter.Fill(data_table);

                    conexaoBancoDados().Close();

                    return (data_table);
                }
            }
            catch (Exception ex)
            {
                conexaoBancoDados().Close();
                throw ex;
            }

        }

        // método para pesquisar pelo autor do LIVRO
        public static DataTable selectLivroByAutor(string autor)
        {
            SQLiteDataAdapter data_adapter = null;
            DataTable data_table = new DataTable();

            try
            {
                using (var cmd = conexaoBancoDados().CreateCommand())
                {
                    cmd.CommandText = "SELECT isbn as 'ISBN', titulo as 'Título', categoria as 'Categoria', autores as 'Autores', editora as 'Editora', edicao as 'Edição', npags as 'Nº Páginas' FROM livros WHERE autores LIKE '%" + autor + "%' ORDER BY titulo ASC";

                    data_adapter = new SQLiteDataAdapter(cmd.CommandText, conexaoBancoDados());

                    data_adapter.Fill(data_table);

                    conexaoBancoDados().Close();

                    return (data_table);
                }
            }
            catch (Exception ex)
            {
                conexaoBancoDados().Close();
                throw ex;
            }

        }

        // método para pesquisar pela editora do LIVRO
        public static DataTable selectLivroByEditora(string editora)
        {
            SQLiteDataAdapter data_adapter = null;
            DataTable data_table = new DataTable();

            try
            {
                using (var cmd = conexaoBancoDados().CreateCommand())
                {
                    cmd.CommandText = "SELECT isbn as 'ISBN', titulo as 'Título', categoria as 'Categoria', autores as 'Autores', editora as 'Editora', edicao as 'Edição', npags as 'Nº Páginas' FROM livros WHERE editora LIKE '%" + editora + "%' ORDER BY titulo ASC";

                    data_adapter = new SQLiteDataAdapter(cmd.CommandText, conexaoBancoDados());

                    data_adapter.Fill(data_table);

                    conexaoBancoDados().Close();

                    return (data_table);
                }
            }
            catch (Exception ex)
            {
                conexaoBancoDados().Close();
                throw ex;
            }

        }

        // método para pesquisar pelo ISBN do LIVRO
        public static DataTable selectLivroByISBN(string ISBN)
        {
            SQLiteDataAdapter data_adapter = null;
            DataTable data_table = new DataTable();

            try
            {
                using (var cmd = conexaoBancoDados().CreateCommand())
                {
                    cmd.CommandText = "SELECT isbn as 'ISBN', titulo as 'Título', categoria as 'Categoria', autores as 'Autores', editora as 'Editora', edicao as 'Edição', npags as 'Nº Páginas' FROM livros WHERE ISBN LIKE '%" + ISBN + "%' ORDER BY titulo ASC";

                    data_adapter = new SQLiteDataAdapter(cmd.CommandText, conexaoBancoDados());

                    data_adapter.Fill(data_table);

                    conexaoBancoDados().Close();

                    return (data_table);
                }
            }
            catch (Exception ex)
            {
                conexaoBancoDados().Close();
                throw ex;
            }

        }

        // método para pesquisar empréstimo pelo ISBN do LIVRO
        public static DataTable selectEmprestimoByISBN(string ISBN)
        {
            SQLiteDataAdapter data_adapter = null;
            DataTable data_table = new DataTable();

            try
            {
                using (var cmd = conexaoBancoDados().CreateCommand())
                {
                    cmd.CommandText = "SELECT emprestimos.id_emprestimo as 'ID Empréstimo', emprestimos.ISBN as 'ISBN', livros.titulo as 'Título', emprestimos.cpf as 'CPF', leitores.nome as 'Nome', leitores.telefone as 'Telefone', leitores.email as 'e-mail', data_emprestimo as 'Data de Empréstimo', data_devolucao as 'Data de Devolução' FROM emprestimos INNER JOIN leitores ON leitores.CPF = emprestimos.cpf, livros ON livros.isbn = emprestimos.ISBN WHERE emprestimos.ISBN LIKE '%" + ISBN + "%' ORDER BY emprestimos.id_emprestimo";

                    data_adapter = new SQLiteDataAdapter(cmd.CommandText, conexaoBancoDados());

                    data_adapter.Fill(data_table);

                    conexaoBancoDados().Close();

                    return (data_table);
                }
            }
            catch (Exception ex)
            {
                conexaoBancoDados().Close();
                throw ex;
            }

        }

        // método para pesquisar empréstimo pelo Título do LIVRO
        public static DataTable selectEmprestimoByTitulo(string titulo)
        {
            SQLiteDataAdapter data_adapter = null;
            DataTable data_table = new DataTable();

            try
            {
                using (var cmd = conexaoBancoDados().CreateCommand())
                {
                    cmd.CommandText = "SELECT emprestimos.id_emprestimo as 'ID Empréstimo', emprestimos.ISBN as 'ISBN', livros.titulo as 'Título', emprestimos.cpf as 'CPF', leitores.nome as 'Nome', leitores.telefone as 'Telefone', leitores.email as 'e-mail', data_emprestimo as 'Data de Empréstimo', data_devolucao as 'Data de Devolução' FROM emprestimos INNER JOIN leitores ON leitores.CPF = emprestimos.cpf, livros ON livros.isbn = emprestimos.ISBN WHERE livros.titulo LIKE '%" + titulo + "%' ORDER BY emprestimos.id_emprestimo";

                    data_adapter = new SQLiteDataAdapter(cmd.CommandText, conexaoBancoDados());

                    data_adapter.Fill(data_table);

                    conexaoBancoDados().Close();

                    return (data_table);
                }
            }
            catch (Exception ex)
            {
                conexaoBancoDados().Close();
                throw ex;
            }

        }

        // método para pesquisar empréstimo pelo Nome do LEITOR
        public static DataTable selectEmprestimoByNomeLeitor(string nome)
        {
            SQLiteDataAdapter data_adapter = null;
            DataTable data_table = new DataTable();

            try
            {
                using (var cmd = conexaoBancoDados().CreateCommand())
                {
                    cmd.CommandText = "SELECT emprestimos.id_emprestimo as 'ID Empréstimo', emprestimos.ISBN as 'ISBN', livros.titulo as 'Título', emprestimos.cpf as 'CPF', leitores.nome as 'Nome', leitores.telefone as 'Telefone', leitores.email as 'e-mail', data_emprestimo as 'Data de Empréstimo', data_devolucao as 'Data de Devolução' FROM emprestimos INNER JOIN leitores ON leitores.CPF = emprestimos.cpf, livros ON livros.isbn = emprestimos.ISBN WHERE leitores.nome LIKE '%" + nome + "%' ORDER BY emprestimos.id_emprestimo";

                    data_adapter = new SQLiteDataAdapter(cmd.CommandText, conexaoBancoDados());

                    data_adapter.Fill(data_table);

                    conexaoBancoDados().Close();

                    return (data_table);
                }
            }
            catch (Exception ex)
            {
                conexaoBancoDados().Close();
                throw ex;
            }

        }

        // método para pesquisar empréstimo pelo CPF do LEITOR
        public static DataTable selectEmprestimoByCPFLeitor(string cpf)
        {
            SQLiteDataAdapter data_adapter = null;
            DataTable data_table = new DataTable();

            try
            {
                using (var cmd = conexaoBancoDados().CreateCommand())
                {
                    cmd.CommandText = "SELECT emprestimos.id_emprestimo as 'ID Empréstimo', emprestimos.ISBN as 'ISBN', livros.titulo as 'Título', emprestimos.cpf as 'CPF', leitores.nome as 'Nome', leitores.telefone as 'Telefone', leitores.email as 'e-mail', data_emprestimo as 'Data de Empréstimo', data_devolucao as 'Data de Devolução' FROM emprestimos INNER JOIN leitores ON leitores.CPF = emprestimos.cpf, livros ON livros.isbn = emprestimos.ISBN WHERE emprestimos.cpf LIKE '%" + cpf + "%' ORDER BY emprestimos.id_emprestimo";

                    data_adapter = new SQLiteDataAdapter(cmd.CommandText, conexaoBancoDados());

                    data_adapter.Fill(data_table);

                    conexaoBancoDados().Close();

                    return (data_table);
                }
            }
            catch (Exception ex)
            {
                conexaoBancoDados().Close();
                throw ex;
            }

        }

        // método para selecionar empréstimo por Categoria
        public static DataTable selectEmprestimoByCategoria()
        {
            SQLiteDataAdapter data_adapter = null;
            DataTable data_table = new DataTable();

            try
            {
                using (var cmd = conexaoBancoDados().CreateCommand())
                {
                    cmd.CommandText = @"SELECT
                                         livros.categoria as 'Categoria',
                                         COUNT(livros.categoria) as 'Quantidade'
                                        FROM 
                                            emprestimos
                                        INNER JOIN 
                                            leitores ON leitores.CPF = emprestimos.cpf,
                                            livros ON livros.isbn = emprestimos.ISBN
                                        GROUP BY 
                                            livros.categoria";

                    data_adapter = new SQLiteDataAdapter(cmd.CommandText, conexaoBancoDados());

                    data_adapter.Fill(data_table);

                    conexaoBancoDados().Close();

                    return (data_table);
                }
            }
            catch (Exception ex)
            {
                conexaoBancoDados().Close();
                throw ex;
            }

        }
    }
}
