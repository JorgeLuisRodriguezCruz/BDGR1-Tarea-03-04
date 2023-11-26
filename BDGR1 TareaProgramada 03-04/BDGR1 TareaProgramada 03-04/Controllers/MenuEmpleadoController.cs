using BDGR1_TareaProgramada_03_04.Data;
using BDGR1_TareaProgramada_03_04.Models;
using Microsoft.AspNetCore.Mvc;

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
            return View();
        } 

        public IActionResult ConsultaPlanillaSemana()
        {
            return View();
        }
        
        public IActionResult ConsultaPlanillaMes()
        {
            return View();
        }

        public IActionResult Cerrar()
        {
            return RedirectToAction("InicioSesion", "Acceso");
        }

        public IActionResult VolverMenuConsulta()
        {
            return RedirectToAction(nameof(ElegirConsulta));
        }
        
        public IActionResult VolverConsultaPlanillaMes()
        {
            return RedirectToAction(nameof(ConsultaPlanillaMes));
        }

        public IActionResult VolverConsultaPlanillaSemana()
        {
            return RedirectToAction(nameof(ConsultaPlanillaSemana));
        }
        

    }
}
