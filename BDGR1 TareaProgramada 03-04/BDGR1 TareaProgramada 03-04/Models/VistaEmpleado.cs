using System.ComponentModel.DataAnnotations;

namespace BDGR1_TareaProgramada_03_04.Models
{
    public class VistaEmpleado
    {  
        public string IdTipoDocIdentidad { get; set; }

        public string IdDepartamento { get; set; }

        public string IdPuesto { get; set; }

        public string Nombre { get; set; }

        public string ValorDocIdentidad { get; set; }


        public VistaEmpleado() { }
        public VistaEmpleado(string Nombre, string ValorDocIdentidad, string IdTipoDocIdentidad, string IdDepartamento, string IdPuesto) 
        { 
            this.Nombre = Nombre;
            this.ValorDocIdentidad = ValorDocIdentidad;
            this.IdTipoDocIdentidad = IdTipoDocIdentidad;
            this.IdDepartamento = IdDepartamento;
            this.IdPuesto = IdPuesto;
        }
    }
}
