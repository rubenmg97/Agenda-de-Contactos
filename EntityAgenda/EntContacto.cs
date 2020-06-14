using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiDev.Entity.Agenda
{
    public class EntContacto
    {
        public int Id { get; set; }
        public String Nombre { get; set; }
        public String Paterno { get; set; }
        public String Materno { get; set; }
        public DateTime Nacimiento { get; set; }
        public int Edad { get; set; }
        public String NomFoto { get; set; }
        public int UserId { get; set; }
        public Boolean Cumpleaños { get; set; }

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
