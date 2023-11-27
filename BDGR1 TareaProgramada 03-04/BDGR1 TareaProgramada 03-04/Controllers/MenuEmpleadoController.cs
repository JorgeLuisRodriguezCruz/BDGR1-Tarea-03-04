using BDGR1_TareaProgramada_03_04.Data;
using BDGR1_TareaProgramada_03_04.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BDGR1_TareaProgramada_03_04.Controllers
{
    public class MenuEmpleadoController : Controller
    {
        private readonly AppDBContext _context;

        public MenuEmpleadoController (AppDBContext context)
        {
            _context = context;
        }

        public IActionResult ElegirConsulta ()
        {
            TempData["Volver"] = TempData["Volver"] as string;
            //TempData["Volver"] = vuelve;
            return View();
        } 

        public IActionResult ConsultaPlanillaSemana()
        {
            IEnumerable<EntidadPlanillaSemana> planillaSemana = new List<EntidadPlanillaSemana>();
            TempData["Volver"] = TempData["Volver"] as string;
            return View( planillaSemana );
        }
        
        public IActionResult ConsultaPlanillaMes()
        {
            IEnumerable<EntidadPlanillaMes> planillaMes = new List<EntidadPlanillaMes>();
            TempData["Volver"] = TempData["Volver"] as string;
            return View(planillaMes); 
        }

        public IActionResult Cerrar()
        {
            string? volver = TempData["Volver"] as string;

            if (volver.ToUpper().Contains("ADMIN") == true)
            {
                return RedirectToAction("ListaEmpleado", "MunuAdmin");
            }
            if (volver.ToUpper().Contains("MENU") == true)
            {
                return RedirectToAction("InicioSesion", "Acceso");
            }

            return RedirectToAction("InicioSesion", "Acceso");
        }

        public IActionResult VolverMenuConsulta()
        {
            TempData["Volver"] = TempData["Volver"] as string;
            return RedirectToAction(nameof(ElegirConsulta));
        }
        
        public IActionResult VolverConsultaPlanillaMes()
        {
            TempData["Volver"] = TempData["Volver"] as string;
            return RedirectToAction(nameof(ConsultaPlanillaMes));
        }

        public IActionResult VolverConsultaPlanillaSemana()
        {
            TempData["Volver"] = TempData["Volver"] as string;
            return RedirectToAction(nameof(ConsultaPlanillaSemana));
        }
        

    }
}
