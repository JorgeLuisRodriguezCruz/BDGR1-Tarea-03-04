using BDGR1_TareaProgramada_03_04.Data;
using BDGR1_TareaProgramada_03_04.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Xml.Linq;

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
            IEnumerable<EntidadTipoDocIdentidad> tiposDocsIdentidad = _context.TiposDocsIdentidad.FromSqlInterpolated($"EXEC ObtenerTiposDocsIdentidad");
            IEnumerable<EntidadDepartamento> tiposDepartamentos = _context.Departamentos.FromSqlInterpolated($"EXEC ObtenerDepartamentos");
            IEnumerable<EntidadPuesto> tiposPuestos = _context.Puestos.FromSqlInterpolated($"EXEC ObtenerPuestos");


            List<SelectListItem> tiposDocsId = listaTiposDocIdentidad(tiposDocsIdentidad);
            List<SelectListItem> tiposDeparta = listaTiposDepartamento(tiposDepartamentos);
            List<SelectListItem> tiposPuesto = listaTiposPuesto(tiposPuestos);

            ViewBag.OpcionesTipoDocID = tiposDocsId;
            ViewBag.OpcionesTipoDepart = tiposDeparta;
            ViewBag.OpcionesTipoPuesto = tiposPuesto;
            
            return View( pasarElementosAVista(empleados, tiposDocsId, tiposDeparta, tiposPuesto) );
        }

        private List<SelectListItem> listaTiposDocIdentidad (IEnumerable<EntidadTipoDocIdentidad> tiposDocsIdentidad)
        {
            List<EntidadTipoDocIdentidad> tiposDocId = tiposDocsIdentidad.ToList(); 
            return tiposDocId.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre.ToString(),
                    Value = d.Id.ToString(),
                    Selected = false
                };
            });
        }

        private List<SelectListItem> listaTiposDepartamento(IEnumerable<EntidadDepartamento> tiposDepartamento)
        {
            List<EntidadDepartamento> tiposDepart = tiposDepartamento.ToList();
            return tiposDepart.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre.ToString(),
                    Value = d.Id.ToString(),
                    Selected = false
                };
            });
        }

        private List<SelectListItem> listaTiposPuesto(IEnumerable<EntidadPuesto> tiposPuestos)
        {
            List<EntidadPuesto> tiposPuesto = tiposPuestos.ToList();
            return tiposPuesto.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre.ToString(),
                    Value = d.Id.ToString(), 
                    Selected = false
                };
            });
        }

        private VistaEmpleado tranformarParaVista(EntidadEmpleado empleado, List<SelectListItem> tiposDocsIdentidad, List<SelectListItem> tiposDepartamento, List<SelectListItem> tiposPuestos)
        {
            VistaEmpleado visEmpleado = new VistaEmpleado ();
            visEmpleado.Nombre = empleado.Nombre;
            visEmpleado.ValorDocIdentidad = empleado.ValorDocIdentidad;

            foreach (var tipoDoc in tiposDocsIdentidad)
            { 
                if ( int.Parse (tipoDoc.Value) == empleado.IdTipoDocIdentidad)
                {
                    visEmpleado.IdTipoDocIdentidad = tipoDoc.Text;
                }
            }
            foreach (var tipoDep in tiposDepartamento)
            {
                if (int.Parse(tipoDep.Value) == empleado.IdDepartamento)
                { 
                    visEmpleado.IdDepartamento = tipoDep.Text;
                }
            }
            foreach (var tipoPuesto in tiposPuestos)
            {
                if (int.Parse(tipoPuesto.Value) == empleado.IdPuesto)
                {
                    visEmpleado.IdPuesto = tipoPuesto.Text;
                }
            } 
            return visEmpleado;
        }

        private List<VistaEmpleado> pasarElementosAVista(IEnumerable<EntidadEmpleado> empleados, List<SelectListItem> tiposDocsIdentidad, List<SelectListItem> tiposDepartamento, List<SelectListItem> tiposPuestos)
        {
            List<VistaEmpleado> listaEmpleados = new List<VistaEmpleado>();
            foreach (var empleado in empleados)
            {
                listaEmpleados.Add(tranformarParaVista(empleado, tiposDocsIdentidad, tiposDepartamento, tiposPuestos));
            }
            return listaEmpleados;
        }

        public IActionResult Insertar()
        { 
            IEnumerable<EntidadTipoDocIdentidad> tiposDocsIdentidad = _context.TiposDocsIdentidad.FromSqlInterpolated($"EXEC ObtenerTiposDocsIdentidad");
            IEnumerable<EntidadDepartamento> tiposDepartamentos = _context.Departamentos.FromSqlInterpolated($"EXEC ObtenerDepartamentos");
            IEnumerable<EntidadPuesto> tiposPuestos = _context.Puestos.FromSqlInterpolated($"EXEC ObtenerPuestos");
             
            ViewBag.OpcionesTipoDocID = listaTiposDocIdentidad(tiposDocsIdentidad);
            ViewBag.OpcionesTipoDepart = listaTiposDepartamento(tiposDepartamentos);
            ViewBag.OpcionesTipoPuesto = listaTiposPuesto(tiposPuestos);
            return View();
        }

        public IActionResult Editar()
        {
            IEnumerable<EntidadTipoDocIdentidad> tiposDocsIdentidad = _context.TiposDocsIdentidad.FromSqlInterpolated($"EXEC ObtenerTiposDocsIdentidad");
            IEnumerable<EntidadDepartamento> tiposDepartamentos = _context.Departamentos.FromSqlInterpolated($"EXEC ObtenerDepartamentos");
            IEnumerable<EntidadPuesto> tiposPuestos = _context.Puestos.FromSqlInterpolated($"EXEC ObtenerPuestos");
             
            string? valIdentidad = TempData["ValorId"] as string;

            if (valIdentidad != null)
            {
                IEnumerable<EntidadEmpleado> empleado = _context.Empleados.FromSqlInterpolated($"EXEC BuscarEmpleado {valIdentidad}"); 

                ViewBag.OpcionesTipoDocID = listaTiposDocIdentidad(tiposDocsIdentidad);
                ViewBag.OpcionesTipoDepart = listaTiposDepartamento(tiposDepartamentos);
                ViewBag.OpcionesTipoPuesto = listaTiposPuesto(tiposPuestos);
                TempData["ValorIdOrg"] = valIdentidad;

                return View(empleado.First());
            }

            return NotFound();
        }

        public IActionResult Eliminar() 
        {
            string? valIdentidad = TempData["ValorId"] as string;

            if (valIdentidad != null)
            {
                IEnumerable<EntidadEmpleado> empleado = _context.Empleados.FromSqlInterpolated($"EXEC BuscarEmpleado {valIdentidad}");
                
                IEnumerable<EntidadEmpleado> empleados = _context.Empleados.FromSqlInterpolated($"EXEC EnlistarEmpleados");
                IEnumerable<EntidadTipoDocIdentidad> tiposDocsIdentidad = _context.TiposDocsIdentidad.FromSqlInterpolated($"EXEC ObtenerTiposDocsIdentidad");
                IEnumerable<EntidadDepartamento> tiposDepartamentos = _context.Departamentos.FromSqlInterpolated($"EXEC ObtenerDepartamentos");
                IEnumerable<EntidadPuesto> tiposPuestos = _context.Puestos.FromSqlInterpolated($"EXEC ObtenerPuestos");


                List<SelectListItem> tiposDocsId = listaTiposDocIdentidad(tiposDocsIdentidad);
                List<SelectListItem> tiposDeparta = listaTiposDepartamento(tiposDepartamentos);
                List<SelectListItem> tiposPuesto = listaTiposPuesto(tiposPuestos);


                return View(tranformarParaVista(empleado.First(), tiposDocsId, tiposDeparta, tiposPuesto));
            } 
            return NotFound(); 
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

        [HttpPost]
        public IActionResult SeleccionadoRedir(string valorIdentidad, string nuevaDir)
        {
            if (valorIdentidad != null && valorIdentidad != "")
            {
                try
                { 
                    IEnumerable < EntidadEmpleado > empleado = _context.Empleados.FromSqlInterpolated($"EXEC BuscarEmpleado {valorIdentidad}");

                    if (empleado != null && empleado.Count() > 0)
                    {
                        TempData["ValorId"] = valorIdentidad;

                        if (nuevaDir.ToUpper().Contains("EDITAR EMPLEADO") == true) 
                        {
                            return RedirectToAction(nameof(Editar));
                        }
                        if (nuevaDir.ToUpper().Contains("ELIMINAR EMPLEADO") == true)
                        { 
                            return RedirectToAction(nameof(Eliminar));
                        }
                        if (nuevaDir.ToUpper().Contains("IMPERSONAR EMPLEADO") == true)
                        { 
                            return RedirectToAction(nameof(Eliminar));
                        }
                    }

                }
                catch (Exception ex)
                {
                    ViewBag.Subtitulo = nuevaDir;
                    ViewBag.Mensaje = "Error: --> " + ex.Message; ; 
                    return View(nameof(SeleccionarEmpleado));
                }
            }
            ViewBag.Subtitulo = nuevaDir;
            ViewBag.Mensaje = "Error: El empleado no se ha encontrado.";
            return View(nameof(SeleccionarEmpleado)); 
        }

        [HttpPost]
        public IActionResult Insertar(string Nombre, string ValorDocIdentidad, string tipoDocId, string departamento, string puesto)
        { 
            try
            {  
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "InsertarEmpleado";
                cmd.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.NVarChar) { Value = Nombre });
                cmd.Parameters.Add(new SqlParameter("@ValorDocIdentidad", SqlDbType.NVarChar) { Value = ValorDocIdentidad });
                cmd.Parameters.Add(new SqlParameter("@IdTipoDocIdentidad", SqlDbType.Int) { Value = tipoDocId });
                cmd.Parameters.Add(new SqlParameter("@IdDepartamento", SqlDbType.Int) { Value = departamento });
                cmd.Parameters.Add(new SqlParameter("@IdPuesto", SqlDbType.Int) { Value = puesto });
                cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception ex) { Console.WriteLine("ERROR --> " + ex.Message); }
            return RedirectToAction (nameof(ListaEmpleado));
        }

        [HttpPost]
        public IActionResult Editar(string Nombre, string ValorDocIdentidad, string tipoDocId, string departamento, string puesto)
        {
            string? valIdentidadOriginal = TempData["ValorIdOrg"] as string;

            if (valIdentidadOriginal != null)
            {
                try
                {
                    SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                    SqlCommand cmd = conn.CreateCommand();
                    conn.Open();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure; 
                    cmd.CommandText = "ModificarEmpleado";
                    cmd.Parameters.Add(new SqlParameter("@ValorIdentidaOrg", SqlDbType.NVarChar) { Value = valIdentidadOriginal });
                    cmd.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.NVarChar) { Value = Nombre }); 
                    cmd.Parameters.Add(new SqlParameter("@ValorDocIdentidad", SqlDbType.NVarChar) { Value = ValorDocIdentidad });
                    cmd.Parameters.Add(new SqlParameter("@IdTipoDocIdentidad", SqlDbType.Int) { Value = tipoDocId });
                    cmd.Parameters.Add(new SqlParameter("@IdDepartamento", SqlDbType.Int) { Value = departamento });
                    cmd.Parameters.Add(new SqlParameter("@IdPuesto", SqlDbType.Int) { Value = puesto });
                    cmd.ExecuteNonQuery();
                    conn.Close();

                }
                catch (Exception ex) { Console.WriteLine("ERROR --> " + ex.Message); }
                return RedirectToAction(nameof(ListaEmpleado));
            }

            return RedirectToAction(nameof(ListaEmpleado));
        }

        [HttpPost]
        public IActionResult Eliminar(string ValorDocIdentidad)
        {
            if (ValorDocIdentidad != null && ValorDocIdentidad != "")
            {
                try
                {
                    SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                    SqlCommand cmd = conn.CreateCommand();
                    conn.Open();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "EliminarEmpleado"; 
                    cmd.Parameters.Add(new SqlParameter("@ValorIdentidad", SqlDbType.NVarChar) { Value = ValorDocIdentidad }); 
                    cmd.ExecuteNonQuery();
                    conn.Close();

                }
                catch (Exception ex) { Console.WriteLine("ERROR --> " + ex.Message); }
                return RedirectToAction(nameof(ListaEmpleado));
            }
            return RedirectToAction(nameof(ListaEmpleado));
        }

        [HttpPost]
        public ActionResult ListaEmpleado(string LBNombre_, string LBCantidad_, string TipoDocId_, string TipoDepartamento_, string TipoPuesto_)
        {
            IEnumerable<EntidadEmpleado> empleados = new List<EntidadEmpleado>();
            try
            {
                if (LBNombre_ != null && LBNombre_ != "")
                {
                    empleados = _context.Empleados.FromSqlInterpolated($"EXEC EmpleadosPorNombre {LBNombre_}");
                }
                if (LBCantidad_ != null && LBCantidad_ != "")
                {
                    empleados = _context.Empleados.FromSqlInterpolated($"EXEC EmpleadosPorCantidad {LBCantidad_}");
                }
                if (TipoDocId_ != null && TipoDocId_ != "")
                {
                    empleados = _context.Empleados.FromSqlInterpolated($"EXEC EmpleadosPorTipoDocIdentidad {TipoDocId_}");
                }
                if (TipoDepartamento_ != null && TipoDepartamento_ != "")
                {
                    empleados = _context.Empleados.FromSqlInterpolated($"EXEC EmpleadosPorTipoDepartamento {TipoDepartamento_}");
                }
                if (TipoPuesto_ != null && TipoPuesto_ != "")
                {
                    empleados = _context.Empleados.FromSqlInterpolated($"EXEC EmpleadosPorTipoPuesto {TipoPuesto_}");
                }
            } catch (Exception ex)  { 
                Console.WriteLine("ERROR --> " + ex.Message); 
                empleados = new List<EntidadEmpleado>();
            } 

            IEnumerable<EntidadTipoDocIdentidad> tiposDocsIdentidad = _context.TiposDocsIdentidad.FromSqlInterpolated($"EXEC ObtenerTiposDocsIdentidad");
            IEnumerable<EntidadDepartamento> tiposDepartamentos = _context.Departamentos.FromSqlInterpolated($"EXEC ObtenerDepartamentos");
            IEnumerable<EntidadPuesto> tiposPuestos = _context.Puestos.FromSqlInterpolated($"EXEC ObtenerPuestos");
             
            List<SelectListItem> tiposDocsId = listaTiposDocIdentidad(tiposDocsIdentidad);
            List<SelectListItem> tiposDeparta = listaTiposDepartamento(tiposDepartamentos);
            List<SelectListItem> tiposPuesto = listaTiposPuesto(tiposPuestos);

            ViewBag.OpcionesTipoDocID = tiposDocsId;
            ViewBag.OpcionesTipoDepart = tiposDeparta;
            ViewBag.OpcionesTipoPuesto = tiposPuesto;
             
            return View(pasarElementosAVista(empleados, tiposDocsId, tiposDeparta, tiposPuesto));
        }


    }
}
