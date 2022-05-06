using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;


namespace Clase
{
    
    public class Lista
    {


        private class Nodo_Lista
        {
            
            public int numAtencion=0; //El numero de personas atendidas en un día
           public DateTime FechaDA;//Fecha de antencion 
            public string DNI; //variable para verificar dpi
            public Nodo_Lista Next;
           

        }


        Nodo_Lista Head = new Nodo_Lista();
        

        public Lista()
        {
            Head = null;
          
        }

        public void Add( DateTime fecha)
        {
            Nodo_Lista newNodo = new Nodo_Lista();
            Nodo_Lista aux = Head;

            newNodo.FechaDA = fecha;
            newNodo.Next = null;

            if (Head == null)
            {
                
   
                Head = newNodo;
                return;
            }
            else
            {
                while (aux.Next != null)       //Recorro la lista con el while
                { 
                    aux = aux.Next;
                }   
                    aux.Next = newNodo;
            }

        }

        public void AddDPI(string dni)
        {
            Nodo_Lista newNodo = new Nodo_Lista();
            Nodo_Lista aux = Head;

            newNodo.DNI = dni;
            newNodo.Next = null;

            if (Head == null)
            {

                
                Head = newNodo;
                return;
            }
            else
            {
                while (aux.Next != null)       //Recorro la lista con el while
                {
                    aux = aux.Next;
                }
                aux.Next = newNodo;
            }

        }

        public int GetDPI(string dpi)
        {
            Nodo_Lista newNodo = new Nodo_Lista();
            Nodo_Lista aux = Head;
            int cont = 0;
            while (aux != null)       //Recorro la lista con el while
            {
                if (aux.DNI== dpi)  //Si la fecha del nodo es igual a la fecha de interés
                {
                    cont++;             //El contador se sumará en 1
                }

                aux = aux.Next;
            }
            return cont; //Retorno cont, si cont es mayor a 3, se hará la excepción 
        }


        public int GetDay(DateTime fecha)
        {
            Nodo_Lista newNodo = new Nodo_Lista();
            Nodo_Lista aux = Head;
            int cont = 0;
            while (aux != null)       //Recorro la lista con el while
            {
                if (aux.FechaDA == fecha)  //Si la fecha del nodo es igual a la fecha de interés
                {
                    cont++;             //El contador se sumará en 1
                }

                aux = aux.Next;
            }
            return cont ; //Retorno cont, si cont es mayor a 3, se hará la excepción 
        }


        //************************
    }
}
