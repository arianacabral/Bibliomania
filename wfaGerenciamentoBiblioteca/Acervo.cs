using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wfaGerenciamentoBiblioteca
{
    class Acervo
    {
        // atributos
        private String m_ISBN;
        private String m_titulo;
        private String m_categoria;
        private String m_autores;
        private String m_editora;
        private int m_edicao;
        private int m_npags;

        // construtor padrão
        public Acervo()
        {
            m_ISBN = "";
            m_titulo = "";
            m_categoria = "";
            m_autores = "";
            m_editora = "";
            m_edicao = 0;
            m_npags = 0;
        }

        // construtor com argumentos
        public Acervo(String ISBN, String titulo, String categoria, String autores, String editora, int edicao, int n_pags)
        {
            m_ISBN = ISBN;
            m_titulo = titulo;
            m_categoria = categoria;
            m_autores = autores;
            m_editora = editora;
            m_edicao = edicao;
            m_npags = n_pags;
        }

        // getters
        public String getISBN()
        {
            return (m_ISBN);
        }

        public String getTitulo()
        {
            return (m_titulo);
        }

        public String getCategoria()
        {
            return (m_categoria);
        }

        public String getAutores()
        {
            return (m_autores);
        }

        public String getEditora()
        {
            return (m_editora);
        }

        public int getEdicao()
        {
            return (m_edicao);
        }

        public int getNumPaginas()
        {
            return (m_npags);
        }

        // setters
        public void setISBN(String ISBN)
        {
            m_ISBN = ISBN;
        }

        public void setTitulo(String titulo)
        {
            m_titulo = titulo;
        }

        public void setCategoria(String categoria)
        {
            m_categoria = categoria;
        }

        public void setAutores(String autores)
        {
            m_autores = autores;
        }

        public void setEditora(String editora)
        {
            m_editora = editora;
        }

        public void setEdicao(int edicao)
        {
            m_edicao = edicao;
        }

        public void setNumPaginas(int nPaginas)
        {
            m_npags = nPaginas;
        }
    }

}
