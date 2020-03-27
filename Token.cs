using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1
{
    class Token
    {
        


        private int fila, columna;
        private tipo token;
        int no;
        int tokens;
        private string lexema;
        public Token(tipo token, string lexema, int fila, int columna)
        {
            this.token = token;
            this.lexema = lexema;
            this.fila = fila;
            this.columna = columna;

        }



        public string getLexema()
        {
            return lexema;
        }

        public int getFila()
        {
            return fila;
        }

        public int getColumna()
        {
            return columna;
        }
    }
}




