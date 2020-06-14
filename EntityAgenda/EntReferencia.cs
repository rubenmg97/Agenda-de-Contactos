using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiDev.Entity.Agenda
{
    public class EntReferencia
    {
        public int Id { get; set; }
        public String Dato { get; set; }
        public int TipoId { get; set; }
        public int ContactoId { get; set; }
        public EntTipo Tipo { get; set; }
    }
}
