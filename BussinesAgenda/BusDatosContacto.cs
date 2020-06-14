using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiDev.Data.Agenda;
using TiDev.Entity.Agenda;

namespace TiDev.Bussines.Agenda
{
    public class BusDatosContacto
    {
        DatDatosContacto data = new DatDatosContacto();
        BusContacto comandoC = new BusContacto();
        public List<EntDatosContacto> Obtener(int Id)
        {
            List<EntDatosContacto> ls = new List<EntDatosContacto>();

            List<EntContacto> lsC = new List<EntContacto>();
            lsC=comandoC.ObtenerPorUsuario(Id);

            foreach (EntContacto c in lsC)
            {
                EntDatosContacto contacto = new EntDatosContacto();
                contacto=data.Obtener(c.Id);
                ls.Add(contacto);
            }
            return ls;    
        }

        public List<EntDatosContacto> Obtener(String valor, int Id)
        {
            List<EntDatosContacto> ls = new List<EntDatosContacto>();

            List<EntContacto> lsC = new List<EntContacto>();
            lsC = comandoC.Obtener(valor,Id);

            foreach (EntContacto c in lsC)
            {
                EntDatosContacto contacto = new EntDatosContacto();
                contacto = data.Obtener(c.Id);
                ls.Add(contacto);
            }
            return ls;
        }

    }
}
