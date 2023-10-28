using BDGR1_TareaProgramada_03_04.Data;
using BDGR1_TareaProgramada_03_04.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace BDGR1_TareaProgramada_03_04.Controllers
{
    public class MunuAdminController : Controller
    {
        private readonly AppDBContext _context;
         
        public MunuAdminController (AppDBContext context)
        {
            _context = context;
        }

        public IActionResult ListaEmpleado()
        {
            IEnumerable<EntidadEmpleado> empleados = _context.Empleados.FromSqlInterpolated($"EXEC EnlistarEmpleados");
            return View(empleados);
        }

        public IActionResult SeleccionarEmpleado()
        {
            return View();
        }

        public IActionResult SecEditar()
        {
            ViewBag.Subtitulo = "Editar empleado";
            return View(nameof(SeleccionarEmpleado));
        }

        public IActionResult SecEliminar()
        {
            ViewBag.Subtitulo = "Eliminar empleado";
            return View(nameof(SeleccionarEmpleado));
        }

        public IActionResult SecImpersonar()
        {
            ViewBag.Subtitulo = "Impersonar empleado";
            return View(nameof(SeleccionarEmpleado));
        }

        public IActionResult CerrarS()
        {
            return RedirectToAction("InicioSesion", "Acceso");
        }


    }
}
