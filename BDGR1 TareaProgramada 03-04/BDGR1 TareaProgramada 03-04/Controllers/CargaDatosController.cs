using BDGR1_TareaProgramada_03_04.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Xml.Linq;

namespace BDGR1_TareaProgramada_03_04.Controllers
{
    public class CargaDatosController : Controller
    {
        private readonly AppDBContext _context;

        public CargaDatosController (AppDBContext context)
        {
            _context = context;
        }

        public ActionResult Cargar ()
        {
            string dirUrlCatalogos = "C:\\Users\\rodri\\Desktop\\GIT\\BDGR1-Tarea-03-04\\BDGR1 TareaProgramada 03-04\\BDGR1 TareaProgramada 03-04\\DataXML\\Catalogos.xml";
            string dirUrlOperaciones = "C:\\Users\\rodri\\Desktop\\GIT\\BDGR1-Tarea-03-04\\BDGR1 TareaProgramada 03-04\\BDGR1 TareaProgramada 03-04\\DataXML\\Operaciones.xml"; 
            string contenidoCatalogos = "";
            string contenidoOperaciones = "";

            try
            {
                contenidoCatalogos = System.IO.File.ReadAllText(dirUrlCatalogos);
                contenidoOperaciones = System.IO.File.ReadAllText(dirUrlOperaciones); 
                
                //Se cargan catalogos
                    //_context.Database.ExecuteSqlRaw("CargarCatalogos @xmlData", new SqlParameter("@xmlData", contenidoCatalogos));
                //Se cargan operaciones 
                    //_context.Database.ExecuteSqlRaw("CargarOperaciones @xmlData", new SqlParameter("@xmlData", contenidoOperaciones));
                
                
                /*
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "CargarCatalogos"; 
                cmd.Parameters.Add(new SqlParameter("@xmlData", SqlDbType.Xml) { Value = contenidoCatalogos });
                cmd.ExecuteNonQuery();
                conn.Close();
                */

            }
            catch (Exception ex) { Console.WriteLine("ERROR --> " + ex.Message); }
            return RedirectToAction("InicioSesion", "Acceso");
        }
    }
}
