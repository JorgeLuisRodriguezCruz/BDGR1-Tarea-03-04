using System.ComponentModel.DataAnnotations;

namespace BDGR1_TareaProgramada_03_04.Models
{
    public class EntidadPlanillaSemana
    {

        [Key]
        public int Id { get; set; }
         
        public int SalarioBruto { get; set; }

        public int SalarioNeto { get; set; }

        public int NumeroDeducciones { get; set; }

        public int HorasExtras { get; set; }

        public int HorasExtrasDobles { get; set; }
    }
}
