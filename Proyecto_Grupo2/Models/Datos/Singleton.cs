using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clase;
using Proyecto_Grupo2.Models;

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

        // Arbol AVL y objeto Paciente para mostrar 
        public int bandera;
        public Paciente AuxP = new Paciente();
        public Clase.Arbol_AVL<Paciente> miAVL = new Clase.Arbol_AVL<Paciente>();
        public Clase.Lista ListaPacientes = new Clase.Lista();
        // Listas usadas para el seguimiento de los Pacientes 
        public Clase.Lista ListaPacientes = new Clase.Lista();
        public List<Paciente> Aux = new List<Paciente>();
        public List<Paciente> LimDental = new List<Paciente>();
        public List<Paciente> Ortodoncia = new List<Paciente>();
        public List<Paciente> Caries = new List<Paciente>();
        public List <Paciente> TraComun  = new List<Paciente>();

    }
}
