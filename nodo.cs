using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1
{
    class nodo
    {

        public string transicion;
        public int Num;
        public nodo primero, segundo;

        public nodo(string tr, int nu)
        {
            Num = nu;
            transicion = tr;
            primero = segundo = null;
        }
    }
}
