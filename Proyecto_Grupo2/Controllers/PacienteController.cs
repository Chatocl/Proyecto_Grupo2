using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proyecto_Grupo2.Models.Datos;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using Proyecto_Grupo2.Models;
using System.IO;



namespace Proyecto_Grupo2.Controllers
{
        
    public class PacienteController : Controller
    {
        //Constructor

          
        private IHostingEnvironment Environment;
        // GET: AVLController
        public PacienteController(IHostingEnvironment _everioment)
        {
            Environment = _everioment;
        }
        public ActionResult Index_Paciente()
        {
            if (Singleton.Instance.bandera == 1)
            {
                Singleton.Instance.bandera = 0;
                return View(Singleton.Instance.Aux);
            }
            else
            {
                return View(Singleton.Instance.miAVL.ObtenerLista());
            }
        }
        // GET: AVLController/Details/5
        public ActionResult Details_Paciente(Paciente id)
        {
            Paciente viewPaciente = Singleton.Instance.miAVL.Find(id);
            return View(viewPaciente);
        }

        // GET: AVLController/Create
        public ActionResult Create_Paciente()
        {
            return View();
        }

        // POST: AVLController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_Paciente(IFormCollection collection)
        {
             
            Paciente AuxPac = new Paciente();
            try
            {
                string aux="";
                var NewPaciente = new Models.Paciente
                {

                    Nombre = collection["nombre"],
                    DPI = collection["dpi"],
                    Edad = collection["edad"],
                    FDU = Convert.ToDateTime(collection["FDU"]),
                    Telefono = collection["telefono"],
                    Descripcion = collection["descripcion"]
                };

                aux = collection["FDP"];
                if (aux!="")
                {
                   
                    if (Convert.ToDateTime(collection["FDP"]) > Convert.ToDateTime(DateTime.Today)&& Convert.ToDateTime(collection["FDP"]) > Convert.ToDateTime(collection["FDU"]))
                    {
                            
                       // int a = (Add de la lista) La misma lista hace la validación de los 8 días

                        NewPaciente.FDP = Convert.ToDateTime(collection["FDP"]);
                    }
                    else
                    {
                        AuxPac = NewPaciente;
                        TempData["FEC"] = "Ingreso una fecha pasada para una proxima consulta";
                        throw new Exception(null);
                    }
                }
                else
                {
                    NewPaciente.FDP = null;
                }

                Singleton.Instance.miAVL.Add(NewPaciente);
                Singleton.Instance.bandera = 0;
                return RedirectToAction(nameof(Index_Paciente));
            }
            catch
            {
                return View(AuxPac);
            }
        }

        // GET: AVLController/Edit/5
        public ActionResult Edit_Paciente(Paciente item)
        {
            Paciente viewPaciente = Singleton.Instance.miAVL.Find(item);
            Singleton.Instance.AuxP = viewPaciente;
            return View(viewPaciente);
        }

        // POST: AVLController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_Paciente(Paciente id, IFormCollection collection)
        {
            try
            {
                string aux = "";
                Singleton.Instance.miAVL.Find(Singleton.Instance.AuxP).Nombre = collection["nombre"];
                Singleton.Instance.miAVL.Find(Singleton.Instance.AuxP).DPI = collection["dpi"];
                Singleton.Instance.miAVL.Find(Singleton.Instance.AuxP).Edad = collection["edad"];
                Singleton.Instance.miAVL.Find(Singleton.Instance.AuxP).Telefono = collection["telefono"];
                Singleton.Instance.miAVL.Find(Singleton.Instance.AuxP).FDU = Convert.ToDateTime(collection["FDU"]);
                Singleton.Instance.miAVL.Find(Singleton.Instance.AuxP).Descripcion = collection["descripcion"];
               
                aux = Convert.ToString(Convert.ToDateTime(collection["FDP"]));
                if (aux != "")
                {
                    Singleton.Instance.miAVL.Find(Singleton.Instance.AuxP).FDP = Convert.ToDateTime(collection["FDP"]);
                }
                else
                {
                    Singleton.Instance.miAVL.Find(Singleton.Instance.AuxP).FDP = null;
                }
                return RedirectToAction(nameof(Index_Paciente));
            }
            catch
            {
                return View();
            }
        }

        // GET: AVLController/Delete/5
        public ActionResult Delete_Paciente(Paciente id)
        {
            Paciente viewPaciente = Singleton.Instance.miAVL.Find(id);
            Singleton.Instance.AuxP = viewPaciente;
            return View(viewPaciente);
        }

        // POST: AVLController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete_Paciente(Paciente id, IFormCollection collection)
        {
            try
            {
                Singleton.Instance.miAVL.Remove(Singleton.Instance.AuxP);
                return RedirectToAction(nameof(Index_Paciente));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Busqueda_Paciente(string Busqueda)
        {
            try
            {
                if (Busqueda != null)
                {
                    Paciente viewVehiculo = Singleton.Instance.miAVL.ObtenerLista().FirstOrDefault(a => a.Nombre == Busqueda);
                    if (viewVehiculo == null)
                    {
                         viewVehiculo = Singleton.Instance.miAVL.ObtenerLista().FirstOrDefault(a => a.DPI == Busqueda);
                        if (viewVehiculo == null)
                        {
                            TempData["Bus"] = "No se encontro el Paciente";
                        }
                        else
                        {
                            return View(viewVehiculo);
                        }
                    }
                    else
                    {
                        return View(viewVehiculo);
                    }
                }
                return View();
            }
            catch
            {
                TempData["Bus"] = "No se encontro el Paciente";
                return View();
            }
        }
      
        public ActionResult Seguimiento_Paciente(string Busqueda)
        {
            string Slista = Busqueda;
            int Meses = 0;
            Singleton.Instance.LimDental.Clear();
            Singleton.Instance.Ortodoncia.Clear();
            Singleton.Instance.Caries.Clear();
            Singleton.Instance.TraComun.Clear();
            Singleton.Instance.Aux = Singleton.Instance.miAVL.ObtenerLista();
            for (int i = 0; i < Singleton.Instance.Aux.Count(); i++)
            {
                Meses = Math.Abs((DateTime.Now.Month - Singleton.Instance.Aux[i].FDU.Value.Month) + 12 * (DateTime.Now.Year - Singleton.Instance.Aux[i].FDU.Value.Year));

                if (Singleton.Instance.Aux[i].Descripcion == "" && Meses >= 6)
                {
                    Singleton.Instance.LimDental.Add(Singleton.Instance.Aux[i]);
                }
                else if (Singleton.Instance.Aux[i].Descripcion.Contains("Ortodoncia") && Meses >= 2)
                {
                    Singleton.Instance.Ortodoncia.Add(Singleton.Instance.Aux[i]); 
                }
                else if (Singleton.Instance.Aux[i].Descripcion.Contains("Caries") && Meses >= 4)
                {
                    Singleton.Instance.Caries.Add(Singleton.Instance.Aux[i]);
                }
                else if (Singleton.Instance.Aux[i].Descripcion !="" && Meses>=6)
                {
                    Singleton.Instance.TraComun.Add(Singleton.Instance.Aux[i]);
                }
            }
            if (Slista=="LD")
            {
                return View(Singleton.Instance.LimDental);  
            }
            else if (Slista=="OR")
            {
                return View(Singleton.Instance.Ortodoncia);
            }
            else if (Slista=="CA")
            {
                return View(Singleton.Instance.Caries);
            }
            else
            {
                 return View(Singleton.Instance.TraComun);
            }
            return View();
        }
    }
}
