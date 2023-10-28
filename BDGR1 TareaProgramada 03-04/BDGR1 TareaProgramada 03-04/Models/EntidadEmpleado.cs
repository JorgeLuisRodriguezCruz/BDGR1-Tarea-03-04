using System.ComponentModel.DataAnnotations;

namespace BDGR1_TareaProgramada_03_04.Models
{
    public class EntidadEmpleado
    {
        [Key]
        public int Id { get; set; }

        public int IdTipoDocIdentidad { get; set; }

        public int IdDepartamento { get; set; }

        public int IdPuesto { get; set; }

        public string Nombre { get; set; }

        public string ValorDocIdentidad { get; set; }

    }
}
