using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wfaGerenciamentoBiblioteca
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// 
        /// O Biblioteca (versão 1.0) é um sistema de gerenciamento de biblioteca. 
        /// 
        /// Suporte e atualizações: https://github.com/arianacabral/Biblioteca
        /// 
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
