using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clase
{
    public class Nodo<T> where T: IComparable<T>
    {
        public Nodo<T> Izquierdo { get; set; }
        public Nodo<T> Derecho { get; set; }
        public T Valor { get; set; }


        //Para el AVL
        public int FE { get; set; }

    }
}
