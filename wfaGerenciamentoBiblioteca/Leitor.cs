using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wfaGerenciamentoBiblioteca
{
    class Leitor
    {
        // atributos

        static String m_nome;
        private String m_CPF;
        private String m_telefone;
        private String m_email;
        private String m_endereco;
        private String m_num;
        private String m_bairro;
        private String m_cidade;
        private String m_uf;

        // construtor padrão
        public Leitor()
        {
            m_nome = "";
            m_CPF = "";
            m_telefone = "";
            m_email = "";
            m_endereco = "";
            m_num = "";
            m_bairro = "";
            m_cidade = "";
            m_uf = "";

        }

        // construtor com argumentos
        public Leitor(String nome, String CPF, String telefone, String email,
            String endereco, String numero, String bairro, String cidade, String uf)
        {
            m_nome = nome;
            m_CPF = CPF;
            m_telefone = telefone;
            m_email = email;
            m_endereco = endereco;
            m_num = numero;
            m_bairro = bairro;
            m_cidade = cidade;
            m_uf = uf;
        }

        // getters
        public String getNome()
        {
            return (m_nome);
        }

        public String getCPF()
        {
            return (m_CPF);
        }

        public String getTelefone()
        {
            return (m_telefone);
        }

        public String getEmail()
        {
            return (m_email);
        }

        public String getEndereco()
        {
            return (m_endereco);
        }
        public String getNumeroResidencia()
        {
            return (m_num);
        }

        public String getBairro()
        {
            return (m_bairro);
        }

        public String getCidade()
        {
            return (m_cidade);
        }

        public String getUF()
        {
            return (m_uf);
        }

        // setters
        public void setNome(String nome)
        {
            m_nome = nome;
        }

        public void setCPF(String CPF)
        {
            m_CPF = CPF;
        }

        public void setTelefone(String telefone)
        {
            m_telefone = telefone;
        }

        public void setEmail(String email)
        {
            m_email = email;
        }

        public void setEndereco(String endereco)
        {
            m_endereco = endereco;
        }
        public void setNumeroResidencia(String numero)
        {
            m_num = numero;
        }

        public void setBairro(String bairro)
        {
            m_bairro = bairro;
        }

        public void setCidade(String cidade)
        {
            m_cidade = cidade;
        }

        public void setUF(String UF)
        {
            m_uf = UF;
        }

    }
}
