using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wfaGerenciamentoBiblioteca
{
    class DadosEmprestimo
    {
        // atributos da classe
        private Leitor m_leitor;
        private Acervo m_livro;
        private String m_dataEmprestimo;
        private String m_dataDevolucao;
        private int m_id;

        public DadosEmprestimo()
        {
            m_leitor = new Leitor();
            m_livro = new Acervo();
            m_dataEmprestimo = setToday();
            m_dataDevolucao = set7dias();
            m_id = 0;

        }

        // método para verificar se a data de devolução não é sábado ou domingo
        public bool isDiaUtil(DateTime x)
        {
            if (x.DayOfWeek == DayOfWeek.Sunday || x.DayOfWeek == DayOfWeek.Saturday)
            {
                return (false);
            }
            else
            {
                return(true);
            }
        }

        // método para gerar data atual
        public String setToday()
        {
            DateTime data = DateTime.Now;

            string dataAtual = data.Day.ToString() + "/" + data.Month.ToString() + "/" + data.Year.ToString();

            return (dataAtual);
        }


        // método para gerar o dia de devolução
        public String set7dias()
        {
            DateTime data = DateTime.Now;

            int dias = 0;

            while (dias < 7)
            {
                data = data.AddDays(1);
                if (isDiaUtil(data))
                {
                    dias++;
                }
            }

            string devolucao = data.Day.ToString() + "/" + data.Month.ToString() + "/" + data.Year.ToString();

            return (devolucao);
        }


        // getters
        public String getDataEmprestimo()
        {
            return (m_dataEmprestimo);
        }

        public String getDataDevolucao()
        {
            return (m_dataDevolucao);
        }

        public Leitor getLeitor()
        {
            return (m_leitor);
        }
        public Acervo getLivro()
        {
            return (m_livro);
        }

        public int getIdEmprestimo()
        {
            return (m_id);
        }

        // setters
        public void setDataEmprestimo(String dataEmprestimo)
        {
            m_dataEmprestimo = dataEmprestimo;
        }

        public void setDataDevolucao(String dataDevolucao)
        {
            m_dataDevolucao = dataDevolucao;
        }

        public void setLeitor(Leitor leitor)
        {
            m_leitor = leitor;
        }

        public void setLivro(Acervo livro)
        {
            m_livro = livro;
        }

        public void setIdEmprestimo(int ID)
        {
            m_id = ID;
        }

    }
}
