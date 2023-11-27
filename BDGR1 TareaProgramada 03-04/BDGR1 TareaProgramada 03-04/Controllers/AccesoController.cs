using BDGR1_TareaProgramada_03_04.Data;
using BDGR1_TareaProgramada_03_04.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Xml.Linq;

namespace BDGR1_TareaProgramada_03_04.Controllers
{
    public class AccesoController : Controller
    {
        private readonly AppDBContext _context;

        public AccesoController (AppDBContext context)
        {
            _context = context;
        }

        public IActionResult InicioSesion ()
        {
            return View();
        }


        [HttpPost]
        public ActionResult InicioSesion(string Nombre, string Contrasena)
        {  
            IEnumerable<EntidadUsuario> usuario = null; 

            try
            { 
                usuario = _context.Usuarios.FromSqlInterpolated($"EXEC BuscarUsuario {Nombre}, {Contrasena}");

            } catch (Exception ex) { Console.WriteLine("ERROR --> " + ex.Message); }
            
            if (usuario != null && usuario.Count() > 0)
            {
                int tipo = usuario.First().Tipo;
                switch(tipo)
                {
                    case 1: //Menu Admin
                        return RedirectToAction("ListaEmpleado", "MunuAdmin");

                    case 2: //Menu Empleado
                        TempData["Volver"] = "Menu";
                        return RedirectToAction("ElegirConsulta", "MenuEmpleado");
                }

            }

            ViewBag.Mensaje = "Error: El suario no ha sido encontrado.";
            return View();
        }



    }
}
