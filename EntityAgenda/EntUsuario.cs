using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiDev.Entity.Agenda
{
    public class EntUsuario
    {
        public int Id { get; set; }
        public String Nombre { get; set; }
        public String Paterno { get; set; }
        public String Materno { get; set; }
        public String NomUsuario { get; set; }
        public DateTime Nacimiento { get; set; }
        public int Edad { get; set; }
        public String Correo { get; set; }
        public String Password { get; set; }
        public String Facebook { get; set; }
        public String Linkedin { get; set; }
        public String NomFoto { get; set; }

        private String nombreCompleto;
        public String NombreCompleto
        {
            get
            {
                nombreCompleto = Nombre + " " + Paterno + " " + Materno;
                return nombreCompleto;
            }
        }

    }
}
