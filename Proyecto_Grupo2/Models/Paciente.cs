using System;
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

        [Display(Name = "Número de DPI o partida de nacimiento")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "Tamaño incorrecto de DPI")]
        public string DPI { get; set; }

        [Display(Name = "Edad del paciente")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Edad { get; set; }

        [Display(Name = "Número de teléfono")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "Tamaño incorrecto de DPI")]
        public string Telefono { get; set; }
        //  [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}", ApplyFormatInEditMode =true)]
        [Display(Name = "Fecha de última consulta")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        
        public DateTime? FDU { get; set; }

        [Display(Name ="Fecha de próxima consulta (Opcional)")]
        [DataType(DataType.Date)]
        public DateTime? FDP { get; set; }
        [Display(Name ="Descripción del último diagnóstico o tratamiento que posee (Opcional)")]
        public string Descripcion { get; set; }

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
