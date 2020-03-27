using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1
{
    class State
    {
        private int numero;
        private List<Transition> listTrans;
    
        public State(int numero)
        {
            this.numero = numero;
            listTrans = new List<Transition>();
        }

        public List<Transition> getListTrans()
        {
            return listTrans;

        }

        public int getNumero()
        {
            return numero;
        }



        public void setListTrans(List<Transition> listTrans)
        {
            this.listTrans = listTrans;
        }
        public void setNumero(int numero)
        {
            this.numero = numero;
        }

        public void setStateInicial(Boolean stateInicial)
        {
            this.stateInicial = stateInicial;
        }

        public void setStateAceptacion(Boolean stateAceptacion)
        {
            this.stateAceptacion = stateAceptacion;
        }
    }
}
