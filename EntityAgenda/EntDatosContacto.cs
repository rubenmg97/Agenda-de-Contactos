using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiDev.Entity.Agenda
{
    public class EntDatosContacto
    {
        public EntContacto Contacto { get; set; }
        public List<EntReferencia> Referencias { get; set; }
    }
}
