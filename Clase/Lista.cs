using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;


namespace Clase
{
    
    class Lista
    {


        private class Nodo_Lista
        {
            public
            int numAtencion=0; //El numero de personas atendidas en un día
           public DateTime FechaDA;//Fecha de antencion 

            public Nodo_Lista Next;
           

        }


        Nodo_Lista Head = new Nodo_Lista();
        

        public Lista()
        {
            Head = null;
          
        }

        public void Add(int dia, DateTime fecha)
        {
            Nodo_Lista newNodo = new Nodo_Lista();
            Nodo_Lista aux = Head;

            
            newNodo.FechaDA = fecha;
            newNodo.Next = null;

            if (Head == null)
            {
                
                newNodo.numAtencion++;
                Head = newNodo;
                return;
            }
            else
            {
                while (aux.Next != null)
                {

                    if(aux.FechaDA == fecha)
                    {
                        if(aux.numAtencion==8)
                        {
                            return ;
                        }
                        else
                        {
                            aux.numAtencion++;
                        }
                    }
                    aux = aux.Next;
                }
                
                newNodo.numAtencion = dia;
                aux.Next = newNodo;
                return;
            }



        }

        


        public int GetDay(DateTime dateTime)
        {
            Nodo_Lista newNodo = new Nodo_Lista();
            Nodo_Lista aux = Head;

            while (aux != null)
            {
                if (dateTime == aux.FechaDA)
                {
                    if(aux.numAtencion<8)
                    {
                        return aux.numAtencion;
                    }
                    else
                    {
                        return -1;
                    }
                    //break;
                }
                else
                {

                    aux = aux.Next;
                   
                }
            }
            return -1 ;
        }


        //************************
    }
}
