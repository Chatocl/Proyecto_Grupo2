﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Proyecto_Grupo2.Models.Datos;


namespace Proyecto_Grupo2.Models
{
    public class Paciente : IComparable<Paciente>
    {
        [Display(Name = "Nombre completo del paciente")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Nombre { get; set; }

        [Display (Name ="Número de DPI o Partida de nacimiento")]
        [Required (ErrorMessage ="El campo {0} es requerido.")]
        public string DPI { get; set; }
        [Display (Name ="Edad del paciente")]
        [Required(ErrorMessage ="El campo {0} es requerido.")]
        public string Edad { get; set; }
        [Display (Name ="Número de telefono")]
        [Required(ErrorMessage ="El campo {0} es requerido.")]
        public string Telefono { get; set; }
        [Display(Name ="Fecha de su última consulta")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage ="El campo {0} es requerido.")]
        public DateTime? FDUltimaConsulta { get; set; }
        [Display(Name ="Fecha de su próxima consulta(opcional)")]
        [DataType(DataType.Date)]
        public DateTime? FDProximaConsulta { get; set; }
        [Display(Name ="Descripción del ultimo diagnostico o tratamiento que posee(opcional)")]
        public string Descipion { get; set; }

        public int CompareTo(Paciente otro)
        {
            if (otro == null)
            {
                return 0;
            }
            else
            {
                return this.Nombre.CompareTo(otro.Nombre);
            }
        }

    }
}
