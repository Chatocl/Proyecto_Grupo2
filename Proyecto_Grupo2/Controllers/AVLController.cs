using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proyecto_Grupo2.Models.Datos;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using System.Linq;
using Proyecto_Grupo2.Models;
using System.IO;



namespace Proyecto_Grupo2.Controllers
{
    public class AVLController : Controller
    {
        private IHostingEnvironment Environment;
        // GET: AVLController
        public AVLController(IHostingEnvironment _everioment)
        {
            Environment = _everioment;
        }
        public ActionResult Index()
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
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AVLController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: AVLController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var NewPaciente = new Models.Paciente
                {

                    Nombre = collection["nombre"],
                    ID = collection["id"],
                    Edad = collection["edad"],
                    Telefono = collection["telefono"],

                    //Corregir para los siguientes datos 
                    //FDUltimaConsulta=collection["fdultimaconsulta"],
                    //FDProximaConsulta = collection["fdproximaconsulta"],
                    Descipion = collection["descripcion"]
                };
                Singleton.Instance.miAVL.Add(NewPaciente);
                Singleton.Instance.bandera = 0;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AVLController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AVLController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AVLController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AVLController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
