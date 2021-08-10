using System;
using System.Collections.Generic;
using System.Text;

namespace Interbloqueos
{
    class Vertice
    {
        public object Valor { get; set; }
        public bool Marcado { get; set; }
        public List<Vertice> Aristas { get; set; }

        public Vertice(object Valor)
        {
            this.Valor = Valor;
            Marcado = false;
            Aristas = new List<Vertice>();
        }
        public bool Sin_Aristas()
        {
            if (this.Aristas.Count == 0)
            {
                return true;
            }
            else { return false; }
        }
    }
}
