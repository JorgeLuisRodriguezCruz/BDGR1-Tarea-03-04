using System.ComponentModel.DataAnnotations;

namespace BDGR1_TareaProgramada_03_04.Models
{
    public class EntidadEmpleado
    {
        [Key]
        public int Id { get; set; } 

        public string Nombre { get; set; }

        public string ValorIdentificacion { get; set; }

        public int Departamento { get; set; }

        public int Puesto { get; set; }
    }
}
