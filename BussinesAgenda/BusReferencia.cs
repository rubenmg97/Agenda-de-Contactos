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
    public class BusReferencia
    {
        DatReferencia data = new DatReferencia();
        public List<EntReferencia> Obtener()
        {
            List<EntReferencia> ls = new List<EntReferencia>();
            DataTable tabla = new DataTable();
            tabla = data.Obtener();
            foreach (DataRow fila in tabla.Rows)
            {
                EntReferencia r = new EntReferencia();
                r.Id = Convert.ToInt32(fila["Id"]);
                r.TipoId = Convert.ToInt32(fila["TipoId"]);
                r.ContactoId = Convert.ToInt32(fila["ContactoId"]);
                r.Dato = fila["Dato"].ToString();

                EntTipo t = new EntTipo();
                t.Id = Convert.ToInt32(fila["IdTipo"]);
                t.Nombre = fila["Nombre"].ToString();
                t.Estado = Convert.ToBoolean(fila["Estado"]);
                r.Tipo = t;

                ls.Add(r);
            }
            return ls;
        }

        public List<EntReferencia> ObtenerPorContacto(int Id)
        {
            List<EntReferencia> ls = new List<EntReferencia>();
            DataTable tabla = new DataTable();
            tabla = data.ObtenerPorContacto(Id);
            foreach (DataRow fila in tabla.Rows)
            {
                EntReferencia r = new EntReferencia();
                r.Id = Convert.ToInt32(fila["Id"]);
                r.TipoId = Convert.ToInt32(fila["TipoId"]);
                r.ContactoId = Convert.ToInt32(fila["ContactoId"]);
                r.Dato = fila["Dato"].ToString();

                EntTipo t = new EntTipo();
                t.Id = Convert.ToInt32(fila["IdTipo"]);
                t.Nombre = fila["Nombre"].ToString();
                t.Estado = Convert.ToBoolean(fila["Estado"]);
                r.Tipo = t;

                ls.Add(r);
            }
            return ls;
        }

        public EntReferencia Obtener(int id)
        {
            EntReferencia r = new EntReferencia();
            DataRow fila = data.Obtener(id);
            r.Id = Convert.ToInt32(fila["Id"]);
            r.TipoId = Convert.ToInt32(fila["TipoId"]);
            r.ContactoId = Convert.ToInt32(fila["ContactoId"]);
            r.Dato = fila["Dato"].ToString();
            EntTipo t = new EntTipo();
            t.Id = Convert.ToInt32(fila["IdTipo"]);
            t.Nombre = fila["Nombre"].ToString();
            t.Estado = Convert.ToBoolean(fila["Estado"]);
            r.Tipo = t;

            return r;
        }

        public void Delete(EntReferencia referencia)
        {
            int filasAfectadas = data.Delete(referencia.Id);
            if (filasAfectadas != 1)
            {
                throw new ApplicationException("Error al Borrar Referencia");
            }
        }

        public void Edit(EntReferencia referencia)
        {
            int filasAfectadas = data.Edit(referencia.Id, referencia.Dato, referencia.TipoId, referencia.ContactoId);
            if (filasAfectadas != 1)
            {
                throw new ApplicationException("Error al Editar Referencia");
            }
        }
        public void Create(EntReferencia referencia, int IdContacto)
        {
            int filasAfectadas = data.Create(referencia.Dato, referencia.TipoId, IdContacto);
            if (filasAfectadas != 1)
            {
                throw new ApplicationException("Error al Crear Referencia");
            }
        }

    }
}
