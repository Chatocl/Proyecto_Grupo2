using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proyecto_Grupo2.Models.Datos;

namespace Proyecto_Grupo2.Models.Datos
{
    public class Singleton
    {
        private static Singleton _instance = null;
        public static Singleton Instance
        {
            get
            {
                if (_instance == null) _instance = new Singleton();
                return _instance;
            }
        }
        public int bandera;
        public List<Paciente> Aux = new List<Paciente>();
        public Arbol_AVL<Paciente> miAVL = new Arbol_AVL<Paciente>();
    }
}
