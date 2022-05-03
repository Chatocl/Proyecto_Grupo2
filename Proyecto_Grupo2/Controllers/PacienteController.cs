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
            try
            {
                string aux="";
                var NewPaciente = new Models.Paciente
                {

                    Nombre = collection["nombre"],
                    DPI = collection["dpi"],
                    Edad = collection["edad"],
                    Telefono = collection["telefono"],
                    FDU =Convert.ToDateTime(collection["FDU"]),
                    Descripcion = collection["descripcion"]
                };
                aux = collection["FDP"];
                if (aux!="")
                {
                    NewPaciente.FDP = Convert.ToDateTime(collection["FDP"]);
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
                return View();
            }
        }

        // GET: AVLController/Edit/5
        public ActionResult Edit_Paciente(Paciente item)
        {
            Paciente viewPaciente = Singleton.Instance.miAVL.Find(item);
            return View(viewPaciente);
        }

        // POST: AVLController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_Paciente(int id, IFormCollection collection)
        {
            try
            {
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
            return View(viewPaciente);
        }

        // POST: AVLController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete_Paciente(int id, IFormCollection collection)
        {
            try
            {
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
    }
}
