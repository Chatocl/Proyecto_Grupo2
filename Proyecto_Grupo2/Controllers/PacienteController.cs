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
           return View(Singleton.Instance.miAVL.ObtenerLista());
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
                string aux = "";
                var NewPaciente = new Models.Paciente
                {

                    Nombre = collection["nombre"],
                    DPI = collection["dpi"],
                    Edad = collection["edad"],
                    Telefono = collection["telefono"],
                    Descripcion = collection["descripcion"]
                };

                if (Convert.ToDateTime(collection["FDU"]) < Convert.ToDateTime(DateTime.Today))
                {
                    NewPaciente.FDU = Convert.ToDateTime(collection["FDU"]);
                }
                else
                {
                    AuxPac = NewPaciente;
                    TempData["FEC"] = "Ingreso una fecha incorrecta en la consulta pasada";
                    throw new Exception(null);
                }


                aux = collection["FDP"];
                if (aux!="")
                {
                   
                    if (Convert.ToDateTime(collection["FDP"]) > Convert.ToDateTime(DateTime.Today)&& Convert.ToDateTime(collection["FDP"]) > Convert.ToDateTime(collection["FDU"]))
                    {

                        int a = Singleton.Instance.ListaPacientes.GetDay(Convert.ToDateTime(collection["FDP"]));
                        if(a ==8)
                        {
                            AuxPac = NewPaciente;
                            TempData["VOP"] = "Unicamente se pueden atender a 8 personas por día.";
                            throw new Exception(null);
                        }
                        else
                        {
                            NewPaciente.FDP = Convert.ToDateTime(collection["FDP"]);
                            Singleton.Instance.ListaPacientes.Add(Convert.ToDateTime(NewPaciente.FDP));
                        }
           
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


                Singleton.Instance.ListaPacientes.AddDPI(collection["dpi"]);

                int b = Singleton.Instance.ListaPacientes.GetDPI(collection["dpi"]);

                if (b == 1)
                {
                    NewPaciente.DPI = collection["dpi"];
                }
                else
                {
                    AuxPac = NewPaciente;
                    TempData["DRP"] = "EL dpi que desea ingresar, ya se encuentra registrado.";
                    throw new Exception(null);
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
            Paciente AuxPac = new Paciente();
            Singleton.Instance.AuxP = viewPaciente;
            return View();
        }

        // POST: AVLController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_Paciente(Paciente id, IFormCollection collection)
        {
            Paciente AuxPac = new Paciente();
            try
            {
                var NewPaciente = new Models.Paciente
                {
                    Nombre = collection["nombre"],
                    DPI = collection["dpi"],
                    Edad = collection["edad"],
                    FDU = Convert.ToDateTime(collection["FDU"]),
                    Telefono = collection["telefono"],
                    Descripcion = collection["descripcion"]
                };
                string aux = collection["F"];
                Singleton.Instance.miAVL.Find(Singleton.Instance.AuxP).Nombre = NewPaciente.Nombre;
                Singleton.Instance.miAVL.Find(Singleton.Instance.AuxP).DPI = NewPaciente.DPI;
                Singleton.Instance.miAVL.Find(Singleton.Instance.AuxP).Edad = NewPaciente.Edad;
                Singleton.Instance.miAVL.Find(Singleton.Instance.AuxP).Telefono = NewPaciente.Telefono;
                Singleton.Instance.miAVL.Find(Singleton.Instance.AuxP).FDU = Convert.ToDateTime(NewPaciente.FDU);
                Singleton.Instance.miAVL.Find(Singleton.Instance.AuxP).Descripcion = NewPaciente.Descripcion;
                if (aux != "")
                {

                    if (Convert.ToDateTime(collection["FDP"]) > Convert.ToDateTime(DateTime.Today) && Convert.ToDateTime(collection["FDP"]) > Convert.ToDateTime(collection["FDU"]))
                    {
                        int a = Singleton.Instance.ListaPacientes.GetDay(Convert.ToDateTime(collection["FDP"]));
                        if (a == 8)
                        {
                            AuxPac = NewPaciente;
                            TempData["VOP"] = "Unicamente se pueden atender a 8 personas por día.";
                            throw new Exception(null);
                        }
                        else
                        {
                            Singleton.Instance.miAVL.Find(Singleton.Instance.AuxP).FDP = Convert.ToDateTime(NewPaciente.FDP);
                        }
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
                    Singleton.Instance.miAVL.Find(Singleton.Instance.AuxP).FDP = null;
                }

                return RedirectToAction(nameof(Index_Paciente));
            }
            catch
            {
                return View(AuxPac);
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
                else if (Singleton.Instance.Aux[i].Descripcion.Contains("Ortodoncia") || Singleton.Instance.Aux[i].Descripcion.Contains("ortodoncia"))
                {
                  if (Meses >= 2)
                  {
                    Singleton.Instance.Ortodoncia.Add(Singleton.Instance.Aux[i]);
                  }   
                }
                else if (Singleton.Instance.Aux[i].Descripcion.Contains("Caries") ||Singleton.Instance.Aux[i].Descripcion.Contains("Caries") )
                {
                    if (Meses >= 4)
                    {
                        Singleton.Instance.Caries.Add(Singleton.Instance.Aux[i]);
                    }
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
