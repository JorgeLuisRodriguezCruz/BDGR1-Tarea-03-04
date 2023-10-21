using System.ComponentModel.DataAnnotations;

namespace BDGR1_TareaProgramada_03_04.Models
{
    public class EntidadUsuario
    {
        [Key]
        public int Id { get; set; }

        public int Tipo{ get; set; }

        public string Nombre { get; set; }

        public string Contraseña { get; set; }


    }
}
