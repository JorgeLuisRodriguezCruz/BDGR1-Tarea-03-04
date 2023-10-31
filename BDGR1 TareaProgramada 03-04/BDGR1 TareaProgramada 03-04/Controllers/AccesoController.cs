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
            string dirUrl = "C:\\Users\\rodri\\Desktop\\GIT\\BDGR1-Tarea-03-04\\BDGR1 TareaProgramada 03-04\\BDGR1 TareaProgramada 03-04\\DataXML\\Catalogos.xml";
            string contenidoCatalogos = "";

            try
            {
                contenidoCatalogos = System.IO.File.ReadAllText(dirUrl);
                //Console.WriteLine(contenidoCatalogos);
                //var result = _context.Usuarios.FromSqlInterpolated($"EXEC CargarCatalogos {contenidoCatalogos}");
                //<?xml version="1.0" encoding="utf-8" ?>

                XDocument.Parse (contenidoCatalogos);

                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "CargarCatalogos";
                cmd.Parameters.Add(new SqlParameter("@xmlData", SqlDbType.Xml) { Value = contenidoCatalogos });
                cmd.ExecuteNonQuery();
                conn.Close();

                usuario = _context.Usuarios.FromSqlInterpolated($"EXEC BuscarUsuario {Nombre}, {Contrasena}");

            } catch (Exception ex) { Console.WriteLine("ERROR --> " + ex.Message); }

            /*try
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure; 
                cmd.CommandText = "BuscarUsuario";
                cmd.Parameters.Add("@Nombre", System.Data.SqlDbType.NVarChar, 50).Value = "Gael";
                cmd.Parameters.Add("@Contrasena", System.Data.SqlDbType.NVarChar, 50).Value = "DelFin123";
                cal = cmd.ExecuteNonQuery();

                

                conn.Close();

            } catch (Exception ex)

            {

            }
            //UserEntity usuario;
            
             
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.CommandText = "CargaXML";
                //cmd.CommandText = "carggXML"; 
                cmd.CommandText = "ChargeXMLdata";
                //cmd.Parameters.Add("@xmlData", System.Data.SqlDbType.NVarChar, 500).Value = xmlContent; 
                cmd.ExecuteNonQuery();
                conn.Close(); 
             * 
             * 
            if (Nombre != "")
            {
                try
                {
                    UserEntity usuario = (UserEntity)_context.Usuario.FirstOrDefault(e => e.Nombre == Nombre);

                    if (usuario != null && usuario.Contraseña.Equals(Contraseña))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Mensaje = "ERROR ##" + ex.ToString();
                    return View();
                }
            }
            ViewBag.Mensaje = "Error: Combinación de usuario y password no existe en la BD.";
            */

            if (usuario != null && usuario.Count() > 0)
            {
                int tipo = usuario.First().Tipo;
                switch(tipo)
                {
                    case 1: //Menu Admin
                        return RedirectToAction("ListaEmpleado", "MunuAdmin");

                    case 2: //Menu Empleado
                        return View();
                }
                
            }

            ViewBag.Mensaje = "Error: El suario no ha sido encontrado.";
            return View();
        }



    }
}
