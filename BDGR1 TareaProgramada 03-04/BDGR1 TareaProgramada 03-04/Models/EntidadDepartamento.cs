using System.ComponentModel.DataAnnotations;

namespace BDGR1_TareaProgramada_03_04.Models
{
    public class EntidadDepartamento
    {

        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }
    }
}
