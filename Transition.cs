using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1
{
    class Transition
    {
        private State inicial;
        private State final;
        private String simbolo;

        public Transition(State inicial, State final, String simbolo)
        {
            this.inicial = inicial;
            this.final = final;
            this.simbolo = simbolo;
        }

        public State getInicial()
        {
            return inicial;
        }

        public State getFinal()
        {
            return final;
        }

        public String getSimbolo()
        {
            return simbolo;
        }

        public void setInicial(State inicial)
        {
            this.inicial = inicial;
        }

        public void setFinal(State final)
        {
            this.final = final;
        }

        public void setSimbolo(String simbolo)
        {
            this.simbolo = simbolo;
        }
    }
}
